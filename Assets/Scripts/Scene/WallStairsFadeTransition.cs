using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStairsFadeTransition : MonoBehaviour {

    Material Wall1_ToFade;
    Material WindowOrnament_ToFade;
    Material Windowframes_ToFade;
    Material Stairs_ToFade;
    Material SolidBlack_ToFade;

    public GameObject OuterPlane;
    public GameObject HeatDistortion;

    // Use this for initialization
    void Start () {
        Wall1_ToFade = Resources.Load("Materials/Wall1_ToFade", typeof(Material)) as Material;
        WindowOrnament_ToFade = Resources.Load("Materials/WindowOrnament_ToFade", typeof(Material)) as Material;
        Windowframes_ToFade = Resources.Load("Materials/Windowframes_ToFade", typeof(Material)) as Material;
        Stairs_ToFade = Resources.Load("Materials/Stairs_ToFade", typeof(Material)) as Material;
        SolidBlack_ToFade = Resources.Load("Materials/SolidBlack_ToFade", typeof(Material)) as Material;

        HeatDistortion.SetActive(false);
        FadeOut();
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Character")
        {
            if(gameObject.name == "ColliderEnter")
            {
                FadeIn();
                Debug.Log("Enter is active!");
            }
            else if (gameObject.name == "ColliderExit")
            {
                FadeOut();
                Debug.Log("Exit is active!");
            }
        }
    }

    void FadeIn()
    {
        OuterPlane.SetActive(false);
        HeatDistortion.SetActive(true);
        //SetTransparent(SolidBlack_ToFade);

        SetOpaque(Wall1_ToFade);
        SetOpaque(Stairs_ToFade);

        //StartCoroutine(FadeMaterial(true, SolidBlack_ToFade));
        StartCoroutine(FadeMaterial(false, Wall1_ToFade));
        StartCoroutine(FadeMaterial(false, Stairs_ToFade));
        StartCoroutine(FadeMaterial(false, WindowOrnament_ToFade));
        StartCoroutine(FadeMaterial(false, Windowframes_ToFade));
    }

    void FadeOut()
    {
        OuterPlane.SetActive(true);
        HeatDistortion.SetActive(false);
        //SetOpaque(SolidBlack_ToFade);

        SetTransparent(Wall1_ToFade);
        SetTransparent(Stairs_ToFade);

        //StartCoroutine(FadeMaterial(false, SolidBlack_ToFade));
        StartCoroutine(FadeMaterial(true, Wall1_ToFade));
        StartCoroutine(FadeMaterial(true, Stairs_ToFade));
        StartCoroutine(FadeMaterial(true, WindowOrnament_ToFade));
        StartCoroutine(FadeMaterial(true, Windowframes_ToFade));
    }

    private IEnumerator FadeMaterial(bool FadeOut, Material m)
    {
        if(!FadeOut && m.color.a != 1f)
        {
            for (float i = 0f; i <= 1f; i += Time.deltaTime / 1f)
            {
                if (m.color != null)
                {
                    m.color = new Color(m.color.r, m.color.g, m.color.b, i);
                }
                yield return null;
            }
            m.color = new Color(m.color.r, m.color.g, m.color.b, 1f);
        }
        else if(FadeOut && m.color.a != 0f)
        {
            for (float i = 1f; i >= 0f; i -= Time.deltaTime / 1f)
            {
                if (m.color != null)
                {
                    m.color = new Color(m.color.r, m.color.g, m.color.b, i);
                }
                yield return null;
            }
            m.color = new Color(m.color.r, m.color.g, m.color.b, 0f);
        }
    }

    private IEnumerator FadeTint(bool FadeOut, Material m)
    {
        if (!FadeOut && m.color.a != 1f)
        {
            for (float i = 0f; i <= 1f; i += Time.deltaTime / 1f)
            {
                m.SetColor("_TintColor", new Color(m.color.r, m.color.g, m.color.b, i));
                yield return null;
            }
            m.color = new Color(m.color.r, m.color.g, m.color.b, 1f);
        }
        else if(FadeOut && m.color.a != 0f)
        {
            for (float i = 1f; i >= 0f; i -= Time.deltaTime / 1f)
            {
                m.SetColor("_TintColor", new Color(m.color.r, m.color.g, m.color.b, i));
                yield return null;
            }
            m.color = new Color(m.color.r, m.color.g, m.color.b, 0f);
        }
    }

    void SetTransparent(Material m)
    {
        m.SetFloat("_Mode", 2);
        m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        m.SetInt("_ZWrite", 0);
        m.DisableKeyword("_ALPHATEST_ON");
        m.EnableKeyword("_ALPHABLEND_ON");
        m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        m.renderQueue = 3000;
    }

    void SetOpaque(Material m)
    {
        m.SetFloat("_Mode", 0);
        m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        m.SetInt("_ZWrite", -1);
        m.DisableKeyword("_ALPHATEST_OFF");
        m.EnableKeyword("_ALPHABLEND_OFF");
        m.DisableKeyword("_ALPHAPREMULTIPLY_OFF");
        m.renderQueue = 2000;
    }
}
