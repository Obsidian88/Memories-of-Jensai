using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windAnimation : MonoBehaviour
{
    private Animator anim;
    private WindDirection wind;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        wind = GameObject.FindWithTag("WindSystem").GetComponent < WindDirection > ();
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetInteger("windDirection", wind.windDirection);
        anim.SetFloat("windStrength", wind.currentWindstrength);
    }
}
