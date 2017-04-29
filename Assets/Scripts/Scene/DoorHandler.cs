// Used to handle the door GUI and logic
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorHandler : MonoBehaviour {

    public AudioClip SoundOfDoorOpening;
    public AudioClip SoundOfDoorClosing;
    public Animator DoorAnimator;
    public Image EnterDoorPanel;

    private AudioSource Source { get { return GetComponent<AudioSource>(); } }
    private bool eWasPressed = false;

    //public Object sceneToLoadWhenEnteringDoor;
    private AsyncOperation async = null;
    public enum Scenes
    {
        // Needs to be done dynamically at some point.. currently only static names
        DayNightCycle, Prototype
    }
    public Scenes sceneToLoadWhenEnteringDoor;

    // Arriving on the destination scene, spawn the character on the designated door (there might be more doors in one scene)
    public string DestinationDoor;

    void Start()
    {
        Source.volume = 0.7f;
        Source.playOnAwake = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            DoorAnimator.SetTrigger("OpenDoor");
            EnterDoorPanel.gameObject.SetActive(true);
            Source.clip = SoundOfDoorOpening;
            Source.PlayOneShot(SoundOfDoorOpening);
        }
    }

    // Handles while character is within trigger range
    void OnTriggerStay(Collider other)
    {
        if (other.name == "Character")
        {
            if (Input.GetKeyDown(KeyCode.E) && (eWasPressed == false))
            {
                eWasPressed = true;
                // Load level
                StartCoroutine(LoadScene(sceneToLoadWhenEnteringDoor.ToString()));
            }
        }
    }

    public void chooseLevel(int levelArrayIdx)
    {
        string[] levels = new string[] { "Level1", "Level2", "Level3" };
        SceneManager.LoadSceneAsync(levels[levelArrayIdx]);
    }

    IEnumerator LoadScene(string levelname)
    {
        async = SceneManager.LoadSceneAsync(levelname);
        while (!async.isDone)
        {
            yield break;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.name == "Character")
        {
            DoorAnimator.SetTrigger("CloseDoor");
            EnterDoorPanel.gameObject.SetActive(false);
            Source.clip = SoundOfDoorClosing;
            Source.PlayOneShot(SoundOfDoorClosing);
        }
    }
}