using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _Rigidbody2D;
    private Animator animator;

    public float horizontal;
    public float vertical;   // por ahora no se usa, puede servir luego para salto
    public float speed = 3f;
    public float jumpForce = 3f;

    private bool isJumping = false;

    void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento lateral
        horizontal = Input.GetAxisRaw("Horizontal");

        // Animación de correr (Idle ↔ Run)
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        if (horizontal > 0) transform.localScale = new Vector3(1, 1, 1);
        if (horizontal < 0) transform.localScale = new Vector3(-1, 1, 1);

        // Salto
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            _Rigidbody2D.linearVelocity = new Vector2(_Rigidbody2D.linearVelocity.x, jumpForce);
            animator.SetBool("isJumping", true);
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        _Rigidbody2D.linearVelocity = new Vector2(horizontal * speed, _Rigidbody2D.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }
}
