using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float Damage = 25.0f;
    [SerializeField] private int BulletForce = 5000;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * BulletForce);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Damage -= 0.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
