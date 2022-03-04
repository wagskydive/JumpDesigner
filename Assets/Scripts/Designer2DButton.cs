using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Designer2DButton : MonoBehaviour
{
    public void DesignerButtonPressed()
    {
        FindObjectOfType<GameLoader>().DesignerButtonPressed();
    }
}
