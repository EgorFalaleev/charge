using UnityEngine;

public class Enemy : MonoBehaviour
{
    // state variables
    [SerializeField] private float health = 100;

    public void GetDamage(float damagePoints)
    {
        health -= damagePoints;

        if (health <= 0) Destroy(gameObject);
    }
}
