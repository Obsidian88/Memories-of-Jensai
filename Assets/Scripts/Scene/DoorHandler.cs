// Used to handle the door GUI
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorHandler : MonoBehaviour {

    public AudioClip SoundOfDoorOpening;
    public AudioClip SoundOfDoorClosing;
    public Animator DoorAnimator;
    public Image EnterDoorPanel;
    private AudioSource Source { get { return GetComponent<AudioSource>(); } }

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