using UnityEngine;

public class PowerUp : MonoBehaviour // Script para el comportamiento de los PowerUp.
{
    public enum Type { Coin, ExtraLife, MagicMushroom, Startpower }

    public Type type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect(collision.gameObject); //collision.gameObject --> Mario.
        }
    }

    private void Collect(GameObject player) // Referencia al jugador para los power up de magic mushroom y startpower.
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();
                break;
            case Type.ExtraLife:
                AudioManager.AudioInstance.ExtraLifeClip();
                GameManager.Instance.AddLife();
                break;
            case Type.MagicMushroom:
                AudioManager.AudioInstance.PowerUpClip();
                player.GetComponent<PlayerState>().Grow();
                break;
            case Type.Startpower:
                player.GetComponent<PlayerState>().Starpower();
                break;
        }

        Destroy(gameObject);
    }
}
