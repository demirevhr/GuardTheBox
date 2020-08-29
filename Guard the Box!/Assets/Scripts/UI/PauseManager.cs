using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour {
    public GameObject pauseUI;
    private string mainMenuScene = "MainMenu";
    void Awake() {
        pauseUI.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu() {
        pauseUI.SetActive(!pauseUI.activeSelf);
        if (pauseUI.activeSelf) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }
    public void ContinueGame() {
        TogglePauseMenu();
    }
    public void TryAgain() {
        TogglePauseMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu() {
        TogglePauseMenu();
        SceneManager.LoadScene(mainMenuScene);
    }
}
