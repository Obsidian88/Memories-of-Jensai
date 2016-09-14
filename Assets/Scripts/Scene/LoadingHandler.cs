using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingHandler : MonoBehaviour {

    public Text loadingText;
    public Text loadingText2;
    public Texture2D emptyProgressBar;
	public Texture2D fullProgressBar;
	
	private string levelname = "CharacterAnimationTester";

    private AsyncOperation async = null;
	//private int resolution = 0;

    void Start()
    {
		// if(PlayerPrefs.HasKey("GraphicsResolution"))
		// {
		// resolution = PlayerPrefs.GetInt("GraphicsResolution");
		// switch resolution
		// case 0:
		// Screen.SetResolution(ResolutionWidth, ResolutionHeight, ToggleFullscreen.isOn)
		// }
		if(StaticData.currentLevel != "")
		{
		levelname = StaticData.currentLevel;
		}
        StartCoroutine(LoadScene(levelname));
    }

    // Updates once per frame
    void Update()
    {
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1 - 0.5f) + 0.5f);
        loadingText2.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1 - 0.5f) + 0.5f);
    }
	
    // Progressbar.. maybe for later..
	//void OnGUI() 
	//{
	//	if (async != null) 
	//	{
	//		// Alternatively: Use Scrollbar of Unity UI .. somehow
	//		GUI.DrawTexture(new Rect(0, 0, 100, 50), emptyProgressBar);
	//		GUI.DrawTexture(new Rect(0, 0, 100 * async.progress, 50), fullProgressBar);
	//	}
	//}

    IEnumerator LoadScene(string levelname)
    {
        // Can be taken out later .. just for drama-effect now :)
        yield return new WaitForSeconds(0.5f);

        async = SceneManager.LoadSceneAsync(levelname);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield break;
        }
    }
}