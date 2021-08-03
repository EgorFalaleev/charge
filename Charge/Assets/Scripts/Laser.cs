using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // configuration parameters
    [SerializeField] private float damage = 1f;
    [Range (1f, 4f)]
    [SerializeField] private float circleRadius = 1f;

    // references
    public List<Enemy> enemies;
    public Player player;
    private LineRenderer laserBeam;
    [SerializeField] private Enemy enemyToAttack;
    [SerializeField] private RectTransform laserChargeCircle;

    // state variables
    private float distanceToEnemySqr;

    private void Awake()
    {
        laserBeam = GetComponent<LineRenderer>();
    }
    
    private void Start()
    {
        laserBeam.startWidth = 0.1f;

        // at the beginning we suppose no enemy is inside the circle
        distanceToEnemySqr = Mathf.Infinity;

        UpdateRadius(circleRadius);
    }

    private void Update()
    {
        FindClosestEnemy(enemies);

        // attack enemy if it is inside the laser circle
        if (enemyToAttack && Vector2.SqrMagnitude(transform.position - enemyToAttack.transform.position) <= circleRadius * circleRadius)
        {
            player.isAttacking = true;
            AttackEnemy(damage, player.GetChargeLevel());
        }
        else
        {
            player.isAttacking = false;
            laserBeam.positionCount = 0;
        }
    }

    private void AttackEnemy(float damage, float chargeLevel)
    {
        if (chargeLevel > 0f)
        {
            // create a laser from player to enemy
            laserBeam.positionCount = 2;
            laserBeam.SetPosition(0, transform.position);
            laserBeam.SetPosition(1, enemyToAttack.transform.position);

            enemyToAttack.GetDamage(damage * Time.deltaTime);
        }
        else laserBeam.positionCount = 0;
    }

    private void FindClosestEnemy(List<Enemy> activeEnemies)
    {
        if (activeEnemies.Count == 0)
        {
            FindObjectOfType<SceneLoader>().LoadNextLevel();

            return;
        }

        float minDistanceToEnemySqr = Mathf.Infinity;

        enemyToAttack = null;

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i])
            {
                distanceToEnemySqr = Vector2.SqrMagnitude(transform.position - activeEnemies[i].transform.position);

                if (distanceToEnemySqr < minDistanceToEnemySqr)
                {
                    minDistanceToEnemySqr = distanceToEnemySqr;
                    enemyToAttack = activeEnemies[i];
                }
            }
            else
            {
                activeEnemies.RemoveAt(i);
            }
        }
    }

    public void RemoveEnemy(Enemy enemyToRemove)
    {
        enemies.Remove(enemyToRemove);
    }

    public void UpdateRadius(float radius)
    {
        transform.localScale = new Vector2(radius / 1.5f, radius / 1.5f);
        laserChargeCircle.sizeDelta = new Vector2(323.5f * radius, 323.5f * radius);
    }
}
