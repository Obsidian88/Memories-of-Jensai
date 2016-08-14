// Placed on a clock to control the clock and also show UI-information about the time
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum HandDirection
	{
		x, y, z
	}

public class ClockHandler : MonoBehaviour {
	
    // The game object which represents the hour hand of the clock.
	public Transform hourHand;
    // The game object which represents the minute hand of the clock.
	public Transform minuteHand;
	
    public HandDirection CurrentDirection;
	public Text TimeStatusText;
	private RawImage TimeStatusImage;
    public Texture Moon;
    public Texture Sun;
    float currentTimeOfDay;
    string currentHourS;
    string currentMinuteS;
    private GameObject[] Torchflames;

    // The number of degrees per hour on our clock face.
    float hoursToDegrees = 360f / 12f;
    // The number of degrees per minute on our clock face.
	float minutesToDegrees = 360f / 60f;

    // A reference to the SunCycle script.
	SunCycle SunHandler;

	void Awake() {
        // Find stuff..
		SunHandler = GameObject.Find("SunCycle").GetComponent<SunCycle>();
		TimeStatusImage = GameObject.Find("RawImageTimeStatus").GetComponent<RawImage>();

        Torchflames = GameObject.FindGameObjectsWithTag("TorchFlame");
        foreach (GameObject Flame in Torchflames)
        {
            Flame.gameObject.SetActive(false);
        }
    }

	void Update() {
        // Calculate the current hour and minute according to the currentTimeOfDay
        // variable in the SunCycle.
        // The extra calculation for the current minute is to make sure it stays
        // between 0 and 60 and not keeps increasing as the hours increase.
        float currentHour = 24 * SunHandler.currentTimeOfDay;
        float currentMinute = 60 * (currentHour - Mathf.Floor(currentHour));
        currentHourS = Mathf.Floor(currentHour).ToString();
        currentMinuteS = Mathf.Floor(currentMinute).ToString();

        UpdateText();
		
		if(currentHour >= 8 && currentHour <= 20)
		{
            TimeStatusImage.texture = Sun;
            foreach (GameObject Flame in Torchflames)
            {
                Flame.gameObject.SetActive(false);
            }
        }
        else
        {
            TimeStatusImage.texture = Moon;
            foreach (GameObject Flame in Torchflames)
            {
                Flame.gameObject.SetActive(true);
            }
        }
		
        // Rotate the hands of the clock according to the values we've defined.
        // Depending on the facing-direction of the clock, a different axis must be used.
		switch (CurrentDirection)
		{
			case HandDirection.x:
				hourHand.localRotation = Quaternion.Euler(currentHour * hoursToDegrees, 0, 0);
				minuteHand.localRotation = Quaternion.Euler(currentMinute * minutesToDegrees, 0, 0);
                break;
			case HandDirection.y:
				hourHand.localRotation = Quaternion.Euler(0, currentHour * hoursToDegrees, 0);
				minuteHand.localRotation = Quaternion.Euler(0, currentMinute * minutesToDegrees, 0);
				break;
			case HandDirection.z:
				hourHand.localRotation = Quaternion.Euler(0, 0, currentHour * hoursToDegrees);
				minuteHand.localRotation = Quaternion.Euler(0, 0, currentMinute * minutesToDegrees);
				break;
			default:
				hourHand.localRotation = Quaternion.Euler(currentHour * hoursToDegrees, 0, 0);
				minuteHand.localRotation = Quaternion.Euler(currentMinute * minutesToDegrees, 0, 0);
				break;
		}
	}

    void UpdateText()
    {
        if (currentHourS.Length == 1)
        {
            currentHourS = "0" + currentHourS;
        }
        if (currentMinuteS.Length == 1)
        {
            currentMinuteS = "0" + currentMinuteS;
        }
        TimeStatusText.text = "The time is " + currentHourS + ":" + currentMinuteS;
    }
}