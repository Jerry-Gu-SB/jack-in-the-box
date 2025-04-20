using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script based on: https://discussions.unity.com/t/need-camera-to-follow-player-but-not-the-players-rotation/9212/9
public class CameraManager : MonoBehaviour
{
    public Transform player;
    public float yOffset = -7f;

    void Update()
    {
        if (player == null) return;
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition() {
        Vector3 newPosition = transform.position;
        newPosition.x = player.position.x;
        newPosition.y = player.position.y - yOffset;
        newPosition.z = transform.position.z;
        
        transform.position = newPosition;
    }
}

