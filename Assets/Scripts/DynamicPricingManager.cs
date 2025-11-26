using System.Collections.Generic;
using UnityEngine;

public class DynamicPricingManager : MonoBehaviour {
    public static DynamicPricingManager Instance;

    public float costIncreasePerSpawn = 0.08f; // how much the spawned topp type cost increases per spawn
    public float meleeDecayOnRangedSpawn = 0.05f; // how much melee troops cost decreases when ranged is spawned
    public float rangedDecayOnMeleeSpawn = 0.06f; // the same but for ranged - melee
    public float specialDecayOnMeleeSpawn = 0.04f; // special - melee
    public float specialDecayOnRangedSpawn = 0.04f; // special - ranged
    public float meleeDecayOnSpecialSpawn = 0f; // melee - special
    public float rangedDecayOnSpecialSpawn = 0f; // special spawns don't affect other troops for now

    // limits for changing the costs
    public float maxCostMultiplier = 2.0f;
    public float minCostMultiplier = 0.7f;

    // the current (dynamic) costs for each side
    [HideInInspector] public int meleeCostLeft;
    [HideInInspector] public int rangedCostLeft;
    [HideInInspector] public int specialCostLeft;

    [HideInInspector] public int meleeCostRight;
    [HideInInspector] public int rangedCostRight;
    [HideInInspector] public int specialCostRight;

    private Dictionary<string, Dictionary<string, Dictionary<string, float>>> costMultipliers = new(); // [side][era][type] -> multiplier

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        InitializeTracking();
        UpdateAllCostVariables();
    }

    private void InitializeTracking() {
        string[] sides = { "Left", "Right" };
        string[] eras = { "Era1", "Era2", "Era3", "Era4", "Era5" };
        string[] types = { "Melee", "Ranged", "Special" };

        foreach (string side in sides) {
            costMultipliers[side] = new Dictionary<string, Dictionary<string, float>>();

            foreach (string era in eras) {
                costMultipliers[side][era] = new Dictionary<string, float>();

                foreach (string type in types) {
                    costMultipliers[side][era][type] = 1.0f; // default
                }
            }
        }
    }

    public void RegisterSpawn(bool isLeftSide, string era, string characterType) {
        string side = isLeftSide ? "Left" : "Right";

        // increase cost of the spawned type
        float currentMultiplier = costMultipliers[side][era][characterType];
        float newMultiplier = currentMultiplier + costIncreasePerSpawn;
        costMultipliers[side][era][characterType] = Mathf.Clamp(newMultiplier, minCostMultiplier, maxCostMultiplier); // max is max, can't go over

        // decrease cost of other types based on what was spawned
        switch (characterType) {
            case "Melee":
                // spawning a melee troop reduces ranged and special troops costs
                DecreaseOtherTypeCost(side, era, "Ranged", rangedDecayOnMeleeSpawn);
                DecreaseOtherTypeCost(side, era, "Special", specialDecayOnMeleeSpawn);
                break;

            case "Ranged":
                // spawning a ranged troop reduces melee and special troops costs
                DecreaseOtherTypeCost(side, era, "Melee", meleeDecayOnRangedSpawn);
                DecreaseOtherTypeCost(side, era, "Special", specialDecayOnRangedSpawn);
                break;

            case "Special":
                // spawning special troops doesn't affect other types for now
                DecreaseOtherTypeCost(side, era, "Melee", meleeDecayOnSpecialSpawn);
                DecreaseOtherTypeCost(side, era, "Ranged", rangedDecayOnSpecialSpawn);
                break;
        }

        // update the UI costs and the variables
        UpdateAllCostVariables();
        UIManager.Instance.UpdateTroopButtons(isLeftSide, GameManager.Instance.charactersForEras, era);
        UIManager.Instance.UpdateTroopButtons(!isLeftSide, GameManager.Instance.charactersForEras, isLeftSide ? GameManager.Instance.currentEraRight : GameManager.Instance.currentEraLeft);
    }

    private void DecreaseOtherTypeCost(string side, string era, string characterType, float decayAmount) {
        float currentMultiplier = costMultipliers[side][era][characterType];
        float newMultiplier = currentMultiplier - decayAmount;
        costMultipliers[side][era][characterType] = Mathf.Clamp(newMultiplier, minCostMultiplier, maxCostMultiplier); // min is the min, can't go below
    }

    public int GetDynamicCost(bool isLeftSide, string era, string characterType, int baseCost) {
        string side = isLeftSide ? "Left" : "Right";

        if (!costMultipliers.ContainsKey(side) || !costMultipliers[side].ContainsKey(era) || !costMultipliers[side][era].ContainsKey(characterType)) {
            return baseCost;
        }

        float multiplier = costMultipliers[side][era][characterType];
        int dynamicCost = Mathf.RoundToInt(baseCost * multiplier);

        return dynamicCost;
    }

    private void UpdateAllCostVariables() {
        // current eras
        string leftEra = GameManager.Instance.currentEraLeft;
        string rightEra = GameManager.Instance.currentEraRight;

        // base costs
        int baseMeleeLeft = GameManager.Instance.charactersForEras[leftEra]["Melee"].GetComponent<CharacterScript>().data.value;
        int baseRangedLeft = GameManager.Instance.charactersForEras[leftEra]["Ranged"].GetComponent<CharacterScript>().data.value;
        int baseSpecialLeft = GameManager.Instance.charactersForEras[leftEra]["Special"].GetComponent<CharacterScript>().data.value;

        int baseMeleeRight = GameManager.Instance.charactersForEras[rightEra]["Melee"].GetComponent<CharacterScript>().data.value;
        int baseRangedRight = GameManager.Instance.charactersForEras[rightEra]["Ranged"].GetComponent<CharacterScript>().data.value;
        int baseSpecialRight = GameManager.Instance.charactersForEras[rightEra]["Special"].GetComponent<CharacterScript>().data.value;

        // dynamic costs
        meleeCostLeft = GetDynamicCost(true, leftEra, "Melee", baseMeleeLeft);
        rangedCostLeft = GetDynamicCost(true, leftEra, "Ranged", baseRangedLeft);
        specialCostLeft = GetDynamicCost(true, leftEra, "Special", baseSpecialLeft);

        meleeCostRight = GetDynamicCost(false, rightEra, "Melee", baseMeleeRight);
        rangedCostRight = GetDynamicCost(false, rightEra, "Ranged", baseRangedRight);
        specialCostRight = GetDynamicCost(false, rightEra, "Special", baseSpecialRight);
    }
}