using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public AudioClip enemyDieNoise; 

    private Vector3 initialPosition;
    private bool dieLock = false; 

    private void Start()
    {
        initialPosition = transform.position; 
    }


    // Update is called once per frame
    void Update()
    {
        if (!dieLock)
        {
            float step = moveSpeed * Time.deltaTime;

            if (!LevelManager.IsGameLost && !LevelManager.IsLevelWon)
            {
                if (target != null)
                {
                    transform.LookAt(target);

                    // Keep current y value no matter what
                    Vector3 desiredPos = new Vector3(target.position.x, transform.position.y, target.position.z);

                    transform.position = Vector3.MoveTowards(transform.position, desiredPos, step);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dieLock)
        {
            LevelManager.IsGameLost = true; 
        }

        if (other.CompareTag("Enemy"))
        {
            dieLock = true;
            gameObject.GetComponent<Animator>().SetTrigger("CrabDieTrigger");
            AudioSource.PlayClipAtPoint(enemyDieNoise, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
            StartCoroutine(DieCoroutine());
        }
    }

    public void ResetInitialPosition()
    {
        transform.position = initialPosition; 
    }

    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        ResetInitialPosition();
        dieLock = false; 
    }
}