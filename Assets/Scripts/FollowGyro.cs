using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGyro : MonoBehaviour
{
    [Header("Tweaks")]
    [SerializeField]
    private Quaternion baseRotation = new Quaternion(0, 0, 1, 0);

    void Start()
    {
        GyroManager.Instance.EnableGyro();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = GyroManager.Instance.GetGyroRotation() * baseRotation;
        transform.localEulerAngles = new Vector3(-transform.localEulerAngles.y, transform.localEulerAngles.x,0);
    }
}
