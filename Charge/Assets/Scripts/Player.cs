using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // configuration parameters
    [SerializeField] private float chargeChangeSpeed = 0.01f;
    [SerializeField] private float movementSpeed = 1f;

    // references
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private GameObject laserCircle;
    [SerializeField] private Slider chargeLevelSlider;
    [SerializeField] private Text playerHPText;
    [SerializeField] private GameObject playerChargeImage; 
    private Camera camera;

    // state variables
    public bool isAttacking;
    private bool isPlayerMoving = false;
    private float health = 100f;
    [Range(0f, 1f)]
    [SerializeField] private float chargeLevel = 0.5f;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        chargeLevelSlider.value = chargeLevel;
        playerHPText.text = "Player HP: " + health;
    }

    private void Update()
    {
        playerChargeImage.transform.position = camera.WorldToScreenPoint(transform.position);

        Move();
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

        chargeLevelSlider.value = chargeLevel;
    }

    private void Move()
    {
        Vector3 direction = Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical;

        if (direction == Vector3.zero) isPlayerMoving = false;
        else
        {
            isPlayerMoving = true;
            transform.Translate(direction * movementSpeed * Time.deltaTime);
        }
    }
}
