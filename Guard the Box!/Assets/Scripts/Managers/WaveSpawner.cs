using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    public int enemiesAlive { get; private set; }
    
    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemyPrefab;
        public int enemiesCount;
        public float spawnRate;
    }

    public Wave[] waves;

    public Transform spawnPosition;

    public int waveNumber { get; private set; } = 0;
    public float timeBetweenWaves = 5f;
    private float countdown = 1f; // Time to build before waves start, then reused for between waves
    void Start() {
        GameEvents.eventSystem.OnEnemyKilled += EnemyKilled;
        GameEvents.eventSystem.OnEnemyReachEnd += EnemyDied;
        enemiesAlive = 0;
    }

    void Update() {
        if (enemiesAlive > 0) {
            return;
        }

        if (countdown <= 0f) {
            if (waveNumber < waves.Length) {
                GameEvents.eventSystem.WaveCleared();
                StartCoroutine(SpawnWave(waves[waveNumber]));
                countdown = timeBetweenWaves;
                return;
            } else {
                GameEvents.eventSystem.GameWon();
            }
        }

        countdown -= Time.deltaTime;
    }

    public int Waves { get { return waves.Length;} }
    IEnumerator SpawnWave(Wave wave) {
        ++waveNumber;
        
        for (int j = 0; j < wave.enemiesCount; ++j) {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1 / wave.spawnRate);
        }

        yield break;
    }

    void SpawnEnemy(Transform enemy) {
        Instantiate(enemy, spawnPosition.position, spawnPosition.rotation);
        enemiesAlive++;
    }

    private void EnemyKilled(int moneyDrop) {
        enemiesAlive--;
    }

    private void EnemyDied() {
        enemiesAlive--;
    }
}
