using Chronity;
using UnityEngine;

public class ChronityTest : MonoBehaviour
{
    private Timer timer;

    public void StartTimer()
    {
        timer = this.RegisterTimer(5, () => Debug.Log("Hello World"), x => Debug.Log($"Timer Updated: {x}"));
    }

    public void PauseTimer()
    {
        timer.Pause();
    }

    public void ResumeTimer()
    {
        timer.Resume();
    }

    public void CancelTimer()
    {
        timer.Cancel();
    }

    public void DestroyAttached()
    {
        Destroy(gameObject);
    }

    public void ChangeTimeScale()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}