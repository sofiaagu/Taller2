using UnityEngine;
using TMPro;

public class GameController1 : MonoBehaviour
{
    public Timer tiempoEscena;

    public TextMeshProUGUI TextCoin1;
    public TextMeshProUGUI TextCoin2;
    public TextMeshProUGUI TextCoin3;
    public TextMeshProUGUI TextTotal;

    void Update()
    {
        if (GameManager.Instance != null)
        {
            TextCoin1.text = GameManager.Instance.ScoreCoin.ToString();
            TextCoin2.text = GameManager.Instance.ScoreCoin2.ToString();
            TextCoin3.text = GameManager.Instance.ScoreCoin3.ToString();
            TextTotal.text = GameManager.Instance.TotalScore.ToString();
        }
    }
}

//    public void addTime()
//    {
//        tiempoEscena.TimerStop();
//        float getTimeScene = tiempoEscena.StopTime;
//        GameManager.Instance.TotalTime(getTimeScene);
//        Debug.Log("Tiempo Escena 1 " + GameManager.Instance.GlobalTime);
//    }
//}
