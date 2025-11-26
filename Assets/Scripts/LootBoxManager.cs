using System.Collections.Generic;
using UnityEngine;

public class LootBoxManager : MonoBehaviour {
    public GameObject lootBoxPrefab;
    public Transform[] spawnLocations = new Transform[5];

    private float minSpawnInterval = 30f; // 30
    private float maxSpawnInterval = 60f; // 60

    private float nextSpawnTime;
    private Dictionary<Transform, bool> availableSpawners = new();

    public static LootBoxManager Instance;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start() {
        foreach (Transform spawner in spawnLocations) {
            availableSpawners[spawner] = true;
        }
        SetNextSpawnTime();
    }

    void Update() {
        if (Time.time >= nextSpawnTime) {
            SpawnLootBox();
            SetNextSpawnTime();
        }
    }

    void SpawnLootBox() {
        List<Transform> emptySpawners = new();
        foreach (Transform spawner in availableSpawners.Keys) {
            if (availableSpawners[spawner]) {
                emptySpawners.Add(spawner);
            }
        }

        if (emptySpawners.Count == 0)
            return;

        Transform spawnPoint = emptySpawners[Random.Range(0, emptySpawners.Count)];
        GameObject lootBoxObj = Instantiate(lootBoxPrefab, spawnPoint.position, spawnPoint.rotation);
        LootBox lootBox = lootBoxObj.GetComponent<LootBox>();
        lootBox.spawner = spawnPoint;
        availableSpawners[spawnPoint] = false;
    }

    public void FreeSpawner(Transform spawner) {
        if (availableSpawners.ContainsKey(spawner)) {
            availableSpawners[spawner] = true;
        }
    }

    void SetNextSpawnTime() {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}