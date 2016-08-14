using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public int Facing = 6; // facing direction, clock: 3 is right, 6 is down, 9 is left, 12 is up etc.
    private float MaxSpeed = 15f;

    private Rigidbody Rigidbody;

    private Animator Anim;
    private SpriteRenderer Renderer;

    private CameraHandler CameraHandler;

    // Use this for initialization
    void Start () {
        Rigidbody = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
       // Renderer.shadowCastingMode = DontDestroyOnLoad;
       // transform.GetComponent<Renderer>().shadowCastingMode = shadowCastingMode.On;
       CameraHandler = GameObject.Find("Main Camera").GetComponent<CameraHandler>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Different Axis for different Camera & Characterperspektives / Rotations
        if (CameraHandler.Facing == 0)
        {
            Rigidbody.velocity = new Vector3(moveHorizontal * MaxSpeed, Rigidbody.velocity.y, moveVertical * MaxSpeed);
        }
        if (CameraHandler.Facing == 6 || CameraHandler.Facing == -6)
        {
            Rigidbody.velocity = new Vector3(-moveHorizontal * MaxSpeed, Rigidbody.velocity.y, -moveVertical * MaxSpeed);
        }
        if (CameraHandler.Facing == 3 || CameraHandler.Facing == -9)
        {
            Rigidbody.velocity = new Vector3(moveVertical * MaxSpeed, Rigidbody.velocity.y, -moveHorizontal * MaxSpeed);
        }
        if (CameraHandler.Facing == 9 || CameraHandler.Facing == -3)
        {
            Rigidbody.velocity = new Vector3(-moveVertical * MaxSpeed, Rigidbody.velocity.y, moveHorizontal * MaxSpeed);
        }
        
        //if (moveHorizontal > 0 && moveVertical > 0)
        //{
        //    Facing = 15;
        //    Anim.SetInteger("facing", 15);
        //    Anim.SetFloat("hSpeed", Mathf.Abs(moveHorizontal));
        //    Anim.SetFloat("vSpeed", Mathf.Abs(moveVertical));
        //}
        /*else*/ if (moveHorizontal > 0 && moveVertical == 0)
        {
            Facing = 3;
            Anim.SetInteger("facing", 3);
            Anim.SetFloat("hSpeed", Mathf.Abs(moveHorizontal));
        }
        //else if (moveHorizontal > 0 && moveVertical < 0)
        //{ 
        //    Facing = 45;
        //    Anim.SetInteger("facing", 45);
        //    Anim.SetFloat("hSpeed", Mathf.Abs(moveHorizontal));
        //    Anim.SetFloat("vSpeed", Mathf.Abs(moveVertical));
        //}
        else if (moveVertical < 0 && moveHorizontal == 0)
        {
            Facing = 6;
            Anim.SetInteger("facing", 6);
            Anim.SetFloat("vSpeed", Mathf.Abs(moveVertical));
        }
        //else if (moveVertical < 0 && moveHorizontal < 0)
        //{
        //    Facing = 75;
        //    Anim.SetInteger("facing", 75);
        //    Anim.SetFloat("hSpeed", Mathf.Abs(moveHorizontal));
        //    Anim.SetFloat("vSpeed", Mathf.Abs(moveVertical));
        //}
        else if (moveHorizontal < 0 && moveVertical == 0)
        {
            Facing = 9;
            Anim.SetInteger("facing", 9);
            Anim.SetFloat("hSpeed", Mathf.Abs(moveHorizontal));
        }
        //else if (moveHorizontal < 0 && moveVertical > 0)
        //{
        //    Facing = 105;
        //    Anim.SetInteger("facing", 105);
        //    Anim.SetFloat("hSpeed", Mathf.Abs(moveHorizontal));
        //    Anim.SetFloat("vSpeed", Mathf.Abs(moveVertical));
        //}
        else if (moveVertical > 0 && moveHorizontal == 0)
        {
            Facing = 12;
            Anim.SetInteger("facing", 12);
            Anim.SetFloat("vSpeed", Mathf.Abs(moveVertical));
        }
    }
}
