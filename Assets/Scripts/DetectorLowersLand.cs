using UnityEngine;

public class DetectorLowersLand : MonoBehaviour
{

    float timerRunning = 0;
    [SerializeField]
    float timeDelay = 4;

    [SerializeField]
    float lowerAmount = 1500;

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
        transform.Translate(Vector3.down * lowerAmount);

    }

    private void OnTriggerEnter(Collider other)
    {

        isTriggered = true;
    }
}
