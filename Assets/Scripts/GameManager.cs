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
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
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

    }
}
