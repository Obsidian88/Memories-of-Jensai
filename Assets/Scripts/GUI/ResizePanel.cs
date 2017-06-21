using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ResizePanel : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
	
	public Vector2 minSize = new Vector2 (100, 100);
	public Vector2 maxSize = new Vector2 (400, 400);

	public Color HoverColor;
	public Color DragColor;

    public Image PanelToResize;
	private RectTransform panelRectTransform;
	private Vector2 originalLocalPointerPosition;
	private Vector2 originalSizeDelta;
	private Image image;
	private Color originalItemColor;
	private bool hovering = false;

	void Awake () {
        //panelRectTransform = transform.parent.GetComponent<RectTransform> ();
        panelRectTransform = PanelToResize.GetComponent<RectTransform>();
        image = gameObject.GetComponentInParent<Image>();
		originalItemColor = image.color;
	}
	
	public void OnPointerDown (PointerEventData data) {
		originalSizeDelta = panelRectTransform.sizeDelta;
		RectTransformUtility.ScreenPointToLocalPointInRectangle (panelRectTransform, data.position, data.pressEventCamera, out originalLocalPointerPosition);
	}
	
	public void OnDrag (PointerEventData data) {
		if (panelRectTransform == null)
			return;

		//Highlightcolor
		image.color = DragColor;
		Vector2 localPointerPosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle (panelRectTransform, data.position, data.pressEventCamera, out localPointerPosition);
		Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
		
		Vector2 sizeDelta = originalSizeDelta + new Vector2 (offsetToOriginal.x, -offsetToOriginal.y);
		sizeDelta = new Vector2 (
			Mathf.Clamp (sizeDelta.x, minSize.x, maxSize.x),
			Mathf.Clamp (sizeDelta.y, minSize.y, maxSize.y)
		);
		
		panelRectTransform.sizeDelta = sizeDelta;

	}

	public void OnEndDrag (PointerEventData data)
	{
		if (hovering) {
			image.color = HoverColor;
		} 
		else 
		{
			image.color = originalItemColor;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		image.color = HoverColor;
		hovering = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		image.color = originalItemColor;
		hovering = false;
	}
}