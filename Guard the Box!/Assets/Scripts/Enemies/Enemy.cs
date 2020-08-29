using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    public float speed { get; private set; } = 10f;

    public float turnSpeed { get; private set; } = 6f;

    [SerializeField]
    private float health;
    [SerializeField]
    private int moneyDrop;

    private bool isDead = false;

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0 && !isDead) {
            Die();
        }
    }

    private void Die() {
        isDead = true;
        Destroy(gameObject);
        GameEvents.eventSystem.EnemyKilled(moneyDrop);
    }
}
