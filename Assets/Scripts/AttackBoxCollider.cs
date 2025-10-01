using Unity.VisualScripting;
using UnityEngine;

public class AttackBoxCollider : MonoBehaviour
{
    CharacterScript character;

    private void Start() {
        character = GetComponentInParent<CharacterScript>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponentInParent<CharacterScript>() &&
            other.gameObject.GetComponentInParent<CharacterScript>().direction != character.direction &&
            (other.gameObject.CompareTag("CharacterLeft") || other.gameObject.CompareTag("CharacterRight"))) {

            // if a singular character starts attacking 2 characters, instead of attacking once it will attack twice because there are two colliders hence the 
            // coroutine is called twice, so we must check if we're not attacking before starting an attack
            if (character.isAttacking == false) {
                character.isAttacking = true;
                character.targetToDamage = other.gameObject;
                character.isTargetingBase = false;
                character.HitEnemy();

                // if the target is focusing the base but a character spawns in front of it then it must focus the character that spawned, we stop the current
                // attack and realign the target
            } else if (character.isTargetingBase) {
                character.StopAllCoroutines();
                character.isAttacking = true;
                character.targetToDamage = other.gameObject;
                character.isTargetingBase = false;
                character.HitEnemy();
            }


            // if the ranged character is going forward, starts attacking another character but for some reason it's also in range
            // of the base, then it'll focus the base 'cause it's the last gameObject entering its attack box collider
            // so we need to also take into consideration that we must focus the initial target
        } else if (character.isAttacking == false && other.gameObject.GetComponent<BaseScript>() &&
            ((other.gameObject.CompareTag("BaseLeft") && character.direction == -1) ||
            (other.gameObject.CompareTag("BaseRight") && character.direction == 1))) {

            character.isAttacking = true;
            character.targetToDamage = other.gameObject;
            character.isTargetingBase = true;
            character.HitEnemy();
        }


    }
}
