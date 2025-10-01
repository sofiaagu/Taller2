using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private float globalTime;

    public int TotalScore { get; private set; }

    private int scoreCoin = 0;
    private int scoreCoin2 = 0;
    private int scoreCoin3 = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        globalTime = 0;
        TotalScore = 0;
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void TotalTime(float timeScene)
    {
        globalTime += timeScene;
    }
    public void TotalCoin(int coin)
    {
        scoreCoin += coin;
        TotalScore += coin;
    }
    public void TotalCoin2(int Coin2)
    {
        scoreCoin2 += Coin2;
        TotalScore += Coin2;
    }
    public void TotalCoin3(int Coin3)
    {
        scoreCoin3 += Coin3;
        TotalScore += Coin3;
    }


    public float GlobalTime { get => globalTime; set => globalTime = value; }
    public int ScoreCoin { get => scoreCoin; set => scoreCoin = value; }
    public int ScoreCoin2 { get => scoreCoin2; set => scoreCoin2 = value; }
    public int ScoreCoin3 { get => scoreCoin3; set => scoreCoin3 = value; }

}