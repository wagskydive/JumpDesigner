using System;
using System.Collections.Generic;
using UnityEngine;

public class PID_ValuesTester : MonoBehaviour
{
    public static event Action<Vector3> OnxzPidValuesChanged;
    public static event Action<Vector3> OnyPidValuesChanged;
    public static event Action<Vector3> OnwPidValuesChanged;



    [SerializeField]
    public float xzKp, xzKi, xzKd;
    public Vector3 xzLastPID;

    [SerializeField]
    public float yKp, yKi, yKd;
    public Vector3 yLastPID;

    [SerializeField]
    public float wKp, wKi, wKd;
    public Vector3 wLastPID;





    private void Start()
    {
        xzLastPID = new Vector3(xzKp, xzKi, xzKd);
        yLastPID = new Vector3(yKp,yKi, yKd);
        wLastPID = new Vector3(wKp, wKi, wKd);
        OnxzPidValuesChanged?.Invoke(xzLastPID);
        OnxzPidValuesChanged?.Invoke(yLastPID);
        OnxzPidValuesChanged?.Invoke(wLastPID);
    }
    // Update is called once per frame
    void Update()
    {
        if (xzKp != xzLastPID.x || xzKi != xzLastPID.y|| xzKd != xzLastPID.z)
        {
            xzLastPID = new Vector3(xzKp, xzKi, xzKd);
            OnxzPidValuesChanged?.Invoke(xzLastPID);
        }
        if (yKp != yLastPID.x || yKi != yLastPID.y || yKd != yLastPID.z)
        {
            yLastPID = new Vector3(yKp, yKi, yKd);
            OnyPidValuesChanged?.Invoke(yLastPID);
        }
        if (wKp != wLastPID.x || wKi != wLastPID.y || wKd != wLastPID.z)
        {
            wLastPID = new Vector3(wKp, wKi, wKd);
            OnwPidValuesChanged?.Invoke(wLastPID);
        }

    }
}
