using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathPanel : MonoBehaviour
{
    public GameObject deathPanel; // Asigna el panel desde el inspector
    private bool isDead = false;

    void Start()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(false); // Oculta el panel al iniciar
        }
    }

    // 🔹 Llamar este método cuando la vida del jugador llegue a 0
    public void OnPlayerDeath()
    {
        if (!isDead && deathPanel != null)
        {
            isDead = true;
            deathPanel.SetActive(true);
            Time.timeScale = 0f; // Pausa el juego
        }
    }

    // 🔹 Botón: volver al menú
    public void Volver()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // 🔹 Botón: reiniciar nivel actual
    public void Reiniciar()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
