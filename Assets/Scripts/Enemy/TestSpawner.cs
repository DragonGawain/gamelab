using System.Collections;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private Transform enemyPrefab2;
    [SerializeField] private Transform enemyPrefab3;
    [SerializeField] private Canvas enemy2Canvas;
    //[SerializeField] private Camera enemyCam;
    [SerializeField] private Camera mainCam;


    private bool enemy2Spawned = false;

    private void Start()
    {
        enemy2Canvas.gameObject.SetActive(false); // Hide canvas initially
        //enemyCam.enabled = false;
        mainCam.enabled = true;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnEnemy(enemyPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !enemy2Spawned)
        {
            SpawnEnemy(enemyPrefab2);
            StartCoroutine(ShowCanvasAndPause(enemy2Canvas, 5f)); // Show the canvas for 3 seconds and pause the game
            enemy2Spawned = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnEnemy(enemyPrefab3);
        }
    }

    IEnumerator ShowCanvasAndPause(Canvas canvas, float delay)
    {
        canvas.gameObject.SetActive(true);
        //enemyCam.enabled = true;

        //Time.timeScale = 0; // Pause the game

        // Wait for the specified amount of time
        float pauseEndTime = Time.realtimeSinceStartup + delay;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return null;
        }

        canvas.gameObject.SetActive(false);
        //enemyCam.enabled = false;


        //Time.timeScale = 1; // Resume the game
    }

    void SpawnEnemy(Transform enemyPrefab)
    {
        Instantiate(enemyPrefab, transform.position + Vector3.up, Quaternion.identity);
    }
}
