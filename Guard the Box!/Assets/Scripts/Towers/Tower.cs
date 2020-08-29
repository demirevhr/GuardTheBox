using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [Header("Tower specifications")]
    [SerializeField] private float range = 15f;
    [SerializeField] private float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Laser tower specs")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public int damagePerSec = 50;

    [Header("Setup parameters")]
    [SerializeField] private float turnSpeed = 5f;
    private float fireCountDown = 0f;
    private string enemyTag = "Enemy";
    private Enemy targetEnemy;
    private Transform target;

    public Transform rotatingPart;

    void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        lineRenderer = this.GetComponent<LineRenderer>();
    }
    void Update() {
        if (target == null) {
            if (useLaser) {
                if (lineRenderer.enabled) {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                }
            }
            return;
        }

        LookAtTarget();

        if (useLaser) {
            Laser();
        }
        else {
            if (fireCountDown <= 0f) {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

            fireCountDown -= Time.deltaTime;
        }
    }
    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else {
            target = null;
        }
    }
    
    void LookAtTarget() {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        rotatingPart.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);
    }

    void Shoot() {
        GameObject projectileGO = (GameObject) Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Projectile projectile = projectileGO.GetComponent<Projectile>();

        projectile.SeekTarget(target);
    }

    void Laser() {
        targetEnemy.TakeDamage(damagePerSec * Time.deltaTime);

        if (!lineRenderer.enabled) {
            lineRenderer.enabled = true;
            impactEffect.Play();

        }

        Vector3 direction = target.transform.position - impactEffect.transform.position;
        impactEffect.transform.position = target.transform.position + direction.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(direction);

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
