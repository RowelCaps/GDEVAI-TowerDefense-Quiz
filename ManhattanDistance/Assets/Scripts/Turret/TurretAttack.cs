using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour {

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject turretBullet;
    [SerializeField] private float distanceToAttack = 6f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private int Damage = 5;

    [SerializeField] private Transform nearestEnemy;
    private bool isTargetingEnemy = false;
    private float timeCounter = 0f;

    public bool IsTargetingEnemy { get { return isTargetingEnemy; } }

    // Use this for initialization
    void Start() {

    }

    private void Update()
    {
        timeCounter += Time.deltaTime;

        if (!isTargetingEnemy)
            setNearestEnemey();
        else
        {
            enemyDistanceListener();
            LookAtEnemy();
            attack();
        }
    }

    private void attack()
    {
        if (timeCounter < attackSpeed)
            return;

        print("attacking");

        GameObject bulletInstance = Instantiate(turretBullet, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletInstance.GetComponent<Bullet>();
        bullet.Damage = this.Damage;
        bullet.Target = nearestEnemy;
        bullet.InitialPosition = GameObject.Find("FirePoint").transform.position;

        timeCounter = 0;
    }

    private void LookAtEnemy()
    {
        if (nearestEnemy == null)
            return;

        Vector3 dir = nearestEnemy.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void setNearestEnemey()
    {
        if (isTargetingEnemy)
            return;

        foreach(Transform enemy in GameManager.Shared.EnemiesCurrentlyActive)
        {
            if(Vector3.Distance(transform.position, enemy.position) < distanceToAttack)
            {
                isTargetingEnemy = true;
                nearestEnemy = enemy;
            }
        }
    }

    private void enemyDistanceListener()
    {
        if (nearestEnemy == null)
        {
            isTargetingEnemy = false;
            return;
        }

        if (Vector3.Distance(nearestEnemy.position, transform.position) > distanceToAttack)
        {
            nearestEnemy = null;
            isTargetingEnemy = false;
        }
    }
}
