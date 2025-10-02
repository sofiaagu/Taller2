using UnityEngine;

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

    // Este m�todo puede ser llamado cuando la salud del jugador llega a 0
    public void OnPlayerDeath()
    {
        if (!isDead && deathPanel != null)
        {
            isDead = true;
            deathPanel.SetActive(true); // Muestra el panel
            // Aqu� puedes agregar m�s l�gica, como pausar el juego
        }
    }
}
