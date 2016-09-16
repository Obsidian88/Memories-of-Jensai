using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntegerCountdown : MonoBehaviour {

    private string TextWithCountdown;
    private int IndexToReplace;
    public int StartNumberToCountDownFrom = 8;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void StartCountdown()
    {
        TextWithCountdown = GetComponent<Text>().text;
        IndexToReplace = TextWithCountdown.IndexOf("%");
        TextWithCountdown = TextWithCountdown.Remove(IndexToReplace, 1).Insert(IndexToReplace, StartNumberToCountDownFrom.ToString());
        GetComponent<Text>().text = TextWithCountdown;
        StartCoroutine(PostEverySecond(StartNumberToCountDownFrom));
    }

    IEnumerator PostEverySecond(int LastNumber)
    {
        if(LastNumber > 0)
        { 
        yield return new WaitForSeconds(1f);
        TextWithCountdown = TextWithCountdown.Remove(IndexToReplace, 1).Insert(IndexToReplace, (LastNumber - 1).ToString());

        GetComponent<Text>().text = TextWithCountdown;
        StartCoroutine(PostEverySecond(LastNumber - 1));
        }
    }
}
