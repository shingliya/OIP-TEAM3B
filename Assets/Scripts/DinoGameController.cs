using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DinoGameController : MonoBehaviour
{
    public GameObject objectPrefab; // The prefab of the object you want to spawn
    public int maxObjectsToSpawn = 10; // The maximum number of objects to spawn
    public Bounds spawnArea;
    public float spawnInterval = 1f; // Time interval between spawns

    private int objectsSpawned = 0;

    private int points = 0;
    private bool isGameOver = false;

    public TextMeshProUGUI scoreText; // Reference to the UI Text component for displaying the score

    public GameObject gameOverPanel;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnObjectsCoroutine());
        UpdateScoreText();
    }

    private IEnumerator SpawnObjectsCoroutine()
    {
        while (objectsSpawned < maxObjectsToSpawn)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

   private void StopSpawnCoroutine()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }


    private void SpawnObject()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        objectsSpawned++;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(spawnArea.min.x, spawnArea.max.x);
        float y = Random.Range(spawnArea.min.y, spawnArea.max.y);
        float z = Random.Range(spawnArea.min.z, spawnArea.max.z);
        return new Vector3(x, y, z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnArea.center, spawnArea.size);
    }

    public void OnFishDestroyed()
    {
        points += 10; // Increase points when a fish is destroyed
        objectsSpawned--; // Decrease objectsSpawned count
        UpdateScoreText();

         // Check for the game-over condition
        if (!isGameOver && points >= 100)
        {
            isGameOver = true;
            GameOver();
        }
    }

    private void UpdateScoreText()
    {
        // Display the current score in the UI Text
        scoreText.text = "Score: " + points.ToString();
    }

    private void GameOver()
    {
        // Perform any game-over actions, such as displaying a game-over panel or message
        // In this example, we'll just activate the gameOverPanel game object (set it as inactive initially in the Inspector)
        gameOverPanel.SetActive(true);
        StopSpawnCoroutine();
    }
}
