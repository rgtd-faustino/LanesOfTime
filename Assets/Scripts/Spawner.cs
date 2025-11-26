using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public int laneNumber;
    public bool isLeftSide;

    [Header("Pricing Settings")]
    [Tooltip("Percentage to increase cost per spawn (e.g., 0.05 = 5%)")]
    public float costIncreasePerSpawn = 0.08f;

    [Tooltip("Percentage to decrease cost per decay interval (e.g., 0.03 = 3%)")]
    public float costDecayRate = 0.04f;

    [Tooltip("How often (in seconds) to decay costs for unused units")]
    public float decayInterval = 10f;

    [Tooltip("Maximum cost multiplier (e.g., 2.0 = 200% of base cost)")]
    public float maxCostMultiplier = 2.5f;

    [Tooltip("Minimum cost multiplier (e.g., 0.5 = 50% of base cost)")]
    public float minCostMultiplier = 0.7f;

    // Track spawn counts: [side][era][type] -> spawn count
    private Dictionary<string, Dictionary<string, Dictionary<string, int>>> spawnCounts = new();

    // Track cost multipliers: [side][era][type] -> multiplier
    private Dictionary<string, Dictionary<string, Dictionary<string, float>>> costMultipliers = new();

    // Track time since last spawn: [side][era][type] -> time
    private Dictionary<string, Dictionary<string, Dictionary<string, float>>> timeSinceLastSpawn = new();

    private float decayTimer = 0f;


    public void SpawnCharacter() {
        string characterType;
        string currentEra;
        if (isLeftSide) {
            characterType = GameManager.Instance.characterTypeLeft;
            currentEra = GameManager.Instance.currentEraLeft;
        } else {
            characterType = GameManager.Instance.characterTypeRight;
            currentEra = GameManager.Instance.currentEraRight;
        }

        GameObject prefab = GameManager.Instance.charactersForEras[currentEra][characterType];
        CharacterData characterData = prefab.GetComponent<CharacterScript>().data;
        int baseCost = characterData.value;

        // Get dynamic cost
        int dynamicCost = DynamicPricingManager.Instance.GetDynamicCost(isLeftSide, currentEra, characterType, baseCost);

        // Check if player has enough coins
        int currentCoins = isLeftSide ? PlayerScript.Instance.coinsAmountP1 : PlayerScript.Instance.coinsAmountP2;
        if (currentCoins < dynamicCost) {
            Debug.Log("Not enough coins!");
            return;
        }

        // Spawn character
        CharacterScript characterScript = Instantiate(prefab, transform.position, prefab.transform.rotation).GetComponent<CharacterScript>();

        if (isLeftSide) {
            characterScript.gameObject.tag = "CharacterLeft";
            characterScript.gameObject.layer = LayerMask.NameToLayer("CharacterLeft");
            characterScript.transform.rotation = Quaternion.Euler(characterData.rotationLeft);
        } else {
            characterScript.gameObject.tag = "CharacterRight";
            characterScript.gameObject.layer = LayerMask.NameToLayer("CharacterRight");
            characterScript.transform.rotation = Quaternion.Euler(characterData.rotationRight);
        }

        characterScript.direction = isLeftSide ? 1 : -1;

        /*switch (laneNumber) {
            case 0:
                characterScript.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                break;
            case 1:
                characterScript.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                break;
            case 2:
                characterScript.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                break;
            case 3:
                characterScript.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                break;
            case 4:
                characterScript.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                break;
        }*/

        // the character script was instanced on this frame so the start method will only be called the next frame
        // but i can call the prefab and use that instead
        //PlayerScript.Instance.ChangeCoins(isLeftSide, - characterScript.value);

        // Register spawn and deduct dynamic cost
        DynamicPricingManager.Instance.RegisterSpawn(isLeftSide, currentEra, characterType);
        PlayerScript.Instance.ChangeCoins(isLeftSide, -dynamicCost);
    }
}