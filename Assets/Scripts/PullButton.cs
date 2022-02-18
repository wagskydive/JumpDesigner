using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullButton : MonoBehaviour
{
    public void PullButtonPress()
    {
        ISelectable selectable = SelectionHandler.Instance.player;
        if(selectable != null)
        {
             MovementController movementController = selectable.transform.gameObject.GetComponent<MovementController>();
            if(movementController != null)
            {
                movementController.PullParachute();
            }
        }
    }
}
