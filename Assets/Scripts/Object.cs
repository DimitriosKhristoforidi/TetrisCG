using UnityEngine;

public class Object : MonoBehaviour {

    float lastFall = 0f;

    private void Update() {
        PCInput();
    }

    void PCInput() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveLeft();
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            MoveRight();
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Rotate();
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1) {
            MoveDown();
        }
    }

    public void MoveLeft() {
        transform.position += new Vector3(-1, 0, 0);
        if (isValidGridPosition()) {
            UpdateGrid();
        }
        else {
            transform.position += new Vector3(1, 0, 0);
        }
    }

    public void MoveRight() {
        transform.position += new Vector3(1, 0, 0);
        if (isValidGridPosition()) {
            UpdateGrid();
        }
        else {
            transform.position += new Vector3(-1, 0, 0);
        }
    }

    public void Rotate() {
        transform.Rotate(new Vector3(0, 0, -90));
        if (isValidGridPosition()) {
            UpdateGrid();
        }
        else {
            transform.Rotate(new Vector3(0, 0, 90));
        }
    }

    public void MoveDown() {
        transform.position += new Vector3(0, -1, 0);
        if (isValidGridPosition()) {
            UpdateGrid();
        }
        else {
            FindObjectOfType<Grid>().DeleteWholeRows();
            if (isAboveGrid(this)) {
                FindObjectOfType<UIController>().GameOver();
                FindObjectOfType<Grid>().SaveBestScore();
            }
            else {
                FindObjectOfType<Spawner>().SpawnRandomObject();
                enabled = false;
            }
        }
        lastFall = Time.time;
    }

    public bool isAboveGrid(Object tetrisObject) {
        for (int x = 0; x < Grid.row; ++x) {
            foreach (Transform childTransform in tetrisObject.transform) {
                Vector2 position = FindObjectOfType<Grid>().RoundVector(childTransform.position);
                if (position.y > Grid.col - 1) {
                    return true;
                }
            }
        }
        return false;
    }

    bool isValidGridPosition() {
        foreach (Transform clildTransform in transform) {
            Vector2 vector = FindObjectOfType<Grid>().RoundVector(clildTransform.position);
            if (!FindObjectOfType<Grid>().isInsideBorder(vector)) {
                return false;
            }
            if(FindObjectOfType<Grid>().grid[(int)vector.x, (int)vector.y] != null && FindObjectOfType<Grid>().grid[(int)vector.x, (int)vector.y].parent != transform) {
                return false;
            }
        }
        return true;
    }
    
    void UpdateGrid() {
        for(int y = 0; y < Grid.col; ++y) {
            for(int x = 0; x < Grid.row; ++x) {
                if (FindObjectOfType<Grid>().grid[x, y] != null) {
                    if (FindObjectOfType<Grid>().grid[x, y].parent == transform) {
                        FindObjectOfType<Grid>().grid[x, y] = null;
                    }
                }
            }
        }

        foreach(Transform childTransform in transform) {
            Vector2 vector = FindObjectOfType<Grid>().RoundVector(childTransform.position);
            FindObjectOfType<Grid>().grid[(int)vector.x, (int)vector.y] = childTransform;
        }
    }
    
}
