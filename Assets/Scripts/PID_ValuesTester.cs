using System;
using System.Collections.Generic;
using UnityEngine;

public class PID_ValuesTester : MonoBehaviour
{
    public static event Action<Vector3> OnxzPidValuesChanged;
    public static event Action<Vector3> OnyPidValuesChanged;
    public static event Action<Vector3> OnwPidValuesChanged;


    public static event Action<Vector3> OnCanopywPidValuesChanged;



    [SerializeField]
    public float xzKp, xzKi, xzKd;
    public Vector3 xzLastPID;

    [SerializeField]
    public float yKp, yKi, yKd;
    public Vector3 yLastPID;

    [SerializeField]
    public float wKp, wKi, wKd;
    public Vector3 wLastPID;
    
    [SerializeField]
    public float CanopywKp, CanopywKi, CanopywKd;
    public Vector3 CanopywLastPID;





    private void Start()
    {
        xzLastPID = new Vector3(xzKp, xzKi, xzKd);
        yLastPID = new Vector3(yKp,yKi, yKd);
        wLastPID = new Vector3(wKp, wKi, wKd);
        CanopywLastPID = new Vector3(CanopywKp, CanopywKi, CanopywKd);
        OnxzPidValuesChanged?.Invoke(xzLastPID);
        OnyPidValuesChanged?.Invoke(yLastPID);
        OnwPidValuesChanged?.Invoke(wLastPID);
        OnCanopywPidValuesChanged?.Invoke(wLastPID);
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
        if (CanopywKp != CanopywLastPID.x || CanopywKi != CanopywLastPID.y || CanopywKd != CanopywLastPID.z)
        {
            CanopywLastPID = new Vector3(CanopywKp, CanopywKi, CanopywKd);
            OnCanopywPidValuesChanged?.Invoke(CanopywLastPID);
        }
    }
}
