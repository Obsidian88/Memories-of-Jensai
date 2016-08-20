using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    
    private float MaxSpeed = 15f;

    private Rigidbody Rigidbody;


    // Use this for initialization
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Rigidbody.velocity = new Vector3(moveHorizontal * MaxSpeed, Rigidbody.velocity.y, moveVertical * MaxSpeed);
        
    }


}
