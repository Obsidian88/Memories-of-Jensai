using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image receivingImage;
	private Color normalColor;
	public Color highlightColor = Color.yellow;

    private Sprite dropSprite;
    private string dropText;

    public void OnEnable ()
	{
		if (containerImage != null)
			normalColor = containerImage.color;
	}
	
	public void OnDrop(PointerEventData data)
	{
		if (receivingImage == null)
			return;

        Text receivingText = receivingImage.gameObject.GetComponentInChildren<Text>(true);
        if (receivingText == null)
            return;

        //Debug.Log("ReceivingText: " + receivingText);
        //Debug.Log("ReceivingText.text: " + receivingText.text);

        containerImage.color = normalColor;
        GetDropSpriteAndDropText(data);
        //Debug.Log(dropText.text);

		if (receivingImage.GetComponent<Image>().sprite == null) 
		{
            // Case of dragging items in the GUI bar
            // Case that destinationslot is empty
            // Put dropSprite in destSlot and remove destSlot
            data.pointerDrag.GetComponent<Image>().sprite = null;
            data.pointerDrag.GetComponent<Image>().color = new Color(0.427f, 0.427f, 0.427f, 1f);
            data.pointerDrag.GetComponentInChildren<Text>(true).text = "";
        }
        else if(receivingImage.GetComponent<Image>().sprite.name == "Background")
        {
            // Case of dragging items in the bag
            data.pointerDrag.GetComponent<Image>().sprite = receivingImage.GetComponent<Image>().sprite;
            data.pointerDrag.GetComponentInChildren<Text>(true).text = "";
            //Debug.Log("1. Bedingung");
            //Debug.Log(dropText.text);
        }
		else 
		{
			if (data.pointerDrag.GetComponent<Image> ().sprite == null)
				return;
			// Swap both of them
			data.pointerDrag.GetComponent<Image>().sprite = receivingImage.GetComponent<Image>().sprite;
            data.pointerDrag.GetComponentInChildren<Text>(true).text = receivingText.text;
            //GetDropSprite (data) = receivingImage.GetComponent<Image>().sprite;
        }

        // At destination, put new image and text
		if (dropSprite != null)
        { 
			//receivingImage.overrideSprite = dropSprite;
			receivingImage.GetComponent<Image>().sprite = dropSprite;
            receivingImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        if(dropText != null)
        {
            //Debug.Log("Kondition erreicht. receivingText.text: " + receivingText.text + " - dropText.text: " + dropText);
            receivingText.text = dropText;
        }
    }


	public void OnPointerEnter(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		GetDropSpriteAndDropText(data);
		if (dropSprite != null)
			containerImage.color = highlightColor;
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		containerImage.color = normalColor;
	}
	
	private void GetDropSpriteAndDropText(PointerEventData data)
	{
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return;
		
		var dragMe = originalObj.GetComponent<DragMe>();
		if (dragMe == null)
			return;
		
		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return;

        var srcText = originalObj.GetComponentInChildren<Text>(true);
        if (srcText == null)
            return;

        dropSprite = srcImage.sprite;
        dropText = srcText.text;
        //Debug.Log("Droptext: " + dropText.text);
	}
}