using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{


    public bool isCampaign = false; 
    [HideInInspector] public int levelDifficulty;


    public event System.Action<bool, int> OnGameSceneLoaded;

    public static GameStateManager Instance;

    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);

        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
