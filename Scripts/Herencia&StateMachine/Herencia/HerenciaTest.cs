using UnityEngine;

public class HerenciaTest : MonoBehaviour
{
    void Start()
    {
        Bowser b1 = new Bowser("Bowser", 2, 2f, 5f, false);
        b1.Move(); // Objeto Bowser b1 está usando un método de Enemy
        b1.CanShoot(); // Ahora b1 esta usando uno de su propia clase
        Debug.Log(b1.lives); // b1 accedde a su propiedad heredada

        MiniBowser mb1 = new MiniBowser("Bowser", 2, 2f, 5f, 2.5f, false);
        mb1.Move(); // Objeto MiniBowser mb1 está usando un método de Enemy
        mb1.Sheller(); // Ahora mb1 esta usando uno de su propia clase
        Debug.Log(b1.lives); // mb1 accedde a su propiedad heredada
    }
}
