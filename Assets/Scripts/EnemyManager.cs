using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float amplitude = 0.5f; // ã‰º‚ÌU‚ê•
    public float speed = 1f;   // ã‰º‚Ì‘¬‚³

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // ‰ŠúˆÊ’u‚ğ‹L˜^
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }
}
