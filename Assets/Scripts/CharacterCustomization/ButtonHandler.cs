// Customization Screen

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {

    public Text HaircutIndexText;    // Text that displays additional information of currently selected hair (number / name / gender etc.)
    public HaircutHandler Hair;      // Actual Haircut-gameObject
    
    public Text EyecolorIndexText;   // Text that displays additional information of currently selected eyecolor (number / color)
    public EyecolorHandler Eye;      // Actual Eyecolor-gameObject

    public Text ClothTorsoIndexText;// Text that displays additional information of currently selected torso-cloth (number)
    public ClothHandler Torso;      // Actual Torso-gameObject
    public Text ClothLegIndexText;  // Text that displays additional information of currently selected leg-cloth (number)
    public ClothHandler Leg;        // Actual Leg-gameObject
    
    public SkincolorHandler Skin;   // Actual Skincolor-gameObject
    
    public GameObject Player;       // Actual Player-gameObject
    private SpriteRenderer Playersprite;

    public Slider HueSlider;
    public Text HueText;
    public Slider SatSlider;
    public Text SatText;
    public Slider ValSlider;
    public Text ValText;

    // Use this for initialization
    void Start () {
        Playersprite = Player.GetComponent<SpriteRenderer>();
        ColorConverter Converter = GetComponent<ColorConverter>(); // Used for conversions of RGB to HSV (Unity uses RGB - HSV is better for manipulating)

        Hair.LoadHairCuts();
        Eye.LoadEyeColors();
        Skin.LoadSkinColors();
        Torso.LoadTorsoCloth();
        Leg.LoadLegCloth();

        if (PlayerPrefs.HasKey("Haircut"))
        {
            Hair.HaircutIndex = PlayerPrefs.GetInt("Haircut");
            HaircutIndexText.text = FillUpDigit(Hair.HaircutIndex + 1) + "/" + FillUpDigit(Hair.HaircutIndexMax); // Needed for initial text (when no button was clicked yet)
        }
        else
        {
            Debug.Log("Haircutindex could not be found and a random one was taken.");
            Hair.HaircutIndex = Random.Range(1, Hair.HaircutIndexMax);
            HaircutIndexText.text = FillUpDigit(Hair.HaircutIndex + 1) + "/" + FillUpDigit(Hair.HaircutIndexMax); // Needed for initial text (when no button was clicked yet)
        }

        if (PlayerPrefs.HasKey("Eyecolor"))
        {
            Eye.EyecolorIndex = PlayerPrefs.GetInt("Haircut");
            EyecolorIndexText.text = FillUpDigit(Eye.EyecolorIndex + 1) + "/" + FillUpDigit(Eye.EyecolorIndexMax);  // Needed for initial text (when no button was clicked yet)
        }
        else
        {
            Debug.Log("Eyecolorindex could not be found and a random one was taken.");
            Eye.EyecolorIndex = Random.Range(1, Eye.EyecolorIndexMax);
            EyecolorIndexText.text = FillUpDigit(Eye.EyecolorIndex + 1) + "/" + FillUpDigit(Eye.EyecolorIndexMax);  // Needed for initial text (when no button was clicked yet)
        }

        if (PlayerPrefs.HasKey("TorsoCloth"))
        {
            Torso.ClothTorsoIndex = PlayerPrefs.GetInt("TorsoCloth");
            ClothTorsoIndexText.text = FillUpDigit(Torso.ClothTorsoIndex + 1) + "/" + FillUpDigit(Torso.ClothTorsoIndexMax);  // Needed for initial text (when no button was clicked yet)
        }
        else
        {
            Debug.Log("TorsoIndex could not be found and a random one was taken.");
            Torso.ClothTorsoIndex = Random.Range(1, Torso.ClothTorsoIndexMax);
            ClothTorsoIndexText.text = FillUpDigit(Torso.ClothTorsoIndex + 1) + "/" + FillUpDigit(Torso.ClothTorsoIndexMax);  // Needed for initial text (when no button was clicked yet)
        }

        if (PlayerPrefs.HasKey("LegCloth"))
        {
            Leg.ClothLegsIndex = PlayerPrefs.GetInt("LegCloth");
            ClothLegIndexText.text = FillUpDigit(Leg.ClothLegsIndex + 1) + "/" + FillUpDigit(Leg.ClothLegsIndexMax);  // Needed for initial text (when no button was clicked yet)
        }
        else
        {
            Debug.Log("LegIndex could not be found and a random one was taken.");
            Leg.ClothLegsIndex = Random.Range(1, Leg.ClothLegsIndexMax);
            ClothLegIndexText.text = FillUpDigit(Leg.ClothLegsIndex + 1) + "/" + FillUpDigit(Leg.ClothLegsIndexMax);  // Needed for initial text (when no button was clicked yet)
        }
        
        if (PlayerPrefs.HasKey("SkincolorR") && PlayerPrefs.HasKey("SkincolorG") && PlayerPrefs.HasKey("SkincolorB"))
        {
            Color color = new Color(PlayerPrefs.GetFloat("SkincolorR"), PlayerPrefs.GetFloat("SkincolorG"), PlayerPrefs.GetFloat("SkincolorB"));
            Playersprite.color = color;
            float h, s, v;

            Converter.ColorToHSV(color, out h, out s, out v);
            HueSlider.value = h;
            SatSlider.value = s;
            ValSlider.value = v;
            HueText.text = "Hue: " + (Mathf.Round(h * 100f) / 100f).ToString();
            if ((Mathf.Round(SatSlider.value * 100f) / 100f) == 0)
            {
                SatText.text = "Saturation: " + "0.00";
            }
            else
            {
                SatText.text = "Saturation: " + (Mathf.Round(SatSlider.value * 100f) / 100f).ToString();
            }

            if ((Mathf.Round(ValSlider.value * 100f) / 100f) == 1)
            {
                ValText.text = "Lightness: " + "1.00";
            }
            else
            {
                ValText.text = "Lightness: " + (Mathf.Round(ValSlider.value * 100f) / 100f).ToString();
            }
        }
        else
        {
            Debug.Log("Skincolor could not be found and a standard one was taken.");
            Playersprite.color = new Color(1f, 1f, 1f);
            HueText.text = "Hue: " + "0";
            SatText.text = "Saturation: " + "0.00";
            ValText.text = "Lightness: " + "1.00";
        }
    }
    
    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        { 
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            applyCustomization();
        }
    }

    // Button: Apply
    public void applyCustomization()
    {
        StaticData.currentLevel = "Scene1";
        SceneManager.LoadScene("LoadingScreen");
    }
    
    // Button: Haircut left
    public void previousHaircut()
    {
        if(Hair.HaircutIndex == 0)
        {
            Hair.HaircutIndex = Hair.HaircutIndexMax - 1;
        }
        else
        {
            Hair.HaircutIndex = Hair.HaircutIndex - 1;
        }
        PlayerPrefs.SetInt("Haircut", Hair.HaircutIndex);
        HaircutIndexText.text = FillUpDigit(Hair.HaircutIndex + 1) + "/" + FillUpDigit(Hair.HaircutIndexMax);
    }
    
    // Button: Haircut right
    public void nextHaircut()
    {
        if(Hair.HaircutIndex == Hair.HaircutIndexMax - 1)
        {
            Hair.HaircutIndex = 0;
        }
        else
        {
            Hair.HaircutIndex = Hair.HaircutIndex + 1;
        }
        PlayerPrefs.SetInt("Haircut", Hair.HaircutIndex);
        HaircutIndexText.text = FillUpDigit(Hair.HaircutIndex + 1) + "/" + FillUpDigit(Hair.HaircutIndexMax);
    }
    
    // Button: Eyecolor left
    public void previousEyecolor()
    {
        if(Eye.EyecolorIndex == 0)
        {
            Eye.EyecolorIndex = Eye.EyecolorIndexMax - 1;
        }
        else
        {
            Eye.EyecolorIndex = Eye.EyecolorIndex - 1;
        }
        PlayerPrefs.SetInt("Eyecolor", Eye.EyecolorIndex);
        EyecolorIndexText.text = FillUpDigit(Eye.EyecolorIndex + 1) + "/" + FillUpDigit(Eye.EyecolorIndexMax);
    }
    
    // Button: Eyecolor right
    public void nextEyecolor()
    {
        if(Eye.EyecolorIndex == Eye.EyecolorIndexMax - 1)
        {
            Eye.EyecolorIndex = 0;
        }
        else
        {
            Eye.EyecolorIndex = Eye.EyecolorIndex + 1;
        }
        PlayerPrefs.SetInt("Eyecolor", Eye.EyecolorIndex);
        EyecolorIndexText.text = FillUpDigit(Eye.EyecolorIndex + 1) + "/" + FillUpDigit(Eye.EyecolorIndexMax);
    }

    // Button: Torso left
    public void previousTorso()
    {
        if (Torso.ClothTorsoIndex == 0)
        {
            Torso.ClothTorsoIndex = Torso.ClothTorsoIndexMax - 1;
        }
        else
        {
            Torso.ClothTorsoIndex = Torso.ClothTorsoIndex - 1;
        }
        PlayerPrefs.SetInt("TorsoCloth", Torso.ClothTorsoIndex);
        ClothTorsoIndexText.text = FillUpDigit(Torso.ClothTorsoIndex + 1) + "/" + FillUpDigit(Torso.ClothTorsoIndexMax);
    }

    // Button: Torso right
    public void nextTorso()
    {
        if (Torso.ClothTorsoIndex == Torso.ClothTorsoIndexMax - 1)
        {
            Torso.ClothTorsoIndex = 0;
        }
        else
        {
            Torso.ClothTorsoIndex = Torso.ClothTorsoIndex + 1;
        }
        PlayerPrefs.SetInt("TorsoCloth", Torso.ClothTorsoIndex);
        ClothTorsoIndexText.text = FillUpDigit(Torso.ClothTorsoIndex + 1) + "/" + FillUpDigit(Torso.ClothTorsoIndexMax);
    }

    // Button: Legs left
    public void previousLegs()
    {
        if (Leg.ClothLegsIndex == 0)
        {
            Leg.ClothLegsIndex = Leg.ClothLegsIndexMax - 1;
        }
        else
        {
            Leg.ClothLegsIndex = Leg.ClothLegsIndex - 1;
        }
        PlayerPrefs.SetInt("LegCloth", Leg.ClothLegsIndex);
        ClothLegIndexText.text = FillUpDigit(Leg.ClothLegsIndex + 1) + "/" + FillUpDigit(Leg.ClothLegsIndexMax);
    }

    // Button: Legs right
    public void nextLegs()
    {
        if (Leg.ClothLegsIndex == Leg.ClothLegsIndexMax - 1)
        {
            Leg.ClothLegsIndex = 0;
        }
        else
        {
            Leg.ClothLegsIndex = Leg.ClothLegsIndex + 1;
        }
        PlayerPrefs.SetInt("LegCloth", Leg.ClothLegsIndex);
        ClothLegIndexText.text = FillUpDigit(Leg.ClothLegsIndex + 1) + "/" + FillUpDigit(Leg.ClothLegsIndexMax);
    }
    
    public void changeSkinColor() // Is called when a slider changes
    {
        ColorConverter Converter = GetComponent<ColorConverter>();
        Playersprite.color = Converter.ColorFromHSV(HueSlider.value, SatSlider.value, ValSlider.value, 1f);
        PlayerPrefs.SetFloat("SkincolorR", Playersprite.color.r);
        PlayerPrefs.SetFloat("SkincolorG", Playersprite.color.g);
        PlayerPrefs.SetFloat("SkincolorB", Playersprite.color.b);

        // Refresh textvalues..
        HueText.text = "Hue: " + (Mathf.Round(HueSlider.value * 1f) / 1f).ToString();

        if((Mathf.Round(SatSlider.value * 100f) / 100f) == 0)
            {
                SatText.text = "Saturation: " + "0.00";
            }
        else
            { 
            SatText.text = "Saturation: " + (Mathf.Round(SatSlider.value * 100f) / 100f).ToString();
            }

        if((Mathf.Round(ValSlider.value * 100f) / 100f) == 1)
            {
                ValText.text = "Lightness: " + "1.00";
            }
        else
            { 
            ValText.text = "Lightness: " + (Mathf.Round(ValSlider.value * 100f) / 100f).ToString();
            }
    }
    
    public void randomize() // Need a randomize-button for people that don't care about selection
    {
        PlayerPrefs.SetInt("Haircut", Random.Range(0, (Hair.HaircutIndexMax)));
        HaircutIndexText.text = FillUpDigit(Hair.HaircutIndex + 1) + "/" + FillUpDigit(Hair.HaircutIndexMax);

        PlayerPrefs.SetInt("Eyecolor", Random.Range(0, (Eye.EyecolorIndexMax)));
        EyecolorIndexText.text = FillUpDigit(Eye.EyecolorIndex + 1) + "/" + FillUpDigit(Eye.EyecolorIndexMax);

        PlayerPrefs.SetInt("TorsoCloth", Random.Range(0, (Torso.ClothTorsoIndexMax)));
        ClothTorsoIndexText.text = FillUpDigit(Torso.ClothTorsoIndex + 1) + "/" + FillUpDigit(Torso.ClothTorsoIndexMax);

        PlayerPrefs.SetInt("LegCloth", Random.Range(0, (Leg.ClothLegsIndexMax)));
        ClothLegIndexText.text = FillUpDigit(Leg.ClothLegsIndex + 1) + "/" + FillUpDigit(Leg.ClothLegsIndexMax);

        HueSlider.value = Random.Range(-90f, 90f);
        SatSlider.value = Random.Range(0f, 0.25f);
        ValSlider.value = Random.Range(0.5f, 1.0f);
    }

    public string FillUpDigit(int input) // Need to put this in a global library .. a helpers file with only public functions which might be used multiple times
    {
        if (input > 9)
        {
            return input.ToString();
        }
        else
        {
            return "0" + input.ToString();
        }
    }

}