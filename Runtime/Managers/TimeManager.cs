using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Yube.Singleton<TimeManager>
{
    public bool IsTimeStopped { get; private set; } = false;

    public Action OnTimeRestored = null;

    public void StopTime(float duration)
    {
        if (IsTimeStopped)
            return;
        IsTimeStopped = true;
        StartCoroutine(ReinitTimeScale(duration));
        Time.timeScale = 0.01f;
    }

    private IEnumerator ReinitTimeScale(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        IsTimeStopped = false;
        OnTimeRestored?.Invoke();
    }

    private void ReinitTimeScale()
    {
        Time.timeScale = 1.0f;
    }
}
