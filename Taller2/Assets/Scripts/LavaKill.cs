using UnityEngine;

public class LavaKill : MonoBehaviour
{
    [Header("Sonidos")]
    public AudioClip sonidoAmbienteLava;  // Sonido constante de la lava (loop)
    public AudioClip sonidoContacto;      // Sonido cuando el player toca la lava
    public AudioClip sonidoPisoLava;      // Sonido cuando cae en piso de lava (opcional)

    [Header("Configuración de Audio")]
    [Range(0f, 1f)]
    public float volumenAmbiente = 0.3f;  // Volumen del sonido ambiente
    [Range(0f, 1f)]
    public float volumenContacto = 1f;     // Volumen del sonido de contacto

    private AudioSource audioSourceAmbiente;  // Para el sonido constante
    private AudioSource audioSourceContacto;  // Para el sonido de contacto

    void Start()
    {
        // Crear AudioSource para sonido ambiente (loop)
        audioSourceAmbiente = gameObject.AddComponent<AudioSource>();
        audioSourceAmbiente.clip = sonidoAmbienteLava;
        audioSourceAmbiente.loop = true;           // Se repite constantemente
        audioSourceAmbiente.playOnAwake = true;    // Empieza automáticamente
        audioSourceAmbiente.volume = volumenAmbiente;

        // Si hay sonido ambiente, reproducirlo
        if (sonidoAmbienteLava != null)
        {
            audioSourceAmbiente.Play();
        }

        // Crear AudioSource para sonido de contacto (one-shot)
        audioSourceContacto = gameObject.AddComponent<AudioSource>();
        audioSourceContacto.playOnAwake = false;
        audioSourceContacto.loop = false;
        audioSourceContacto.volume = volumenContacto;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // Lava normal → quita 1 vida
        if (this.CompareTag("Lava"))
        {
            Debug.Log("Player tocó Lava normal → pierde 1 vida");

            // Reproducir sonido de contacto
            if (sonidoContacto != null && audioSourceContacto != null)
            {
                audioSourceContacto.PlayOneShot(sonidoContacto, volumenContacto);
            }

            GameManager.Instance.QuitarVida(1);
        }

        // PisoLava → pierde toda la vida
        if (this.CompareTag("PisoLava"))
        {
            Debug.Log("Player cayó en PisoLava → pierde todas las vidas");

            // Reproducir sonido de contacto (o uno específico)
            AudioClip sonidoAReproducir = sonidoPisoLava != null ? sonidoPisoLava : sonidoContacto;

            if (sonidoAReproducir != null && audioSourceContacto != null)
            {
                audioSourceContacto.PlayOneShot(sonidoAReproducir, volumenContacto);
            }

            GameManager.Instance.QuitarVida(GameManager.Instance.currentHealth);
        }
    }
}