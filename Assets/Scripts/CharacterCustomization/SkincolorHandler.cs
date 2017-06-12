using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkincolorHandler : MonoBehaviour {

    public GameObject Player;
    public SpriteRenderer[] PlayerSpriteRenderer;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
    }
    
    public void LoadSkinColors(Color newColor)
    {
    PlayerSpriteRenderer = Player.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer Renderer in PlayerSpriteRenderer)
            {
                Renderer.color = newColor;
            }
    }
}