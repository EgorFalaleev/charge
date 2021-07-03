using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [Range (1f, 4f)]
    [SerializeField] private float circleRadius = 1f;

    private SpriteRenderer circleSprite;
    private GameObject enemy;
    private Enemy enemyScript;
    private Vector3 enemyPos;
    private float distanceToEnemy;


    private void Awake()
    {
        circleSprite = GetComponent<SpriteRenderer>();
        UpdateRadius(circleRadius);
        enemy = GameObject.FindWithTag("Enemy");
        enemyPos = enemy.transform.position;
        enemyScript = enemy.GetComponent<Enemy>();

        // at the beginning we suppose no enemy is inside the circle
        distanceToEnemy = circleRadius + 1f;
    }

    private void Update()
    {
        distanceToEnemy = Vector2.Distance(transform.position, enemyPos);

        if (distanceToEnemy <= circleRadius)
        {
            circleSprite.color = Color.green;
            AttackEnemy(damage);
        }
        else circleSprite.color = Color.red;
    }

    private void AttackEnemy(int damage)
    {
        enemyScript.GetDamage(damage);
    }

    public void UpdateRadius(float radius)
    {
        transform.localScale = new Vector2(circleRadius / 1.5f, circleRadius / 1.5f);
    }
}
