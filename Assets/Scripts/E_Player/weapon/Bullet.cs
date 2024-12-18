using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float Damage = 25.0f;
    public GameObject player;

    // Update is called once per frame
    void FixedUpdate()
    {
        Damage -= 0.2f;
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectsWithTag("Player")[0].transform.position) > 100f)
        {
            Destroy(gameObject);
        }
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
