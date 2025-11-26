using System.Collections.Generic;
using Unity.VisualScripting;
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

    public Dictionary<string, Dictionary<string, GameObject>> charactersForEras = new();
    public GameObject MeleePrefabEra1;
    public GameObject RangedPrefabEra1;
    public GameObject SpecialPrefabEra1;

    public GameObject MeleePrefabEra2;
    public GameObject RangedPrefabEra2;
    public GameObject SpecialPrefabEra2;

    public GameObject MeleePrefabEra3;
    public GameObject RangedPrefabEra3;
    public GameObject SpecialPrefabEra3;

    public GameObject MeleePrefabEra4;
    public GameObject RangedPrefabEra4;
    public GameObject SpecialPrefabEra4;

    public GameObject MeleePrefabEra5;
    public GameObject RangedPrefabEra5;
    public GameObject SpecialPrefabEra5;

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
        charactersForEras["Era1"] = new Dictionary<string, GameObject>() {
            { "Melee", MeleePrefabEra1 },
            { "Ranged", RangedPrefabEra1 },
            { "Special", SpecialPrefabEra1 }
        };
        charactersForEras["Era2"] = new Dictionary<string, GameObject>() {
            { "Melee", MeleePrefabEra2 },
            { "Ranged", RangedPrefabEra2 },
            { "Special", SpecialPrefabEra2 }
        };
        charactersForEras["Era3"] = new Dictionary<string, GameObject>() {
            { "Melee", MeleePrefabEra3 },
            { "Ranged", RangedPrefabEra3 },
            { "Special", SpecialPrefabEra3 }
        };
        charactersForEras["Era4"] = new Dictionary<string, GameObject>() {
            { "Melee", MeleePrefabEra4 },
            { "Ranged", RangedPrefabEra4 },
            { "Special", SpecialPrefabEra4 }
        };
        charactersForEras["Era5"] = new Dictionary<string, GameObject>() {
            { "Melee", MeleePrefabEra5 },
            { "Ranged", RangedPrefabEra5 },
            { "Special", SpecialPrefabEra5 }
        };

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


    public void PauseGame() {
        Time.timeScale = 0f; // this pauses all time-based operations (movement, timers, animations, physics, etc)
    }

    public void ResumeGame() {
        Time.timeScale = 1f; // to resume normal time
    }


    public void SetGameOver(bool timeRanOff) {
        float p1HP = PlayerScript.Instance.sliderBaseHPP1.value;
        float p2HP = PlayerScript.Instance.sliderBaseHPP2.value;

        if (timeRanOff) {
            if (p1HP > p2HP) {
                Destroy(PlayerScript.Instance.baseP2);
                UIManager.Instance.SetGameOverInterface(0);

            } else if (p1HP < p2HP) {
                Destroy(PlayerScript.Instance.baseP1);
                UIManager.Instance.SetGameOverInterface(1);

            } else if (p1HP == p2HP) {
                UIManager.Instance.SetGameOverInterface(2);
            }

        // the bases are already destroyed when they get to 0
        } else {
            if (p1HP == 0) {
                UIManager.Instance.SetGameOverInterface(0);

            } else if (p2HP == 0) {
                UIManager.Instance.SetGameOverInterface(1);
            }
        }
    }

    public void Update() {
        SetGameOver(false);
    }
}