using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*--------------------------------------------------- SINGLETONS --------------------------------------------------- */
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    /*------------------------------------------------------------------------------------------------------------------ */

    [SerializeField] GameObject panelGameOver;

    [SerializeField] TMP_Text livesTXT;
    [SerializeField] TMP_Text coinsTXT;

    public int lives;
    public int coins;

    private void Start()
    {
        lives = PlayerPrefs.GetInt("Lives");
        coins = PlayerPrefs.GetInt("Coins");
        livesTXT.text = "Lives: " + PlayerPrefs.GetInt("Lives").ToString();
        coinsTXT.text = "Coins: " + PlayerPrefs.GetInt("Coins").ToString();
        panelGameOver.SetActive(false);
    }
    private void Update()
    {
        livesTXT.text = "Lives: " + lives.ToString();
        coinsTXT.text = "Coins: " + coins.ToString();
    }
    public void NewGame() // Función para crear una nueva partida.
    {
        PlayerPrefs.SetInt("Lives", 3);
        PlayerPrefs.SetInt("Coins", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadLevel() // Función para cargar la siguiente pantalla.
    {
        if (PlayerPrefs.GetInt("Levels") <= 1)
        {
            SceneManager.LoadScene(1);
        }
        else if (PlayerPrefs.GetInt("Levels") == 2)
        {
            SceneManager.LoadScene(2);
        }
        else if (PlayerPrefs.GetInt("Levels") == 3)
        {
            SceneManager.LoadScene(3);
        }
        else if (PlayerPrefs.GetInt("Levels") >= 4)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ResetLevel(float delay) // Función para tener delay al resetear la partida.
    {
        Invoke(nameof(ResetLevel), delay);
    }
    public void ResetLevel() // Función para resetear la partida.
    {
        lives--;
        PlayerPrefs.SetInt("Lives", lives);
        if (PlayerPrefs.GetInt("Lives") > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            GameOver();
        }
    }
    public void GameOver() // Función para el Game Over del juego.
    {
        AudioManager.AudioInstance.GameOverClip();
        panelGameOver.SetActive(true);
    }

    public void AddCoin() // Función para añadir monedas.
    {
        coins++;
        PlayerPrefs.SetInt("Coins", coins);
        if (PlayerPrefs.GetInt("Coins") == 100)
        {
            AddLife();
            coins = 0;
            PlayerPrefs.SetInt("Coins", 0);
        }
        AudioManager.AudioInstance.CoinClip();
    }
    public void AddLife() // Función para añadir vidas.
    {
        PlayerPrefs.SetInt("Lives", lives++);
    }
}
