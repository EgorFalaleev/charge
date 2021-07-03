using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject laserCircle;

    private MovementBehaviour movementChecker;
    

    private void Awake()
    {
        movementChecker = GetComponent<MovementBehaviour>();
    }

    private void Update()
    {
        EnableLaserCircle(movementChecker.IsPlayerMoving());
    }

    private void EnableLaserCircle(bool isPlayerMoving)
    {
        // laser circle enable when player is not moving
        laserCircle.SetActive(!isPlayerMoving);
    }
}
