using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroHandler : MonoBehaviour {

    //public GameObject[] GameObjectsToAnimate;
    //public float Duration = 15;
    //private int ObjectIndex = 0;
    //private int ObjectLength;

    public VideoPlayer video;
    public GameObject text;
    private Text textcomponent;
    private bool oneShot = false;

    // Use this for initialization
    void Start () {
        textcomponent = text.GetComponent<Text>();
        //ObjectLength = GameObjectsToAnimate.Length;
        //Debug.Log("Length: " + ObjectLength);
        //GameObjectsToAnimate[ObjectIndex].SetActive(true);
        //StartCoroutine(Wait(Duration, ObjectIndex));
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cancelIntro();
        }
        if(!video.isPlaying && oneShot == false) //!video.isPlaying
        {
            oneShot = true;
            text.SetActive(true);
            StartCoroutine(Spass());
        }
        if (Input.GetKeyDown(KeyCode.Space) && oneShot)
        {
            cancelIntro();
        }
    }

    IEnumerator Spass()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime / 1.5f)
        {
            Debug.Log(textcomponent.color.a);
            textcomponent.color = new Color(textcomponent.color.r, textcomponent.color.g, textcomponent.color.b, i);
            yield return null;
        }
    }

    void cancelIntro()
    { 
        StaticData.LevelToLoad = "Prototype";
        SceneManager.LoadScene("LoadingScreen");
    }

    //IEnumerator Wait(float duration, int index)
    //{
    //    yield return new WaitForSeconds(duration);
    //    GameObjectsToAnimate[index].SetActive(false);
    //    if (index+1 < ObjectLength)
    //    {
    //        GameObjectsToAnimate[index+1].SetActive(true);
    //        StartCoroutine(Wait(Duration, index+1));
    //    }
    //}
}
