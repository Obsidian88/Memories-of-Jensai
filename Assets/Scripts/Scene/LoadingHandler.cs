using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingHandler : MonoBehaviour {

    [SerializeField]
    private Text loadingText;

    private AsyncOperation async;

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    // Updates once per frame
    void Update()
    {
        //loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
    }

    IEnumerator LoadScene()
    {

        // Can be taken out later .. just for drama-effect now :)
        yield return new WaitForSeconds(2);

        async = SceneManager.LoadSceneAsync("Scene1");

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            // loadingBar.value = async.progress;
            yield break;
        }

    }

}