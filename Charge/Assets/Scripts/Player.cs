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
        HandleLaserCircleVisibility(movementChecker.IsPlayerMoving());
    }

    private void HandleLaserCircleVisibility(bool isPlayerMoving)
    {
        // laser circle enables when player is not moving
        laserCircle.SetActive(!isPlayerMoving);
    }
}
