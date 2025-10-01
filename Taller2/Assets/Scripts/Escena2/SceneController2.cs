using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneController2 : MonoBehaviour

{
    public Timer tiempoEscena;

    public TextMeshProUGUI TextCoin1;
    public TextMeshProUGUI TextCoin2;
    public TextMeshProUGUI TextCoin3;
    public TextMeshProUGUI TextTotal;

    private void Start()
    {
       
    }


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
