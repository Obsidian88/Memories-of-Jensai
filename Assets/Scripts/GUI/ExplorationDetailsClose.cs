using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExplorationDetailsClose : MonoBehaviour, IPointerClickHandler
{
    public AudioClip ClosePanelSound;
    private AudioSource source;

    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    // On click of the ClickArea all childelements will be closed
    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (Transform child in transform)
            if(child.transform.gameObject.activeSelf)
            { 
            CloseSpecificPanel(child.gameObject);
            }
    }

    public void CloseSpecificPanel(GameObject child)
    {
        child.SetActive(false);
        source.PlayOneShot(ClosePanelSound);
    }
}
