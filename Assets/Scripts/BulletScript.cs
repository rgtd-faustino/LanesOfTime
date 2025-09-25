using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public CharacterScript character;

    private void Start() {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<CharacterScript>()) {
            CharacterScript enemy = other.gameObject.GetComponent<CharacterScript>();

            if (enemy.direction != character.direction) {
                enemy.health = Mathf.Clamp(enemy.health - character.attackDamage, 0, enemy.data.health);
                enemy.slider.value = enemy.health;

                Destroy(gameObject);
            }
        }

    }
}
