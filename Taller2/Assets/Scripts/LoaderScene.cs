using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScene : MonoBehaviour
{
    
    public string sceneName = "Escena2(Bosque)";


    public float delayBeforeLoad = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisionó tiene el tag "Player"
        if (collision.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (delayBeforeLoad > 0f)
        {
            Invoke(nameof(LoadSceneDelayed), delayBeforeLoad);
        }
        else
        {
            LoadSceneDelayed();
        }
    }

    private void LoadSceneDelayed()
    {
        // Verifica que el nombre de la escena no esté vacío
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("El nombre de la escena está vacío. Por favor asigna un nombre en el Inspector.");
        }
    }
}