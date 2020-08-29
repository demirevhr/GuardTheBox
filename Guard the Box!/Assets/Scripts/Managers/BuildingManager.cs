using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {
    private GameObject towerGhost;
    private GameObject towerToBuild;
    private int towerCost;

    [SerializeField]
    private TowerConfiguration[] towers;

    private string buildPlaceTag = "AvailableTile";
    private string unavailableTileTag = "TileWithTurret";

    void Update() {
        if (GameManager.gameEnded)
            return;
        
        HandleNewObjectHotkey();

        if (towerGhost == null) {
            return;
        }

        MoveTowerWithCursor();
        ReleaseIfClicked();
    }

    private void HandleNewObjectHotkey() {
        for (int i = 0; i < towers.Length; ++i) {
            if (Input.GetKeyDown(towers[i].HotKey)) {                      
                SetTower(i); 
            }
        }      
    }
    private void MoveTowerWithCursor() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float depth = Camera.main.transform.position.y;

        if (Physics.Raycast(ray)) {
            Vector3 newPosition = ray.GetPoint(depth);
            newPosition.y = Tile.towerPlacementHeight;
            towerGhost.transform.position = newPosition;       
        }
    }

    private void ReleaseIfClicked() {
        if (Input.GetMouseButtonDown(0)) {

            BuildAtClosestTile();
            ClearTowerToBuild();
        }
    }

    private void BuildAtClosestTile() {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag(buildPlaceTag);

        float shortestDistance = Mathf.Infinity;
        float maxDistanceToTile = 3f;
        GameObject nearestTile = null;
        float distanceToTile;
        foreach (GameObject tile in tiles) {
            distanceToTile = Vector3.Distance(towerGhost.transform.position, tile.transform.position);
            if (distanceToTile < shortestDistance) {
                shortestDistance = distanceToTile;
                nearestTile = tile;
            }
        }

        if (nearestTile == null || nearestTile.CompareTag(unavailableTileTag) || shortestDistance > maxDistanceToTile) {
            //TODO : show UI - cannot build here
            ClearTowerToBuild();
            return;
        } else {
            Destroy(towerGhost);
            BuildAtTile(nearestTile);
        }
    }

    private void BuildAtTile(GameObject tileDest) {
        Tile tile = tileDest.GetComponent<Tile>();
        GameObject tower = (GameObject) Instantiate(towerToBuild, tile.GetBuildingPosition(), towerToBuild.transform.rotation);
        
        PlayerStats.stats.SpendMoney(towerCost);
        tile.tag = "TileWithTurret";
    }

    private void SetTower(int towerConfigIndex) {
        if (PlayerStats.stats.money < towers[towerConfigIndex].Cost) {
            //TODO : Insufficient funds UI
            Debug.Log("Insufficient funds");
            return;
        }
 
        Cursor.visible = false;

        towerGhost = (GameObject) Instantiate(towers[towerConfigIndex].TowerGhostPrefab);
        towerToBuild = towers[towerConfigIndex].TowerPrefab;
        towerCost = towers[towerConfigIndex].Cost;
    }
    public void SetMachineGun() {
        SetTower(0);
    }

    public void SetRocket() {
        SetTower(1);
    }

    public void SetLaser() {
        SetTower(2);
    }
    private void ClearTowerToBuild() {
        Cursor.visible = true;

        Destroy(towerGhost);
        towerToBuild = null;
        towerCost = 0;
    }
}
