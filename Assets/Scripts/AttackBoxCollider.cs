using Unity.VisualScripting;
using UnityEngine;

public class AttackBoxCollider : MonoBehaviour
{
    CharacterScript character;

    private void Start() {
        character = GetComponentInParent<CharacterScript>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponentInParent<CharacterScript>() && 
            other.gameObject.GetComponentInParent<CharacterScript>().direction != character.direction && other.gameObject.CompareTag("Character")) {
            character.isAttacking = true;
            character.enemyToHit = other.gameObject.GetComponentInParent<CharacterScript>();
            character.HitEnemy();
        }
    }
}
