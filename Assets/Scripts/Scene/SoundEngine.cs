// Template that should be instanciated in every scene

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundEngine : MonoBehaviour
{
    public Text CurrentlyPlaying;

    private string absolutePathMusic = "Soundtrack";
    private string absolutePathAmbient = "AmbientSounds";
    private string currentScene = "Default";
    private string currentRegion = "Default";

    public AudioSource audioMusic;
    public AudioSource audioAmbient1;
    public AudioSource audioAmbient2;
    private AudioClip[] musicClips;
    private AudioClip[] ambientClips;

    private int lastMusic = 0;
    private int lastAmbient = 0;

    void Start()
    {
        LoadAudioClips();
        lastMusic = PlayMusic();
        lastAmbient = PlayAmbient();
        CurrentlyPlaying.text = "Currently playing: " + "\n" + "\"" + musicClips[lastMusic].ToString() + "\"";
    }

    void Update()
    {
        if (audioMusic.time > musicClips[lastMusic].length - 5)
        {
            StartCoroutine("RandomMusicPause");
        }
        if ((audioAmbient1.time > ambientClips[lastAmbient].length - 7) || (audioAmbient2.time > ambientClips[lastAmbient].length - 7))
        {
            StartCoroutine("RandomAmbientPause");
        }
    }

    void LoadAudioClips()
    {
            currentScene = SceneManager.GetActiveScene().name;
            switch (currentScene)
            {
                case "Scene1":
                    currentRegion = "Suntower_Day";
                    break;
                case "Scene2":
                    currentRegion = "Suntower_Night";
                    break;
                default:
                    Debug.LogWarning("Invalid scenename: Scene '" + currentScene + "' could not be found.");
                    break;
            }
        absolutePathMusic = absolutePathMusic + "/" + currentRegion;
        musicClips = Resources.LoadAll <AudioClip> (absolutePathMusic);

        absolutePathAmbient = absolutePathAmbient + "/" + currentRegion;
        ambientClips = Resources.LoadAll <AudioClip> (absolutePathAmbient);

        // Debug
        Debug.Log("Audioclips (Music) were loaded from " + absolutePathMusic);
        Debug.Log("Amount of music clips: " + musicClips.Length);
        Debug.Log("Audioclips (Ambient) were loaded from " + absolutePathAmbient);
        Debug.Log("Amount of ambient clips: " + ambientClips.Length);
    }

    private int PlayMusic()
    {
        int clipToPlay = Random.Range(0, musicClips.Length);
        audioMusic.clip = musicClips[clipToPlay];
        audioMusic.Play();
        return clipToPlay;
    }

    private int PlayAmbient()
    {
        int clipToPlay = Random.Range(0, ambientClips.Length);
        if (audioAmbient1.isPlaying)
        {
            audioAmbient2.clip = ambientClips[clipToPlay];
            audioAmbient2.Play();
        }
        else
        {
            audioAmbient1.clip = ambientClips[clipToPlay];
            audioAmbient1.Play();
        }
        return clipToPlay;
    }

    IEnumerator RandomMusicPause()
    {
        yield return new WaitForSeconds(Random.Range(15, 30)); // Wait between 15 and 60 seconds, then play next musicclip
        lastMusic = PlayMusic();
    }

    IEnumerator RandomAmbientPause()
    {
        yield return new WaitForSeconds(7);
        lastAmbient = PlayAmbient();
    }

}