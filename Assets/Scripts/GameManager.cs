using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI info,info2;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject inGameScreen;
    [SerializeField] TextMeshProUGUI gameOver;
    [SerializeField] TextMeshProUGUI congrats;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] GameObject balanceCamera;
    [SerializeField] GameObject focalPoint;
    public GameObject gameOverNoPU;
    public SpawnManager spawnManager;
    PlayerController playerController;
    public static int score = 0; 
    public static int waveNumber = 0;
    public static int maxWave = 5;
    public static bool isGameActive = false;
    public bool topFloor = true;
    public GameObject pauseScreen;
    public GameObject secondPauseScreen;
    private bool paused;
    // Start is called before the first frame update
     private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();    
    }
    public void StartGame()
    {
        waveNumber = 0;
        score = 0; 
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
     //   
    }
    public void PushGame()
    {
        focalPoint.SetActive(true);
        balanceCamera.SetActive(false);
        spawnManager.SpawnEnemyWave(waveNumber);
        secondPauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1; 
        inGameScreen.gameObject.SetActive(true);
        playerController.transform.position = new Vector3(0,0,0);
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
         waveNumber = 0;
        score = 0; 
        isGameActive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else 
        Application.Quit();
        #endif
    }
    void DestroyAttacker()
    {
      GameObject[] attackers = GameObject.FindGameObjectsWithTag("Attacker");

      int i;
      for(i=0;i<attackers.Length;i++)
      {
          Destroy(attackers[i].gameObject);
      }
    }
    // Update is called once per frame
    void Update()
    {
        if(playerController.transform.position.y <= 100 && playerController.transform.position.y >= 80)
        {
            topFloor = false;
           // isGameActive = false;
            Time.timeScale = 0;
            secondPauseScreen.gameObject.SetActive(true);
            topFloor = false;

        }
        if(waveNumber > maxWave)
        {
            isGameActive = false;
            congrats.gameObject.SetActive(true);
        }
        if(waveNumber <= maxWave ) waveText.text = "Wave : " + waveNumber;
        scoreText.text = "Score : " + score;
        if(playerController.transform.position.y < -2)
        {
            isGameActive = false;
            gameOver.gameObject.SetActive(true);
        }
        if(Input.GetMouseButtonDown(1))
        {
            ChangePaused();
        }
        
    }
    public void HowToPlay()
    {
        info.gameObject.SetActive(true);
        info2.gameObject.SetActive(true);
        StartCoroutine(InfoTime());
    }
    IEnumerator InfoTime()
    {
        yield return new WaitForSeconds(3);
        info.gameObject.SetActive(false);
        info2.gameObject.SetActive(false);
    }
}
