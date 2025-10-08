using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public TextMeshProUGUI coinsText;
    [HideInInspector] public int coinsAmount;




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
        coinsAmount = 50;
        coinsText.text = coinsAmount.ToString();
    }

    // Update is called once per frame
    void Update() {

    }

    public void AddCoins(int n) {
        coinsAmount += n;
        coinsText.text = coinsAmount.ToString();
    }

    public void TakeCoins(int n) {
        coinsAmount -= n;
        coinsText.text = coinsAmount.ToString();
    }
}
