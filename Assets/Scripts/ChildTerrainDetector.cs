using UnityEngine;

public class ChildTerrainDetector : MonoBehaviour
{
    DetectorLowersLand parentDetector;
    private void Awake()
    {
        parentDetector = GetComponentInParent<DetectorLowersLand>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(parentDetector != null)
        {
            parentDetector.isTriggered = true;
        }
    }
}
