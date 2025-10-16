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

    // informações do boneco e lane onde fazer o spawn
    [HideInInspector] public string currentEraLeft;
    [HideInInspector] public string currentEraRight;
    [HideInInspector] public string characterTypeLeft = "";
    [HideInInspector] public string characterTypeRight = "";
    [HideInInspector] public int laneToDeployLeft = 0;
    [HideInInspector] public int laneToDeployRight = 0;

    private int gameDifficulty;
    private bool isCampaignMode;

    public static GameManager Instance;

    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        currentEraLeft = "Era1";
        currentEraRight = "Era1";

        bool isCampaign = GameStateManager.Instance.isCampaign;
        int difficulty = GameStateManager.Instance.levelDifficulty;

        if (isCampaign) {
            if(difficulty >= 1 && difficulty <= 3) {
                currentEraRight = "Era1";

            } else if(difficulty >= 4 && difficulty <= 6) {
                currentEraRight = "Era2";

            } else if (difficulty >= 7 && difficulty <= 9) {
                currentEraRight = "Era3";

            } else if (difficulty >= 10 && difficulty <= 12) {
                currentEraRight = "Era4";

            } else if (difficulty >= 13 && difficulty <= 15) {
                currentEraRight = "Era5";

            }
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


    private void OnEnable() {
        // Subscribe to the event
        if (GameStateManager.Instance != null) {
            GameStateManager.Instance.OnGameSceneLoaded += HandleGameSetup;
            // Get the values immediately since we just loaded
            HandleGameSetup(GameStateManager.Instance.isCampaign, GameStateManager.Instance.levelDifficulty);
        }
    }

    private void OnDisable() {
        // Unsubscribe to prevent memory leaks
        if (GameStateManager.Instance != null) {
            GameStateManager.Instance.OnGameSceneLoaded -= HandleGameSetup;
        }
    }

    private void HandleGameSetup(bool isCampaign, int difficulty) {
        isCampaignMode = isCampaign;
        gameDifficulty = difficulty;

        // Now configure your game based on these values
        // e.g., adjust enemy spawning, set up UI, etc.
    }
}