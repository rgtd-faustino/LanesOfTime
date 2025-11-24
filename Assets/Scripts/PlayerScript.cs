using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public TextMeshProUGUI coinsTextP1;
    [HideInInspector] public int coinsAmountP1;

    public TextMeshProUGUI EXPTextP1;
    private int experienceAmountP1;
    public Slider sliderEXPP1;

    // if it's 1v1
    public TextMeshProUGUI coinsTextP2;
    [HideInInspector] public int coinsAmountP2;
    public Slider sliderEXPP2;

    public TextMeshProUGUI EXPTextP2;
    private int experienceAmountP2;

    public static PlayerScript Instance;

    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinsAmountP1 = 50;
        coinsTextP1.text = coinsAmountP1.ToString();

        coinsAmountP2 = 50;
        coinsTextP2.text = coinsAmountP2.ToString();
    }

    // Update is called once per frame
    void Update() {

    }

    public void ChangeExperience(bool isP1, int n) {
        if (isP1) {
            experienceAmountP1 += n;
            EXPTextP1.text = experienceAmountP1.ToString();
            sliderEXPP1.GetComponent<Slider>().value = experienceAmountP1;

        } else {
            experienceAmountP2 += n;
            EXPTextP2.text = experienceAmountP2.ToString();
            sliderEXPP2.GetComponent<Slider>().value = experienceAmountP2;
        }

    }

    public void ChangeCoins(bool isP1, int n) {
        if (isP1) {
            coinsAmountP1 += n;
            coinsTextP1.text = coinsAmountP1.ToString();
            UIManager.Instance.UpdateTroopButtons(isP1, GameManager.Instance.charactersForEras, GameManager.Instance.currentEraLeft);

        } else {
            coinsAmountP2 += n;
            coinsTextP2.text = coinsAmountP2.ToString();
            UIManager.Instance.UpdateTroopButtons(isP1, GameManager.Instance.charactersForEras, GameManager.Instance.currentEraRight);
        }
    }
}
