using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Menu : MonoBehaviour
{
    // Used for menu UI
    public int ResX = 1920;
    public int ResY = 1080;
    public bool Fullscreen = true;

    public Text SoundstatusText;
    public Text CustomizationStatusText;
    public Text AudiovolumeText;

    // Used for toggle customization button
    public SpriteRenderer Skin;
    public SpriteRenderer Hair;
    public SpriteRenderer Eye;
    public SpriteRenderer Torso;
    public SpriteRenderer Leg;

    public Image MenuPanel;
    public Image OptionsPanel;
    public Image OutsideMask; 

    private bool CustToggle = true;

    private float hSliderValue = 1.0f;

    public Dropdown Resolution;
    public Dropdown Refreshrate;
    public Dropdown Antialiasing;
    public Slider Audiovolume;    

    // Use this for initialization
    void Start()
    {
        MenuPanel.gameObject.SetActive(false);
        OptionsPanel.gameObject.SetActive(false);
        OutsideMask.gameObject.SetActive(false);
        LoadSettings();
        Resolution.onValueChanged.AddListener(delegate { ChangeResolution(); });
        Refreshrate.onValueChanged.AddListener(delegate { ChangeRefreshrate(); });
        Antialiasing.onValueChanged.AddListener(delegate { ChangeAntiAliasing(); });
        Audiovolume.onValueChanged.AddListener(delegate { ChangeAudiovolume(); });
    }

    // Update is called once per frame
    void Update()
    {
        // What happens if you press escape or f10..
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F10))
        {
            if (MenuPanel.gameObject.activeSelf == true)
            {
                if(OptionsPanel.gameObject.activeSelf == true)
                {
                    LeaveOptions();
                }
                else
                {
                    LeaveMenu();
                }
            }
            else
            {
                if (OptionsPanel.gameObject.activeSelf == true)
                {
                    LeaveOptions();
                }
                else
                {
                    LoadMenu();
                }
            }
        }
    }

    public void LoadCustomization()
    {
        SceneManager.LoadScene("Character Customization");
    }

    public void LoadMenu()
    {
        MenuPanel.gameObject.SetActive(true);
        OutsideMask.gameObject.SetActive(true);
    }

    public void LoadOptions()
    {
        MenuPanel.gameObject.SetActive(false);
        OptionsPanel.gameObject.SetActive(true);
    }

    public void LeaveOptions()
    {
        MenuPanel.gameObject.SetActive(true);
        OptionsPanel.gameObject.SetActive(false);
    }

    public void LeaveMenu()
    {
        MenuPanel.gameObject.SetActive(false);
        OutsideMask.gameObject.SetActive(false);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ToggleVsync()
    {
        if(QualitySettings.vSyncCount == 1)
        { 
            QualitySettings.vSyncCount = 0;
            Debug.Log("Graphics: vSync was deactivated");
        }
        else if (QualitySettings.vSyncCount == 0)
        {
            QualitySettings.vSyncCount = 1;
            Debug.Log("Graphics: vSync was activated");
        }
    }

    // TODO: Need a different item like a button or so ..
    public void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel();
        Debug.Log("Graphics: Quality was increased");
    }

    public void DecreaseQuality()
    {
        QualitySettings.DecreaseLevel();
        Debug.Log("Graphics: Quality was decreased");
    }

    private void ChangeResolution()
    {
        if(Resolution.value == 0)
        {
            ResX = 1920;
            ResY = 1080;
            Debug.Log("Graphics: Resolution set to 1080p");
        }
        else if (Resolution.value == 1)
        {
            ResX = 1280;
            ResY = 720;
            Debug.Log("Graphics: Resolution set to 720p");
        }
        else if (Resolution.value == 2)
        {
            ResX = 640;
            ResY = 480;
            Debug.Log("Graphics: Resolution set to 480p");
        }
        else
        {
            ResX = 1280;
            ResY = 720;
            Debug.Log("Graphics: Resolution set to standard (720p)");
        }
        Screen.SetResolution(ResX, ResY, Fullscreen);
    }

    public void ToggleAniso()
    {
        if(QualitySettings.anisotropicFiltering == AnisotropicFiltering.ForceEnable)
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            Debug.Log("Graphics: Anisotropic Filtering was disabled");
        }
        else if (QualitySettings.anisotropicFiltering == AnisotropicFiltering.Disable)
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
            Debug.Log("Graphics: Anisotropic Filtering was enabled");
        }

    }

    public void ToggleTripleBuffering()
    {
        if(QualitySettings.maxQueuedFrames == 3)
        {
            QualitySettings.maxQueuedFrames = 2;
            Debug.Log("Graphics: Tripple Buffering was disabled");
        }
        else if (QualitySettings.maxQueuedFrames == 2)
        {
            QualitySettings.maxQueuedFrames = 3;
            Debug.Log("Graphics: Tripple Buffering was enabled");
        }
    }

    public void ChangeRefreshrate()
    {
        if(Refreshrate.value == 0)
        {
            Screen.SetResolution(ResX, ResY, Fullscreen, 60);
            Debug.Log("Graphics: Refreshrate is now 60Hz");
        }
        else if (Refreshrate.value == 1)
        {
            Screen.SetResolution(ResX, ResY, Fullscreen, 120);
            Debug.Log("Graphics: Refreshrate is now 120Hz");
        }
        else
        {
            Screen.SetResolution(ResX, ResY, Fullscreen, 60);
            Debug.Log("Graphics: Refreshrate is now standard (60Hz)");
        }
    }

    public void ChangeAntiAliasing()
    {
        if(Antialiasing.value == 0)
        {
            QualitySettings.antiAliasing = 0;
            Debug.Log("Graphics: Antialiasing is now disabled");
        }
        else if(Antialiasing.value == 1)
        {
            QualitySettings.antiAliasing = 2;
            Debug.Log("Graphics: Antialiasing is 2x");
        }
        else if (Antialiasing.value == 2)
        {
            QualitySettings.antiAliasing = 4;
            Debug.Log("Graphics: Antialiasing is 4x");
        }
        else if (Antialiasing.value == 3)
        {
            QualitySettings.antiAliasing = 8;
            Debug.Log("Graphics: Antialiasing is 8x");
        }
        else
        {
            QualitySettings.antiAliasing = 0;
            Debug.Log("Graphics: Antialiasing is now standard (disabled)");
        }
    }

    public void ChangeAudiovolume()
    {
        AudioListener.volume = Audiovolume.value;
        PlayerPrefs.SetFloat("Audiovolume", Audiovolume.value);
        AudiovolumeText.text = "Volume: " + (Mathf.Round(Audiovolume.value * 100f) / 100f).ToString();
    }

    void LoadSettings()
    {
        // Load Saved Audio Volume
        if (PlayerPrefs.HasKey("Audiovolume"))
        {
            Audiovolume.value = PlayerPrefs.GetFloat("Audiovolume");
            AudioListener.volume = Audiovolume.value;
            AudiovolumeText.text = "Volume: " + (Mathf.Round(Audiovolume.value * 100f) / 100f).ToString();
        }
        else
        {
            Audiovolume.value = 1f;
            AudioListener.volume = 1f;
            AudiovolumeText.text = "Volume: " + (Mathf.Round(Audiovolume.value * 100f) / 100f).ToString();
        }

        // Load Saved Audio Mute
        if (PlayerPrefs.HasKey("AudioMute"))
        {
            var audioMute = PlayerPrefs.GetInt("AudioMute");
            if (audioMute == 1)
            {
                Audiovolume.value = PlayerPrefs.GetFloat("Audiovolume");
                AudioListener.volume = 0f;
                SoundstatusText.text = "Sound muted";
            }
            else
            {
                Audiovolume.value = PlayerPrefs.GetFloat("Audiovolume");
                AudioListener.volume = Audiovolume.value;
                SoundstatusText.text = "";
            }
        }
        else
        {
            AudioListener.volume = 1f;
        }

        // Load Saved Customization Toggle
        if (PlayerPrefs.HasKey("CustToggle"))
        {
            var customizationToggle = PlayerPrefs.GetInt("CustToggle");
            if (customizationToggle == 1)
            {
                CustToggle = true;
                DisableCustomization();
                CustomizationStatusText.text = "Custom. toggled";
            }
            else
            {
                CustToggle = false;
                EnableCustomization();
                CustomizationStatusText.text = "";
            }
        }
        else
        {
            CustToggle = true;
            DisableCustomization();
            CustomizationStatusText.text = "Custom. toggled";
        }
    }

    public void ToggleAudio()
    {
        if (AudioListener.volume > 0f)
        {
            AudioListener.volume = 0f;
            PlayerPrefs.SetInt("AudioMute", 1);
            SoundstatusText.text = "Sound muted";
        }
        else if (AudioListener.volume == 0f)
        {
            AudioListener.volume = hSliderValue;
            PlayerPrefs.SetInt("AudioMute", 0);
            SoundstatusText.text = "";
        }
    }

    public void ToggleCustomization()
    {
        if (CustToggle == false)
        {
            DisableCustomization();
            CustToggle = true;
            PlayerPrefs.SetInt("CustToggle", 1);
            CustomizationStatusText.text = "Custom. toggled";
        }
        else if (CustToggle == true)
        {
            EnableCustomization();
            CustToggle = false;
            PlayerPrefs.SetInt("CustToggle", 0);
            CustomizationStatusText.text = "";
        }
    }

    void EnableCustomization()
    {
        Hair.enabled = true;
        Eye.enabled = true;
        Torso.enabled = true;
        Leg.enabled = true;
    }

    void DisableCustomization()
    {
        Hair.enabled = false;
        Eye.enabled = false;
        Torso.enabled = false;
        Leg.enabled = false;
    }
}