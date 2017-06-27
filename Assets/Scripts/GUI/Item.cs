using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Texture2D HighlightCursor;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    private SpriteRenderer ren;

    public Shader Highlightshader;
    private Shader Normalshader;

    public AudioClip ItemPickupSound;
    private AudioSource source;

    public GameObject Panel11;
    private Image[] BagSlots;
    private bool pickedUp = false;

    void Start()
    {
        ren = gameObject.GetComponent<SpriteRenderer>();
        Normalshader = ren.material.shader;
        source = gameObject.AddComponent<AudioSource>();
        BagSlots = Panel11.GetComponentsInChildren<Image>(true);
    }

    void OnMouseEnter()
    {
        ren.material.shader = Highlightshader;
        ren.material.SetFloat("OutlineSpread", 0.3f);
        ren.material.SetColor("OutlineColor", new Color (1f,0f,0f,1f));
        Cursor.SetCursor(HighlightCursor, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        ren.material.shader = Normalshader;
        ren.material.SetColor("OutlineColor", new Color(193/255, 193 / 255, 193 / 255, 1f));
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    public void OnMouseDown()
    {
        foreach (Image slot in BagSlots)
        {
            if (slot.gameObject.transform.parent.name == "Slot")
            {
                // Item is already there
                if (slot.sprite.name == gameObject.GetComponent<SpriteRenderer>().sprite.name && !pickedUp)
                {
                    pickedUp = true;
                    source.PlayOneShot(ItemPickupSound);

                    Text text = slot.gameObject.GetComponentInChildren<Text>();
                    
                    // Increment textcount with +1
                    int x = -1;
                    if (Int32.TryParse(text.text, out x))
                    {
                        text.text = (x+1).ToString();
                    }
                }
            }
        }
        foreach (Image slot in BagSlots)
        {
            if (slot.gameObject.transform.parent.name == "Slot")
            {
                // Item is not there, take first free slot (free = background)
                if (slot.sprite.name == "Background" && !pickedUp)
                {
                    slot.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    slot.color = new Color(1f, 1f, 1f, 1f);

                    pickedUp = true;
                    source.PlayOneShot(ItemPickupSound);

                    Text text = slot.gameObject.GetComponentInChildren<Text>(true);
                    text.text = "1";
                    //GameObject textObject = text.gameObject;
                    //textObject.SetActive(true);
                }
            }
        }

        ren.material.shader = Normalshader;
        ren.material.SetColor("OutlineColor", new Color(193 / 255, 193 / 255, 193 / 255, 1f));
        Cursor.SetCursor(null, Vector2.zero, cursorMode);

        // Delayed deletion of the item because of bagpacksound that needs time to play
        ren.enabled = false;
        StartCoroutine(Delay(2f));
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
