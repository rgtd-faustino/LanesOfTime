using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour {

    public static MenuUI Instance;

    public GameObject upgradesUI;
    public GameObject menuUI;
    public GameObject collectionUI;
    public GameObject campaignUI;




    public void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this);

        } else {
            Instance = this;
        }
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void OnBackButton(int n) {
        switch (n) {
            case 0:
                collectionUI.SetActive(false);
                menuUI.SetActive(true);
                break;
            case 1:
                upgradesUI.SetActive(false);
                menuUI.SetActive(true);
                break;
            case 2:
                campaignUI.SetActive(false);
                menuUI.SetActive(true);
                break;
        }
    }

    public void PlayCampaign() {
        menuUI.SetActive(false);
        campaignUI.SetActive(true);
    }

    public void PlayLevel(int level) {
        GameStateManager.Instance.isCampaign = true;
        GameStateManager.Instance.levelDifficulty = level;
        SceneManager.LoadScene("GameplayScene");
    }

    public void Play1v1() {
        GameStateManager.Instance.isCampaign = false;
        GameStateManager.Instance.levelDifficulty = -1;
        SceneManager.LoadScene("GameplayScene");
    }

    public void OpenCollection() {
        menuUI.SetActive(false);
        collectionUI.SetActive(true);
    }

    public void OpenUpgrades() {
        menuUI.SetActive(false);
        upgradesUI.SetActive(true);
    }

    public void QuitGame() {

    }
}
