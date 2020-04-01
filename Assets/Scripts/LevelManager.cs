using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public float timerTime = 30f;
    public GameObject gameOverText;
    public Text scoreText;
    public Text timerText;
    public GameObject levelBeatText; 
    public Transform player;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public AudioClip gameOverNoise;
    public AudioClip levelWinNoise; 
    
    private Vector3 playerStartPos; 
    private float countDown;
    private float minusTenSec; 
    private static int score = 0; 
    private static bool isGameLost;

    public static bool IsGameLost
    {
        get => isGameLost;
        set => isGameLost = value;
    }

    private static bool isLevelWon;

    public static bool IsLevelWon
    {
        get => isLevelWon;
        set => isLevelWon = value;
    }

    private int currentLevel = 1;
    private bool gameOverLock = false; 
    
    void Start()
    {
        countDown = timerTime;
        minusTenSec = timerTime - 10; 
        isGameLost = false;
        isLevelWon = false; 
        timerText.text = countDown.ToString("0.00");
        playerStartPos = player.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOverLock){
            if (isGameLost)
            {
                gameOverLock = true; 
                levelLost();
            }

            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
                timerText.text = countDown.ToString("f2");
                scoreText.text = "Score = " + score; 
            }
            else if (countDown < 0)
            {
                gameOverLock = true; 
                levelLost();
            }
        }
    }
    
    public void levelLost()
    {
        timerText.enabled = false; 
        gameOverText.SetActive(true);
        AudioSource.PlayClipAtPoint(gameOverNoise, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
        scoreText.text = "Final Score = " + score; 
        //AudioSource.PlayClipAtPoint(loseSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
        StartCoroutine(ResetCoroutine());
       
    }

    IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(2);
        countDown = timerTime;
        isGameLost = false; 
        timerText.enabled = true;
        score = 0;
        scoreText.text = "Score = " + score;
        gameOverText.SetActive(false);
        timerText.text = countDown.ToString("0.00");
        player.position = playerStartPos;
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
            enemy.GetComponent<EnemyBehavior>().ResetInitialPosition();
            enemy.GetComponent<Animator>().SetTrigger("CrabResetTrigger");

        }

        if (currentLevel == 1)
        {
            level1.SetActive(true);
            level2.SetActive(false);
            level3.SetActive(false);
            foreach (Transform child in level1.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        if (currentLevel == 2)
        {
            level1.SetActive(false);
            level2.SetActive(true);
            level3.SetActive(false);
            foreach (Transform child in level2.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        if (currentLevel == 3)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(true);
            foreach (Transform child in level3.transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        gameOverLock = false; 
    }
    
    private bool checkFirstTenSeconds()
    {
        return countDown > minusTenSec;
    }

    public void incrementScore(int scoreValue)
    {
        if (checkFirstTenSeconds())
        {
            score += (scoreValue * 2); 
        }
        else
        {
            score += scoreValue; 
        }
    }

    public void setWin()
    {
        levelBeatText.SetActive(true);
        AudioSource.PlayClipAtPoint(levelWinNoise, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
        timerText.enabled = false; 
        isLevelWon = true;
        currentLevel++;
        StartCoroutine(NextLevelCoroutine()); 
    }
    
    
    
    IEnumerator NextLevelCoroutine()
    {
        yield return new WaitForSeconds(2);
        countDown = timerTime;
        isLevelWon = false; 
        timerText.enabled = true;
        score = 0;
        scoreText.text = "Score = " + score;
        levelBeatText.SetActive(false);
        timerText.text = countDown.ToString("0.00");
        player.position = playerStartPos;

        if (currentLevel == 1)
        {
            level1.SetActive(true);
            level2.SetActive(false);
            level3.SetActive(false);
            foreach (Transform child in level1.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        if (currentLevel == 2)
        {
            level1.SetActive(false);
            level2.SetActive(true);
            level3.SetActive(false);
            foreach (Transform child in level2.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        if (currentLevel == 3)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(true);
            foreach (Transform child in level3.transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        gameOverLock = false; 
    }
    
    
}
