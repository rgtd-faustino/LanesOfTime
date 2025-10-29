using System.Collections.Generic;
using UnityEngine;
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
    }

    public void UpdateTroopButtons(bool isLeftSide, Dictionary<string, Dictionary<string, GameObject>> prefabsCost, string currentEra) {
        // costs for each troop's type according to the current era
        int meleeCost = prefabsCost[currentEra]["Melee"].GetComponent<CharacterScript>().data.value;
        int rangedCost = prefabsCost[currentEra]["Ranged"].GetComponent<CharacterScript>().data.value;
        int specialCost = prefabsCost[currentEra]["Special"].GetComponent<CharacterScript>().data.value;

        // if there are more coins than the value of the cost then the player can spawn the troop
        if (isLeftSide) {
            int coins = PlayerScript.Instance.coinsAmountP1;
            meleeButtonLeft.interactable = coins >= meleeCost;
            rangedButtonLeft.interactable = coins >= rangedCost;
            specialButtonLeft.interactable = coins >= specialCost;
        } else {
            int coins = PlayerScript.Instance.coinsAmountP2;
            meleeButtonRight.interactable = coins >= meleeCost;
            rangedButtonRight.interactable = coins >= rangedCost;
            specialButtonRight.interactable = coins >= specialCost;
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
}