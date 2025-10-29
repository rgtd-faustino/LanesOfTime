using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public CharacterScript character;

    private void Start() {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject != character.targetToDamage) {
            return;
        }

        if (character.isTargetingBase) {
            BaseScript target = other.gameObject.GetComponent<BaseScript>();

            if(target != null) {
                target.health = Mathf.Clamp(target.health - character.attackDamage, 0, target.maxHealth);
                target.slider.value = target.health;

                Destroy(gameObject);
            }

        } else {
            CharacterScript target = other.gameObject.GetComponent<CharacterScript>();

            if (target != null && target.direction != character.direction) {
                target.health = Mathf.Clamp(target.health - character.attackDamage, 0, target.data.health);
                target.slider.value = target.health;

                if (target.health == 0) {
                    character.targetsInLine.RemoveAt(0);
                    if (character.targetsInLine.Count != 0)
                        character.targetToDamage = character.targetsInLine[0];
                    else
                        character.targetToDamage = null;
                }

                Destroy(gameObject);
            }
        }

    }
}
