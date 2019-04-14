using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject tetrisObject;
    public Text levelText;
    public int level;

    private void Start() {
        Resume();
        SetLevel();
        gameOverPanel.SetActive(false);
    }

    void SetLevel() {
        level = PlayerPrefs.GetInt("level");
        levelText.text = level.ToString();
        Time.timeScale = level;
    }

    public void Pause() {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void Resume() {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit() {
        SceneManager.LoadScene(0);
    }

    public void GameOver() {
        gameOverPanel.SetActive(true);
        FindObjectOfType<Spawner>().enabled = false;
        Time.timeScale = 0;
    }

    public void Left() {
        tetrisObject.GetComponent<Object>().MoveLeft();
    }

    public void Right() {
        tetrisObject.GetComponent<Object>().MoveRight();
    }

    public void Down() {
        tetrisObject.GetComponent<Object>().MoveDown();
    }

    public void Rotate() {
        tetrisObject.GetComponent<Object>().Rotate();
    }
}
