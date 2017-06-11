// A simple script used for fading the opacity of text.
// It can be used to make it look less static
// and be subtly animated
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextIdlePulseAnimation : MonoBehaviour {

    public Text[] TextsToAnimate;

    [Tooltip("The higher the value the more subtle is the opacitychange")]
    public float OpacityMultiplier = 0.8f;

    [Tooltip("The higher the value the slower the transition")]
    public float TimeMultiplier = 4f;
	
    // Updates once per frame
    void Update()
    {
		foreach(Text CurrentText in TextsToAnimate)
		{
        CurrentText.color = new Color(CurrentText.color.r, CurrentText.color.g, CurrentText.color.b, Mathf.PingPong(Time.time / TimeMultiplier, 1 - OpacityMultiplier) + OpacityMultiplier);
		}
    }
}