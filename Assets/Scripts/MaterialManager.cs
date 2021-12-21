using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    private static MaterialManager instance;
    public static MaterialManager Instance

    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MaterialManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned MaterialManager", typeof(MaterialManager)).GetComponent<MaterialManager>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public Material GreenTransparent { get => Instantiate(Resources.Load("Materials/TransparentGreen")) as Material; }
}