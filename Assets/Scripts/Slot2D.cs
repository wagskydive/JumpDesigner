using UnityEngine;
using UnityEngine.UI;

public class Slot2D : MonoBehaviour
{
    public Transform[] childSlotParents;

    [SerializeField]
    Image visual;

    [SerializeField]
    string resourcePath;

    public int Index { get; private set; }

    private void Awake()
    {
        PositionSlots();
    }


    void PositionSlots()
    {

        RectTransform rectTransform = GetComponent<RectTransform>();


        RectTransform childRect0 = childSlotParents[0].GetComponent<RectTransform>();   
        childRect0.anchoredPosition = new Vector2(-rectTransform.rect.width, rectTransform.rect.height);



        RectTransform childRect1 = childSlotParents[1].GetComponent<RectTransform>();   
        childRect1.anchoredPosition = new Vector2(0, rectTransform.rect.height);


        RectTransform childRect2 = childSlotParents[2].GetComponent<RectTransform>();   
        childRect2.anchoredPosition = new Vector2(rectTransform.rect.width, rectTransform.rect.height);



        RectTransform childRect3 = childSlotParents[3].GetComponent<RectTransform>();   
        childRect3.anchoredPosition = new Vector2(-rectTransform.rect.width, 0);


        RectTransform childRect4 = childSlotParents[4].GetComponent<RectTransform>();   
        childRect4.anchoredPosition = new Vector2(rectTransform.rect.width, 0);



        RectTransform childRect5 = childSlotParents[5].GetComponent<RectTransform>();
        childRect5.anchoredPosition = new Vector2(-rectTransform.rect.width, -rectTransform.rect.height);


        RectTransform childRect6 = childSlotParents[6].GetComponent<RectTransform>();
        childRect6.anchoredPosition = new Vector2(0, -rectTransform.rect.height);


        RectTransform childRect7 = childSlotParents[7].GetComponent<RectTransform>();
        childRect7.anchoredPosition = new Vector2(rectTransform.rect.width, -rectTransform.rect.height);

    }

    public void SetVisual(Formation formation, int skydiver)
    {
        Index = skydiver;
        GetComponentInChildren<Selectable2D>().SetIndex(Index);
        visual.sprite = Resources.Load<Sprite>(resourcePath + formation.GetOrientation(skydiver).ToString());
        transform.localEulerAngles = new Vector3(0, 0, formation.GetRotation(skydiver));
        if(skydiver == 0)
        {
            visual.color = Color.red;
        }
    }



}
