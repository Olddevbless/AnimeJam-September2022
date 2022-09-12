using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager current;
    public GameObject pauseMenu;
    public bool pauseMenuIsActive;
    public GameManager instance;
    public int score;
    public GameObject gameOver;
    [SerializeField] TextMeshProUGUI gameOverScoreText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        
        gameOver.SetActive(false);
        pauseMenuIsActive = false;
        current = this;
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
        scoreText.text = ""+score;
        if (GameObject.FindObjectOfType<PlayerMovement>().playerHealth <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    
    void PauseMenu()
    {
        pauseMenuIsActive = !pauseMenuIsActive;
        if (pauseMenuIsActive)
        {
            Debug.Log("pausing");
            Debug.Log(Time.timeScale);
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }

    }
    public void ExitApplication()
    {
        Application.Quit();
    }
    public void Resume()
    {
        pauseMenuIsActive = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void IncreaseScore(int scoreValue)
    {
        score += scoreValue;
    }
    public IEnumerator GameOver()
    {
        Time.timeScale = 0;
        gameOverScoreText.text = "Your Score is:" + score;
        gameOver.SetActive(true);
        PauseMenu();
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1;
        
    }
 
}
