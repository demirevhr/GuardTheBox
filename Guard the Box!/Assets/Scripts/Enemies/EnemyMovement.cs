using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {
    private Enemy enemy;
    
    private Transform nextWayPoint;
    private int wayPointIndex = 0;

    void Start() {
        nextWayPoint = Pathway.points[0];
        enemy = GetComponent<Enemy>();
    }
    void Update() {
        Vector3 direction = nextWayPoint.position - transform.position;
        
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * enemy.turnSpeed);
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);  

        if (Vector3.Distance(nextWayPoint.position, transform.position) <= 0.5f) {
            if (wayPointIndex == Pathway.points.Length) {
                Destroy(gameObject);
                GameEvents.eventSystem.EnemyReachEnd(); 
            } else {
                UpdateNextWayPoint();
            }
        }
    }

    private void UpdateNextWayPoint() {
        nextWayPoint = Pathway.points[wayPointIndex++];
    }
}
