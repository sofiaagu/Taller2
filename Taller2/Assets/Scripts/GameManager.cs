using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
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

    [Header("Panel de Victoria")]
    public GameObject victoryPanel;


    [Header("UI de Vida")]
    public GameObject heartPrefab;       // Prefab del corazón
    public Transform heartsContainer;    // Contenedor de corazones en el Canvas

    private List<GameObject> hearts = new List<GameObject>();

    [Header("Puntaje")]
    public int scoreCoin = 0;
    public int scoreCoin2 = 0;
    public int scoreCoin3 = 0;

    [Header("UI Victory Panel")]
    public TMPro.TMP_Text livesText;
    public TMPro.TMP_Text timeText;
    public TMPro.TMP_Text itemsText;
    public TMPro.TMP_Text totalItemsText;
    public TMPro.TMP_Text scoreText;

    [Header("Items")]
    public int itemsCollected = 0;
    public int totalItems = 10;
    public int TotalScore { get; set; }

    // ------------------------
    // 🎵 AUDIO
    // ------------------------
    [Header("Audio")]
    public AudioSource audioSource;   // AudioSource que reproducirá la música
    public AudioClip menuMusic;       // Música del menú
    public AudioClip scene1Music;     // Música de la escena 1
    public AudioClip scene2Music;     // Música de la escena 2
    public float fadeDuration = 1.5f; // Duración del fade in/out en segundos

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

        // 🎵 Configuración inicial de Audio
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.playOnAwake = false;

        // 🎵 Música inicial con fade
        StartCoroutine(PlayMusicWithFade(menuMusic));

        victoryPanel = GameObject.Find("VictoryPanel");
        if (victoryPanel != null)
            victoryPanel.SetActive(false); // asegurarse de que empiece oculto
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

        victoryPanel = GameObject.Find("VictoryPanel");
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false); // siempre inicia oculto
        }

        // 🎵 Cambiar música según escena con fade
        switch (scene.buildIndex)
        {
            case 0: // Menú
                StartCoroutine(PlayMusicWithFade(menuMusic));
                break;

            case 1: // Escena 1
                StartCoroutine(PlayMusicWithFade(scene1Music));
                break;

            case 2: // Escena 2
                StartCoroutine(PlayMusicWithFade(scene2Music));
                break;
        }
    }

    // 🎵 Corrutina para cambiar música suavemente
    private IEnumerator PlayMusicWithFade(AudioClip newClip)
    {
        if (newClip == null) yield break;

        // Si ya está sonando esa música, no hacer nada
        if (audioSource.clip == newClip) yield break;

        // Fade out
        float startVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = 0f;

        // Cambiar música
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, startVolume, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = startVolume;
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
                    anim.SetTrigger("die");
                }
            }

            // Mostrar Game Over después de un pequeño retraso
            Invoke(nameof(GameOver), 1.5f);
        }
    }

    public void GameOver()
    {
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
        foreach (var h in hearts)
        {
            if (h != null)
                Destroy(h);
        }
        hearts.Clear();

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsContainer);
            hearts.Add(newHeart);
        }

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

  

    public void Victory()
    {
        if (victoryPanel != null)

        {
            Timer timer = FindFirstObjectByType<Timer>();
            if (timer != null)
            {
                timer.TimerStop();
                float sceneTime = timer.stopTime;
                TotalTime(sceneTime); // acumular al global
            }
            victoryPanel.SetActive(true);

            // 🔹 Buscar referencias dentro del VictoryPanel
            livesText = victoryPanel.transform.Find("LivesText")?.GetComponent<TMPro.TMP_Text>();
            timeText = victoryPanel.transform.Find("TimeText")?.GetComponent<TMPro.TMP_Text>();
            var coin1Text = victoryPanel.transform.Find("Coin1Text")?.GetComponent<TMPro.TMP_Text>();
            var coin2Text = victoryPanel.transform.Find("Coin2Text")?.GetComponent<TMPro.TMP_Text>();
            var coin3Text = victoryPanel.transform.Find("Coin3Text")?.GetComponent<TMPro.TMP_Text>();
            var totalText = victoryPanel.transform.Find("TotalText")?.GetComponent<TMPro.TMP_Text>();

            // 🔹 Actualizar textos
            if (livesText != null)
                livesText.text = currentHealth.ToString();

            if (timeText != null)
                timeText.text = Timer.FormatTime(globalTime);

            if (coin1Text != null)
                coin1Text.text = scoreCoin.ToString();

            if (coin2Text != null)
                coin2Text.text = scoreCoin2.ToString();

            if (coin3Text != null)
                coin3Text.text = scoreCoin3.ToString();

            if (totalText != null)
                totalText.text = (scoreCoin + scoreCoin2 + scoreCoin3).ToString();

            
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogWarning("No se encontró el Victory Panel en esta escena.");
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

    public float GlobalTime { get => globalTime; set => globalTime = value; }
    public int ScoreCoin { get => scoreCoin; set => scoreCoin = value; }
    public int ScoreCoin2 { get => scoreCoin2; set => scoreCoin2 = value; }
    public int ScoreCoin3 { get => scoreCoin3; set => scoreCoin3 = value; }
}

