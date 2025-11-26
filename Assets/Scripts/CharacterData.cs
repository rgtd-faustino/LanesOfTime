using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data")]
public class CharacterData : ScriptableObject {
    public float health;
    public float speed;
    public float attack;
    public float attackSpeed;
    public int value;
    public int experience;
    public string type;
    public Vector3 rotationLeft = new Vector3(0, 90, 0);
    public Vector3 rotationRight = new Vector3(0, -90, 0);
}
