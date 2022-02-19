using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IPointerExitHandler
{
    public static event Action<Color> OnColorChanged;

    [SerializeField]
    Color currentColor;

    Texture2D ColorTexture;

    RectTransform Rect;

    private void Start()
    {
        Rect = GetComponent<RectTransform>();
        ColorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    bool pointerIsDown;


    public void Update()
    {
        if(pointerIsDown)
        {
            Vector2 delta;


            RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, null, out delta);

            float width = Rect.rect.width;
            float height = Rect.rect.height;

            delta += new Vector2(width * .5f, height * .5f);

            float x = Mathf.Clamp(delta.x / width, 0, 1);
            float y = Mathf.Clamp(delta.y / height, 0, 1);

            int texX = Mathf.RoundToInt(x * ColorTexture.width);
            int texY = Mathf.RoundToInt(y * ColorTexture.height);



            Color colorUnderMouse = ColorTexture.GetPixel(texX, texY);
            Debug.Log("MousePosition = " + delta);
            currentColor = colorUnderMouse;
            OnColorChanged?.Invoke(currentColor);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pointerIsDown = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerIsDown = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerIsDown = false;
    }
}
