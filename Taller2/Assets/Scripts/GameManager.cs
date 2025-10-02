using UnityEngine;
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
    public Transform heartsContainer;    // Contenedor de corazones en el Canvas

    private List<GameObject> hearts = new List<GameObject>();

    [Header("Puntaje")]
    private int scoreCoin = 0;
    private int scoreCoin2 = 0;
    private int scoreCoin3 = 0;

    public int TotalScore { get; private set; }

    // ------------------------
    // Configuración Singleton
    // ------------------------
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);   // Evita duplicados
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Suscribir evento al cargar escenas
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        globalTime = 0;
        TotalScore = 0;

        // Solo inicializa vida si no tenía valor antes
        if (currentHealth <= 0)
            currentHealth = maxHealth;

        // Crear corazones si hay un Canvas en la primera escena
        if (heartsContainer != null)
            CrearHeartsUI();
    }

    // ------------------------
    // Manejo de escenas
    // ------------------------
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscar el contenedor de corazones en la nueva escena
        GameObject container = GameObject.Find("HeartsContainer");
        if (container != null)
        {
            heartsContainer = container.transform;

            // Regenerar la UI
            CrearHeartsUI();
        }
        else
        {
            Debug.LogWarning("No se encontró HeartsContainer en la escena " + scene.name);
        }
    }

    // ------------------------
    // Vida
    // ------------------------
    public void QuitarVida(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        ActualizarUI();

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
            // Aquí podrías cargar escena de derrota o reiniciar
        }
    }

    private void CrearHeartsUI()
    {
        // Limpiar corazones previos
        foreach (var h in hearts)
        {
            if (h != null)
                Destroy(h);
        }
        hearts.Clear();

        // Crear corazones según vida máxima
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsContainer);
            hearts.Add(newHeart);
        }

        // Actualizar según vida actual
        ActualizarUI();
    }

    private void ActualizarUI()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (hearts[i] != null)
                hearts[i].SetActive(i < currentHealth);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // ------------------------
    // Tiempo y puntaje
    // ------------------------
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

    // Propiedades
    public float GlobalTime { get => globalTime; set => globalTime = value; }
    public int ScoreCoin { get => scoreCoin; set => scoreCoin = value; }
    public int ScoreCoin2 { get => scoreCoin2; set => scoreCoin2 = value; }
    public int ScoreCoin3 { get => scoreCoin3; set => scoreCoin3 = value; }
}
