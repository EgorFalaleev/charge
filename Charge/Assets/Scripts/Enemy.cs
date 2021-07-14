using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // configuration parameters
    [Range(1f, 7f)]
    [SerializeField] private float spotRadius = 3f;
    [Range(0f, 10f)]
    [SerializeField] private float enemySpeed = 3f;
    [SerializeField] private float damageWhenDestroyed = 40f;

    // references
    [SerializeField] private Player player;

    // state variables
    [SerializeField] private float health = 100;

    private void OnDrawGizmos()
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;

        Gizmos.color = (directionToPlayer.sqrMagnitude <= spotRadius * spotRadius) ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, spotRadius);
    }

    private void Update()
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;

        if (directionToPlayer.sqrMagnitude <= spotRadius * spotRadius)
        {
            FollowPlayer(directionToPlayer);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.GetDamage(damageWhenDestroyed);

        Destroy(gameObject);
    }

    private void FollowPlayer(Vector2 direction)
    {
        transform.Translate(direction.normalized * enemySpeed * Time.deltaTime);
    }

    public void GetDamage(float damagePoints)
    {
        health -= damagePoints;

        if (health <= 50) GetComponent<SpriteRenderer>().color = Color.yellow;

        if (health <= 0)
        {
            FindObjectOfType<Laser>().RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}
