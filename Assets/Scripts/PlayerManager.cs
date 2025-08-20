using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float speed_;
    [SerializeField]
    private float rotate_speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed_ * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed_ * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.eulerAngles -= new Vector3(rotate_speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.eulerAngles += new Vector3(rotate_speed * Time.deltaTime, 0, 0);
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles -= new Vector3(0, rotate_speed * Time.deltaTime, 0);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles += new Vector3(0, rotate_speed * Time.deltaTime, 0);
        }
    }
}
