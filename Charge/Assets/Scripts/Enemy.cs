using UnityEngine;

public class Enemy : MonoBehaviour
{
    // state variables
    private int health = 100;

    public void GetDamage(int damagePoints)
    {
        health -= damagePoints;

        if (health <= 0) Destroy(gameObject, 1f);
    }
}
