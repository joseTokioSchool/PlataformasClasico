using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuMario : MonoBehaviour
{
    [SerializeField] GameObject level2, level3;

    int n = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(PlayerPrefs.GetInt("Levels"));
            Debug.Log(PlayerPrefs.GetInt("Lives"));
            Debug.Log(PlayerPrefs.GetInt("Coins"));
            PlayerPrefs.SetInt("Levels", n++);
        }
        LevelManager();
    }
    private void LevelManager()
    {
        if (PlayerPrefs.GetInt("Levels") == 1)
        {
            level2.GetComponent<Button>().enabled = false;
            level3.GetComponent<Button>().enabled = false;
        }
        else if (PlayerPrefs.GetInt("Levels") == 2)
        {
            level2.GetComponent<Button>().enabled = true;
            level3.GetComponent<Button>().enabled = false;
        }
        else if (PlayerPrefs.GetInt("Levels") >= 3)
        {
            level2.GetComponent<Button>().enabled = true;
            level3.GetComponent<Button>().enabled = true;
        }
    }
    public void ChangeScene(int n)
    {
        SceneManager.LoadScene(n);
    }
    public void ExitGame(int n)
    {
        Application.Quit(n);
    }
    public void ResetValues() // Funcion para reiniciar el juego.
    {
        PlayerPrefs.SetInt("Lives", 3);
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("Levels", 1);
    }
    public void CheckStats()
    {
        Debug.Log(" | Lives: " + PlayerPrefs.GetInt("Lives") + " | Coins: " + PlayerPrefs.GetInt("Coins"));
    }
}
