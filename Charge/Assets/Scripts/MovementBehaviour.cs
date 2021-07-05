using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    // configuration parameters
    [Range (1f, 7f)]
    [SerializeField] float movementSpeed;
    [SerializeField] Transform innerJoystickCircle;
    [SerializeField] Transform outerJoystickCircle;

    // cached references
    private SpriteRenderer innerJoystickSprite;
    private SpriteRenderer outerJoystickSprite;

    // state variables
    private bool isTouchStarted;
    private Vector2 joystickCenter;
    private Vector2 joystickDirection;
    private Vector2 screenBounds;
    private Vector2 playerBounds;

    private void Awake()
    {
        innerJoystickSprite = innerJoystickCircle.GetComponent<SpriteRenderer>();
        outerJoystickSprite = outerJoystickCircle.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // screen bounds in world space
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        playerBounds = new Vector2(GetComponent<SpriteRenderer>().bounds.extents.x, GetComponent<SpriteRenderer>().bounds.extents.y);
    }

    private void Update()
    {
        // keep player inside the screen
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -screenBounds.x + playerBounds.x, screenBounds.x - playerBounds.x), Mathf.Clamp(transform.position.y, -screenBounds.y + playerBounds.y, screenBounds.y - playerBounds.y));

        if (Input.GetMouseButtonDown(0))
        {
            // determine the center point of the joystick
            joystickCenter = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            DrawJoystick();

        }
        if (Input.GetMouseButton(0))
        {
            isTouchStarted = true;
            joystickDirection = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }

        else isTouchStarted = false;

        if (isTouchStarted)
        {
            Vector2 offset = joystickDirection - joystickCenter;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1f);

            Move(direction);

            innerJoystickCircle.transform.position = new Vector2(joystickCenter.x + direction.x, joystickCenter.y + direction.y);
        }
        else
        {
            innerJoystickSprite.enabled = false;
            outerJoystickSprite.enabled = false;
        }
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    private void DrawJoystick()
    {
        innerJoystickCircle.transform.position = joystickCenter;
        outerJoystickCircle.transform.position = joystickCenter;
        innerJoystickSprite.enabled = true;
        outerJoystickSprite.enabled = true;
    }

    public bool IsPlayerMoving()
    {
        return isTouchStarted;
    }
}
