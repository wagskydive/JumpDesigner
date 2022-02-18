using UnityEngine;

public class CutawayButton : MonoBehaviour
{
    public void CutawayButtonPress()
    {
        ISelectable selectable = SelectionHandler.Instance.player;
        if (selectable != null)
        {
            CanopyController canopyController = selectable.transform.gameObject.GetComponent<CanopyController>();
            if (canopyController != null)
            {
                canopyController.Cutaway();
            }
        }
    }
}
