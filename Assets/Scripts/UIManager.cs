using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;

    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
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