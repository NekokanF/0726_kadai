using System.Collections.Generic;
using UnityEngine;

public class LockOnSystem : MonoBehaviour
{
    public int maxLockOnTargets = 8;   // �ő働�b�N�I����
    public GameObject bulletPrefab;    // �ǔ��e�v���n�u
    public Transform firePoint;        // �e�̔��ˈʒu
    public float lockOnAngle = 60f; // ���b�N�I���\�p�x�i�O�����S�}60�x�j
    [SerializeField]
    private List<GameObject> lockedTargets = new List<GameObject>();

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            LockOnEnemies();
        }

        // ���b�N�I���Ώۂ�����O�Ȃ����
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
                // �ԐF����
                Renderer rend = enemy.GetComponent<Renderer>();
                if (rend != null)
                    rend.material.color = Color.white;

                lockedTargets.RemoveAt(i);
            }
        }
    }


    void LockOnEnemies()
    {
        // --- �O�̃��b�N����
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

        //���ʂɍł��߂��G�i�p�x�I�Ɂj
        GameObject centerEnemy = null;
        float maxDot = -1f; // dot���傫���قǐ���

        foreach (GameObject enemy in enemies)
        {
            Vector3 dirToEnemy = (enemy.transform.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dirToEnemy);
            if (dot > maxDot) // ��Ԑ��ʂɋ߂��G��I��
            {
                maxDot = dot;
                centerEnemy = enemy;
            }
        }

        if (centerEnemy == null) return;

        //��G�����X�g�ɓ����
        lockedTargets.Add(centerEnemy);

        //�c��̓G���������Œǉ�
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

        //�ԐF�ɕς���
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
            hb.target = enemy.transform; // ���b�N�I�������G���^�[�Q�b�g�ɂ���
        }
    }
}