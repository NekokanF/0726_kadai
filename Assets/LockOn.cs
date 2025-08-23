using System.Collections.Generic;
using UnityEngine;

public class LockOnSystem : MonoBehaviour
{
    public int maxLockOnTargets = 8;   // 最大ロックオン数
    public GameObject bulletPrefab;    // 追尾弾プレハブ
    public Transform firePoint;        // 弾の発射位置
    public float lockOnAngle = 60f; // ロックオン可能角度（前方中心±60度）
    [SerializeField]
    private List<GameObject> lockedTargets = new List<GameObject>();

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            LockOnEnemies();
        }

        // ロックオン対象が視野外なら解除
        CheckLockedTargetsAngle();

        if (Input.GetKeyUp(KeyCode.Z))
        {
            FireHomingBullets();
        }
    }

    void CheckLockedTargetsAngle()
    {
        for (int i = lockedTargets.Count - 1; i >= 0; i--)
        {
            GameObject enemy = lockedTargets[i];
            if (enemy == null)
            {
                lockedTargets.RemoveAt(i);
                continue;
            }

            Vector3 dirToEnemy = (enemy.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToEnemy);

            if (angle > lockOnAngle)
            {
                // 赤色解除
                Renderer rend = enemy.GetComponent<Renderer>();
                if (rend != null)
                    rend.material.color = Color.white;

                lockedTargets.RemoveAt(i);
            }
        }
    }


    void LockOnEnemies()
    {
        // --- 前のロック解除
        foreach (GameObject enemy in lockedTargets)
        {
            if (enemy != null)
            {
                Renderer rend = enemy.GetComponent<Renderer>();
                EnemyShake Eshake = enemy.GetComponent<EnemyShake>();
                if (rend != null && Eshake.shakeCheck == false)
                {
                    rend.material.color = Color.white;
                }
            }
        }
        lockedTargets.Clear();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;

        //正面に最も近い敵（角度的に）
        GameObject centerEnemy = null;
        float maxDot = -1f; // dotが大きいほど正面

        foreach (GameObject enemy in enemies)
        {
            Vector3 dirToEnemy = (enemy.transform.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dirToEnemy);
            if (dot > maxDot) // 一番正面に近い敵を選ぶ
            {
                maxDot = dot;
                centerEnemy = enemy;
            }
        }

        if (centerEnemy == null) return;

        //基準敵をリストに入れる
        lockedTargets.Add(centerEnemy);

        //残りの敵を距離順で追加
        List<GameObject> candidates = new List<GameObject>(enemies);
        candidates.Remove(centerEnemy);


        candidates.Sort((a, b) =>
        {
            float distA = Vector3.Distance(centerEnemy.transform.position, a.transform.position);
            float distB = Vector3.Distance(centerEnemy.transform.position, b.transform.position);
            return distA.CompareTo(distB);
        });

        for (int i = 0; i < Mathf.Min(maxLockOnTargets - 1, candidates.Count); i++)
        {
            lockedTargets.Add(candidates[i]);
        }

        //赤色に変える
        foreach (GameObject enemy in lockedTargets)
        {
            Renderer rend = enemy.GetComponent<Renderer>();
            EnemyShake Eshake = enemy.GetComponent<EnemyShake>();
            if (rend != null && Eshake.shakeCheck == false)
            {
                rend.material.color = Color.red;
            }
        }
    }

    void FireHomingBullets()
    {
        foreach (GameObject enemy in lockedTargets)
        {
            if (enemy == null) continue;

            EnemyShake Eshake = enemy.GetComponent<EnemyShake>();
            Renderer rend = enemy.GetComponent<Renderer>();
            if (rend != null && Eshake.shakeCheck == false)
            {
                rend.material.color = Color.white;
            }
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position + Vector3.up * 0.5f, firePoint.rotation * Quaternion.identity);
            HomingBullet hb = bullet.GetComponent<HomingBullet>();
            hb.target = enemy.transform; // ロックオンした敵をターゲットにする
        }
    }
}