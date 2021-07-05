using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // state variables
    [SerializeField] private float health = 100;
    [SerializeField] private Text healthText;

    private void Start()
    {
        healthText.text = "Enemy Health " + health;
    }

    public void GetDamage(float damagePoints)
    {
        health -= damagePoints;

        healthText.text = "Enemy Health " + health;

        if (health <= 0) Destroy(gameObject);
    }
}
