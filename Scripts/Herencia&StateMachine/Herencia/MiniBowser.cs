using UnityEngine;

public class MiniBowser : Enemy
{
    public float shellLife;
    public float shellSpeed;
    public bool isShell;

    public MiniBowser() //Constructor que da valores por defecto
    {
        enemyName = "";
        lives = 0;
        speed = 0;
        shellLife = 0;
        shellSpeed = 0;
        isShell = false;
    }
    public MiniBowser(string n, int l, float s, float sL, float sS, bool iS) // Constructor que asigna valores
    {
        enemyName = n;
        lives = l;
        speed = s;
        shellLife = sL;
        shellSpeed = sS;
        isShell = iS;
    }

    public void Sheller()
    {
        // TODO: Implementar los metodos de Koopa para copiar el mismo comportamiento | Script: Koopa
        Debug.Log("MiniBowser está dentro del caparazón");
    }
}
