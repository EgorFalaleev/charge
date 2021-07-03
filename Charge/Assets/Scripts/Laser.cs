using UnityEngine;

public class Laser : MonoBehaviour
{
    [Range (1f, 4f)]
    [SerializeField] private float circleRadius = 1f;

    private SpriteRenderer circleSprite;

    private void Awake()
    {
        circleSprite = GetComponent<SpriteRenderer>();
        UpdateRadius(circleRadius);
    }

    public void UpdateRadius(float radius)
    {
        transform.localScale = new Vector2(circleRadius, circleRadius);
    }
}
