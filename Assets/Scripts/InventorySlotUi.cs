using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySlotUi : MonoBehaviour
{
    public event Action<InventorySlotUi> OnItemClicked;

    int[] gridPosition;

    public Image icon;

    public Image iconBackGround;

    public Button button;

    OwnedItem displayedItem;

    public void SetGridPosition(int[] gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    

    public void AddItem(OwnedItem item)
    {
        displayedItem = item;
        icon.sprite =  displayedItem.TypeOfOwnable.GetSprite();
        icon.color = Color.white;
        iconBackGround.enabled = true;
        button.interactable = true;
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.color = Color.clear;
        iconBackGround.enabled = false;

        button.interactable = false;
    }

    public void OnButtonClicked()
    {
        OnItemClicked?.Invoke(this);
    }


}


