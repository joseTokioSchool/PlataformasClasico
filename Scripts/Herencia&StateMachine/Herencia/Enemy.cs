using UnityEngine;

public class Enemy : MonoBehaviour // Clase padre con los valores predeterminados de los enemigos
{
    public string enemyName;
    public int lives;
    public float speed;

    public Enemy() //Constructor que da valores por defecto
    {
        enemyName = "";
        lives = 0;
        speed = 0;
    }
    public Enemy(string n, int l, float s) // Constructor que asigna valores
    {
        enemyName = n;
        lives = l;
        speed = s;
    }
    public void Move() // Método para mover al enemigo
    {
        // TODO: Implementar el movimiento de los enemigos y su animacion | Script: EnemyMovement, AnimatedSprites
        Debug.Log("Enemigo en movimiento");
    }
    public void Death() // Método para llamar a la animación de muerte de los enemigos
    {
        // TODO: Implementar el script de animacion de muerte de los enemigos | Script: DeathAnimation
        Debug.Log("Enemigo derrotado");
    }
}
