using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    private string playScene = "LevelSelect";
    public void Play() {
        SceneManager.LoadScene(playScene);
    }

    public void Quit() {
        Application.Quit();
    }
}
