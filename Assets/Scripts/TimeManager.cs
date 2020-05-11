using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float defaultFixedDeltaTime;
    private float defaultTimeScale;

    public void DoSlowMotion(float slowDownFactor)
        {
        defaultTimeScale = Time.timeScale;
        defaultFixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        }

    public void UndoSlowMotion ()
        {
        Time.timeScale = defaultTimeScale;
        Time.fixedDeltaTime = defaultFixedDeltaTime;
        }
}
