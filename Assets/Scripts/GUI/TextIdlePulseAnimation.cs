// A simple script used for fading the opacity of text.
// It can be used to make it look less static
// and be subtly animated
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextIdlePulseAnimation : MonoBehaviour {

    public Text[] TextsToAnimate;
	public float AnimationSpeed = 0.5f;
	
    // Updates once per frame
    void Update()
    {
		foreach(Text CurrentText in TextsToAnimate)
		{
        CurrentText.color = new Color(CurrentText.color.r, CurrentText.color.g, CurrentText.color.b, Mathf.PingPong(Time.time, 1 - AnimationSpeed) + AnimationSpeed);
		}
    }
}