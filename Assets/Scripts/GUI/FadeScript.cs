using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    [Tooltip("Opacity startpoint")]
    public float OpacityStart = 0f;
    [Tooltip("Opacity endpoint")]
    public float OpacityEnd = 1f;

    [Tooltip("Time in seconds it needs to animate one fade-animation")]
    public float Speed = 1f;

    [Tooltip("Duration between fade in and fade out in seconds.")]
    public float Duration = 5f;

    private Text text;
    private Image image;

    void Awake()
    {
        text = gameObject.GetComponent<Text>();
        image = gameObject.GetComponent<Image>();
        StartCoroutine(FadeImage(false, text, image));
        StartCoroutine(Wait(Duration));
    }

    IEnumerator FadeImage(bool fadeAway, Text text, Image image)
    {
        // fade OUT
        if (fadeAway)
        {
            // loop over Speed seconds backwards
            for (float i = OpacityEnd; i >= OpacityStart; i -= Time.deltaTime / Speed)
            {
                // set color with i as alpha
                if(text != null)
                { 
                text.color = new Color(text.color.r, text.color.g, text.color.b, i);
                }
                if (image != null)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, i);
                }
                yield return null;

            }
        }
        // fade IN
        else
        {
            // loop over Speed seconds
            for (float i = OpacityStart; i <= OpacityEnd; i += Time.deltaTime / Speed)
            {
                // set color with i as alpha
                if (text != null)
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, i);
                }
                if (image != null)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, i);
                }
                yield return null;
            }
        }
    }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        StartCoroutine(FadeImage(true, text, image));
    }
}