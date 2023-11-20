using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate() // Corrutina para la animación de los items.
    {
        AudioManager.AudioInstance.ItemClip();

        //Para cuando se hace la animación de salir del bloque ninguno de estos atributos interfiera.

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rb.isKinematic = true;
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.localPosition; // localPosition --> para que solo se mueva el item en caso de que este unido a otro objeto.
        Vector3 endPosition = transform.localPosition + Vector3.up; // Para que el item suba.

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = endPosition; // Para asegurar que el objeto está en la posición final correcta.

        rb.isKinematic = false;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;
    }
}
