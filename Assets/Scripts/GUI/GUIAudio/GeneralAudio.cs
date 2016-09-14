using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GeneralAudio : MonoBehaviour
{

    public AudioClip SoundOfButtonClick;
    public AudioClip SoundOfMenuToggle;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    public Button[] Buttons;
    public Button[] MenuButtons;

    public Toggle[] Toggles;

    public Dropdown[] Dropdowns;

    public Slider[] Sliders;

    public Image MenuPanel;
    public Image OptionsPanel;

    void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = SoundOfButtonClick;
        source.playOnAwake = false;
        // Needed for initial click on sceneload, playOnAwake somehow doesn't intercept it
        source.volume = 0.0f;

        foreach (Button CurrentButton in Buttons)
        {
            CurrentButton.onClick.AddListener(() => PlaysoundButton());
        }

        foreach (Button CurrentMenuButton in MenuButtons)
        {
            CurrentMenuButton.onClick.AddListener(() => PlaysoundButtonMenu());
        }

        foreach (Toggle CurrentToggle in Toggles)
        {
            CurrentToggle.onValueChanged.AddListener(PlaysoundToggle);
        }

        foreach (Dropdown CurrentDropdown in Dropdowns)
        {
            CurrentDropdown.onValueChanged.AddListener(PlaysoundDropdown);
        }

        foreach (Slider CurrentSlider in Sliders)
        {
            CurrentSlider.onValueChanged.AddListener(PlaysoundSlider);
        }
    }

    void Update()
    {
        if (MenuPanel.gameObject.activeSelf == true || OptionsPanel.gameObject.activeSelf == true)
        {
            source.volume = 0.4f;
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F10))
        {
            source.PlayOneShot(SoundOfMenuToggle);
        }
    }

    private void PlaysoundButton()
    {
        source.PlayOneShot(SoundOfButtonClick);
    }

    private void PlaysoundButtonMenu()
    {
        source.PlayOneShot(SoundOfMenuToggle);
    }

    private void PlaysoundToggle(bool isClicked)
    {
        source.PlayOneShot(SoundOfButtonClick);
    }

    private void PlaysoundDropdown(int whatWasClicked)
    {
        source.PlayOneShot(SoundOfButtonClick);
    }

    private void PlaysoundSlider(float whatWasClicked)
    {
        source.PlayOneShot(SoundOfButtonClick);
    }
}
