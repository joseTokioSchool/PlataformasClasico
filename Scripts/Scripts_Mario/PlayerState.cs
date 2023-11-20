using System.Collections;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider; // Referencia al collider de Mario para poder cambiarlo cuando este est� en la version Big.

    public bool Big => bigRenderer.enabled;
    public bool Small => smallRenderer.enabled;
    public bool Dead => deathAnimation.enabled;
    public bool starpower { get; private set; }
    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer;
    }
    public void Hit()
    {
        if (!Dead && !starpower)
        {
            if (Big)
            {
                Shrink();
            }
            else
            {
                Death();
            }
        }

    }
    private void Death() // Funci�n para la muerte de Mario.
    {
        smallRenderer.enabled = false; //Para que la animaci�n no se actualice.
        bigRenderer.enabled = false; //Para que la animaci�n no se actualice.
        deathAnimation.enabled = true;

        AudioManager.AudioInstance.DieClip();
        GameManager.Instance.ResetLevel(2f);
    }
    public void Grow() // Funci�n para hacer grande a Mario.
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }
    private void Shrink() //Funci�n para encoger a Mario.
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        AudioManager.AudioInstance.ShrinkClip();
        StartCoroutine(ScaleAnimation());
    }
    private IEnumerator ScaleAnimation() // Corrutina para la animaci�n de Grow y Shrink.
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 12 == 0) // Para cambiar cada 12 frames los sprites de Mario.
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }

            yield return null;
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void Starpower(float duration = 10f) // duration --> Para poder ajustar el tiempo de duraci�n en el Script PowerUp.
    {
        StartCoroutine(StarpowerAnimation(duration));
    }
    private IEnumerator StarpowerAnimation(float duration) // Corrutina para la animaci�n de Starpower.
    {
        starpower = true;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (Time.frameCount % 4 == 0)
            {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f); // Para cambiar el color del sprite y crear el efecto de Starpower.
            }
            yield return null;
        }

        activeRenderer.spriteRenderer.color = Color.white; // Para volver al color inicial.
        starpower = false;
    }
}
