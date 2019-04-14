using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour{

    public int score;
    public Text scoreText;
    public Text bestText;

    public static int col = 20;
    public static int row = 10;

    public Transform[,] grid = new Transform[row, col];

    private void Start() {
        score = 0;
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text = score.ToString();

        if (PlayerPrefs.HasKey("best")) {
            bestText.text = PlayerPrefs.GetInt("best").ToString();
        }
        else {
            bestText.text = "0";
        }
    }

    public Vector2 RoundVector(Vector2 vector) {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }

    public bool isInsideBorder(Vector2 position) {
        return ((int)position.x >= 0 && (int)position.x < row && (int)position.y >= 0);
    }

    public void SaveBestScore() {
        if(score > PlayerPrefs.GetInt("best")) {
            PlayerPrefs.SetInt("best", score);
        }
    }

    public void DeleteRow(int y) {
        for(int x = 0; x < row; ++x) {
            GameObject.Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
        score += 10;
        scoreText.text = score.ToString();
    }   

    public void DecreaseRow(int y) {
        for (int x = 0; x < row; ++x) {
            if (grid[x, y] != null) {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void DecreaseRowAbove(int y) {
        for (int i = y; i < col; ++i) {
            DecreaseRow(i);
        }
    }

    public bool isRowFull(int y){
        for (int x = 0; x < row; ++x) {
            if (grid[x, y] == null) {
                return false;
            }
        }
        return true;
    }

    public void DeleteWholeRows() {
        for(int y = 0; y < col; ++y) {
            if (isRowFull(y)) {
                DeleteRow(y);
                DecreaseRowAbove(y + 1);
                --y;
            }
        }
    }
}
