using UnityEngine;

public class EnemyMovement : MonoBehaviour // Script para el movimiento de los enemigos.
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;

    private Rigidbody2D rb;
    private Vector2 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enabled = false;
    }
    private void OnBecameVisible() // Función para que los enemigos no se muevan hasta que se ven en pantalla
    {
        enabled = true;
    }
    private void OnBecameInvisible() // Función para que los enemigos se dejen de mover si no están en pantalla
    {
        enabled = false;
    }
    private void OnEnable()
    {
        rb.WakeUp();
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        rb.Sleep();
    }
    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        if (rb.Raycast(direction)) //Para cambiar de dirección cuando choca.
        {
            direction = -direction;
        }
        if (rb.Raycast(Vector2.down)) // Para evitar que la velocidad en Y no se acumule infinitamente si estamos en el suelo.
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
        if (direction.x > 0f) // Para que los enemigos miren hacia donde están moviéndose.
        {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (direction.x < 0f)
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }
}
