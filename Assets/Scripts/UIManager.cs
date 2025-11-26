using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;

    public Button meleeButtonLeft;
    public Button rangedButtonLeft;
    public Button specialButtonLeft;

    public Button meleeButtonRight;
    public Button rangedButtonRight;
    public Button specialButtonRight;

    public GameObject rightButtons; // right buttons não incluindo as stats também por enquanto

    public TextMeshProUGUI clockText;
    private float timeRemaining = 300f;
    public TextMeshProUGUI gameOverInterfaceText;
    public GameObject gameOverInterface;

    public GameObject changeBasePopUpP1;
    public GameObject changeBasePopUpP2;

    public GameObject[] basesP1;
    public GameObject[] basesP2;

    private readonly List<GameObject> hiddenObjects = new();

    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void Start() {
        if (GameStateManager.Instance.isCampaign) {
            rightButtons.SetActive(false);
        } else {
            rightButtons.SetActive(true);
        }

        UpdateClockDisplay();
    }

    public void Update() {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0) {
                timeRemaining = 0;
                GameManager.Instance.SetGameOver(true);
            }

            UpdateClockDisplay();
        }
    }

    private void UpdateClockDisplay() {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        clockText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    public void SetGameOverInterface(int n) {
        switch (n) {
            case 0:
                gameOverInterfaceText.text = "Player 1 Won!";
                break;
            case 1:
                gameOverInterfaceText.text = "Player 2 Won!";
                break;
            case 2:
                gameOverInterfaceText.text = "A Draw has Happened!";
                break;
        }

        gameOverInterface.SetActive(true);
        timeRemaining = 0;
        clockText.text = "0:00";

    }

    public void ChangeScene(int n) {
        if(n == 0)
            SceneManager.LoadScene("MainMenu");
        else
            SceneManager.LoadScene("GameplayScene");
    }

    public void UpdateTroopButtons(bool isLeftSide, Dictionary<string, Dictionary<string, GameObject>> prefabsCost, string currentEra) {
        // costs for each troop's type according to the current era - BASE VALUES
        int baseMeleeCost = prefabsCost[currentEra]["Melee"].GetComponent<CharacterScript>().data.value;
        int baseRangedCost = prefabsCost[currentEra]["Ranged"].GetComponent<CharacterScript>().data.value;
        int baseSpecialCost = prefabsCost[currentEra]["Special"].GetComponent<CharacterScript>().data.value;

        // DYNAMIC VALUES
        int meleeCost = DynamicPricingManager.Instance.GetDynamicCost(isLeftSide, currentEra, "Melee", baseMeleeCost);
        int rangedCost = DynamicPricingManager.Instance.GetDynamicCost(isLeftSide, currentEra, "Ranged", baseRangedCost);
        int specialCost = DynamicPricingManager.Instance.GetDynamicCost(isLeftSide, currentEra, "Special", baseSpecialCost);


        // if there are more coins than the value of the cost then the player can spawn the troop
        if (isLeftSide) {
            int coins = PlayerScript.Instance.coinsAmountP1;
            meleeButtonLeft.interactable = coins >= meleeCost;
            rangedButtonLeft.interactable = coins >= rangedCost;
            specialButtonLeft.interactable = coins >= specialCost;

            meleeButtonLeft.GetComponentInChildren<TextMeshProUGUI>().text = $"{meleeCost}";
            rangedButtonLeft.GetComponentInChildren<TextMeshProUGUI>().text = $"{rangedCost}";
            specialButtonLeft.GetComponentInChildren<TextMeshProUGUI>().text = $"{specialCost}";


        } else {
            int coins = PlayerScript.Instance.coinsAmountP2;
            meleeButtonRight.interactable = coins >= meleeCost;
            rangedButtonRight.interactable = coins >= rangedCost;
            specialButtonRight.interactable = coins >= specialCost;

            meleeButtonRight.GetComponentInChildren<TextMeshProUGUI>().text = $"{meleeCost}";
            rangedButtonRight.GetComponentInChildren<TextMeshProUGUI>().text = $"{rangedCost}";
            specialButtonRight.GetComponentInChildren<TextMeshProUGUI>().text = $"{specialCost}";

        }
    }

    public void LaneToDeployLeft(int lane) {
        GameManager.Instance.ChangeLaneToDeploy(true, lane);
    }

    public void LaneToDeployRight(int lane) {
        GameManager.Instance.ChangeLaneToDeploy(false, lane);
    }

    public void ChangeCharacterTypeLeft(string characterType) {
        GameManager.Instance.characterTypeLeft = characterType;
        GameManager.Instance.SpawnCharacter(true);

    }

    public void ChangeCharacterTypeRight(string characterType) {
        GameManager.Instance.characterTypeRight = characterType;
        GameManager.Instance.SpawnCharacter(false);
    }

    public void ShowChangeBasePopUp(bool isP1) {
        GameManager.Instance.PauseGame();
        //HideAllCharacters();

        if (isP1)
            changeBasePopUpP1.SetActive(true);
        else
            changeBasePopUpP2.SetActive(true);
    }

    private void HideAllCharacters() {
        hiddenObjects.Clear();

        foreach (GameObject character in GameObject.FindGameObjectsWithTag("CharacterLeft")) {
            if (character.activeSelf) {
                hiddenObjects.Add(character);
                character.SetActive(false);
            }
        }

        foreach (GameObject character in GameObject.FindGameObjectsWithTag("CharacterRight")) {
            if (character.activeSelf) {
                hiddenObjects.Add(character);
                character.SetActive(false);
            }
        }

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet")) {
            if (bullet.activeSelf) {
                hiddenObjects.Add(bullet);
                bullet.SetActive(false);
            }
        }
    }

    private void ShowAllCharacters() {
        foreach (GameObject gameObject in hiddenObjects) {
            if (gameObject != null)
                gameObject.SetActive(true);
        }
        hiddenObjects.Clear();
    }

    public void ChangeBaseP1(int baseIndex) {
        for (int i = 0; i < basesP1.Length; i++) {
            // only make the current base that was selected active and hide the others
            if (i == baseIndex)
                basesP1[i].SetActive(true);
            else
                basesP1[i].SetActive(false);
        }

        GameManager.Instance.currentEraLeft = $"Era{baseIndex + 1}"; // update the current era

        changeBasePopUpP1.SetActive(false);
        PlayerScript.Instance.sliderEXPP1.value = 0;
        PlayerScript.Instance.ResetExperience(true);

        // update the troop button costs for the new era
        UIManager.Instance.UpdateTroopButtons(true, GameManager.Instance.charactersForEras, GameManager.Instance.currentEraLeft);

        //ShowAllCharacters();
        GameManager.Instance.ResumeGame();
    }

    public void ChangeBaseP2(int baseIndex) {
        for (int i = 0; i < basesP2.Length; i++) {
            // only make the current base that was selected active and hide the others
            if (i == baseIndex)
                basesP2[i].SetActive(true);
            else
                basesP2[i].SetActive(false);
        }

        GameManager.Instance.currentEraRight = $"Era{baseIndex + 1}"; // update the current era

        changeBasePopUpP2.SetActive(false);
        PlayerScript.Instance.sliderEXPP2.value = 0;
        PlayerScript.Instance.ResetExperience(false);

        // update the troop button costs for the new era
        UIManager.Instance.UpdateTroopButtons(false, GameManager.Instance.charactersForEras, GameManager.Instance.currentEraRight);

        //ShowAllCharacters();
        GameManager.Instance.ResumeGame();
    }
}