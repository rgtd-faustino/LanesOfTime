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

    public void SpawnCharacter(bool isLeft) {
        GameManager.Instance.SpawnCharacter(isLeft);
    }

    public void ChangeCharacterTypeLeft(string characterType) {
        GameManager.Instance.characterTypeLeft = characterType;
    }

    public void ChangeCharacterTypeRight(string characterType) {
        GameManager.Instance.characterTypeRight = characterType;
    }
}