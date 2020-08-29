using UnityEngine;
using UnityEngine.SceneManagement;
public class GameWon : MonoBehaviour {
    public string mainMenuScene = "MainMenu";
    public string levelSelectScene = "LevelSelect";
    public void LevelSelect() {
        SceneManager.LoadScene(levelSelectScene);
    }

    public void Menu() {
        SceneManager.LoadScene(mainMenuScene);
    }
}
