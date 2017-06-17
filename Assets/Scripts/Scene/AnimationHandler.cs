using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class AnimationHandler : MonoBehaviour {

    public GameObject text;				// GameObject for text
    private Text textcomponent;         // Textcompontn on GameObject: Spacebar to continue
    private VideoPlayer videoPlayer1;
    private VideoPlayer videoPlayer2;
    private bool inLoop = false;
    private bool Continue = false;

    private TextIdlePulseAnimation pulsescript;
    private float fadeDuration = 2f;

    public AudioClip SoundOfContinuing;
    private AudioSource source;

    public int FrameOfLoopbegin;
    public int FrameOfLoopend;

    public Image WhitePanel;

    // Use this for initialization
    void Start () {
        GameObject camera = GameObject.Find("Main Camera");

        // Video1
        videoPlayer1 = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer1.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer1.url = "Assets/Resources/Videos/Intro.mp4";
        videoPlayer1.playbackSpeed = 1.0f;

        // Video2
        videoPlayer2 = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer2.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer2.url = "Assets/Resources/Videos/MainTitle.mp4";
        videoPlayer2.playbackSpeed = 1.0f;
        videoPlayer2.enabled = false;

        videoPlayer1.Play();

        // Textstuff
        textcomponent = text.GetComponent<Text>();
        pulsescript = text.GetComponent<TextIdlePulseAnimation>();

        // Setup all the audiostuff
        source = gameObject.AddComponent<AudioSource>();
        source.clip = SoundOfContinuing;
        source.playOnAwake = false;
        source.volume = 0.85f;
    }
	
	// Update is called once per frame
	void Update () {
        // Intro is finished, now play Title
        if (!videoPlayer1.isPlaying) // or: 
        {
            videoPlayer1.enabled = false;
            videoPlayer2.enabled = true;
            videoPlayer2.Play();
        }

        // TITLE
        // If a certain frame is passed, mark it that we are in 'loop'
        if (videoPlayer2.frame >= FrameOfLoopbegin && !inLoop)
        {
            inLoop = true;
            text.SetActive(true);
            StartCoroutine(FadeInText(textcomponent));
        }

        // If in loop and the end of loop is being reached, rewind back
        if (inLoop && videoPlayer2.frame == FrameOfLoopend && !Continue)
        {
            videoPlayer2.frame = FrameOfLoopbegin;
            videoPlayer2.Play(); // not sure if necessary
        }

        // If end of video is reached switch to the next scene
        if (!videoPlayer2.isPlaying && videoPlayer2.enabled == true)
        {
            StaticData.LevelToLoad = "Prototype";
            SceneManager.LoadScene("LoadingScreen");
        }

        // BUTTONACTION
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ESC to skip intro (first video)
            if (videoPlayer1.isPlaying)
            {
                PlayContinueSound();
                videoPlayer1.Stop();
                videoPlayer1.enabled = false;
                videoPlayer2.enabled = true;
                videoPlayer2.Play();
            }
            // ESC to skip title (second video)
            if (videoPlayer2.frame > 30 && !Continue)
            {
                Continue = true;
                PlayContinueSound();
                //StartCoroutine(FadeOutText(textcomponent));
                StaticData.LevelToLoad = "Prototype";
                SceneManager.LoadScene("LoadingScreen");
            }
        }

        // Space / leftMouse to continue from title on
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !Continue && inLoop)
        {
            Continue = true;
            PlayContinueSound();
            EnableWhitePanel();
            StartCoroutine(FadeOutText(textcomponent));
        }
    }

    // Fadeout text over fadeDuration-timespan
    IEnumerator FadeOutText(Text text)
    {
        pulsescript.enabled = false;
        for (float i = 1; i >= 0; i -= Time.deltaTime / fadeDuration)
        {
            // set color with i as alpha
            if (text != null)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, i);
            }
            yield return null;
        }
    }

    // Fadein text over fadeDuration-timespan
    IEnumerator FadeInText(Text text)
    {
        for (float i = 0; i <= 1; i += Time.deltaTime / fadeDuration)
        {
            // set color with i as alpha
            if (text != null)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, i);
            }
            yield return null;
        }
        pulsescript.enabled = true;
    }

    private void PlayContinueSound()
    {
        source.PlayOneShot(SoundOfContinuing);
    }

    private void EnableWhitePanel()
    {
        WhitePanel.gameObject.SetActive(true);
    }
}
