using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hfollow : MonoBehaviour
{
    public Transform player;
    public float followDistance = 2f;

    private void Update()
    {
        // Calculate the desired position based on the player's position and the desired distance
        Vector3 targetPosition = player.position - player.forward * followDistance;

        // Smoothly move the game object towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
    }
}
