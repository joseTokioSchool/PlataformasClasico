using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour // Script para la animación de la moneda y acatualizar el marcador de monedas.
{

    void Start()
    {
        GameManager.Instance.AddCoin();

        StartCoroutine(Animate());
    }
    private IEnumerator Animate() // Corrutina para la animación de la moneda.
    {
        Vector3 restingPosition = transform.localPosition; // localPosition --> para que solo se mueva el bloque en caso de que este unido a otro objeto.
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f; // Para que el bloque suba.

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        Destroy(gameObject);
    }
    private IEnumerator Move(Vector3 from, Vector3 to) // Corrutina para la animación de la moneda.
    {
        float elapsed = 0f;
        float duration = 0.25f;

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
