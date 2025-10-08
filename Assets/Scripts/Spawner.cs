using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public Dictionary<string, Dictionary<string, (GameObject prefab, int value)>> charactersForEras  = new();


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
        charactersForEras["Era1"] = new Dictionary<string, (GameObject prefab, int value)>() {
            { "Melee", (MeleePrefabEra1, 5) },
            { "Ranged", (RangedPrefabEra1, 7) },
            { "Special", (SpecialPrefabEra1, 10) }
        };
            charactersForEras["Era2"] = new Dictionary<string, (GameObject prefab, int value)>() {
            { "Melee", (MeleePrefabEra2, 5) },
            { "Ranged", (RangedPrefabEra2, 7) },
            { "Special", (SpecialPrefabEra2, 10) }
        };
            charactersForEras["Era3"] = new Dictionary<string, (GameObject prefab, int value)>() {
            { "Melee", (MeleePrefabEra3, 5) },
            { "Ranged", (RangedPrefabEra3, 7) },
            { "Special", (SpecialPrefabEra3, 10) }
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

        CharacterScript characterScript = Instantiate(charactersForEras[currentEra][characterType].prefab, transform.position, transform.rotation).GetComponent<CharacterScript>();
        characterScript.type = characterType;

        if (isLeftSide) {
            characterScript.gameObject.tag = "CharacterLeft";
            characterScript.gameObject.layer = LayerMask.NameToLayer("CharacterLeft");

        } else {
            characterScript.gameObject.tag = "CharacterRight";
            characterScript.gameObject.layer = LayerMask.NameToLayer("CharacterRight");
        }

        characterScript.direction = isLeftSide ? 1 : -1;

        switch (laneNumber) {
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
        }


        if (PlayerScript.Instance.coinsAmountP1 >= charactersForEras[currentEra][characterType].value) {
            if (isLeftSide) {
                PlayerScript.Instance.TakeCoinsP1(charactersForEras[currentEra][characterType].value);
                UIManager.Instance.UpdateTroopButtons(true, charactersForEras, currentEra, characterType);

            } else {
                PlayerScript.Instance.TakeCoinsP2(charactersForEras[currentEra][characterType].value);
                UIManager.Instance.UpdateTroopButtons(false, charactersForEras, currentEra, characterType);
            }

        }
    }
}