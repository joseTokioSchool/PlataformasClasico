using UnityEngine;

public class PlayerMovement : MonoBehaviour // Script para el movimiento de Mario.
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    private Collider2D marioCollider;

    private Vector2 velocity;
    private float inputAxis;

    public float speed = 8f;
    public float maxJumpHeight = 185f;
    public float maxJumpTime = 185f;

    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float Gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    public bool Grounded { get; private set; }
    public bool Jumping { get; private set; }
    public bool Running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f; //Para el sprite de correr.
    public bool Sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f); // Para el sprite de derrape.

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        marioCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
    }
    //----------------------- Para la animacion de Mario al tocar la bandera ------------------
    private void OnEnable()
    {
        rb.isKinematic = false;
        marioCollider.enabled = true;
        velocity = Vector2.zero;
        Jumping = false;
    }
    private void OnDisable()
    {
        rb.isKinematic = true;
        marioCollider.enabled = false;
        velocity = Vector2.zero;
        Jumping = false;
    }
    //-----------------------------------------------------------------------------------------
    void Update()
    {
        HorizontalMove();

        Grounded = rb.Raycast(Vector2.down); // Raycast para el salto.

        if (Grounded)
        {
            GroundedMovement();
        }

        ApllyGravity();
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        position += velocity * Time.fixedDeltaTime;

        /*---------------- Para que Mario no pueda irse de la escena hacia la izq. ----------------*/
        Vector2 leftEdge = mainCamera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);
        /*-----------------------------------------------------------------------------------------*/

        rb.MovePosition(position);
    }

    /*------------------------------------------- FUNCIONES -------------------------------------------*/
    private void HorizontalMove()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * speed, speed * Time.deltaTime);

        if (rb.Raycast(Vector2.right * velocity.x)) // Para dejar de aplicar fuerza cuando Mario choca horizontalmente con un objeto y pueda moverse al momento en sentido contrario.
        {
            velocity.x = 0f;
        }

        if (velocity.x > 0f) // Para girar el sprite de mario en dirección donde avance (El sprite solo se gira cuando se acaba de aplicar la fuerza y no cuando se pulsa la tecla).
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f); // Para evitar la acumulación de velocidad en el salto.
        Jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            AudioManager.AudioInstance.JumpClip();
            velocity.y = jumpForce;
            Jumping = true;
        }
    }
    private void ApllyGravity() //Función para aplicar fuerza dependiendo de cuanto pulses el botón de salto y para aplicar fuerza a la caída.
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        velocity.y += Gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, Gravity / 2f); // Evitar que Mario caiga demasiado rápido --> "Velocidad terminal".
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) // Para que Mario rebote cuando elimina a un enemigo
        {
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f;
                Jumping = true;
            }
        }
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp")) // Para que cuando mario choca con la cabeza en un objeto distinto a un PowerUp deje de aplicarse fuerza en el eje "Y" y caiga al momento.
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }
}
