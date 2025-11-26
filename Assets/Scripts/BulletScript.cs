using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public CharacterScript character;

    private void Start() {

    }

    private void OnTriggerEnter(Collider other) {
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

            LootBox targetBox = other.gameObject.GetComponent<LootBox>();
            if (targetBox != null) {
                targetBox.health = Mathf.Clamp(targetBox.health - character.attackDamage, 0, targetBox.maxHealth);
                targetBox.slider.value = targetBox.health;
                if (targetBox.health == 0) {
                    if (targetBox.isGoldBox == 0)
                        PlayerScript.Instance.ChangeCoins(character.direction == 1, targetBox.goldLoot);
                    else
                        PlayerScript.Instance.ChangeExperience(character.direction == 1, targetBox.xpLoot);
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