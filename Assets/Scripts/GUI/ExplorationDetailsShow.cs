using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExplorationDetailsShow : MonoBehaviour
{
    public Texture2D HighlightCursor;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    private SpriteRenderer ren;

    public Shader Highlightshader;
    private Shader Normalshader;
    //public int outlineSize = 5;
    //public Color outlineColor;
    public GameObject PanelToShow;

    public AudioClip OpenPanelSound;
    private AudioSource source;

    void Start()
    {
        ren = gameObject.GetComponent<SpriteRenderer>();
        Normalshader = ren.material.shader;
        source = gameObject.AddComponent<AudioSource>();
    }

    void OnMouseEnter()
    {
        //UpdateOutline(true);
        ren.material.shader = Highlightshader;
        Cursor.SetCursor(HighlightCursor, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        //UpdateOutline(false);
        ren.material.shader = Normalshader;
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    //void UpdateOutline(bool outline)
    //{
    //    MaterialPropertyBlock mpb = new MaterialPropertyBlock();
    //    ren.GetPropertyBlock(mpb);
    //    mpb.SetFloat("_Outline", outline ? 1f : 0);
    //    mpb.SetColor("_OutlineColor", outlineColor);
    //    mpb.SetFloat("_OutlineSize", outlineSize);
    //    ren.SetPropertyBlock(mpb);
    //}

    public void OnMouseDown()
    {
        PanelToShow.transform.parent.gameObject.GetComponent<ExplorationDetailsClose>().enabled = false;
        PanelToShow.SetActive(true);
        StartCoroutine(Delay(1f));
        source.PlayOneShot(OpenPanelSound);
        //Debug.Log("I was clicked!");
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PanelToShow.transform.parent.gameObject.GetComponent<ExplorationDetailsClose>().enabled = true;
    }
}
