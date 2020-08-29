using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    private Renderer tileRenderer;
    [SerializeField] private Color hoverColor;
    private Color startColor;

    static public float towerPlacementHeight = 1.5f;

    void Start() {
        tileRenderer = GetComponent<Renderer>();
        startColor = tileRenderer.material.color;
    }
    private void OnMouseEnter() {
        if (Cursor.visible) {
            tileRenderer.material.color = hoverColor;
        }
    }

    private void OnMouseExit() {
        tileRenderer.material.color = startColor;
    }

    public Vector3 GetBuildingPosition() {
        Vector3 pos = transform.position;
        pos.y = towerPlacementHeight;
        return pos;
    }
}
