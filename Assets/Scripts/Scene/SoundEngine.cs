// Template that should be instanciated in every scene

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundEngine : MonoBehaviour
{
    public Text CurrentlyPlaying;

    private string absolutePathMusic = "Audio/Soundtrack";
    private string absolutePathAmbient = "Audio/AmbientSounds";
    private string currentScene = "Default";
    private string currentRegion = "Default";

    public AudioSource audioMusic;
    public AudioSource audioAmbient1;
    public AudioSource audioAmbient2;
    private AudioClip[] musicClips;
    private AudioClip[] ambientClips;

    private int lastMusic = 0;
    private int lastAmbient = 0;

    private bool musicPause = false;
    private float musicPauseDuration = 15f;

    void Start()
    {
        musicPauseDuration = Random.Range(15.0f, 30.0f);
        LoadAudioClips();
        lastMusic = PlayMusic();
        lastAmbient = PlayAmbient();
    }

    void Update()
    {
        if (!audioMusic.isPlaying)
        {
            if (!musicPause)
            {
                StartCoroutine("RandomMusicPause");
            }
            musicPauseDuration -= Time.deltaTime;
            CurrentlyPlaying.text = "Currently playing: " + "\n" + "Random pause between music (" + Mathf.Round(musicPauseDuration) + ")";
        } 
        else
        {
            SetText(musicClips[lastMusic].ToString());
        }

        if (!audioAmbient1.isPlaying || !audioAmbient2.isPlaying)
        {
            StartCoroutine("RandomAmbientPause");
        }

        // Not working so far .. need to implement a crossfader between two musicclips that fades in and out softly.. ?
        //if (audioMusic.time == musicClips[lastMusic].length - 5)
        //{
        //    StartCoroutine("RandomMusicPause");
        //}
        //if ((audioAmbient1.time == ambientClips[lastAmbient].length - 7) || (audioAmbient2.time > ambientClips[lastAmbient].length - 7))
        //{
        //    StartCoroutine("RandomAmbientPause");
        //}
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
        SetText(musicClips[clipToPlay].ToString());
        return clipToPlay;
    }

    private int PlayAmbient()
    {
        int clipToPlay = Random.Range(0, ambientClips.Length);
        Debug.Log("Last Ambient was: " + lastAmbient);
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
        musicPause = true;
        musicPauseDuration = Random.Range(15.0f, 30.0f);
        yield return new WaitForSeconds(musicPauseDuration); // Wait between 15 and 30 seconds, then play next musicClip
        lastMusic = PlayMusic();
        musicPause = false;
    }

    IEnumerator RandomAmbientPause()
    {
        yield return new WaitForSeconds(7); // Wait 7 seconds, then play next ambientClip
        lastAmbient = PlayAmbient();
    }
	
    private void SetText(string Songname)
    {
        Songname = Songname.Remove(Songname.Length - 24); // Get rid of automatic stringending "(UnityEngine.AudioClip)"
        string currentTime = TimeToMinutesAndSeconds(audioMusic.time);
        string totalTime = TimeToMinutesAndSeconds(musicClips[lastMusic].length);

        // Display everything onscreen
        CurrentlyPlaying.text = "Currently playing: " + "\n" + "\u0022" + Songname + ".mp3\u0022" + "\n" + currentTime + " / " + totalTime;
    }

    private string TimeToMinutesAndSeconds(float time)
    {
        // Get current playtime's minute and second
        string minutes = ((time / 60) % 60).ToString();
        string seconds = (time % 60).ToString();

        // Chop off milliseconds
        if (minutes.IndexOf(".") > 0 && seconds.IndexOf(".") > 0)
        {
            minutes = minutes.Substring(0, minutes.IndexOf("."));
            seconds = seconds.Substring(0, seconds.IndexOf("."));
        }

        // Fill up zeros for better readability
        if (minutes.Length == 1)
        {
            minutes = "0" + minutes;
        }
        if (seconds.Length == 1)
        {
            seconds = "0" + seconds;
        }

        return minutes + ":" + seconds;
    }
}