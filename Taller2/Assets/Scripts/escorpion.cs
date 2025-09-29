using UnityEngine;

public class ScorpionAI : MonoBehaviour
{
    private Vector3 startPos;
    private bool movingRight = true;

    [Header("Movimiento")]
    public float speed = 1f;          // Velocidad del escorpión
    public float moveDistance = 0.5f; // Distancia máxima desde el punto inicial

    void Start()
    {
        startPos = transform.position; // Guardar posición inicial
    }

    void Update()
    {
        // Mover en X
        float step = speed * Time.deltaTime * (movingRight ? 1 : -1);
        transform.Translate(step, 0, 0);

        // Si se pasa de la distancia, voltear
        if (movingRight && transform.position.x >= startPos.x + moveDistance)
        {
            Flip();
        }
        else if (!movingRight && transform.position.x <= startPos.x - moveDistance)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // voltear sprite
        transform.localScale = scale;
    }
}
