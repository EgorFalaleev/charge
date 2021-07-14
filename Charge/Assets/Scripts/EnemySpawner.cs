using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // references
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Player player;

    public void AddEnemy()
    {
        Enemy newEnemy = Instantiate(enemyPrefab, GenerateRandomPosition(player.transform.position), transform.rotation);
        newEnemy.transform.parent = transform;
        FindObjectOfType<Laser>().AddEnemy(newEnemy);
    }

    private Vector3 GenerateRandomPosition(Vector2 origin)
    {
        return new Vector3(Random.Range(origin.x - 8f, origin.x + 8f), Random.Range(origin.y - 5f, origin.y + 5f));
    }
}
