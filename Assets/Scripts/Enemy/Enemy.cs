using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float HealthPoint = 100f;

    public void TakeDamage(float damage)
    {
        HealthPoint -= damage;

        if (HealthPoint <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log(HealthPoint);
    }
}
