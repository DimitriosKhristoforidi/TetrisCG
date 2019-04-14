using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] tetrisObjects;
    public GameObject currentObject;
    public GameObject nextObject;

    bool isGameStarted = false;
    int index = 0;
    int nextIndex = 0;

    private void Start() {
        SpawnRandomObject();
    }

    public void SpawnRandomObject() {
        if (!isGameStarted) {

            isGameStarted = true;
            index = Random.Range(0, tetrisObjects.Length);
            nextIndex = Random.Range(0, tetrisObjects.Length);

            nextObject = (GameObject)Instantiate(tetrisObjects[nextIndex], new Vector3(12f, 12f), Quaternion.identity);
            nextObject.GetComponent<Object>().enabled = false;

            currentObject = (GameObject)Instantiate(tetrisObjects[index], transform.position, Quaternion.identity);
            FindObjectOfType<UIController>().tetrisObject = currentObject;
        }
        else {
            nextObject.transform.position = transform.position;
            currentObject = nextObject;
            currentObject.GetComponent<Object>().enabled = true;
            FindObjectOfType<UIController>().tetrisObject = currentObject;
            nextIndex = Random.Range(0, tetrisObjects.Length);

            nextObject = (GameObject)Instantiate(tetrisObjects[nextIndex], new Vector3(12f, 12f), Quaternion.identity);
            nextObject.GetComponent<Object>().enabled = false;
        }        
    }
}
