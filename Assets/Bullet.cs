using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public Transform target;      // �ǔ��Ώ�
    public float speed = 10f;     // �ړ����x
    public float lifeTime = 5f;   // �������Ŏ���
    public float hitDistance = 0.5f; // �����蔻��̂������l

    void Start()
    {
        Destroy(gameObject, lifeTime); // ��莞�Ԃŏ�����
    }

    void Update()
    {
        if (target == null) return;

        // �^�[�Q�b�g����
        Vector3 dir = (target.position - transform.position).normalized;

        // ������������ς���
        transform.forward = dir;

        // �O�i
        transform.position += transform.forward * speed * Time.deltaTime;

        // ��������
        if (Vector3.Distance(transform.position, target.position) < hitDistance)
        {
            Debug.Log("�^�[�Q�b�g�ɖ����I");
            // �F�����F�ɕς���
            Renderer rend = target.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.color = Color.yellow;
            }

            // �V�F�C�N����
            EnemyShake shake = target.GetComponent<EnemyShake>();
            if (shake != null)
            {
                shake.Shake();
            }

            Destroy(gameObject); // �e������
        }
    }
}
