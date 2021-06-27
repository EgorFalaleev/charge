using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration parameters
    [Range(3, 7)]
    [SerializeField] private float laserRadius = 4.5f;

    // state variables
    private int health = 100;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, laserRadius);
    }
}
