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
	
	public void OnEnable ()
	{
		if (containerImage != null)
			normalColor = containerImage.color;
	}
	
	public void OnDrop(PointerEventData data)
	{
		containerImage.color = normalColor;
		
		if (receivingImage == null)
			return;

		Sprite dropSprite = GetDropSprite (data);

		if (receivingImage.GetComponent<Image>().sprite == null) 
		{
            // Case that destinationslot is empty
            // Put dropSprite in destSlot and remove destSlot
            data.pointerDrag.GetComponent<Image>().sprite = null;
            data.pointerDrag.GetComponent<Image>().color = new Color(0.427f, 0.427f, 0.427f, 1f);
        }
		else 
		{
			if (data.pointerDrag.GetComponent<Image> ().sprite == null)
				return;
			// Swap both of them
			data.pointerDrag.GetComponent<Image>().sprite = receivingImage.GetComponent<Image>().sprite;
			//GetDropSprite (data) = receivingImage.GetComponent<Image>().sprite;
		}


		if (dropSprite != null)
			//receivingImage.overrideSprite = dropSprite;
			receivingImage.GetComponent<Image>().sprite = dropSprite;
            receivingImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }

	public void OnPointerEnter(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		Sprite dropSprite = GetDropSprite (data);
		if (dropSprite != null)
			containerImage.color = highlightColor;
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		containerImage.color = normalColor;
	}
	
	private Sprite GetDropSprite(PointerEventData data)
	{
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;
		
		var dragMe = originalObj.GetComponent<DragMe>();
		if (dragMe == null)
			return null;
		
		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;
		
		return srcImage.sprite;
	}
}