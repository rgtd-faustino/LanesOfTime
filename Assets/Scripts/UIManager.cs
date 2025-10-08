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

    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void UpdateTroopButtons(Dictionary<string, Dictionary<string, (GameObject prefab, int value)>> prefabCost, string currentEra, string characterType) {
        if(PlayerScript.Instance.coinsAmount < prefabCost[currentEra][characterType].value) {
            meleeButtonLeft.interactable = false;
            rangedButtonLeft.interactable = false;
            specialButtonLeft.interactable = false;

        } else if(PlayerScript.Instance.coinsAmount < prefabCost[currentEra][characterType].value) {
            meleeButtonLeft.interactable = true;
            rangedButtonLeft.interactable = false;
            specialButtonLeft.interactable = false;


        } else if (PlayerScript.Instance.coinsAmount < prefabCost[currentEra][characterType].value) {
            meleeButtonLeft.interactable = true;
            rangedButtonLeft.interactable = true;
            specialButtonLeft.interactable = false;

        } else if (PlayerScript.Instance.coinsAmount >= prefabCost[currentEra][characterType].value) {
            meleeButtonLeft.interactable = true;
            rangedButtonLeft.interactable = true;
            specialButtonLeft.interactable = true;
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