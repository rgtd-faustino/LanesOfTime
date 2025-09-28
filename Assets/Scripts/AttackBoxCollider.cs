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
            character.isAttacking = true;
            character.targetToDamage = other.gameObject;
            character.isTargetingBase = false;
            character.HitEnemy();

        } else if (other.gameObject.GetComponent<BaseScript>() &&
            ((other.gameObject.CompareTag("BaseLeft") && character.direction == -1) ||
            (other.gameObject.CompareTag("BaseRight") && character.direction == 1))) {
            character.isAttacking = true;
            character.targetToDamage = other.gameObject;
            character.isTargetingBase = true;
            character.HitEnemy();
        }


    }
}
