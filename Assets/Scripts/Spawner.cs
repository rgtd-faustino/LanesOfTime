using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public Dictionary<string, Dictionary<string, GameObject>> charactersForEras  = new Dictionary<string, Dictionary<string, GameObject>>();


    public int laneNumber;
    public bool isLeftSide;

    public GameObject MeleePrefabEra1;
    public GameObject RangedPrefabEra1;
    public GameObject SpecialPrefabEra1;

    public GameObject MeleePrefabEra2;
    public GameObject RangedPrefabEra2;
    public GameObject SpecialPrefabEra2;

    public GameObject MeleePrefabEra3;
    public GameObject RangedPrefabEra3;
    public GameObject SpecialPrefabEra3;

    void Start() {
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
    }

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

        CharacterScript characterScript = Instantiate(charactersForEras[currentEra][characterType], transform.position, transform.rotation).GetComponent<CharacterScript>();

        characterScript.direction = isLeftSide ? 1 : -1;

        switch (laneNumber) {
            case 1:
                characterScript.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
                break;
            case 2:
                characterScript.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                break;
        }
    }
}