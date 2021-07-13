using UnityEngine;

public class Laser : MonoBehaviour
{
    // configuration parameters
    [SerializeField] private float damage = 1f;
    [Range (1f, 4f)]
    [SerializeField] private float circleRadius = 1f;

    // references
    public Enemy enemy;
    public Player player;
    private SpriteRenderer circleSprite;
    private LineRenderer laserBeam;
    [SerializeField] private RectTransform laserChargeCircle;
    
    // state variables
    private Vector3 enemyPos;
    private float distanceToEnemy;

    private void Awake()
    {
        circleSprite = GetComponent<SpriteRenderer>();
        laserBeam = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        laserBeam.startWidth = 0.1f;

        // at the beginning we suppose no enemy is inside the circle
        distanceToEnemy = circleRadius + 1f;

        UpdateRadius(circleRadius);
    }

    private void Update()
    {
        if (enemy)
        {
            enemyPos = enemy.transform.position;
            distanceToEnemy = Vector2.SqrMagnitude(enemyPos - transform.position);
        }

        // attack only if enemy exists
        if (distanceToEnemy <= circleRadius * circleRadius && enemy)
        {
            player.isAttacking = true;
            float charge = player.GetChargeLevel();

            AttackEnemy(damage * Time.deltaTime, charge);
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
            laserBeam.SetPosition(1, enemyPos);

            enemy.GetDamage(damage);
        }
        else laserBeam.positionCount = 0;
    }

    public void UpdateRadius(float radius)
    {
        transform.localScale = new Vector2(radius / 1.5f, radius / 1.5f);
        laserChargeCircle.sizeDelta = new Vector2(323.5f * radius, 323.5f * radius);
    }
}
