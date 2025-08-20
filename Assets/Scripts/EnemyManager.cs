using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float amplitude = 0.5f; // �㉺�̐U�ꕝ
    public float speed = 1f;   // �㉺�̑���

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // �����ʒu���L�^
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }
}
