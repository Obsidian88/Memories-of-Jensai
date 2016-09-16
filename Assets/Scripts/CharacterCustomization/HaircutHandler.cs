using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HaircutHandler : MonoBehaviour {

    public int HaircutIndex;
    public int HaircutIndexMax;

    private string absolutePathForSprites = "Sprites/CharacterSprites/Legacy/Customization/Haircuts";
    private Sprite[] HairSprites;

    public SpriteRenderer Renderer;

    // Use this for initialization
    void Start () {
        LoadHairCuts();
    }

    // Update is called once per frame
    void Update () {
        // Actual scene..
        //if (PlayerPrefs.HasKey("Haircut"))
        //{
        //    HaircutIndex = PlayerPrefs.GetInt("Haircut");
        //}

        // Customization Screen..
        Renderer.sprite = HairSprites[HaircutIndex];
    }

    public void LoadHairCuts()
    {
        HairSprites = Resources.LoadAll<Sprite>(absolutePathForSprites);
        HaircutIndexMax = HairSprites.Length;

        if (PlayerPrefs.HasKey("Haircut"))
        {
            HaircutIndex = PlayerPrefs.GetInt("Haircut");
        }
        else
        {
            Debug.Log("Haircutindex could not be found and a random one was taken.");
            HaircutIndex = Random.Range(1, HaircutIndexMax);
			PlayerPrefs.SetInt("Haircut", HaircutIndex);
        }
    }
}