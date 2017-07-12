using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReached : MonoBehaviour {

    public GameObject AI;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Character")
        {
            AI.SetActive(false);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Character")
        {
            AI.SetActive(true);
        }
    }
}
