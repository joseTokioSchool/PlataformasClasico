using UnityEngine;

public class InitializePlayerPrefsMarioBros : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Levels") == false) // Valor 1 para nivel 1, Valor 2 para niveles 1 y 2, Valor 3 para niveles 1, 2 y 3.
        {
            PlayerPrefs.SetInt("Levels", 1);
        }
        if (PlayerPrefs.HasKey("Lives") == false)
        {
            PlayerPrefs.SetInt("Lives", 3);
        }
        if (PlayerPrefs.HasKey("Coins") == false)
        {
            PlayerPrefs.SetInt("Coins", 0);
        }
    }
}
