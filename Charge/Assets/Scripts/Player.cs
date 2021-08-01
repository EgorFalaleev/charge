using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // configuration parameters
    [SerializeField] private float chargeChangeValue = 0.01f;
    [SerializeField] private float movementSpeed = 1f;

    // references
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private GameObject laserCircle;
    [SerializeField] private Slider chargeLevelSlider;
    [SerializeField] private Text playerHPText;
    [SerializeField] private GameObject playerChargeCircle;
    [SerializeField] private GameObject playerLaserChargeCircle;
    private Camera mainCamera;
    private Image playerChargeImage;
    private Image playerLaserChargeImage;
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
        playerLaserChargeImage = playerLaserChargeCircle.GetComponent<Image>();
    }

    private void Start()
    {
        chargeLevelSlider.value = chargeLevel;
        playerChargeImage.fillAmount = chargeLevel;

        playerHPText.text = "Player HP: " + health;
    }

    private void Update()
    {
        // place the player charge circle on the player object
        playerChargeCircle.transform.position = mainCamera.WorldToScreenPoint(transform.position);
        playerLaserChargeCircle.transform.position = mainCamera.WorldToScreenPoint(transform.position);

        Move();
        HandleLaserCircleVisibility(isPlayerMoving);

        ChangeChargeLevel(isPlayerMoving, CalculateChargeIncreaseSpeed(chargeChangeValue) * Time.deltaTime, chargeChangeValue * Time.deltaTime);
    }

    public float GetChargeLevel()
    {
        return chargeLevel;
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

    private void HandleLaserCircleVisibility(bool isPlayerMoving)
    {
        // laser circle enables when player is not moving
        laserCircle.SetActive(!isPlayerMoving);
        playerLaserChargeCircle.SetActive(!isPlayerMoving);
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

        chargeLevelSlider.value = chargeLevel;
        playerChargeImage.fillAmount = chargeLevel;
        playerLaserChargeImage.fillAmount = chargeLevel;
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
