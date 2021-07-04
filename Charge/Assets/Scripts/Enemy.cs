using UnityEngine;

public class Enemy : MonoBehaviour
{
    // state variables
    private float health = 100;

    public void GetDamage(float damagePoints)
    {
        health -= damagePoints;
        Debug.Log(health);

        if (health <= 0) Destroy(gameObject);
    }
}
