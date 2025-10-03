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

    [Header("UI de Game Over")]
    public GameObject gameOverPrefab;
    private GameObject gameOverPanel;

    [Header("UI de Vida")]
    public GameObject heartPrefab;       // Prefab del corazón
    public Transform heartsContainer;    // Contenedor de corazones en el Canvas

    private List<GameObject> hearts = new List<GameObject>();

    [Header("Puntaje")]
    private int scoreCoin = 0;
    private int scoreCoin2 = 0;
    private int scoreCoin3 = 0;

    public int TotalScore { get; set; }

    // ------------------------
    // Configuración Singleton
    // ------------------------
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto hace que persista entre escenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }

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

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
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
            CrearHeartsUI();
        }

        // Buscar el panel Game Over de esta escena
        GameObject panel = GameObject.Find("gameOverPanel");
        if (panel != null)
        {
            gameOverPanel = panel;
            gameOverPanel.SetActive(false); // siempre inicia oculto
        }
    }

    // ------------------------
    // Vida
    // ------------------------
    public void QuitarVida(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        // ✅ Actualizar corazones en UI
        ActualizarUI();

        if (currentHealth <= 0)
        {
            // Animación de muerte
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Animator anim = player.GetComponent<Animator>();
                if (anim != null)
                {
                    // Usa el nombre EXACTO de tu parámetro en el Animator
                    anim.SetTrigger("die");
                }
            }

            // Mostrar Game Over después de un pequeño retraso
            Invoke(nameof(GameOver), 1.5f);
        }
    }

    public void GameOver()
    {
        // Buscar el controlador del panel en la escena actual
        PlayerDeathPanel panelController = FindFirstObjectByType<PlayerDeathPanel>();

        if (panelController != null)
        {
            panelController.OnPlayerDeath();
        }
        else
        {
            Debug.LogWarning("No se encontró PlayerDeathPanel en la escena.");
        }

        Time.timeScale = 0f; // Pausar juego
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
