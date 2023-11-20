using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour // Script para el cambio de sprites de los estados Idle, Jump, Slide y Run.
{
    public SpriteRenderer spriteRenderer { get; private set; }
    private PlayerMovement movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprites run; // Referenciar en el inspector del prefab.

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false; //Para que la animación de correr no se ejecute cuando mario es eliminado.
    }
    private void LateUpdate()
    {
        run.enabled = movement.Running;
        if (movement.Jumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (movement.Sliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if (!movement.Running)
        {
            spriteRenderer.sprite = idle;
        }
    }
}
