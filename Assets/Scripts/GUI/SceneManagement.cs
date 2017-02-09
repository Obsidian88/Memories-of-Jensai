using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scenes.Scripts;

public class SceneManagement : MonoBehaviour {

	public Button SceneManagementButton; // This is a prefab of the button
    private int AmountOfScenes;

    void Start()
    {
        AmountOfScenes = (Game.Levels).Length;
        float w = SceneManagementButton.GetComponent<RectTransform>().rect.width;
        float h = SceneManagementButton.GetComponent<RectTransform>().rect.height;
        int offset = 29;

        for (int i = 0; i < AmountOfScenes; i++)
        {
            string SceneName = Game.Levels[i];

            Button OneButton = (Button)Instantiate(SceneManagementButton, new Vector3((Screen.width - w) / 2, (Screen.height - h) / 2 - i * offset, 0), Quaternion.identity);

            OneButton.transform.SetParent(transform);
            OneButton.onClick.AddListener(() => LoadScene(SceneName));
            OneButton.GetComponentInChildren<Text>().text = "0" + (i + 1).ToString() + ": " + SceneName;

            //Vector3 pos = OneButton.transform.position;
            //pos.x = 0f;
            //OneButton.transform.position = pos;
        }
    }

    void LoadScene(string SceneName)
	{
		StaticData.LevelToLoad = SceneName;
        SceneManager.LoadScene("LoadingScreen");
	}

}