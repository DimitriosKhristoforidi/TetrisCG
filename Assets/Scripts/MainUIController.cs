using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour {

    public Text bestScore;
    public Text level;
    public int levelIndex;

    private void Start() {
        ShowBestScore();
        ShowLevelIndex();
        Time.timeScale = 1;
    }

    private void Update() {
        level.text = levelIndex.ToString();
    }

    public void Play() {
        SceneManager.LoadScene(1);
    }

    void ShowBestScore() {
        if (PlayerPrefs.HasKey("best")) {
            bestScore.text = PlayerPrefs.GetInt("best").ToString();
        }
        else {
            bestScore.text = "0";
        }
    }

    public void ShowLevelIndex() {
        if (PlayerPrefs.HasKey("best")) {
            levelIndex = PlayerPrefs.GetInt("level");
        }
        else {
            levelIndex = 1;
        }
    }

    public void levelUp() {
        levelIndex += 1;
        if (levelIndex > 5) {
            levelIndex = 5;
        }
        SetLevel();
    }

    public void levelDown() {
        levelIndex -= 1;
        if (levelIndex < 1) {
            levelIndex = 1;
        }
        SetLevel();
    }

    public void SetLevel() {
        PlayerPrefs.SetInt("level", levelIndex);
    }
}
