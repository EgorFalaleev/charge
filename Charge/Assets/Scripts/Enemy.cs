using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // configuration parameters
    [Range(1f, 7f)]
    [SerializeField] private float spotRadius = 3f;
    [SerializeField] private float enemySpeed = 3f;

    // references
    [SerializeField] private Transform playerTf;

    // state variables
    private float health = 100;
    [SerializeField] private Text healthText;

    private void OnDrawGizmos()
    {
        Vector2 directionToPlayer = playerTf.position - transform.position;

        Gizmos.color = (directionToPlayer.sqrMagnitude <= spotRadius * spotRadius) ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, spotRadius);
    }

    private void Start()
    {
        healthText.text = "Enemy Health " + health;
    }

    private void Update()
    {
        Vector2 directionToPlayer = playerTf.position - transform.position;

        if (directionToPlayer.sqrMagnitude <= spotRadius * spotRadius)
        {
            FollowPlayer(directionToPlayer);
        }
    }

    private void FollowPlayer(Vector2 direction)
    {
        transform.Translate(direction.normalized * enemySpeed * Time.deltaTime);
    }

    public void GetDamage(float damagePoints)
    {
        health -= damagePoints;

        healthText.text = "Enemy Health " + health;

        if (health <= 0) Destroy(gameObject);
    }
}
