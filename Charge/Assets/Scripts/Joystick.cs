using UnityEngine;

public class Joystick : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float movementSpeed;
    [SerializeField] Transform innerJoystickCircle;
    [SerializeField] Transform outerJoystickCircle;

    // cached references
    private SpriteRenderer innerJoystickSprite;
    private SpriteRenderer outerJoystickSprite;

    // state variables
    private bool isTouchStarted;
    private Vector2 pointA;
    private Vector2 pointB;

    private void Awake()
    {
        innerJoystickSprite = innerJoystickCircle.GetComponent<SpriteRenderer>();
        outerJoystickSprite = outerJoystickCircle.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // determine the center point of the joystick
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            DrawJoystick();
        }

        if (Input.GetMouseButton(0))
        {
            isTouchStarted = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else isTouchStarted = false;

        // touch movement
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                pointA = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z));
                DrawJoystick();
            }

            if (touch.phase == TouchPhase.Moved)
            {
                isTouchStarted = true;
                pointB = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z));
            }
            else isTouchStarted = false;
        }
    }

    private void FixedUpdate()
    {
        if (isTouchStarted)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1f);

            Move(direction);

            innerJoystickCircle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
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
        transform.up = direction;
    }

    private void DrawJoystick()
    {
        innerJoystickCircle.transform.position = pointA;
        outerJoystickCircle.transform.position = pointA;
        innerJoystickSprite.enabled = true;
        outerJoystickSprite.enabled = true;
    }
}
