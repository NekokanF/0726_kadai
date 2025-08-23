using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float amplitude = 0.5f; // 上下の振れ幅
    public float speed = 1f;   　　// 上下の移動の速さ

    private Vector3 startPos;　　　//　初期位置

    void Start()
    {
        startPos = transform.position; // 初期位置を記録
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }
}
