using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShake : MonoBehaviour
{
    public float shakeDuration = 0.2f; // �V�F�C�N����
    public float shakeMagnitude = 0.2f; // �h�ꕝ
    public bool shakeCheck = false;

    private Vector3 originalLocalPos;
    private float shakeTime;

    void Start()
    {
        originalLocalPos = transform.localPosition; // �����ʒu��ێ�
    }

    public void Shake()
    {
        shakeTime = shakeDuration;
    }

    void Update()
    {
        if (shakeTime > 0)
        {
            shakeCheck = true;
            transform.localPosition = originalLocalPos + Random.insideUnitSphere * shakeMagnitude;
            shakeTime -= Time.deltaTime;

            if (shakeTime <= 0)
            {
                shakeCheck = false;
                transform.localPosition = originalLocalPos; // ���ɖ߂�
                Renderer rend = this.gameObject.GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.material.color = Color.white;
                }
            }
        }
        if (shakeCheck==true)
        {
            Renderer rend = this.gameObject.GetComponent<Renderer>();
            rend.material.color = Color.yellow;
        }
    }
}
