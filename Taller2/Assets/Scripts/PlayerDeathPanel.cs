using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Agregar esta línea

public class PlayerDeathPanel : MonoBehaviour
{
    public GameObject deathPanel; // Asigna el panel desde el inspector
    public Button botonVolver;    // Asigna el botón Volver
    public Button botonReiniciar; // Asigna el botón Reiniciar
    private bool isDead = false;

    void Start()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(false); // Oculta el panel al iniciar
        }

        // Configurar botones manualmente para asegurar que funcionen con Time.timeScale = 0
        if (botonVolver != null)
        {
            botonVolver.onClick.RemoveAllListeners();
            botonVolver.onClick.AddListener(Volver);
        }

        if (botonReiniciar != null)
        {
            botonReiniciar.onClick.RemoveAllListeners();
            botonReiniciar.onClick.AddListener(Reiniciar);
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

            Debug.Log("Panel de muerte activado en escena: " + SceneManager.GetActiveScene().name);
            Debug.Log("Botones configurados: Volver=" + (botonVolver != null) + ", Reiniciar=" + (botonReiniciar != null));
        }
    }

    // 🔹 Botón: volver al menú
    public void Volver()
    {
        Debug.Log("🔵 Botón VOLVER presionado");
        Time.timeScale = 1f; // Reanuda el tiempo

        // Resetear valores del GameManager antes de ir al menú
        ResetearGameManager();

        // Volver al menú (siempre es la escena 0)
        SceneManager.LoadScene(0);
    }

    // 🔹 Botón: reiniciar nivel actual
    public void Reiniciar()
    {
        Debug.Log("🔵 Botón REINICIAR presionado");
        Time.timeScale = 1f; // Reanuda el tiempo
        isDead = false; // Resetear estado de muerte

        // Resetear solo los valores de la escena actual
        ResetearEscenaActual();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 🔹 Resetear todos los valores del GameManager (para volver al menú)
    private void ResetearGameManager()
    {
        if (GameManager.Instance != null)
        {
            // Resetear vida a máximo
            GameManager.Instance.currentHealth = GameManager.Instance.maxHealth;

            // Resetear puntajes de todas las escenas
            GameManager.Instance.ScoreCoin = 0;
            GameManager.Instance.ScoreCoin2 = 0;
            GameManager.Instance.ScoreCoin3 = 0;
            GameManager.Instance.TotalScore = 0;

            // Resetear tiempo global
            GameManager.Instance.GlobalTime = 0f;

            Debug.Log("GameManager reseteado completamente");
        }
    }

    // 🔹 Resetear solo los valores de la escena actual (para reiniciar nivel)
    private void ResetearEscenaActual()
    {
        if (GameManager.Instance != null)
        {
            // Resetear vida a máximo
            GameManager.Instance.currentHealth = GameManager.Instance.maxHealth;

            // Determinar qué escena es y resetear solo su puntaje
            int escenaActual = SceneManager.GetActiveScene().buildIndex;

            // Resetear el timer de la escena ANTES de restar del global
            Timer timer = FindFirstObjectByType<Timer>();
            float tiempoEscenaActual = 0f;

            if (timer != null)
            {
                tiempoEscenaActual = timer.stopTime; // Guardar cuánto tiempo llevabas en esta escena
                timer.ResetTimer(); // Resetear el timer local
            }

            // Guardar los puntajes de escenas previas antes de resetear
            int puntajeEscena1Previo = GameManager.Instance.ScoreCoin + GameManager.Instance.ScoreCoin2 + GameManager.Instance.ScoreCoin3;

            // Aquí debes ajustar según el buildIndex de tus escenas
            // Por ejemplo: Escena 1 = buildIndex 1, Escena 2 = buildIndex 2, etc.
            switch (escenaActual)
            {
                case 1: // Primera escena de juego
                    // Resetear LOS 3 TIPOS de monedas de escena 1
                    GameManager.Instance.ScoreCoin = 0;
                    GameManager.Instance.ScoreCoin2 = 0;
                    GameManager.Instance.ScoreCoin3 = 0;
                    // En escena 1, resetear tiempo global completamente
                    GameManager.Instance.GlobalTime = 0f;
                    // Resetear puntaje total
                    GameManager.Instance.TotalScore = 0;
                    Debug.Log("Escena 1 reseteada: Todas las monedas a 0");
                    break;

                case 2: // Segunda escena de juego
                    // Restar el tiempo que llevabas en esta escena
                    GameManager.Instance.GlobalTime -= tiempoEscenaActual;
                    if (GameManager.Instance.GlobalTime < 0)
                        GameManager.Instance.GlobalTime = 0;

                    // Calcular cuántos puntos tenías en escena 2 para restarlos del total
                    int puntosEscena2 = GameManager.Instance.TotalScore - puntajeEscena1Previo;
                    GameManager.Instance.TotalScore -= puntosEscena2;

                    // Asegurar que TotalScore no sea negativo
                    if (GameManager.Instance.TotalScore < 0)
                        GameManager.Instance.TotalScore = 0;

                    Debug.Log($"Escena 2 reseteada. Puntaje de escena 1 mantenido: {GameManager.Instance.TotalScore}");
                    Debug.Log($"Monedas escena 1: Coin={GameManager.Instance.ScoreCoin}, Coin2={GameManager.Instance.ScoreCoin2}, Coin3={GameManager.Instance.ScoreCoin3}");
                    break;

                case 3: // Tercera escena de juego
                    // Restar el tiempo que llevabas en esta escena
                    GameManager.Instance.GlobalTime -= tiempoEscenaActual;
                    if (GameManager.Instance.GlobalTime < 0)
                        GameManager.Instance.GlobalTime = 0;

                    // Calcular cuántos puntos tenías en escena 3 para restarlos del total
                    int puntosEscena3 = GameManager.Instance.TotalScore - puntajeEscena1Previo;
                    GameManager.Instance.TotalScore -= puntosEscena3;

                    // Asegurar que TotalScore no sea negativo
                    if (GameManager.Instance.TotalScore < 0)
                        GameManager.Instance.TotalScore = 0;

                    Debug.Log($"Escena 3 reseteada. Puntaje de escenas 1 y 2 mantenido: {GameManager.Instance.TotalScore}");
                    break;
            }

            Debug.Log($"Escena {escenaActual} reseteada. Tiempo global: {GameManager.Instance.GlobalTime}");
        }
    }
}