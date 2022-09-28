using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    public GameObject PauseMenu;
    public GameObject HelpMenu;
    public GameObject GameOverMenu;
    public GameObject WinGameMenu;
    public Slider playerLife;
    public Slider bossLife;
    public static bool isPaused;
    bool isGameScene;

    void Start() {
        PauseMenu.SetActive(false);
        isPaused = false;
    }

    public void PauseGame() {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame() {
        PauseMenu.SetActive(false);
        HelpMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void RestartGame() {
        SceneManager.LoadScene("GameScene");
    }

    void Update() {
        isGameScene = SceneManager.GetActiveScene().name == "GameScene";
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isGameScene) {
                if (isPaused) { ResumeGame(); }
                else { PauseGame(); }
            }
        }

        if (playerLife.value <= 0) {
            GameOverMenu.SetActive(true);
        }

        else if (bossLife.value <=0) {
            WinGameMenu.SetActive(true);
        }
    }
}
