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

    public VideoPlayer video1;
    public VideoPlayer video2;
    public VideoPlayer video3;
    public GameObject text;
    private Text textcomponent;
    private bool cancelling = false;

    // Use this for initialization
    void Start () {
        textcomponent = text.GetComponent<Text>();
        //ObjectLength = GameObjectsToAnimate.Length;
        //Debug.Log("Length: " + ObjectLength);
        //GameObjectsToAnimate[ObjectIndex].SetActive(true);
        //StartCoroutine(Wait(Duration, ObjectIndex));
        StartCoroutine(PlayVideoUntilLoop());
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && cancelling == false)
        {
            cancelling = true;
            StartCoroutine(PlayVideoAfterLoop());
            // cancelIntro();
        }
        if (Input.GetKeyDown(KeyCode.Space) && cancelling == false)
        {
            cancelling = true;
            StartCoroutine(PlayVideoAfterLoop());
            //cancelIntro();
        }
    }

    IEnumerator PlayVideoUntilLoop()
    {
        video1.url = "Assets/Resources/Videos/MainTitleStart.mp4";
        video1.Play();
        video2.url = "Assets/Resources/Videos/MainTitleMiddleLoop.mp4";
        video2.Prepare();

        while (video1.isPlaying)
        {
            yield return null;
        }
        //video1.Stop();

        video2.isLooping = true;
        video2.Play();
        video3.url = "Assets/Resources/Videos/MainTitleEnd.mp4";
        video3.Prepare();
    }

    IEnumerator PlayVideoAfterLoop()
    {
        video2.isLooping = false;
        while (video2.isPlaying)
        {
            yield return null;
        }
        video2.Stop();
        video3.Play();
        while (video3.isPlaying)
        {
            yield return null;
        }
        video3.Stop();
        StaticData.LevelToLoad = "Prototype";
        SceneManager.LoadScene("LoadingScreen");
    }















    void cancelIntro()
    {
    }

    void videoFinished(UnityEngine.Video.VideoPlayer vp)
        {
        vp.Stop();
        text.SetActive(true);
        StartCoroutine(Spass());
        vp.url = "Assets/Resources/Videos/MainTitleMiddleLoop.mp4";
        vp.Play();
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
