using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour { 
    static public bool gameEnded;

    public GameObject gameOverUI;
    public GameObject gameWonUI;

    private void Start() {
        gameEnded = false;
        gameOverUI.SetActive(false);
        GameEvents.eventSystem.OnGameOver += EndGame;
        GameEvents.eventSystem.OnGameWon += WinGame;
    }
    void Update() {
        if (gameEnded)
            return;
    }

    private void EndGame() {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

    private void WinGame() {
        gameEnded = true;
        gameWonUI.SetActive(true);
    }
}
