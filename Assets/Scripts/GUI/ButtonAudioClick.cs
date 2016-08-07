using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonAudioClick : MonoBehaviour {

    public AudioClip SoundOfButtonClick;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    private Button button { get { return GetComponent<Button>(); } }

    // Use this for initialization
    void Start () {
        gameObject.AddComponent<AudioSource>();
        source.clip = SoundOfButtonClick;
        source.playOnAwake = false;
        source.volume = 0.45f;
        button.onClick.AddListener(() => PlaysoundButton());
    }

    private void PlaysoundButton()
    {
        source.PlayOneShot(SoundOfButtonClick);
    }

}