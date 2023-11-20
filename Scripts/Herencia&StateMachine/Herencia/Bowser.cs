using UnityEngine;

public class Bowser : Enemy
{
    public float jump;
    public bool canShoot;

    public Bowser() //Constructor que da valores por defecto
    {
        enemyName = "";
        lives = 0;
        speed = 0;
        jump = 0f;
        canShoot = false;
    }
    public Bowser(string n, int l, float s, float j, bool cS) // Constructor que asigna valores
    {
        enemyName = n;
        lives = l;
        speed = s;
        jump = j;
        canShoot = cS;
    }

    public void CanShoot() // Método para disparar del enemigo
    {
        // TODO: Implementar los scripts correspondientes al estado de Atacar | Script: NCPAttackState
        Debug.Log("Bowser está disparando");
    }
}
