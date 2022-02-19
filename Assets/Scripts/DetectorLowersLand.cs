using UnityEngine;

public class DetectorLowersLand : MonoBehaviour
{

    float timerRunning = 0;
    [SerializeField]
    float timeDelay = 4;

    public bool isTriggered;

    private void Update()
    {
        if (isTriggered)
        {
            timerRunning += Time.deltaTime;
            if(timerRunning >= timeDelay)
            {
                LowerBox();
                ResetTimer();
            }
        }
    }

    void ResetTimer()
    {
        timerRunning = 0;
        isTriggered = false;
    }

    void LowerBox()
    {
        transform.Translate(Vector3.down * 2000);

    }

    private void OnTriggerEnter(Collider other)
    {

        isTriggered = true;
    }
}
