using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [Header("Spawners")]
    public Spawner[] leftSpawners = new Spawner[3];
    public Spawner[] rightSpawners = new Spawner[3];

    [Header("ButtonArrows")]
    public GameObject[] leftButtons = new GameObject[3];
    public GameObject[] rightButtons = new GameObject[3];
    public Sprite buttonOnRight;
    public Sprite buttonOnLeft;
    public Sprite buttonOffRight;
    public Sprite buttonOffLeft;

    public string currentEraLeft = "Era1";
    public string currentEraRight = "Era1";
    public string characterTypeLeft = "";
    public string characterTypeRight = "";

    [HideInInspector] public int laneToDeployLeft = 0;
    [HideInInspector] public int laneToDeployRight = 0;


    public static GameManager Instance;

    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void ChangeLaneToDeploy(bool isLeft, int lane) {

        if (isLeft) {
            leftButtons[laneToDeployLeft].GetComponent<Image>().sprite = buttonOffRight;
            laneToDeployLeft = lane;

        } else {
            rightButtons[laneToDeployRight].GetComponent<Image>().sprite = buttonOffLeft;
            laneToDeployRight = lane;
        }

        ChangeButtonSprite(isLeft);
    }

    public void SpawnCharacter(bool isLeft) {
        if (isLeft) {
            leftSpawners[laneToDeployLeft].SpawnCharacter();
        } else {
            rightSpawners[laneToDeployRight].SpawnCharacter();
        }
    }

    private void ChangeButtonSprite(bool isLeft) {
        if(isLeft)
            leftButtons[laneToDeployLeft].GetComponent<Image>().sprite = buttonOnRight;
        else
            rightButtons[laneToDeployRight].GetComponent<Image>().sprite = buttonOnLeft;
    }
}