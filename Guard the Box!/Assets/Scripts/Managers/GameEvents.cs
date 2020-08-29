using UnityEngine;
using System;

public class GameEvents : MonoBehaviour {
    public static GameEvents eventSystem;
    private void Awake() {
        eventSystem = this;
    }

    public event Action<int> OnUpdateMoney;
    public void UpdateMoney(int money) {
        OnUpdateMoney?.Invoke(money);
    }

    public event Action<int> OnUpdateLives;
    public void UpdateLives(int lives) {
        OnUpdateLives?.Invoke(lives);
    }

    public event Action OnWaveCleared;
    public void WaveCleared() {
        OnWaveCleared?.Invoke();
    }

    public event Action<int> OnEnemyKilled;
    public void EnemyKilled(int moneyWon) {
        OnEnemyKilled?.Invoke(moneyWon);
    }

    public event Action OnEnemyReachEnd;
    public void EnemyReachEnd() {
        OnEnemyReachEnd?.Invoke();
    }

    public event Action OnGameOver;
    public void GameOver() {
        OnGameOver?.Invoke();
    }

    public event Action OnGameWon;
    public void GameWon() {
        OnGameWon?.Invoke();
    }
}
