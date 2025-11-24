using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour {

    private Rigidbody rb;

    [HideInInspector] public int direction;
    private bool isLeftSideCharacter;

    public CharacterData data;
    [HideInInspector] public float health;
    [HideInInspector] public float speed;
    [HideInInspector] public float attackSpeed;
    [HideInInspector] public float attackDamage;
    [HideInInspector] public int value;
    [HideInInspector] public int experience;
    [HideInInspector] public string type;
    [HideInInspector] public Slider slider;

    private BoxCollider attackCollider;
    [HideInInspector] public GameObject targetToDamage;
    [HideInInspector] public List<GameObject> targetsInLine;
    [HideInInspector] public bool isTargetingBase;
    public BaseScript enemyBase;
    public bool isAttacking;

    [SerializeField] private GameObject firePosRight;
    [SerializeField] private GameObject firePosLeft;
    [SerializeField] private GameObject bulletPrefab;
    private GameObject firePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody>();
        isLeftSideCharacter = direction == 1;
        health = data.health;
        speed = data.speed;
        attackSpeed = data.attackSpeed;
        attackDamage = data.attack;
        value = data.value;
        experience = data.experience;
        type = data.type;
        attackCollider = GetComponentInChildren<BoxCollider>();
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = health;
        slider.value = health;

        if (isLeftSideCharacter)
            firePosition = firePosRight;
        else
            firePosition = firePosLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0) {
            Destroy(gameObject);

            if (isLeftSideCharacter) {
                PlayerScript.Instance.ChangeCoins(false, value);
                PlayerScript.Instance.ChangeExperience(false, experience);

            } else {
                PlayerScript.Instance.ChangeCoins(true, value);
                PlayerScript.Instance.ChangeExperience(true, experience);
            }
        }

        if (isAttacking) {
            return;
        }

        Vector3 movement = direction * speed * Time.deltaTime * Vector3.right;
        rb.MovePosition(rb.position + movement);
    }

    public void HitEnemy() {

        switch (type) {
            case "Melee":
                StartCoroutine(ApplyDamageMelee());
                break;

            case "Ranged":
                StartCoroutine(ApplyDamageRanged());
                break;

            case "Special":
                break;
        }
    }

    private bool IsTargetAlive() {
        if (targetToDamage == null)
            return false;

        if (isTargetingBase) {
            return targetToDamage.GetComponent<BaseScript>().health > 0;
        } else {
            return targetToDamage.GetComponent<CharacterScript>().health > 0;
        }
    }

    // quando arranjar um boneco a animação dele bater é que vai chamar esta função e não vai haver necessidade de fazer uma coroutine
    // porque a animação é que vai fazer o attackSpeed e assim
    private IEnumerator ApplyDamageMelee() {
        while (targetsInLine.Count != 0) {
            if (targetsInLine.Count == 0) break;

            targetToDamage = targetsInLine[0];

            if (IsTargetAlive() == false) {
                targetsInLine.RemoveAt(0);
                continue;
            }

            if (isTargetingBase) {
                BaseScript target = targetToDamage.GetComponent<BaseScript>();
                target.health = Mathf.Clamp(target.health - attackDamage, 0, target.maxHealth);
                target.slider.value = target.health;

            } else {
                CharacterScript target = targetToDamage.GetComponentInParent<CharacterScript>();
                target.health = Mathf.Clamp(target.health - attackDamage, 0, target.data.health);
                target.slider.value = target.health;

                if (target.health == 0) {
                    targetsInLine.RemoveAt(0);
                    if (targetsInLine.Count != 0)
                        targetToDamage = targetsInLine[0];
                    else
                        targetToDamage = null;
                }

            }

            yield return new WaitForSeconds(attackSpeed);
        }

        isAttacking = false;
    }

    private IEnumerator ApplyDamageRanged() {
        while (targetsInLine.Count != 0) {
            if (targetsInLine.Count == 0) break;

            targetToDamage = targetsInLine[0];

            if (!IsTargetAlive()) {
                targetsInLine.RemoveAt(0);
                continue;
            }

            GameObject bullet = Instantiate(bulletPrefab, firePosition.transform.position, firePosition.transform.rotation);
            bullet.GetComponent<BulletScript>().character = this;
            float bulletSpeed = 10f;
            Vector3 direction = (targetToDamage.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;

            yield return new WaitForSeconds(attackSpeed);
        }

        isAttacking = false;
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("WorldLimit")) {
            Destroy(gameObject);
        }
    }
}
