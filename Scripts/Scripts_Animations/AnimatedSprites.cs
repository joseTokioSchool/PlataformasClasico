using UnityEngine;

public class AnimatedSprites : MonoBehaviour // Script para las animaciones y poder reutilizarlo.
{
    public Sprite[] sprites;
    public float framerate = 1f / 6f; // Para recorrer 6 fotogramas por segundo.

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;
        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        if (frame >= 0 && frame < sprites.Length) // Para asegurar que no se salga de los l�mites de la matriz y de un error.
        {
            spriteRenderer.sprite = sprites[frame];
        }
    }
}
