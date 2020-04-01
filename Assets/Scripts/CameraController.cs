using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 distanceToPlayer;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distanceToPlayer = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
//        if (!LevelTimer.isGameOver && player != null)
//        {
            transform.position = player.transform.position + distanceToPlayer;
        //}
    }
}
