using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    [HideInInspector] public float health;
    [HideInInspector] public float maxHealth;
    public Slider slider;
    [SerializeField] private GameObject castle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = 100f;
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0) {
            Destroy(gameObject);
            Destroy(castle);
        }
    }
}
