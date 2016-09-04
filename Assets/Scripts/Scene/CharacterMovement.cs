using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    
    public float MaxSpeed = 15f;
    public bool Dash = false;
    public float DashMulti = 3f;
    public float DashTime = 3f;
    public float DashCooldown = 3f;

    public float moveVertical;
    public float moveHorizontal;

    private float cooldown = 0f;
    private float duration = 0f;
    private float CurMaxSpeed = 0f;

    private Rigidbody Rigidbody;


    // Use this for initialization
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");
        float dash = Input.GetAxis("Jump");
        
        if (cooldown<=0)
        {
            if (dash > 0) { 
                if (!Dash && moveHorizontal + moveVertical != 0)
                {
                    Dash = true;
                    duration = DashTime;
                    CurMaxSpeed = MaxSpeed * DashMulti;
                }
                else
                {
                    duration -= Time.deltaTime;
                }

                if (duration <= 0.0f)
                {
                    Dash = false;
                    cooldown = DashCooldown;
                }

            }
            if (Dash && dash == 0)
            {
                Dash = false;
                cooldown = DashCooldown;
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
        }

        if (!Dash)
        {
            CurMaxSpeed = MaxSpeed;
        }

        Rigidbody.velocity = new Vector3(moveHorizontal * CurMaxSpeed, Rigidbody.velocity.y, moveVertical * CurMaxSpeed);
        
    }


}
