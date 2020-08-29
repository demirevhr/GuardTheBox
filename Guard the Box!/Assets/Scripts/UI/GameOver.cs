using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour {
    public string mainMenuScene = "MainMenu";
    public void TryAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu() {
        SceneManager.LoadScene(mainMenuScene);
    }
}
