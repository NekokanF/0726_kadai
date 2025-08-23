using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShake : MonoBehaviour
{
    public float shakeDuration = 0.2f;  // —h‚ê‚éŽžŠÔ
    public float shakeMagnitude = 0.2f; // —h‚ê•
    public bool shakeCheck = false;     // —h‚ê‚Ä‚¢‚é‚©‚Ç‚¤‚©

    private Vector3 Pos;   
    private float shakeTime;

    void Start()
    {
        Pos = transform.localPosition; // ‰ŠúˆÊ’u‚ð•ÛŽ
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
            transform.localPosition = Pos + Random.insideUnitSphere * shakeMagnitude;
            shakeTime -= Time.deltaTime;

            if (shakeTime <= 0)
            {
                shakeCheck = false;
                transform.localPosition = Pos; // Œ³‚É–ß‚·
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
