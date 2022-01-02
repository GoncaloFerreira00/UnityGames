using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position-player.position;
        Debug.Log(offset);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position+offset;
    }
}
