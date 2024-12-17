using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float Damage = 25.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Damage -= 0.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Debug.Log("hit wall");
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy");
            other.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
