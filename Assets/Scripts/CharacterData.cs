using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data")]
public class CharacterData : ScriptableObject {
    public float health;
    public float speed;
    public float attack;
    public float attackSpeed;
    public int value;
    public int experience;
}
