using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {

    public int Facing = 6; // facing direction, clock: 3 is right, 6 is down, 9 is left, 12 is up etc.

    private Animator Anim;
    private SpriteRenderer Renderer;


    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();
        Anim.SetInteger("facing", Facing);
    }

    // Update is called once per frame
    void Update () {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        Anim.SetFloat("vSpeed", moveVertical);
        Anim.SetFloat("hSpeed", moveHorizontal);

        if (moveVertical == 0 && moveHorizontal == 0)
        {
            Anim.SetBool("idle", true);
        }
        else
        {
            Anim.SetBool("idle", false);
        }

        if (moveHorizontal > 0 && moveVertical > 0)
        {
            Facing = 1;
        }
        if (moveHorizontal > 0 && moveVertical == 0)
        {
            Facing = 2;
        }
        if (moveHorizontal > 0 && moveVertical < 0)
        {
            Facing = 3;
        }
        else if (moveVertical < 0 && moveHorizontal == 0)
        {
            Facing = 4;
        }
        else if (moveVertical < 0 && moveHorizontal < 0)
        {
            Facing = 5;
        }
        else if (moveVertical == 0 && moveHorizontal < 0)
        {
            Facing = 6;
        }
        else if (moveVertical > 0 && moveHorizontal < 0)
        {
            Facing = 7;
        }
        else if (moveVertical > 0 && moveHorizontal == 0)
        {
            Facing = 0;
        }

        Anim.SetInteger("facing", Facing);

    }


}
