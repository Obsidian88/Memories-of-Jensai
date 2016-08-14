using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EyecolorHandler : MonoBehaviour {

    public int EyecolorIndex;
    public int EyecolorIndexMax;
    
    private string absolutePathForSprites = "Sprites/CharacterSprites/Customization/Eyecolors";
    private Sprite[] EyeSprites;

    public SpriteRenderer Renderer;

    // Use this for initialization
    void Start () {
        LoadEyeColors();
    }
    
    // Update is called once per frame
    void Update () {
        //if (PlayerPrefs.HasKey("Eyecolor"))
        //{
        //    EyecolorIndex = PlayerPrefs.GetInt("Eyecolor");
        //}
        Renderer.sprite = EyeSprites[EyecolorIndex];
    }

    public void LoadEyeColors()
    {
        EyeSprites = Resources.LoadAll<Sprite>(absolutePathForSprites);
        EyecolorIndexMax = EyeSprites.Length;

        if (PlayerPrefs.HasKey("Eyecolor"))
        {
            EyecolorIndex = PlayerPrefs.GetInt("Eyecolor");
        }
        else
        {
            Debug.Log("Eyecolorindex could not be found and a random one was taken.");
            EyecolorIndex = Random.Range(1, EyecolorIndexMax);
			PlayerPrefs.SetInt("Eyecolor", EyecolorIndex);
        }
    }
}