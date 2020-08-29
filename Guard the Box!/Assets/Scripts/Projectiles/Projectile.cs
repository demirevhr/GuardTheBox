using UnityEngine;

public class Projectile : MonoBehaviour {
    private Transform target;
    public GameObject impactEffect;

    [SerializeField] 
    private float speed = 80f;
    [SerializeField]
    private int damage;

    private float turnSpeed = 6f;

    void Update() {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;

        float distance = speed * Time.deltaTime;

        if (direction.magnitude <= distance) {
            HitTarget();
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        transform.Translate(direction.normalized * distance, Space.World);
    }

    public void SeekTarget(Transform newTarget) {
        target = newTarget;
    }
     
    public void HitTarget() {
        GameObject effectInstance = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 1f);
        Destroy(gameObject);
        target.GetComponent<Enemy>()?.TakeDamage(damage);
    }
}
