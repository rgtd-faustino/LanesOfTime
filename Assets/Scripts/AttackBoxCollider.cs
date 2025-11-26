using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackBoxCollider : MonoBehaviour {
    CharacterScript character;

    private void Start() {
        character = GetComponentInParent<CharacterScript>();
    }

    private void OnTriggerEnter(Collider other) {
        // is this is an enemy character?
        CharacterScript otherChar = other.gameObject.GetComponentInParent<CharacterScript>();
        LootBox lootBox = other.gameObject.GetComponent<LootBox>();

        if ((otherChar != null && otherChar.direction != character.direction && (other.gameObject.CompareTag("CharacterLeft") || other.gameObject.CompareTag("CharacterRight")))
            || lootBox != null) {

            character.targetsInLine.Add(other.gameObject);

            // if it's currently attacking the base then prioritize the enemy troop
            if (character.isTargetingBase) {
                // first we remove the base from the targets list
                foreach (GameObject target in character.targetsInLine) {
                    if (target.GetComponent<BaseScript>() != null) {
                        character.targetsInLine.Remove(target);
                        break;
                    }
                }

                // and then stop the current attack to start attacking the enemy
                character.StopAllCoroutines();
                character.isTargetingBase = false;
                character.targetToDamage = other.gameObject;
                character.isAttacking = true;
                character.HitEnemy();


            } else if (character.isAttacking == false) { // if not attacking at all then start attacking this enemy
                character.targetToDamage = other.gameObject;
                character.isTargetingBase = false;
                character.isAttacking = true;
                character.HitEnemy();
            }


            // if it's already attacking an enemy we don't do anything so the character kills the current target and then moves onto the new enemy that was added to the list


        } else if (other.gameObject.GetComponent<BaseScript>() != null &&
                 ((other.gameObject.CompareTag("BaseLeft") && character.direction == -1) ||
                  (other.gameObject.CompareTag("BaseRight") && character.direction == 1))) { // or is it the enemy base?

            // only attack the base if not already attacking something
            if (character.isAttacking == false) {
                character.targetsInLine.Add(other.gameObject);
                character.targetToDamage = other.gameObject;
                character.isTargetingBase = true;
                character.isAttacking = true;
                character.HitEnemy();
            }
        }
    }
}
