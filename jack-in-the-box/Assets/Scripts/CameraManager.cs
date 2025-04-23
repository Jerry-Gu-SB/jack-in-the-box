using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    private float distance = -6f;

    void Update()
    {
        if (player != null)
        {
            Vector3 newPos = player.position;
            newPos.z = -10;
            newPos.y -= distance;
            transform.position = newPos;
        }
    }
}
