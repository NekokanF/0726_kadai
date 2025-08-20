using UnityEngine;
using UnityEngine.Rendering;

public class EnemyRandom : MonoBehaviour
{
    public GameObject EnemyPrefab;    // ��������G��Prefab
    public Transform player;          // �v���C���[��Transform
    public int enemyCount = 24;       // ������
    public float spawnDistance = 10f; // �v���C���[����̑O������
    public float spawnwide = 10f;      // �������̃����_���͈�
    public float spawnvertical = 5f;  // �c�����̃����_���͈�

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // �v���C���[�̑O����ʒu
            Vector3 basePos = player.position + player.forward * spawnDistance;

            // �O����ʒu�̎��͂Ƀ����_���z�u�iXZ���ʁj
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnwide, spawnwide), // X����
                Random.Range(-spawnvertical, spawnvertical), // Y�͌Œ�i�n�ʁj
                0                                        // Z����
            );

            Vector3 spawnPos = basePos + randomOffset;

            // �G�𐶐�
            Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}
