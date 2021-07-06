using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // configuration parameters
    [SerializeField] private float smoothTime = 10f;
    [SerializeField] private Vector3 cameraOffset;

    // references
    [SerializeField] private Transform targetTf;

    // state variables
    private Vector3 velocity = Vector3.zero;

      private void LateUpdate()
      {
          Vector3 desiredPosition = targetTf.position + cameraOffset;
          transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime * Time.deltaTime);
      }
}
