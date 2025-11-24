using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public int laneNumber;
    public bool isLeftSide;



    void Start() {

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

        GameObject prefab = GameManager.Instance.charactersForEras[currentEra][characterType];
        CharacterScript characterScript = Instantiate(prefab, transform.position, transform.rotation).GetComponent<CharacterScript>();

        if (isLeftSide) {
            characterScript.gameObject.tag = "CharacterLeft";
            characterScript.gameObject.layer = LayerMask.NameToLayer("CharacterLeft");
            characterScript.transform.rotation = Quaternion.Euler(0, 90, 0);

        } else {
            characterScript.gameObject.tag = "CharacterRight";
            characterScript.gameObject.layer = LayerMask.NameToLayer("CharacterRight");
            characterScript.transform.rotation = Quaternion.Euler(0, -90, 0);
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
        CharacterData characterData = prefab.GetComponent<CharacterScript>().data;
        int value = characterData.value;
        
        PlayerScript.Instance.ChangeCoins(isLeftSide, - value);

    }
}