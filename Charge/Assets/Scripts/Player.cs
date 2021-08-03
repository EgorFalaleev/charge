using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // configuration parameters
    [SerializeField] private float chargeChangeValue = 0.01f;
    [SerializeField] private float movementSpeed = 1f;

    // references
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private SpriteRenderer laserCircle;
    [SerializeField] private Text playerHPText;
    [SerializeField] private GameObject playerChargeCircle;
    [SerializeField] private Image playerLaserChargeCircle;
    private Camera mainCamera;
    private Image playerChargeImage;
    private SceneLoader sceneLoader;

    // state variables
    public bool isAttacking;
    private bool isPlayerMoving = false;
    private float health = 100f;
    [Range(0f, 1f)]
    [SerializeField] private float chargeLevel = 0.5f;

    private void Awake()
    {
        mainCamera = Camera.main;
        sceneLoader = FindObjectOfType<SceneLoader>();
        playerChargeImage = playerChargeCircle.GetComponent<Image>();
    }

    private void Start()
    {
        playerChargeImage.fillAmount = chargeLevel;

        playerHPText.text = "Player HP: " + health;
    }

    private void Update()
    {
        // place the player charge circle on the player object
        playerChargeCircle.transform.position = mainCamera.WorldToScreenPoint(transform.position);
        playerLaserChargeCircle.transform.position = mainCamera.WorldToScreenPoint(transform.position);

        Move();
        HandleLaserCircleTransparency(isPlayerMoving);

        ChangeChargeLevel(isPlayerMoving, CalculateChargeIncreaseSpeed(chargeChangeValue) * Time.deltaTime, 2f * chargeChangeValue * Time.deltaTime);
    }

    public float GetChargeLevel()
    {
        return chargeLevel;
    }

    public bool GetMovementState()
    {
        return isPlayerMoving;
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        playerHPText.text = "Player HP: " + health;
        if (health <= 0)
        {
            sceneLoader.RestartLevel();
        }
    }

    private void HandleLaserCircleTransparency(bool isPlayerMoving)
    {
        // laser circle is semi-transparent when player is moving
        laserCircle.color = isPlayerMoving ? new Color(1, 1, 1, 0.3f) : new Color(1, 1, 1, 1);
        playerLaserChargeCircle.color = isPlayerMoving ? new Color(0.2313726f, 0.6941177f, 0.8980393f, 0.3f) : new Color(0.2313726f, 0.6941177f, 0.8980393f, 1);
    }

    private void ChangeChargeLevel(bool isMoving, float chargeIncreaseSpeed, float chargeDecreaseSpeed)
    {
        if (isMoving)
        {
            isAttacking = false;
            chargeLevel += chargeIncreaseSpeed;
            if (chargeLevel >= 1f) chargeLevel = 1f;
        }
        else if (isAttacking)
        {
            chargeLevel -= chargeDecreaseSpeed;
            if (chargeLevel <= 0f) chargeLevel = 0f;
        }

        playerChargeImage.fillAmount = chargeLevel;
        playerLaserChargeCircle.fillAmount = chargeLevel;
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

    private float CalculateChargeIncreaseSpeed(float initialChargeSpeed)
    {
        float horizontal = Mathf.Abs(joystick.Horizontal);
        float vertical = Mathf.Abs(joystick.Vertical);

        // min charge speed
        if (horizontal < 0.3f && vertical < 0.3f) return initialChargeSpeed / 10f;

        // mid charge speed
        else if (horizontal < 0.7f && vertical < 0.7f) return initialChargeSpeed / 3f;

        // max charge speed
        else return initialChargeSpeed;
    }
}
