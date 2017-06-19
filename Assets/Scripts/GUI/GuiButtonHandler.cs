using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiButtonHandler : MonoBehaviour {

    private bool enableDebug = false;

    public Image PanelBag;
    public Image PanelCharacter;
    public Image PanelJournal;
    public Image PanelMap;

    public AudioClip OpenClose;
    public AudioClip ExecuteAction;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        source = gameObject.AddComponent<AudioSource>();
    }

    private void PlaysoundOpenClose()
    {
        source.PlayOneShot(OpenClose);
    }

    private void PlaysoundExecuteAction()
    {
        source.PlayOneShot(ExecuteAction);
    }

    // Update is called once per frame
    void Update () {
        if((Input.GetKeyDown(KeyCode.Alpha1)))
        {
            executeAction1();
        }
        if ((Input.GetKeyDown(KeyCode.Alpha2)))
        {
            executeAction2();
        }
        if ((Input.GetKeyDown(KeyCode.Alpha3)))
        {
            executeAction3();
        }
        if ((Input.GetKeyDown(KeyCode.Alpha4)))
        {
            executeAction4();
        }
        if ((Input.GetKeyDown(KeyCode.Alpha5)))
        {
            executeAction5();
        }
        if ((Input.GetKeyDown(KeyCode.Alpha6)))
        {
            executeAction6();
        }
        if ((Input.GetKeyDown(KeyCode.Alpha7)))
        {
            executeAction7();
        }
        if ((Input.GetKeyDown(KeyCode.Alpha8)))
        {
            executeAction8();
        }

        if ((Input.GetKeyDown(KeyCode.B)))
        {
            openBag();
        }
        if ((Input.GetKeyDown(KeyCode.C)))
        {
            openCharacter();
        }
        if ((Input.GetKeyDown(KeyCode.J)))
        {
            openJournal();
        }
        if ((Input.GetKeyDown(KeyCode.M)))
        {
            openMap();
        }

    }

    public void executeAction1()
    {
        if (enableDebug) { Debug.Log("Action1 was executed"); }
        PlaysoundExecuteAction();
    }

    public void executeAction2()
    {
        if (enableDebug) { Debug.Log("Action2 was executed"); }
        PlaysoundExecuteAction();
    }

    public void executeAction3()
    {
        if (enableDebug) { Debug.Log("Action3 was executed"); }
        PlaysoundExecuteAction();
    }

    public void executeAction4()
    {
        if (enableDebug) { Debug.Log("Action4 was executed"); }
        PlaysoundExecuteAction();
    }

    public void executeAction5()
    {
        if (enableDebug) { Debug.Log("Action5 was executed"); }
        PlaysoundExecuteAction();
    }

    public void executeAction6()
    {
        if (enableDebug) { Debug.Log("Action6 was executed"); }
        PlaysoundExecuteAction();
    }

    public void executeAction7()
    {
        if (enableDebug) { Debug.Log("Action7 was executed"); }
        PlaysoundExecuteAction();
    }

    public void executeAction8()
    {
        if (enableDebug) { Debug.Log("Action8 was executed"); }
        PlaysoundExecuteAction();
    }


    public void openBag()
    {
        if (enableDebug) { Debug.Log("Bagpanel was opened"); }
        PlaysoundOpenClose();
        if(PanelBag.gameObject.activeSelf == true)
        {
            PanelBag.gameObject.SetActive(false);
        }
        else
        {
            PanelBag.gameObject.SetActive(true);
        }
    }

    public void openCharacter()
    {
        if (enableDebug) { Debug.Log("Characterpanel was opened"); }
        PlaysoundOpenClose();
        if (PanelCharacter.gameObject.activeSelf == true)
        {
            PanelCharacter.gameObject.SetActive(false);
        }
        else
        {
            PanelCharacter.gameObject.SetActive(true);
        }
    }

    public void openJournal()
    {
        if (enableDebug) { Debug.Log("Journalpanel was opened"); }
        PlaysoundOpenClose();
        if (PanelJournal.gameObject.activeSelf == true)
        {
            PanelJournal.gameObject.SetActive(false);
        }
        else
        {
            PanelJournal.gameObject.SetActive(true);
        }
    }

    public void openMap()
    {
        if (enableDebug) { Debug.Log("Mappanel was opened"); }
        PlaysoundOpenClose();
        if (PanelMap.gameObject.activeSelf == true)
        {
            PanelMap.gameObject.SetActive(false);
        }
        else
        {
            PanelMap.gameObject.SetActive(true);
        }
    }
}
