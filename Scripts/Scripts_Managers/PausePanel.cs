using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] GameObject panelPause;
    private bool isPaused;
    void Start()
    {
        panelPause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.AudioInstance.PauseClip();

            ShowPausePanel();
            UpdateGameState();
        }
    }

    public void UpdateGameState()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void ShowPausePanel()
    {
        if (!isPaused)
        {
            panelPause.SetActive(true);
        }
        else
        {
            panelPause.SetActive(false);
        }
    }
}
