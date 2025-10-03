using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // cuando el jugador entra en la meta
        {
            Timer t = FindFirstObjectByType<Timer>();
            if (t != null)
            {
                t.TimerStop();
                GameManager.Instance.TotalTime(t.stopTime);
            }
            GameManager.Instance.Victory();
        }
    }
}
