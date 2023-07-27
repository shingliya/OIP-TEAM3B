using System.Collections;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public float burstForce = 10f; // The force applied when bursting
    public float minRotation = 0f; // Minimum y-rotation in degrees
    public float maxRotation = 360f; // Maximum y-rotation in degrees
    public float minBurstInterval = 1f; // Minimum time interval between bursts
    public float maxBurstInterval = 4f; // Maximum time interval between bursts

    private Rigidbody rb;
    private float timeSinceLastBurst;
    private float targetYRotation;
    private bool isBursting;

    private DinoGameController gameController; // Reference to the DinoGameController script

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameController = FindObjectOfType<DinoGameController>();
        PickRandomRotation();
        timeSinceLastBurst = 0f;
    }

    private void Update()
    {
        timeSinceLastBurst += Time.deltaTime;

        if (!isBursting && timeSinceLastBurst >= GetRandomBurstInterval())
        {
            PickRandomRotation();
            Burst();
        }
    }

    private void PickRandomRotation()
    {
        targetYRotation = Random.Range(minRotation, maxRotation);
        transform.rotation = Quaternion.Euler(90f, targetYRotation, 0f);
    }

    private void Burst()
    {
        isBursting = true;
        rb.velocity = Vector3.zero; // Reset velocity before applying burst force

        // Convert the target y-rotation to a direction vector in the x-z plane
        Vector3 burstDirection = Quaternion.Euler(0f, targetYRotation, 0f) * Vector3.forward;

        // Apply a force in the burst direction to the Rigidbody
        rb.AddForce(burstDirection * burstForce, ForceMode.Impulse);

        // Set a random burst interval before invoking StopMovement
        float randomBurstInterval = GetRandomBurstInterval();
        Invoke("StopMovement", randomBurstInterval);
    }

    private float GetRandomBurstInterval()
    {
        return Random.Range(minBurstInterval, maxBurstInterval);
    }

    private void StopMovement()
    {
        rb.velocity = Vector3.zero;
        isBursting = false;
        timeSinceLastBurst = 0f;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop bursting and disappear when colliding with the player
            StopMovement();
            gameController.OnFishDestroyed();
            gameObject.SetActive(false);
        }
    }
}
