using UnityEngine;

public class GameManager : MonoBehaviour {
    [Header("Spawners")]
    public Spawner[] leftSpawners = new Spawner[3];
    public Spawner[] rightSpawners = new Spawner[3];

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
        if (isLeft)
            laneToDeployLeft = lane;
        else
            laneToDeployRight = lane;
    }

    public void SpawnCharacter(bool isLeft) {
        if (isLeft) {
            leftSpawners[laneToDeployLeft].SpawnCharacter();
        } else {
            rightSpawners[laneToDeployRight].SpawnCharacter();
        }
    }
}