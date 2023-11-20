using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision) // Función para detectar si Goomba esta siendo aplastado o si Goomba esta golpeando a Mario.
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerState player = collision.gameObject.GetComponent<PlayerState>();

            if (player.starpower) // Para que Goomba sea golpeado en caso de estar en modo Starman.
            {
                Hit();
            }
            else if (collision.transform.DotTest(transform, Vector2.down)) //Si Goomba recibe el golpe entonces se aplasta.
            {
                Flatten();
            }
            else // Si Goomba no recibe el golpe significa que es el jugador el que lo recibe.
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Función para que Goomba pueda ser eliminado por los caparazones de Koopa.
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }
    private void Flatten() // Función para la animación de Goomba siendo aplastado y eliminarlo de la escena.
    {
        AudioManager.AudioInstance.KickClip();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<AnimatedSprites>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }

    private void Hit() // Función para el golpe de Koopa a Goomba y este sea eliminado.
    {
        AudioManager.AudioInstance.KickClip();
        GetComponent<AnimatedSprites>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}
