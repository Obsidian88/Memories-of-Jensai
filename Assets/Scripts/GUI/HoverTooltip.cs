using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverTooltip : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler {

    public int PanelWidth;
    public int PanelHeight;
    public Color Panelcolor;

    [TextArea(3, 10)]
    public string Tooltiptext;
    public Color Textcolor;

    public float DelayAfterFadeInStarts;
    public float Fadespeed;

    //[Range(0f, 1f)]
    //public float Opacity; // 0 - 1

    private GameObject Tooltip;
    private GameObject TooltipText;
    private Coroutine co;

    void Update()
    {
        // If Tooltip is currently active and shown: stick it to the mousecursor
        if(Tooltip != null)
        {
            Tooltip.transform.position = new Vector2(Input.mousePosition.x + PanelWidth / 2, Input.mousePosition.y + PanelHeight / 2);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        co = StartCoroutine(Delay(DelayAfterFadeInStarts, true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(co);
        StartCoroutine(FadeTooltip(true, Tooltip));
        StartCoroutine(FadeTooltip(true, TooltipText));
        //DestroyTooltip(Tooltip);
    }

    public void CreateTooltip()
    {
        // BackgroundPanel
        GameObject panel = new GameObject("PanelTooltip");
        panel.AddComponent<CanvasRenderer>();
        Image c = panel.AddComponent<Image>();
        c.color = new Color(Panelcolor.r, Panelcolor.g, Panelcolor.b, 0f);
        panel.transform.SetParent(gameObject.transform.parent.gameObject.transform);
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(PanelWidth, PanelHeight);

        // TextPanel
        GameObject panel2 = new GameObject("TooltipText");
        panel2.AddComponent<CanvasRenderer>();
        panel2.transform.SetParent(panel.transform);
        panel2.AddComponent<RectTransform>();
        panel2.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        panel2.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        // Margin of 5 in all directions
        panel2.GetComponent<RectTransform>().offsetMin = new Vector2(5, 5);
        panel2.GetComponent<RectTransform>().offsetMax = new Vector2(-5, -5);

        Text mytext = panel2.AddComponent<Text>();
        mytext.text = Tooltiptext;
        mytext.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        mytext.color = new Color(Textcolor.r, Textcolor.g, Textcolor.b, 0f);

        StartCoroutine(FadeTooltip(false, panel));
        StartCoroutine(FadeTooltip(false, panel2));

        panel.transform.position = new Vector2(Input.mousePosition.x + PanelWidth/2, Input.mousePosition.y + PanelHeight/2);

        Tooltip = panel;
        TooltipText = panel2;
    }

    public void DestroyTooltip(GameObject Tooltip)
    {
        Destroy(Tooltip);
    }

    private IEnumerator Delay(float delay, bool create)
    {
        yield return new WaitForSeconds(delay);
        if(create)
        { 
            CreateTooltip();
        }
        else
        {
            Destroy(Tooltip);
        }
    }

    IEnumerator FadeTooltip(bool fadeAway, GameObject Tooltip)
    {
        //if(Tooltip != null)
        //{ 
        if(Tooltip.GetComponent<Image>() != null)
        {
            //This is an image
            Image component = Tooltip.GetComponent<Image>();
            if (fadeAway)
            {
                // fade OUT
                for (float i = Panelcolor.a; i >= 0; i -= Time.deltaTime / Fadespeed)
                {
                    component.color = new Color(component.color.r, component.color.g, component.color.b, i);
                    yield return null;
                }
                StartCoroutine(Delay(0.5f, false));
            }
            else
            {
                // fade IN
                for (float i = 0; i <= Panelcolor.a; i += Time.deltaTime / Fadespeed)
                {
                    component.color = new Color(component.color.r, component.color.g, component.color.b, i);
                    yield return null;
                }
            }
        }
        else if(Tooltip.GetComponent<Text>() != null)
        {
            // This is text
            Text component = Tooltip.GetComponent<Text>();
            if (fadeAway)
            {
                // fade OUT
                for (float i = Textcolor.a; i >= 0; i -= Time.deltaTime / Fadespeed)
                {
                    component.color = new Color(component.color.r, component.color.g, component.color.b, i);
                    yield return null;
                }
            }
            else
            {
                // fade IN
                for (float i = 0; i <= Textcolor.a; i += Time.deltaTime / Fadespeed)
                {
                    component.color = new Color(component.color.r, component.color.g, component.color.b, i);
                    yield return null;
                }
            }
        }
        //}
    }

}
