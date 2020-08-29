using UnityEngine;

[System.Serializable]
public class TowerConfiguration {
    [SerializeField]
    private GameObject towerGhostPrefab;
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private KeyCode hotKey;
    [SerializeField]
    private int cost;

    public KeyCode HotKey { get { return hotKey; } }
    public GameObject TowerGhostPrefab { get { return towerGhostPrefab; } }
    public GameObject TowerPrefab { get { return towerPrefab; } }
    public int Cost { get { return cost; } }
}
