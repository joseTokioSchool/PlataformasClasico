using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public Sprite emptyBlock;
    public int maxHits = -1; //Es menos uno ya que en algunos casos queremos golpear el bloque infinitamente.

    private bool animating;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player")) // !animating / maxHits != 0 --> para no permitir un segundo golpeo mientras se está realizando la animación o si se ha alcanzado el nº max de golpeos.
        {
            if (collision.transform.DotTest(transform, Vector2.up)) // collision.transform es Mario y el trasform pasado por párametro en la función DotTest() es el del bloque.
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true; // Para mostrar los bloques ocultos una vez golpeados.

        maxHits--;

        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        if (item != null) //Para crear un objeto en el bloque cuando este se golpea.
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate());
    }
    private IEnumerator Animate() // Corrutina para la animación del bloque.
    {
        animating = true;

        Vector3 restingPosition = transform.localPosition; // localPosition --> para que solo se mueva el bloque en caso de que este unido a otro objeto.
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f; // Para que el bloque suba.

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        animating = false;
    }
    private IEnumerator Move(Vector3 from, Vector3 to) // Corrutina para la animación del bloque.
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to; // Para asegurar que el objeto está en la posición final correcta.
    }
}
