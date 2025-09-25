using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{

    private Rigidbody rb;

    [HideInInspector] public int direction;
    private bool isLeftSideCharacter;

    public CharacterData data;
    [HideInInspector] public float health;
    [HideInInspector] public float speed;
    [HideInInspector] public float attackSpeed;
    [HideInInspector] public float attackDamage;
    [HideInInspector] public string type;
    [HideInInspector] public Slider slider;

    private BoxCollider attackCollider;
    [HideInInspector] public CharacterScript enemyToHit;
    public bool isAttacking;

    [SerializeField] private GameObject firePosRight;
    [SerializeField] private GameObject firePosLeft;
    [SerializeField] private GameObject bulletPrefab;
    private GameObject firePosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody>();
        isLeftSideCharacter = direction == 1 ? true : false;
        health = data.health;
        speed = data.speed;
        attackSpeed = data.attackSpeed;
        attackDamage = data.attack;
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

    // quando arranjar um boneco a animação dele bater é que vai chamar esta função e não vai haver necessidade de fazer uma coroutine
    // porque a animação é que vai fazer o attackSpeed e assim
    private IEnumerator ApplyDamageMelee() {
        while (enemyToHit != null && enemyToHit.health > 0) {
            enemyToHit.health = Mathf.Clamp(enemyToHit.health - attackDamage, 0, enemyToHit.data.health);
            enemyToHit.slider.value = enemyToHit.health;
            yield return new WaitForSeconds(attackSpeed);
        }

        isAttacking = false;
        enemyToHit = null;
    }

    private IEnumerator ApplyDamageRanged() {
        while (enemyToHit != null && enemyToHit.health > 0) {
            GameObject bullet = Instantiate(bulletPrefab, firePosition.transform.position, firePosition.transform.rotation);

            bullet.GetComponent<BulletScript>().character = this;

            float bulletSpeed = 10f;
            Vector3 direction = (enemyToHit.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;

            yield return new WaitForSeconds(attackSpeed);
        }

        isAttacking = false;
        enemyToHit = null;
    }
}
