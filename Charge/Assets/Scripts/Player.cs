using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // configuration parameters
    [SerializeField] private float chargeChangeSpeed = 0.01f;

    // references
    private MovementBehaviour movementChecker;
    [SerializeField] private GameObject laserCircle;
    [SerializeField] private Text chargeLevelText;
    [SerializeField] private Text playerHPText;

    // state variables
    public bool isAttacking;
    private float health = 100f;
    [Range(0f, 1f)]
    [SerializeField] private float chargeLevel = 0.5f;

    private void Awake()
    {
        movementChecker = GetComponent<MovementBehaviour>();
    }

    private void Start()
    {
        chargeLevelText.text = "Charge level: " + chargeLevel;
        playerHPText.text = "Player HP: " + health;
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

    public void GetDamage(float damage)
    {
        health -= damage;
        playerHPText.text = "Player HP: " + health;
        if (health <= 0) Destroy(gameObject);
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

        chargeLevelText.text = "Charge Level: " + chargeLevel;
    }
}
