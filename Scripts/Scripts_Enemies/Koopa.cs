using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;

    private bool shelled;
    private bool pushed;

    private void OnCollisionEnter2D(Collision2D collision) // Funci�n para detectar si Koopa elimina a Mario o si Mario aplasta a Koopa y este entra en modo caparaz�n.
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            PlayerState player = collision.gameObject.GetComponent<PlayerState>();

            if (player.starpower)
            {
                Hit();
            }
            else if (collision.transform.DotTest(transform, Vector2.down)) //Si Koopa recibe el golpe entonces entra en Modo Caparaz�n.
            {
                EnterShell();
            }
            else // Si Koopa no recibe el golpe significa que es el jugador el que lo recibe.
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Funci�n para detectar si KoopaCaparazon esta siendo empujado y si lo est�, este puede elminiar tanto a Mario como a otros enemigos.
    {
        if (shelled && collision.CompareTag("Player"))
        {
            if (!pushed) // Si no estas siendo empujado, entonces empuja.
            {
                Vector2 direction = new Vector2(transform.position.x - collision.transform.position.x, 0f); // Para obtener la direcci�n entre dos objetos, restamos sus posiciones para obtener un vector que apunta de uno a otro.
                PushShell(direction); // Pasamos ese vector que obtenemos a la funcion EmpujarCaparazon().
            }
            else // Si estas siendo empujado y choca con el jugador, entonces golpea el jugador.
            {
                PlayerState player = collision.GetComponent<PlayerState>();

                if (player.starpower) // Para que Koopa sea golpeado en caso de estar en modo Starman.
                {
                    Hit();
                }
                else
                {
                    player.Hit();
                }
            }
        }
        else if (!shelled && collision.gameObject.layer == LayerMask.NameToLayer("Shell")) // Para que un Koopa pueda eliminar a otro Koopa.
        {
            Hit();
        }
    }
    private void EnterShell() // Funci�n para cambiar el sprite de Koopa al sprite del caparaz�n y desactivar su movimiento y su animaci�n.
    {
        AudioManager.AudioInstance.KickClip();
        shelled = true;

        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<AnimatedSprites>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }

    private void PushShell(Vector2 direction) // Funci�n para el comportamiento de empujar el caparaz�n
    {
        AudioManager.AudioInstance.KickClip();
        pushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false; // Volvemos a activar el rigidbody para que pueda moverse de nuevo.

        EnemyMovement movement = GetComponent<EnemyMovement>();
        movement.direction = direction.normalized; // Para establecer la direcci�n del caparaz�n en funci�n de la direcci�n que le pasamos por par�metro (normalizamos la variable para que no sea una magnitud, de lo contrario ser�a velocidad, no direcci�n).
        movement.speed = shellSpeed; // Para establecer la velocidad a la que se mueve el caparaz�n.
        movement.enabled = true; // Volvemos a activar el script de movimiento que previamente se desactiva en la funci�n EnterShell().

        gameObject.layer = LayerMask.NameToLayer("Shell"); // Para cambiar el layer de Koopa a Shell y pueda golpear a los enemigos tambi�n.
    }
    private void Hit() // Funci�n para el golpe de Koopa a Koopa y este sea eliminado.
    {
        AudioManager.AudioInstance.KickClip();
        GetComponent<AnimatedSprites>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}
