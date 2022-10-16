using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    public float overTime = 1.0f;
    private void Start()
    {
        gameObject.tag = "bullet";
        Destroy(gameObject,overTime);
    }
    private void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="trap")
        {
            Destroy(gameObject);
        }
    }
}
