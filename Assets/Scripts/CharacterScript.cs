using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    private Rigidbody rb;
    [HideInInspector] public int direction;
    private int speed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = direction * speed * Time.deltaTime * Vector3.right;
        rb.MovePosition(rb.position + movement);
    }
}
