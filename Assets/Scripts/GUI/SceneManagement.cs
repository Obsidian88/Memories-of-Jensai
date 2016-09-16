using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

	public Button SceneManagementButton; // This is a prefab of the button
	private string[] Scenes = StaticGetAllAvailableScenes.Levels;
    private int AmountOfScenes;

    void Start()
    {
        AmountOfScenes = Scenes.Length;
        int w = 200;
        int h = 100;

        for (int i = 0; i < AmountOfScenes; i++)
		{
			string SceneName = Scenes[i];

            Button OneButton = (Button) Instantiate(SceneManagementButton, new Vector3((Screen.width - w) / 2 + i * 250, (Screen.height - h) / 2, 0), Quaternion.identity);

            OneButton.transform.SetParent(transform);
            OneButton.onClick.AddListener(() => LoadScene(SceneName));
			OneButton.GetComponentInChildren<Text>().text = "Load Level: " + SceneName;
		}
    }

	void LoadScene(string SceneName)
	{
		StaticData.LevelToLoad = SceneName;
        SceneManager.LoadScene("LoadingScreen");
	}

}