using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float chargeLevel = 0.5f;
    [SerializeField] private float chargeChangeSpeed = 0.01f;
    [SerializeField] private GameObject laserCircle;

    public bool isAttacking;

    private MovementBehaviour movementChecker;
    
    private void Awake()
    {
        movementChecker = GetComponent<MovementBehaviour>();
    }

    private void Update()
    {
        bool isPlayerMoving = movementChecker.IsPlayerMoving();

        HandleLaserCircleVisibility(isPlayerMoving);
        ChangeChargeLevel(isPlayerMoving, chargeChangeSpeed * Time.deltaTime);
    }

    public float GetChargeLevel()
    {
        return chargeLevel;
    }

    private void HandleLaserCircleVisibility(bool isPlayerMoving)
    {
        // laser circle enables when player is not moving
        laserCircle.SetActive(!isPlayerMoving);
    }

    private void ChangeChargeLevel(bool isMoving, float chargeSpeed)
    {
        if (isMoving)
        {
            isAttacking = false;
            chargeLevel += chargeSpeed;
            if (chargeLevel >= 1f) chargeLevel = 1f;
        }
        else if (isAttacking)
        {
            chargeLevel -= chargeSpeed;
            if (chargeLevel <= 0f) chargeLevel = 0f;
        }
    }
}
