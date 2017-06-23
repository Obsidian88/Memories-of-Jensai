using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollViewClose : MonoBehaviour, IPointerClickHandler
{
    private Rect windowPos;
    private RectTransform Panel;

    // Use this for initialization
    void Start () {
        //windowPos = gameObject.GetComponent<Rect>();
        Panel = gameObject.GetComponent<RectTransform>();
        windowPos = new Rect(Panel.position.x, Panel.position.y, Panel.rect.width, Panel.rect.height);
        Debug.Log(Panel.rect.x + " - " + Panel.rect.y + " - " + Panel.rect.width + " - " + Panel.rect.height);
    }
	
	// Update is called once per frame
	void Update () {
	}

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (Input.GetMouseButton(0) && gameObject.activeSelf == true)
    //    {

    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (Input.GetMouseButton(0) && gameObject.activeSelf == true)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && gameObject.activeSelf == true && !windowPos.Contains(eventData.position))
        {
            gameObject.SetActive(false);
        }
    }
}
