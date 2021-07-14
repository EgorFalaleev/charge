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
    }

    private void AttackEnemy(float damage, float chargeLevel, int enemyNumber)
    {
        Debug.Log("Enemy " + enemyNumber);

        if (chargeLevel > 0f)
        {
            // create a laser from player to enemy
            laserBeam.positionCount = 2;
            laserBeam.SetPosition(0, transform.position);
            laserBeam.SetPosition(1, enemies[enemyNumber].transform.position);

            enemies[enemyNumber].GetDamage(damage);
        }
        else laserBeam.positionCount = 0;
    }

    private void FindClosestEnemy(List<Enemy> activeEnemies)
    {
        float minDistanceToEnemySqr = Mathf.Infinity;

        if (enemyToAttack) enemyToAttack.GetComponent<SpriteRenderer>().color = Color.red;

        enemyToAttack = null;

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            distanceToEnemySqr = Vector2.SqrMagnitude(transform.position - activeEnemies[i].transform.position);

            if (distanceToEnemySqr < minDistanceToEnemySqr)
            {
                minDistanceToEnemySqr = distanceToEnemySqr;
                enemyToAttack = activeEnemies[i];
            }
        }

        enemyToAttack.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void UpdateRadius(float radius)
    {
        transform.localScale = new Vector2(radius / 1.5f, radius / 1.5f);
        laserChargeCircle.sizeDelta = new Vector2(323.5f * radius, 323.5f * radius);
    }
}
