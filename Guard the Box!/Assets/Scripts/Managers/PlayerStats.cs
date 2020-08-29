using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public static PlayerStats stats;
    public int lives { get; private set; }
    public int money { get; private set; }
    [SerializeField]
    private int startMoney = 10;
    [SerializeField]
    private int startLives = 10;

    private void Start() {
        stats = this;
        
        money = startMoney;
        lives = startLives;
        GameEvents.eventSystem.OnEnemyReachEnd += TakeLife;
        GameEvents.eventSystem.OnEnemyKilled += EnemyKilled;

        GameEvents.eventSystem.UpdateMoney(money);
    }


    public void SpendMoney(int price) {
        money -= price;
        GameEvents.eventSystem.UpdateMoney(money);
    }

    private void TakeLife() {
        lives -= 1;

        GameEvents.eventSystem.UpdateLives(lives);
        if (lives <= 0) {
            GameEvents.eventSystem.GameOver();
        }
    }

    private void EnemyKilled(int moneyWon) {
        money += moneyWon;
        GameEvents.eventSystem.UpdateMoney(money);
    }
}