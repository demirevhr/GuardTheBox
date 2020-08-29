using UnityEngine;
using TMPro;

public class PlayerStatsUI : MonoBehaviour {
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;
    private void Start() {
        GameEvents.eventSystem.OnUpdateMoney += UpdateMoney;
        GameEvents.eventSystem.OnUpdateLives += UpdateLives;

    }
    public void UpdateMoney(int money) {
        moneyText.text = money.ToString();
    }

    public void UpdateLives(int lives) {
        livesText.text = lives.ToString();
    }
}
