using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject characterPrefab;
    public int laneNumber;
    public bool isLeftSide;

    public void SpawnCharacter() {
        CharacterScript characterScript = Instantiate(characterPrefab, transform.position, transform.rotation).GetComponent<CharacterScript>();

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