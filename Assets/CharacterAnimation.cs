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
    }

    // Update is called once per frame
    void Update () {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = 0f;

        if (moveVertical == 0)
        {
            Anim.SetBool("idle", true);
        }
        else if (moveVertical > 0)
        {
            Anim.SetBool("idle", false);
            Facing = 12;
            Anim.SetInteger("facing", 12);
            Anim.SetFloat("vSpeed", moveVertical);
        }
        else if (moveVertical < 0)
        {
            Anim.SetBool("idle", false);
            Facing = 6;
            Anim.SetInteger("facing", 6);
            Anim.SetFloat("vSpeed", moveVertical);
        }


    }


}
