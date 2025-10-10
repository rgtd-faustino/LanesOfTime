using UnityEngine;

public class GameStateManager : MonoBehaviour
{


    public bool isCampaign = false;




    public static GameStateManager Instance;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
