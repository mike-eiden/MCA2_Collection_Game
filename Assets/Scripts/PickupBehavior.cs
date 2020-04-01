using System.Collections;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public static int pickupCount = 0;
    public Transform chestTop;
    public LevelManager levelManager;
    public int scoreValue = 1;
    public AudioClip pickupNoise; 

    private bool lockScore = false; 
    private Vector3 startScale;


    private void Start()
    {
        startScale = transform.localScale; 
    }

    private void OnEnable()
    {
        pickupCount++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !lockScore)
        {
            AudioSource.PlayClipAtPoint(pickupNoise, GameObject.FindGameObjectWithTag("MainCamera").transform.position);

            pickupCount--;
            levelManager.incrementScore(scoreValue);
            lockScore = true; 
            
            gameObject.GetComponent<Animator>().SetTrigger("ChestOpenTrigger");

            StartCoroutine(TurnOffCoroutine());
        }
    }

    IEnumerator TurnOffCoroutine()
    {
        if (pickupCount == 0)
        {
            levelManager.setWin();
        }
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        transform.localScale = startScale;
        chestTop.localEulerAngles = Vector3.zero;
        gameObject.GetComponent<Animator>().SetTrigger("ResetChestTrigger");
        lockScore = false;
    }
    

}