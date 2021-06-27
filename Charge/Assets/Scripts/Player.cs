using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration parameters
    [Range(3, 7)]
    [SerializeField] private float laserRadius = 4.5f;
    [SerializeField] private Transform laserCircle;

    // cached references
    private MovementBehaviour joystick;

    // state variables
    private int health = 100;

    private void Awake()
    {
        laserCircle = GetComponentInChildren<Transform>();
        joystick = GetComponent<MovementBehaviour>();
    }

    private void Update()
    {
        if (!joystick.IsPlayerMoving()) DrawLaserCircle(laserRadius);
    }

    private void DrawLaserCircle(float radius)
    {
        laserCircle.localScale = new Vector2(radius, radius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, laserRadius);
    }
}
