using UnityEngine;

public class Player : MonoBehaviour
{
    private float PlayerHealth = 100f;
    
    public void GetDamage(float Damage)
    {
        PlayerHealth -= Damage;
        if (PlayerHealth <= 0)
        {
            // End Game
            Debug.Log("End Game");
        }
    }
}
