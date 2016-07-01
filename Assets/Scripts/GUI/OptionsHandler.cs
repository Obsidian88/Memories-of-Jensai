// Template that should be instanciated in every scene

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour {

    // Later on this script could overlay a rectangle GUI with buttons to have all options bundled together visually..
    public Text SoundstatusText;
    public float hSliderValue = 1.0f;
    // Use this for initialization
    void Start () {
        AudioListener.pause = false;
    }

    // Update is called once per frame
    void Update () 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        { 
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("Character Customization");
        }
        if(Input.GetKeyDown(KeyCode.M))
        {

        }
    }

    void OnGUI()
    {

        if(GUI.Button(new Rect(25, 5, 80, 20), "Mute Audio"))
        {
            ToggleAudio();
        }
        hSliderValue = GUI.HorizontalSlider(new Rect(25,25,100,30), hSliderValue, 0.0f, 1.0f);  // Rect(x, y, width, height)
        GUI.Label(new Rect(25,35,150,20), "Audiovolume: " + (Mathf.Round(hSliderValue * 100f) / 100f).ToString());
        AudioListener.volume = hSliderValue;
    }

    void ToggleAudio()
    {
            if(AudioListener.pause == false)
            {
                AudioListener.pause = true;
                SoundstatusText.text = "Sound is muted";
            }
            else if (AudioListener.pause == true)
            {
                AudioListener.pause = false;
                SoundstatusText.text = "";
            }
    }
}