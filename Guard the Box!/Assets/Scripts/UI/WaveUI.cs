using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour {
    private WaveSpawner spawner;

    [SerializeField]
    private TextMeshProUGUI waveNumber;
    private int maxWaves;
    private int currentWave;
    void Start() {
        spawner = spawner = FindObjectOfType<WaveSpawner>();
        currentWave = 0;
        maxWaves = spawner.Waves;
        GameEvents.eventSystem.OnWaveCleared += UpdateWave;
    }

    public void UpdateWave() {
        if (++currentWave <= maxWaves) {
            waveNumber.text = string.Format("WAVE {0}/{1}", currentWave, maxWaves);
        }
    }
}