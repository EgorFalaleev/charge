using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    // configuration parameters
    [Range(1f, 7f)]
    [SerializeField] private float spotRadius = 3f;
    [Range(0f, 10f)]
    [SerializeField] private float damageWhenDestroyed = 40f;

    // references
    [SerializeField] private Player player;
    [SerializeField] private Gradient enemyGradient;
    private SpriteRenderer enemySprite;
    private AIPath aIPath;

    // state variables
    [SerializeField] private float health = 100;

    private void OnDrawGizmos()
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;

        Gizmos.color = (directionToPlayer.sqrMagnitude <= spotRadius * spotRadius) ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, spotRadius);
    }

    private void Awake()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        aIPath = GetComponent<AIPath>();
    }

    private void Start()
    {
        CalculateColour(health);
        aIPath.enabled = false;
    }

    private void Update()
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;

        if (directionToPlayer.sqrMagnitude <= spotRadius * spotRadius)
        {
            aIPath.enabled = true;
        }
        else aIPath.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetDamage(damageWhenDestroyed);

            Destroy(gameObject);
        }
    }

    private void CalculateColour(float currentHealth)
    {
        float relativeHealth = (100 - currentHealth) / 100;
        enemySprite.color = enemyGradient.Evaluate(relativeHealth);
    }

    public void GetDamage(float damagePoints)
    {
        health -= damagePoints;
        CalculateColour(health);

        if (health <= 0)
        {
            FindObjectOfType<Laser>().RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}
