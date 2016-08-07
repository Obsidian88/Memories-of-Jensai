using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderAudioClick : MonoBehaviour
{

    public AudioClip SoundOfSliderClick;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    private Slider slider { get { return GetComponent<Slider>(); } }

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = SoundOfSliderClick;
        source.playOnAwake = false;
        source.volume = 0.2f;
        Invoke("AddListener", 0.5f);
        
    }

    private void AddListener()
    {
        slider.onValueChanged.AddListener(PlaysoundSlider);
    }

    private void PlaysoundSlider(float newValue)
    {
        source.PlayOneShot(SoundOfSliderClick);
    }

}