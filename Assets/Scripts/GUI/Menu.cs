using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Menu : MonoBehaviour
{
    // Used for menu UI
    private int ResolutionWidth = 1920;
    private int ResolutionHeight = 1080;
    //private bool Fullscreen = true;

	public Image SoundstatusPanel;			// A Panel that tells if audio is muted or not and contains an icon + text
    public Image ClothstatusPanel;          // A Panel that tells if cloth is toggled or not and contains an icon + text

    //public Text CustomizationStatusText; 	// Tells if customization is toggled or not
    public Text AudiovolumeText; 			// Tells current SliderAudiovolume (in menu)

    // Used for toggle customization button
    //public SpriteRenderer Skin;
    public SpriteRenderer Hair;
    public SpriteRenderer Eye;
    //public SpriteRenderer Torso;
    //public SpriteRenderer Leg;

	// Different panels
    public Image MenuPanel;
    public Image OptionsPanel;
    public Image OutsideMask;

	public Toggle ToggleVsync;
	public Toggle ToggleAniso;
	public Toggle ToggleTriple;
    public Toggle ToggleFullscreen;
    //public Toggle ToggleCustomization;
    public Dropdown DropdownResolution;
	public Dropdown DropdownQuality;
    public Dropdown DropdownRefreshrate;
    public Dropdown DropdownAntialiasing;
    public Slider SliderAudiovolume;
	public Button ButtonAudioMute;

    // Use this for initialization
    void Start()
    {
		// Set all menues initially to invisible (just in case)
        MenuPanel.gameObject.SetActive(false);
        OptionsPanel.gameObject.SetActive(false);
        OutsideMask.gameObject.SetActive(false);
		
        LoadSettings();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F10))
        {
			ToggleMenues();
		}
    }

	
	
	//
	// MENU
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
	
	// What happens if you press Escape or F10 depending on currently opened menue..
	public void ToggleMenues()
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
	
	public void LoadCustomization()
    {
		StaticData.currentLevel = "Character Customization";
        SceneManager.LoadScene("LoadingScreen");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

	
	
	//
	// OPTIONS
    public void ChangeVsync()
    {
        if(QualitySettings.vSyncCount == 1)
        {
            PlayerPrefs.SetInt("GraphicsVsync", 0);
            QualitySettings.vSyncCount = 0;
            Debug.Log("Graphics: vSync was deactivated");
        }
        else if (QualitySettings.vSyncCount == 0)
        {
            PlayerPrefs.SetInt("GraphicsVsync", 1);
            QualitySettings.vSyncCount = 1;
            Debug.Log("Graphics: vSync was activated");
        }
    }

    public void ChangeAniso()
    {
        if(QualitySettings.anisotropicFiltering == AnisotropicFiltering.ForceEnable)
        {
            PlayerPrefs.SetInt("GraphicsAniso", 0);
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            Debug.Log("Graphics: Anisotropic Filtering was disabled");
        }
        else if (QualitySettings.anisotropicFiltering == AnisotropicFiltering.Disable)
        {
            PlayerPrefs.SetInt("GraphicsAniso", 1);
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
            Debug.Log("Graphics: Anisotropic Filtering was enabled");
        }

    }

    public void ChangeTripleBuffering()
    {
        if(QualitySettings.maxQueuedFrames == 3)
        {
            PlayerPrefs.SetInt("GraphicsTriple", 0);
            QualitySettings.maxQueuedFrames = 2;
            Debug.Log("Graphics: Tripple Buffering was disabled");
        }
        else if (QualitySettings.maxQueuedFrames == 2)
        {
            PlayerPrefs.SetInt("GraphicsTriple", 1);
            QualitySettings.maxQueuedFrames = 3;
            Debug.Log("Graphics: Tripple Buffering was enabled");
        }
    }

    //public void ChangeCustomization()
    //{
    //    if (ToggleCustomization.isOn == true)
    //    {
    //        PlayerPrefs.SetInt("HideCloth", 1);
    //        DisableCustomization();
    //        ClothstatusPanel.gameObject.SetActive(true);
    //    }
    //    else if (ToggleCustomization.isOn == false)
    //    {
    //        PlayerPrefs.SetInt("HideCloth", 0);
    //        EnableCustomization();
    //        ClothstatusPanel.gameObject.SetActive(false);
    //    }
    //}

    void EnableCustomization()
    {
        Hair.enabled = true;
        Eye.enabled = true;
        //Torso.enabled = true;
        //Leg.enabled = true;
    }

    void DisableCustomization()
    {
        Hair.enabled = false;
        Eye.enabled = false;
        //Torso.enabled = false;
        //Leg.enabled = false;
    }

    public void ChangeFullscreen()
    {
        if (ToggleFullscreen.isOn == true)
        {
            PlayerPrefs.SetInt("GraphicsFullscreen", 1);
            Screen.SetResolution(ResolutionWidth, ResolutionHeight, true);
            Debug.Log("Graphics: Fullscreen was set to true");
        }
        else if (ToggleFullscreen.isOn == false)
        {
            PlayerPrefs.SetInt("GraphicsFullscreen", 0);
            Screen.SetResolution(ResolutionWidth, ResolutionHeight, false);
            Debug.Log("Graphics: Fullscreen was set to false");
        }
    }

    public void ToggleAudio()
    {
        if (PlayerPrefs.GetInt("AudioMute") == 0)
        {
            AudioListener.volume = 0f;
            PlayerPrefs.SetInt("AudioMute", 1);
            ButtonAudioMute.GetComponentInChildren<Text>().text = "Unmute";
            SoundstatusPanel.gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("AudioMute") == 1)
        {
            AudioListener.volume = SliderAudiovolume.value + 0.1f;
            PlayerPrefs.SetInt("AudioMute", 0);
            ButtonAudioMute.GetComponentInChildren<Text>().text = "Mute";
            SoundstatusPanel.gameObject.SetActive(false);
        }
    }


    // Dropdown
    public void ChangeResolution()
    {
        if(DropdownResolution.value == 0)
        {
            ResolutionWidth = 1920;
            ResolutionHeight = 1080;
			PlayerPrefs.SetInt("GraphicsResolution", 0);
            Debug.Log("Graphics: Resolution set to 1080p");
        }
        else if (DropdownResolution.value == 1)
        {
            ResolutionWidth = 1280;
            ResolutionHeight = 720;
			PlayerPrefs.SetInt("GraphicsResolution", 1);
            Debug.Log("Graphics: Resolution set to 720p");
        }
        else if (DropdownResolution.value == 2)
        {
            ResolutionWidth = 640;
            ResolutionHeight = 480;
			PlayerPrefs.SetInt("GraphicsResolution", 2);
            Debug.Log("Graphics: Resolution set to 480p");
        }
        else
        {
            ResolutionWidth = 1280;
            ResolutionHeight = 720;
			PlayerPrefs.SetInt("GraphicsResolution", 1);
            Debug.Log("Graphics: Resolution set to standard (720p)");
        }
        Screen.SetResolution(ResolutionWidth, ResolutionHeight, ToggleFullscreen.isOn);
    }
	
	public void ChangeQuality()
	{
        PlayerPrefs.SetInt("GraphicsQuality", DropdownQuality.value);
        QualitySettings.SetQualityLevel(DropdownQuality.value, false);
        Debug.Log("Graphics: Renderquality was set to " + QualitySettings.GetQualityLevel());
	}
	
	public void ChangeRefreshrate()
    {
        if(DropdownRefreshrate.value == 0)
        {
            PlayerPrefs.SetInt("GraphicsRefreshrate", 0);
            Screen.SetResolution(ResolutionWidth, ResolutionHeight, ToggleFullscreen.isOn, 60);
            Debug.Log("Graphics: Refreshrate is now 60Hz");
        }
        else if (DropdownRefreshrate.value == 1)
        {
            PlayerPrefs.SetInt("GraphicsRefreshrate", 1);
            Screen.SetResolution(ResolutionWidth, ResolutionHeight, ToggleFullscreen.isOn, 120);
            Debug.Log("Graphics: Refreshrate is now 120Hz");
        }
        else
        {
            PlayerPrefs.SetInt("GraphicsRefreshrate", 0);
            Screen.SetResolution(ResolutionWidth, ResolutionHeight, ToggleFullscreen.isOn, 60);
            Debug.Log("Graphics: Refreshrate is now standard (60Hz)");
        }
    }

    public void ChangeAntiAliasing()
    {
        if(DropdownAntialiasing.value == 0)
        {
            QualitySettings.antiAliasing = 0;
            Debug.Log("Graphics: Antialiasing is now disabled");
        }
        else if(DropdownAntialiasing.value == 1)
        {
            QualitySettings.antiAliasing = 2;
            Debug.Log("Graphics: Antialiasing is 2x");
        }
        else if (DropdownAntialiasing.value == 2)
        {
            QualitySettings.antiAliasing = 4;
            Debug.Log("Graphics: Antialiasing is 4x");
        }
        else if (DropdownAntialiasing.value == 3)
        {
            QualitySettings.antiAliasing = 8;
            Debug.Log("Graphics: Antialiasing is 8x");
        }
        else
        {
            QualitySettings.antiAliasing = 0;
            Debug.Log("Graphics: Antialiasing is now standard (disabled)");
        }
        PlayerPrefs.SetInt("GraphicsAntialiasing", DropdownAntialiasing.value);
    }

	// Slider
    public void ChangeAudiovolume()
    {
        AudioListener.volume = SliderAudiovolume.value;
        PlayerPrefs.SetFloat("SliderAudiovolume", SliderAudiovolume.value);
        AudiovolumeText.text = "Volume: " + (Mathf.Round(SliderAudiovolume.value * 100f) / 100f).ToString();
		if(SliderAudiovolume.value == 0f && PlayerPrefs.GetInt("AudioMute") == 0)
		{
			ToggleAudio();
			PlayerPrefs.SetInt("AudioMuteThroughVolume", 1);
		}
		if(SliderAudiovolume.value > 0f && PlayerPrefs.GetInt("AudioMuteThroughVolume") == 1)
		{
			ToggleAudio();
			PlayerPrefs.SetInt("AudioMuteThroughVolume", 0);
		}
    }

	
	//
	// LOADING DATA
    void LoadSettings()
    {

        // Customization Status Toggle
   //     if (PlayerPrefs.HasKey("HideCloth"))
   //     {
   //         if (PlayerPrefs.GetInt("HideCloth") == 1)
   //         {
			//	ToggleCustomization.isOn = true;
   //             DisableCustomization();
   //             ClothstatusPanel.gameObject.SetActive(true);
   //         }
   //         else
   //         {
			//	ToggleCustomization.isOn = false;
   //             EnableCustomization();
   //             ClothstatusPanel.gameObject.SetActive(false);
   //         }
   //     }
   //     else
   //     {
			//ToggleCustomization.isOn = true;
   //         DisableCustomization();
   //         ClothstatusPanel.gameObject.SetActive(true);
   //         PlayerPrefs.SetInt("CustToggle", 1);
   //     }

		// Vsync Toggle
        if(PlayerPrefs.HasKey("GraphicsVsync"))
        {
            if(PlayerPrefs.GetInt("GraphicsVsync") == 1)
            {
                ToggleVsync.isOn = true;
                //QualitySettings.vSyncCount = 1;
            }
            else
            {
                ToggleVsync.isOn = false;
                //QualitySettings.vSyncCount = 0;
            }
        }
        else
        {
            ToggleVsync.isOn = false;
            //QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("GraphicsVsync", 0);
        }

        // Aniso Toggle
        if (PlayerPrefs.HasKey("GraphicsAniso"))
        {
            if (PlayerPrefs.GetInt("GraphicsAniso") == 1)
            {
                ToggleAniso.isOn = true;
                //QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
            }
            else
            {
                ToggleAniso.isOn = false;
                //QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            }
        }
        else
        {
            ToggleAniso.isOn = false;
            //QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            PlayerPrefs.SetInt("GraphicsAniso", 0);
        }


        // Triple Toggle
        if (PlayerPrefs.HasKey("GraphicsTriple"))
        {
            if (PlayerPrefs.GetInt("GraphicsTriple") == 1)
            {
                ToggleTriple.isOn = true;
                //QualitySettings.maxQueuedFrames = 3;
            }
            else
            {
                ToggleTriple.isOn = false;
                //QualitySettings.maxQueuedFrames = 2;
            }
        }
        else
        {
            ToggleTriple.isOn = false;
            //QualitySettings.maxQueuedFrames = 2;
            PlayerPrefs.SetInt("GraphicsTriple", 0);
        }

        // Fullscreen Toggle
        if (PlayerPrefs.HasKey("GraphicsFullscreen"))
        {
            if (PlayerPrefs.GetInt("GraphicsFullscreen") == 1)
            {
                ToggleFullscreen.isOn = true;
                //Screen.SetResolution(ResolutionWidth, ResolutionHeight, true);
            }
            else
            {
                ToggleFullscreen.isOn = false;
                //Screen.SetResolution(ResolutionWidth, ResolutionHeight, false);
            }
        }
        else
        {
            ToggleFullscreen.isOn = false;
            //Screen.SetResolution(ResolutionWidth, ResolutionHeight, false);
            PlayerPrefs.SetInt("GraphicsFullscreen", 0);
        }


        //Res, Quality, Refresharete, AA
        //4x Dropdown


        // Resolution Dropdown
        if (PlayerPrefs.HasKey("GraphicsResolution"))
        {
            DropdownResolution.value = PlayerPrefs.GetInt("GraphicsResolution");
        }

        // Quality Dropdown
        if (PlayerPrefs.HasKey("GraphicsQuality"))
        {
            DropdownQuality.value = PlayerPrefs.GetInt("GraphicsQuality");
        }

        // Refreshrate Dropdown
        if (PlayerPrefs.HasKey("GraphicsRefreshrate"))
        {
            DropdownRefreshrate.value = PlayerPrefs.GetInt("GraphicsRefreshrate");
        }

        // Antialiasing Dropdown
        if (PlayerPrefs.HasKey("GraphicsAntialiasing"))
        {
            DropdownAntialiasing.value = PlayerPrefs.GetInt("GraphicsAntialiasing");
        }


        // Audio Volume Slider
        if (PlayerPrefs.HasKey("Audiovolume"))
        {
            SliderAudiovolume.value = PlayerPrefs.GetFloat("Audiovolume");
            AudioListener.volume = SliderAudiovolume.value;
            AudiovolumeText.text = "Volume: " + (Mathf.Round(SliderAudiovolume.value * 100f) / 100f).ToString();
			
			if(SliderAudiovolume.value == 0f && PlayerPrefs.GetInt("AudioMute") == 0)
			{
			ToggleAudio();
			PlayerPrefs.SetInt("AudioMuteThroughVolume", 1);
			}
			if(SliderAudiovolume.value > 0f && PlayerPrefs.GetInt("AudioMuteThroughVolume") == 1)
			{
			ToggleAudio();
			PlayerPrefs.SetInt("AudioMuteThroughVolume", 0);
			}
        }
        else
        {
            SliderAudiovolume.value = 1f;
            AudioListener.volume = SliderAudiovolume.value;
            AudiovolumeText.text = "Volume: " + (Mathf.Round(SliderAudiovolume.value * 100f) / 100f).ToString();
            PlayerPrefs.SetFloat("Audiovolume", SliderAudiovolume.value);
			PlayerPrefs.SetInt("AudioMuteThroughVolume", 0);
        }

        // Audio Mute Button
        if (PlayerPrefs.HasKey("AudioMute"))
        {
            if (PlayerPrefs.GetInt("AudioMute") == 1)
            {
                SliderAudiovolume.value = PlayerPrefs.GetFloat("Audiovolume");
                AudioListener.volume = 0f;
                ButtonAudioMute.GetComponentInChildren<Text>().text = "Unmute";
                SoundstatusPanel.gameObject.SetActive(true);
            }
            else
            {
                SliderAudiovolume.value = PlayerPrefs.GetFloat("Audiovolume");
                AudioListener.volume = SliderAudiovolume.value;
                ButtonAudioMute.GetComponentInChildren<Text>().text = "Mute";
                SoundstatusPanel.gameObject.SetActive(false);
            }
        }
        else
        {
            AudioListener.volume = 1f;
            PlayerPrefs.SetInt("AudioMute", 0);
        }
    }

}