using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkincolorHandler : MonoBehaviour {

    public SpriteRenderer Renderer;

    // Use this for initialization
    void Start () {
        LoadSkinColors();
    }
    
    // Update is called once per frame
    void Update () {
    }

    public void LoadSkinColors()
    {
        if (PlayerPrefs.HasKey("SkincolorR") && PlayerPrefs.HasKey("SkincolorG") && PlayerPrefs.HasKey("SkincolorB"))
        {
            Renderer.color = new Color(PlayerPrefs.GetFloat("SkincolorR"),PlayerPrefs.GetFloat("SkincolorG"),PlayerPrefs.GetFloat("SkincolorB"));
        }
        else
        {
            Debug.Log("Skincolor could not be found and a standard one was taken.");
            Renderer.color = new Color(1f, 1f, 1f);
            PlayerPrefs.SetFloat("SkincolorR", 1f);
            PlayerPrefs.SetFloat("SkincolorG", 1f);
            PlayerPrefs.SetFloat("SkincolorB", 1f);
        }
    }
}