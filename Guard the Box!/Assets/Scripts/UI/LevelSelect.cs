using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    private string level1Scene = "Level1";
    private string level2Scene = "Level2";
    private string level3Scene = "Level3";


    public void Level1() {
        SceneManager.LoadScene(level1Scene);
    }

    public void Level2() {
        SceneManager.LoadScene(level2Scene);
    }

    public void Level3() {
        SceneManager.LoadScene(level3Scene);
    }
}
