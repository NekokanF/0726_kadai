using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public Transform target;      // 追尾対象
    public float speed = 10f;     // 移動速度
    public float lifeTime = 5f;   // 自動消滅時間
    public float hitDistance = 0.5f; // 当たり判定のしきい値

    void Start()
    {
        Destroy(gameObject, lifeTime); // 一定時間で消える
    }

    void Update()
    {
        if (target == null) return;

        // ターゲット方向
        Vector3 dir = (target.position - transform.position).normalized;

        // 少しずつ向きを変える
        transform.forward = dir;

        // 前進
        transform.position += transform.forward * speed * Time.deltaTime;

        // 命中判定
        if (Vector3.Distance(transform.position, target.position) < hitDistance)
        {
            Debug.Log("ターゲットに命中！");
            // 色を黄色に変える
            Renderer rend = target.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.color = Color.yellow;
            }

            // シェイクする
            EnemyShake shake = target.GetComponent<EnemyShake>();
            if (shake != null)
            {
                shake.Shake();
            }

            Destroy(gameObject); // 弾を消す
        }
    }
}
