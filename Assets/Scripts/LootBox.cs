using UnityEngine;
using UnityEngine.UI;

public class LootBox : MonoBehaviour {
    public float fallSpeed = 5f;
    private Rigidbody rb;
    [HideInInspector] public int isGoldBox;
    [HideInInspector] public int goldLoot = 15;
    [HideInInspector] public int xpLoot = 200;
    [HideInInspector] public float maxHealth = 175;
    [HideInInspector] public float health = 175;
    [HideInInspector] public Slider slider;
    [HideInInspector] public Transform spawner;

    void Start() {
        rb = GetComponent<Rigidbody>();
        isGoldBox = Random.Range(0, 2); // 0 = gold, 1 = xp
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    void FixedUpdate() {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, -fallSpeed, rb.linearVelocity.z);
    }

    private void Update() {
        if(health == 0) {
            LootBoxManager.Instance.FreeSpawner(spawner);
            Destroy(gameObject);
        }
    }


}