using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour // Script para las animaciones de Death.
{
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }
    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10; //Para asegurar que la animaci�n de Death aparezca por delante de cualquier objeto en la escena.

        if (deadSprite != null)
        {
            spriteRenderer.sprite = deadSprite;
        }
    }
    private void DisablePhysics() // Para los objetos puedan caer y no colision con ning�n otro objeto de la escena.
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        GetComponent<Rigidbody2D>().isKinematic = true;

        //Para desactivar los scripts de movimiento y no nos de una referencia nula ya que este script lo tendr� el jugador y los enemigos por igual.
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        if (enemyMovement != null)
        {
            enemyMovement.enabled = false;
        }
    }
    private IEnumerator Animate() // Funci�n para la animaci�n de Death.
    {
        float elapsed = 0f; //Cuanto tiempo ha transcurrido.
        float duration = 3f; //Cual es la duraci�n de la animaci�n.

        float jumpVelocity = 10f; //Primero hacemos que el objeto vaya hacia arriba (salte).
        float gravity = -36f; // Para la ca�da del objeto.

        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
