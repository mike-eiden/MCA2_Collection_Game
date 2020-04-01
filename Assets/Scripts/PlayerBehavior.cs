using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpAmount = 250;
    public LevelManager levelManager; 
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!LevelManager.IsGameLost && !LevelManager.IsLevelWon)
            {
            float horz = Input.GetAxis("Horizontal") * 7f;
            float vert = Input.GetAxis("Vertical") * 7f;
            rb.AddForce(new Vector3(horz, 0, vert));
    
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (transform.position.y <= -9.63)
                {
                    rb.AddForce(new Vector3(0, jumpAmount, 0));
                }
            }
        }
        else
        {
            FreezePlayer();
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            LevelManager.IsGameLost = true;
//        }

        if (other.gameObject.CompareTag("water"))
        {
            LevelManager.IsGameLost = true;
        }
    }

    private void FreezePlayer()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
