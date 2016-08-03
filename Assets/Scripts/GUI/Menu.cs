using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private bool showOptions = false;
    private bool showMenu = false;
    public float shadowDrawDistance;
    public int ResX;
    public int ResY;
    public bool Fullscreen;

    public Text SoundstatusText;
    public Text CustomizationStatusText;

    public SpriteRenderer Skin;
    public SpriteRenderer Hair;
    public SpriteRenderer Eye;
    public SpriteRenderer Torso;
    public SpriteRenderer Leg;

    private bool CustToggle = true;

    public float hSliderValue = 1.0f;

    // Use this for initialization
    void Start()
    {
        showMenu = false;
        showOptions = false;
        AudioListener.pause = false;
        if (PlayerPrefs.HasKey("Audiovolume"))
        { 
            hSliderValue = PlayerPrefs.GetFloat("Audiovolume");
            AudioListener.volume = hSliderValue;
        }
        else
        {
            AudioListener.volume = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F10)) && showOptions == false)
        {
            showMenu = !showMenu;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && showOptions == true)
        {
            showOptions = false;
            showMenu = true;
        }
    }

    void OnGUI()
    {
        if(showMenu == true)
        { 
            if (GUI.Button(new Rect(500, 100, 200, 80), "Customization"))
            {
                SceneManager.LoadScene("Character Customization");
            }
            if (GUI.Button(new Rect(500, 210, 200, 80), "Options"))
            {
                showOptions = true;
                showMenu = false;
            }
            if (GUI.Button(new Rect(500, 320, 200, 80), "Quit"))
            {
                Application.Quit();
            }
        }

        if (showOptions == true)
        {

            if (GUI.Button(new Rect(500, 150, 140, 50), "Vsync On"))
            {
                QualitySettings.vSyncCount = 1;
            }
            if (GUI.Button(new Rect(645, 150, 140, 50), "Vsync Off"))
            {
                QualitySettings.vSyncCount = 0;
            }


            //INCREASE QUALITY PRESET
            if (GUI.Button(new Rect(500, 210, 140, 50), "Increase Quality"))
            {
                QualitySettings.IncreaseLevel();
                Debug.Log("Increased quality");
            }
            //DECREASE QUALITY PRESET
            if (GUI.Button(new Rect(645, 210, 140, 50), "Decrease Quality"))
            {
                QualitySettings.DecreaseLevel();
                Debug.Log("Decreased quality");
            }


            //1080p
            if (GUI.Button(new Rect(500, 270, 92, 50), "1080p"))
            {
                Screen.SetResolution(1920, 1080, Fullscreen);
                ResX = 1920;
                ResY = 1080;
                Debug.Log("1080p");
            }
            //720p
            if (GUI.Button(new Rect(597, 270, 92, 50), "720p"))
            {
                Screen.SetResolution(1280, 720, Fullscreen);
                ResX = 1280;
                ResY = 720;
                Debug.Log("720p");
            }
            //480p
            if (GUI.Button(new Rect(694, 270, 92, 50), "480p"))
            {
                Screen.SetResolution(640, 480, Fullscreen);
                ResX = 640;
                ResY = 480;
                Debug.Log("480p");
            }



            //RESOLUTION SETTINGS
            //60Hz
            if (GUI.Button(new Rect(500, 327, 140, 50), "60Hz"))
            {
                Screen.SetResolution(ResX, ResY, Fullscreen, 60);
                Debug.Log("60Hz");
            }
            //120Hz
            if (GUI.Button(new Rect(645, 327, 140, 50), "120Hz"))
            {
                Screen.SetResolution(ResX, ResY, Fullscreen, 120);
                Debug.Log("120Hz");
            }


            //ANISOTROPIC FILTERING SETTINGS
            if (GUI.Button(new Rect(500, 385, 140, 50), "Aniso. Filtering On"))
            {
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Debug.Log("Force enable anisotropic filtering!");
            }
            if (GUI.Button(new Rect(645, 385, 140, 50), "Aniso. Filtering Off"))
            {
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                Debug.Log("Disable anisotropic filtering!");
            }



            //TRIPLE BUFFERING SETTINGS
            if (GUI.Button(new Rect(500, 445, 140, 50), "Triple Buffering On"))
            {
                QualitySettings.maxQueuedFrames = 3;
                Debug.Log("Triple buffering on");
            }
            if (GUI.Button(new Rect(645, 445, 140, 50), "Triple Buffering Off"))
            {
                QualitySettings.maxQueuedFrames = 0;
                Debug.Log("Triple buffering off");
            }


            //0 X AA SETTINGS
            if (GUI.Button(new Rect(500, 505, 67, 50), "No AA"))
            {
                QualitySettings.antiAliasing = 0;
                Debug.Log("0 AA");
            }
            //2 X AA SETTINGS
            if (GUI.Button(new Rect(572, 505, 67, 50), "2x AA"))
            {
                QualitySettings.antiAliasing = 2;
                Debug.Log("2 x AA");
            }
            //4 X AA SETTINGS
            if (GUI.Button(new Rect(644, 505, 67, 50), "4x AA"))
            {
                QualitySettings.antiAliasing = 4;
                Debug.Log("4 x AA");
            }
            //8 x AA SETTINGS
            if (GUI.Button(new Rect(716, 505, 67, 50), "8x AA"))
            {
                QualitySettings.antiAliasing = 8;
                Debug.Log("8 x AA");
            }



            if (GUI.Button(new Rect(500, 570, 140, 50), "Mute Audio"))
            {
                ToggleAudio();
            }
            if (GUI.Button(new Rect(645, 570, 140, 50), "Toggle Cust."))
            {
                ToggleCustomization();
            }

            hSliderValue = GUI.HorizontalSlider(new Rect(500, 650, 280, 30), hSliderValue, 0.0f, 1.0f);  // Rect(x, y, width, height)
            GUI.Label(new Rect(500, 675, 150, 20), "Audiovolume: " + (Mathf.Round(hSliderValue * 100f) / 100f).ToString());
            AudioListener.volume = hSliderValue;
            PlayerPrefs.SetFloat("Audiovolume", hSliderValue);


            //BACK
            if (GUI.Button(new Rect(596, 700, 100, 50), "Back"))
            {
                showOptions = false;
                showMenu = true;
            }

        }
    }

    void ToggleAudio()
    {
        if (AudioListener.pause == false)
        {
            AudioListener.pause = true;
            SoundstatusText.text = "Sound muted";
        }
        else if (AudioListener.pause == true)
        {
            AudioListener.pause = false;
            SoundstatusText.text = "";
        }
    }

    void ToggleCustomization()
    {
        Hair.enabled = !Hair.enabled;
        Eye.enabled = !Eye.enabled;
        Torso.enabled = !Torso.enabled;
        Leg.enabled = !Leg.enabled;

        if (CustToggle == false)
        {
            CustToggle = true;
            CustomizationStatusText.text = "";
        }
        else if (CustToggle == true)
        {

            CustToggle = false;
            CustomizationStatusText.text = "Custom. toggled";
        }
    }
}