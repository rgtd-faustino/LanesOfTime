using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    [HideInInspector] public float health;
    [HideInInspector] public float maxHealth;
    [HideInInspector] public Slider slider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = 100f;
        health = maxHealth;
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
