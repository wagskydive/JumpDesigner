using System;
using UnityEngine;

public class AltitudeOverTerrain : MonoBehaviour
{

    public static event Action<Vector3,float> OnTerrainHit;
    public static event Action OnNoHit;

    [SerializeField]
    Transform character;


    private void Update()
    {

        Ray ray = new Ray(character.position, Vector3.down);

        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 200))
        {
            if (raycastHit.collider.GetType() == typeof(TerrainCollider))
            {

                OnTerrainHit?.Invoke(raycastHit.point, Vector3.Distance(character.position, raycastHit.point));

                return;
            }
        }
        OnNoHit?.Invoke();

    }

}
