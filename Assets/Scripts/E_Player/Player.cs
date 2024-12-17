using UnityEngine;

public class Player : MonoBehaviour
{
    private float PlayerHealth = 100f;
    public bool TakeDam = false;
    PlayerHurt playerHurt;
    public void GetDamage(float Damage)
    {
        PlayerHealth -= Damage;
        playerHurt.ChangeHealth(-Damage);
        if (PlayerHealth <= 0)
        {
            // End Game
            Debug.Log("End Game");
        }
    }
    private void Start()
    {
        playerHurt = GetComponent<PlayerHurt>();
    }

    void Update()
    {
        Debug.Log(PlayerHealth);
        if (TakeDam || Input.GetKeyDown(KeyCode.T))
        {
            TakeDam = false;
            GetDamage(25f);
        }
    }
}
