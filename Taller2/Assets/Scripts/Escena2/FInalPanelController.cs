using UnityEngine;
using System.IO;

public class GameOverPanelController : MonoBehaviour
{
    [Header("Prefab del panel de Game Over")]
    public GameObject panelPrefab; // Prefab que se instanciará sobre el Canvas
    private GameObject panelInstance;

    void Start()
    {
        // Si usas el panel directamente en la escena
        if (panelPrefab == null)
            panelPrefab = gameObject;

        // Asegurarse de que inicie oculto
        panelPrefab.SetActive(false);
    }

    /// <summary>
    /// Muestra el panel de Game Over y pausa el juego
    /// </summary>
    public void ShowPanel()
    {
        // Instanciar el panel si no existe
        if (panelInstance == null)
        {
            Canvas canvas = FindFirstObjectByType<Canvas>(); // Buscar el Canvas de la escena
            if (canvas != null)
            {
                panelInstance = Instantiate(panelPrefab, canvas.transform);
            }
            else
            {
                Debug.LogWarning("No se encontró Canvas en la escena para mostrar el Game Over Panel.");
                return;
            }
        }

        // Activar el panel
        panelInstance.SetActive(true);

        // Pausar el juego
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Oculta el panel y reanuda el juego
    /// </summary>
    public void HidePanel()
    {
        if (panelInstance != null)
        {
            panelInstance.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void SaveJsonButton()
    {
        if (GameManager.Instance == null) return;

        GameOverData data = new GameOverData
        {
            lives = GameManager.Instance.currentHealth,
            totalTime = Timer.FormatTime(GameManager.Instance.GlobalTime),
            coin1 = GameManager.Instance.scoreCoin,
            coin2 = GameManager.Instance.scoreCoin2,
            coin3 = GameManager.Instance.scoreCoin3,
            totalCoins = GameManager.Instance.scoreCoin + GameManager.Instance.scoreCoin2 + GameManager.Instance.scoreCoin3
        };

        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.streamingAssetsPath, "gameover.json");


        File.WriteAllText(path, json);
        Debug.Log("❌ Datos de Game Over guardados en: " + path);
    }
}
[System.Serializable]
public class GameOverData
{
    public int lives;
    public string totalTime;
    public int coin1, coin2, coin3, totalCoins;
}