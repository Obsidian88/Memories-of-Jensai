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

    public ClothHandler Cloth;      // Actual Torso-gameObject
    public Text ClothTorsoIndexText;// Text that displays additional information of currently selected torso-cloth (number)
    public Text ClothLegIndexText;  // Text that displays additional information of currently selected leg-cloth (number)
    
    public SkincolorHandler Skin;   // Actual Skincolor-gameObject
    
    public GameObject Player;       // Actual Player-gameObject
    private SpriteRenderer Playersprite;

    public Slider HueSlider;
    public Text HueText;
    public Slider SatSlider;
    public Text SatText;
    public Slider ValSlider;
    public Text ValText;

    private int TempHaircut;
    private int TempEyecolor;
    private int TempTorso;
    private int TempLegs;
    private float TempR;
    private float TempG;
    private float TempB;

    // Use this for initialization
    void Start () {
        Playersprite = Player.GetComponent<SpriteRenderer>();
        ColorConverter Converter = GetComponent<ColorConverter>(); // Used for conversions of RGB to HSV (Unity uses RGB - HSV is better for manipulating)

        Hair.LoadHairCuts();
        Eye.LoadEyeColors();
        Skin.LoadSkinColors();
        Cloth.LoadTorsoCloth();
        Cloth.LoadLegCloth();

        TempHaircut = PlayerPrefs.GetInt("Haircut");
        TempEyecolor = PlayerPrefs.GetInt("Eyecolor");
        TempTorso = PlayerPrefs.GetInt("TorsoCloth");
        TempLegs = PlayerPrefs.GetInt("LegCloth");
        TempR = PlayerPrefs.GetFloat("SkincolorR");
        TempG = PlayerPrefs.GetFloat("SkincolorG");
        TempB = PlayerPrefs.GetFloat("SkincolorB");

            HaircutIndexText.text = FillUpDigit(Hair.HaircutIndex + 1) + "/" + FillUpDigit(Hair.HaircutIndexMax); // Needed for initial text (when no button was clicked yet)
            EyecolorIndexText.text = FillUpDigit(Eye.EyecolorIndex + 1) + "/" + FillUpDigit(Eye.EyecolorIndexMax);  // Needed for initial text (when no button was clicked yet)
            ClothTorsoIndexText.text = FillUpDigit(Cloth.ClothTorsoIndex + 1) + "/" + FillUpDigit(Cloth.ClothTorsoIndexMax);  // Needed for initial text (when no button was clicked yet)
            ClothLegIndexText.text = FillUpDigit(Cloth.ClothLegsIndex + 1) + "/" + FillUpDigit(Cloth.ClothLegsIndexMax);  // Needed for initial text (when no button was clicked yet)
        
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
        if(Input.GetKeyDown(KeyCode.Return))
        {
            applyCustomization();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cancelCustomization();
        }
    }

    // Button: Apply
    public void applyCustomization()
    {
        PlayerPrefs.SetInt("Haircut", TempHaircut);
        PlayerPrefs.SetInt("Eyecolor", TempEyecolor);
        PlayerPrefs.SetInt("TorsoCloth", TempTorso);
        PlayerPrefs.SetInt("LegCloth", TempLegs);
        PlayerPrefs.SetFloat("SkincolorR", TempR);
        PlayerPrefs.SetFloat("SkincolorG", TempG);
        PlayerPrefs.SetFloat("HSkincolorB", TempB);

        StaticData.currentLevel = "CharacterAnimationTester";
        SceneManager.LoadScene("LoadingScreen");
    }

    // Button: Cancel
    public void cancelCustomization()
    {
        //Hair.HaircutIndex = PlayerPrefs.GetInt("Haircut");
        //Eye.EyecolorIndex = PlayerPrefs.GetInt("Eyecolor");
        //Cloth.ClothTorsoIndex = PlayerPrefs.GetInt("TorsoCloth");
        //Cloth.ClothLegsIndex = PlayerPrefs.GetInt("LegCloth");
        //Playersprite.color = new Color(PlayerPrefs.GetFloat("SkincolorR"), PlayerPrefs.GetFloat("SkincolorG"), PlayerPrefs.GetFloat("SkincolorB"));

        StaticData.currentLevel = "CharacterAnimationTester";
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
        TempHaircut = Hair.HaircutIndex;
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
        TempHaircut = Hair.HaircutIndex;
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
        TempEyecolor = Eye.EyecolorIndex;
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
        TempEyecolor = Eye.EyecolorIndex;
        EyecolorIndexText.text = FillUpDigit(Eye.EyecolorIndex + 1) + "/" + FillUpDigit(Eye.EyecolorIndexMax);
    }

    // Button: Torso left
    public void previousTorso()
    {
        if (Cloth.ClothTorsoIndex == 0)
        {
            Cloth.ClothTorsoIndex = Cloth.ClothTorsoIndexMax - 1;
        }
        else
        {
            Cloth.ClothTorsoIndex = Cloth.ClothTorsoIndex - 1;
        }
        TempTorso = Cloth.ClothTorsoIndex;
        ClothTorsoIndexText.text = FillUpDigit(Cloth.ClothTorsoIndex + 1) + "/" + FillUpDigit(Cloth.ClothTorsoIndexMax);
    }

    // Button: Torso right
    public void nextTorso()
    {
        if (Cloth.ClothTorsoIndex == Cloth.ClothTorsoIndexMax - 1)
        {
            Cloth.ClothTorsoIndex = 0;
        }
        else
        {
            Cloth.ClothTorsoIndex = Cloth.ClothTorsoIndex + 1;
        }
        TempTorso = Cloth.ClothTorsoIndex;
        ClothTorsoIndexText.text = FillUpDigit(Cloth.ClothTorsoIndex + 1) + "/" + FillUpDigit(Cloth.ClothTorsoIndexMax);
    }

    // Button: Legs left
    public void previousLegs()
    {
        if (Cloth.ClothLegsIndex == 0)
        {
            Cloth.ClothLegsIndex = Cloth.ClothLegsIndexMax - 1;
        }
        else
        {
            Cloth.ClothLegsIndex = Cloth.ClothLegsIndex - 1;
        }
        TempLegs = Cloth.ClothLegsIndex;
        ClothLegIndexText.text = FillUpDigit(Cloth.ClothLegsIndex + 1) + "/" + FillUpDigit(Cloth.ClothLegsIndexMax);
    }

    // Button: Legs right
    public void nextLegs()
    {
        if (Cloth.ClothLegsIndex == Cloth.ClothLegsIndexMax - 1)
        {
            Cloth.ClothLegsIndex = 0;
        }
        else
        {
            Cloth.ClothLegsIndex = Cloth.ClothLegsIndex + 1;
        }
        TempLegs = Cloth.ClothLegsIndex;
        ClothLegIndexText.text = FillUpDigit(Cloth.ClothLegsIndex + 1) + "/" + FillUpDigit(Cloth.ClothLegsIndexMax);
    }
    
    public void changeSkinColor() // Is called when a slider changes
    {
        ColorConverter Converter = GetComponent<ColorConverter>();
        Playersprite.color = Converter.ColorFromHSV(HueSlider.value, SatSlider.value, ValSlider.value, 1f);
        TempR = Playersprite.color.r;
        TempG = Playersprite.color.g;
        TempB = Playersprite.color.b;

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
        TempHaircut = Random.Range(0, (Hair.HaircutIndexMax));
        HaircutIndexText.text = FillUpDigit(Hair.HaircutIndex + 1) + "/" + FillUpDigit(Hair.HaircutIndexMax);

        TempEyecolor = Random.Range(0, (Eye.EyecolorIndexMax));
        EyecolorIndexText.text = FillUpDigit(Eye.EyecolorIndex + 1) + "/" + FillUpDigit(Eye.EyecolorIndexMax);

        TempTorso = Random.Range(0, (Cloth.ClothTorsoIndexMax));
        ClothTorsoIndexText.text = FillUpDigit(Cloth.ClothTorsoIndex + 1) + "/" + FillUpDigit(Cloth.ClothTorsoIndexMax);

        TempLegs = Random.Range(0, (Cloth.ClothLegsIndexMax));
        ClothLegIndexText.text = FillUpDigit(Cloth.ClothLegsIndex + 1) + "/" + FillUpDigit(Cloth.ClothLegsIndexMax);

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