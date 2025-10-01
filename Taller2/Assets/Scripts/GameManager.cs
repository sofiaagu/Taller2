using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private float globalTime;

    [Header("Vida")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("UI de Vida")]
    public GameObject heartPrefab;       // Prefab del corazón
    public Transform heartsContainer;    // El panel o contenedor en el Canvas

    private List<GameObject> hearts = new List<GameObject>();
    

    private int scoreCoin = 0;
    private int scoreCoin2 = 0;
    private int scoreCoin3 = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        globalTime = 0;
        TotalScore = 0;
        currentHealth = maxHealth;
        CrearHeartsUI();
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

    private void CrearHeartsUI()
    {
        // Limpiar corazones previos
        foreach (var h in hearts)
        {
            Destroy(h);
        }
        hearts.Clear();

        // Crear corazones según la vida máxima
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsContainer);
            hearts.Add(newHeart);
        }

        ActualizarUI();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscar el contenedor en la nueva escena
        GameObject container = GameObject.Find("HeartsContainer");
        if (container != null)
        {
            heartsContainer = container.transform;
            CrearHeartsUI();
        }
        else
        {
            Debug.LogWarning("No se encontró HeartsContainer en la escena " + scene.name);
        }
    }

    public void QuitarVida(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        ActualizarUI();

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
            // Aquí puedes cargar escena de derrota o reiniciar
        }
    }



    private void ActualizarUI()
    {
        // Activa/desactiva corazones según vida actual
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].SetActive(i < currentHealth);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GlobalTime { get => globalTime; set => globalTime = value; }
    public int ScoreCoin { get => scoreCoin; set => scoreCoin = value; }
    public int ScoreCoin2 { get => scoreCoin2; set => scoreCoin2 = value; }
    public int ScoreCoin3 { get => scoreCoin3; set => scoreCoin3 = value; }
    public int TotalScore { get; private set; }

}