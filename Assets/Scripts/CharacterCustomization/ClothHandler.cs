using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClothHandler : MonoBehaviour {

    public int ClothTorsoIndex;
    public int ClothTorsoIndexMax;
    public int ClothLegsIndex;
    public int ClothLegsIndexMax;
    
    private string absolutePathForTorso = "Sprites/CharacterSprites/Customization/Clothes/Torso";
    private string absolutePathForLegs = "Sprites/CharacterSprites/Customization/Clothes/Legs";
    private Sprite[] TorsoSprites;
    private Sprite[] LegSprites;

    public SpriteRenderer RendererTorso;
    public SpriteRenderer RendererLegs;

    // Use this for initialization
    void Start () {
        LoadTorsoCloth();
        LoadLegCloth();
    }
    
    // Update is called once per frame
    void Update () {
        if (PlayerPrefs.HasKey("TorsoCloth"))
        {
            ClothTorsoIndex = PlayerPrefs.GetInt("TorsoCloth");
        }
        RendererTorso.sprite = TorsoSprites[ClothTorsoIndex];

        if (PlayerPrefs.HasKey("LegCloth"))
        {
            ClothLegsIndex = PlayerPrefs.GetInt("LegCloth");
        }
        RendererLegs.sprite = LegSprites[ClothLegsIndex];
    }

    public void LoadTorsoCloth()
    {
        TorsoSprites = Resources.LoadAll<Sprite>(absolutePathForTorso);
        ClothTorsoIndexMax = TorsoSprites.Length;

        if (PlayerPrefs.HasKey("TorsoCloth"))
        {
            ClothTorsoIndex = PlayerPrefs.GetInt("TorsoCloth");
        }
        else
        {
            Debug.Log("TorsoIndex could not be found and a random one was taken.");
            ClothTorsoIndex = Random.Range(1, ClothTorsoIndexMax);
            PlayerPrefs.SetInt("TorsoCloth", ClothTorsoIndex);
        }
    }

    public void LoadLegCloth()
    {
        LegSprites = Resources.LoadAll<Sprite>(absolutePathForLegs);
        ClothLegsIndexMax = LegSprites.Length;

        if (PlayerPrefs.HasKey("LegCloth"))
        {
            ClothLegsIndex = PlayerPrefs.GetInt("LegCloth");
        }
        else
        {
            Debug.Log("LegIndex could not be found and a random one was taken.");
            ClothLegsIndex = Random.Range(1, ClothLegsIndexMax);
            PlayerPrefs.SetInt("LegCloth", ClothLegsIndex);
        }
    }

}