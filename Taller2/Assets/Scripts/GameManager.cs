using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private float globalTime;
    private int scoreLlave = 0;
    private int scoreCoin = 0;
    private int scoreVida = 0;

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
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void TotalTime(float timeScene)
    {
        globalTime += timeScene;
    }
    public void TotalLlave(int llave)
    {
        scoreLlave += llave;

    }
    public void TotalCoin(int Coin)
    {
        scoreCoin += Coin; 
    }
    public void TotalVida(int Vida)
    {
        scoreVida += Vida;
    }

    public float GlobalTime { get => globalTime; set => globalTime = value; }
    public int ScoreLlave { get => scoreLlave; set => scoreLlave = value; }
    public int ScoreCoin { get => scoreCoin; set => scoreCoin = value; }
    public int ScoreVida { get => scoreVida; set => scoreVida = value; }

}