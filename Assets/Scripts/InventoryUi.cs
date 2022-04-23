using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUi : MonoBehaviour
{

    [SerializeField]
    int[] gridSize = new int[] { 10, 10 };

    GridLayoutGroup gridLayoutGroup;




    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = gridSize[1];
        
        CreateSlots(gridSize[0], gridSize[1]);
    }

    void CreateSlots(int rows, int columns)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject slot = Instantiate(Resources.Load("Prefabs/UiElements/InventorySlot") as GameObject, transform);
                InventorySlotUi slotUi = slot.GetComponent<InventorySlotUi>();
                slotUi.SetGridPosition(new int[] { i, j });
                slotUi.OnItemClicked += OnSlotClicked;

            }
        }
    }

    private void OnSlotClicked(InventorySlotUi obj)
    {

        Debug.Log("Slot clicked");
        if(Input.GetMouseButton(0))
        {
            Debug.Log("Clicked Left");
        }

        if(Input.GetMouseButton(1))
        {
            Debug.Log("Clicked Right");
        }

        
    }

    public void LoadInventory(OwnedItem[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            GameObject slot = transform.GetChild(i).gameObject;
            slot.GetComponent<InventorySlotUi>().AddItem(items[i]);
        }
    }


    
}


