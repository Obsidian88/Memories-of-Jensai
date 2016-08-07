using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIAudio : MonoBehaviour {

    public AudioClip SoundOfButtonClick;
    public AudioClip SoundOfMenuToggle;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;

    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;
    public Toggle toggle4;
    public Toggle toggle5;

    public Dropdown dropdown1;
    public Dropdown dropdown2;
    public Dropdown dropdown3;

    public Slider slider1;

    public Image MenuPanel;
    public Image OptionsPanel;

    private string buttonname;

    void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = SoundOfButtonClick;
        source.playOnAwake = false;
        source.volume = 0.0f;

        //for(int i = 1; i < 5; i++)
        //{
        button1.onClick.AddListener(() => PlaysoundButtonMenu());
        button2.onClick.AddListener(() => PlaysoundButton());
        button3.onClick.AddListener(() => PlaysoundButtonMenu());
        button4.onClick.AddListener(() => PlaysoundButton());
        button5.onClick.AddListener(() => PlaysoundButton());
        button6.onClick.AddListener(() => PlaysoundButtonMenu());
        button7.onClick.AddListener(() => PlaysoundButtonMenu());

        toggle1.onValueChanged.AddListener(PlaysoundToggle);
        toggle2.onValueChanged.AddListener(PlaysoundToggle);
        toggle3.onValueChanged.AddListener(PlaysoundToggle);
        toggle4.onValueChanged.AddListener(PlaysoundToggle);
        toggle5.onValueChanged.AddListener(PlaysoundToggle);

        dropdown1.onValueChanged.AddListener(PlaysoundDropdown);
        dropdown2.onValueChanged.AddListener(PlaysoundDropdown);
        dropdown3.onValueChanged.AddListener(PlaysoundDropdown);

        slider1.onValueChanged.AddListener(PlaysoundSlider);
        //}
    }

    void Start()
    {

    }

    void Update()
    {
        if (MenuPanel.gameObject.activeSelf == true || OptionsPanel.gameObject.activeSelf == true)
        {
            source.volume = 0.4f;
        }
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F10))
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
        //source.PlayOneShot(SoundOfButtonClick);
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
