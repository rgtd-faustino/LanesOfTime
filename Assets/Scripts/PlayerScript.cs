using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public TextMeshProUGUI coinsTextP1;
    [HideInInspector] public int coinsAmountP1;

    // if it's 1v1
    public TextMeshProUGUI coinsTextP2;
    [HideInInspector] public int coinsAmountP2;


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

    public void AddCoinsP1(int n) {
        coinsAmountP1 += n;
        coinsTextP1.text = coinsAmountP1.ToString();
    }

    public void TakeCoinsP1(int n) {
        coinsAmountP1 -= n;
        coinsTextP1.text = coinsAmountP1.ToString();
    }

    public void AddCoinsP2(int n) {
        coinsAmountP2 += n;
        coinsTextP2.text = coinsAmountP2.ToString();
    }

    public void TakeCoinsP2(int n) {
        coinsAmountP2 -= n;
        coinsTextP2.text = coinsAmountP2.ToString();
    }
}
