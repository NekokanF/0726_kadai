using UnityEngine;
using UnityEngine.Rendering;

public class EnemyRandom : MonoBehaviour
{
    public GameObject EnemyPrefab;     // 生成する敵のPrefab
    public Transform player;           // プレイヤーのTransform
    public int enemyCount = 24;        // 生成数
    public float spawnDistance = 10f;  // プレイヤーからの前方距離
    public float spawnwide = 10f;      // 横方向のランダム範囲
    public float spawnvertical = 5f;   // 縦方向のランダム範囲

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // プレイヤーの前方基準位置
            Vector3 basePos = player.position + player.forward * spawnDistance;

            // 前方基準位置の周囲にランダム配置（XZ平面）
            Vector3 randomOffset = new Vector3
            (
           //|----------------X軸---------------|-----------------------Y軸-------------------|-Z軸-|
              Random.Range(-spawnwide, spawnwide), Random.Range(-spawnvertical, spawnvertical), 0
            );
            //出現場所のVectorを作る
            Vector3 spawnPos = basePos + randomOffset;

            // 敵を生成
            Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}
