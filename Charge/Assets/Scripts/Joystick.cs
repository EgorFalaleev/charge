using UnityEngine;

public class Joystick : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float movementSpeed;

    // state variables
    private bool isTouchStarted;
    private Vector2 pointA;
    private Vector2 pointB;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // determine the center point of the joystick
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
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
        }
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(direction * movementSpeed * Time.deltaTime);
        transform.up = direction;
    }
}
