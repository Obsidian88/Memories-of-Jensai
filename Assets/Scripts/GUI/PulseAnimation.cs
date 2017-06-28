// A simple script used for fading the opacity of text.
// It can be used to make it look less static
// and be subtly animated
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PulseAnimation : MonoBehaviour {

    [Tooltip("The higher the value the more subtle is the opacitychange")]
    public float OpacityMultiplier = 0.8f;

    [Tooltip("The higher the value the slower the transition")]
    public float TimeMultiplier = 4f;
	
    // Updates once per frame
    void Update()
    {
		Image Image = gameObject.GetComponent<Image>();
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, Mathf.PingPong(Time.time / TimeMultiplier, 1 - OpacityMultiplier) + OpacityMultiplier);
    }
}