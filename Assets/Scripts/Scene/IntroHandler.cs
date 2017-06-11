using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroHandler : MonoBehaviour {

    public GameObject[] GameObjectsToAnimate;
    public float Duration = 15;
    private int ObjectIndex = 0;
    private int ObjectLength;

	// Use this for initialization
	void Start () {
        
        ObjectLength = GameObjectsToAnimate.Length;
        Debug.Log("Length: " + ObjectLength);
        GameObjectsToAnimate[ObjectIndex].SetActive(true);
        StartCoroutine(Wait(Duration, ObjectIndex));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Wait(float duration, int index)
    {
        yield return new WaitForSeconds(duration);
        GameObjectsToAnimate[index].SetActive(false);
        if (index+1 < ObjectLength)
        {
            GameObjectsToAnimate[index+1].SetActive(true);
            StartCoroutine(Wait(Duration, index+1));
        }
    }
}
