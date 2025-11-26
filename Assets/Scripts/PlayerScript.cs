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
    public Slider sliderBaseHPP1;
    public GameObject baseP1;

    // if it's 1v1
    public TextMeshProUGUI coinsTextP2;
    [HideInInspector] public int coinsAmountP2;

    public GameObject baseP2;
    public Slider sliderBaseHPP2;
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
            experienceAmountP1 = Mathf.Clamp(experienceAmountP1, 0, (int)sliderEXPP1.maxValue);

            EXPTextP1.text = experienceAmountP1.ToString();
            sliderEXPP1.value = experienceAmountP1;

            if (sliderEXPP1.value == sliderEXPP1.maxValue) {
                UIManager.Instance.ShowChangeBasePopUp(isP1);
            }

        } else {
            experienceAmountP2 += n;
            experienceAmountP2 = Mathf.Clamp(experienceAmountP2, 0, (int)sliderEXPP2.maxValue);

            EXPTextP2.text = experienceAmountP2.ToString();
            sliderEXPP2.value = experienceAmountP2;

            if (sliderEXPP2.value == sliderEXPP2.maxValue) {
                UIManager.Instance.ShowChangeBasePopUp(isP1);
            }
        }
    }

    public void ResetExperience(bool isP1) {
        if (isP1) {
            experienceAmountP1 = 0;
            EXPTextP1.text = "0";
            sliderEXPP1.value = 0;

        } else {
            experienceAmountP2 = 0;
            EXPTextP2.text = "0";
            sliderEXPP2.value = 0;
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
