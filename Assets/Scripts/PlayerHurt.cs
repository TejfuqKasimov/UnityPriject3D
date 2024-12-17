using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [SerializeField]private int MaxHealth = 100;
    private int _currentHealth;
    public GameObject bloodySreen;
    public event Action<float> HealthChanged;
    public bool isDead = false;
    public TextMeshProUGUI playerHealthUI;
    public GameObject gameOverUI;
    private void Start()
    {
        playerHealthUI.text = "Health: ";
        _currentHealth = MaxHealth;
    }
    public void ChangeHealth(int changeAmount) 
    {
        _currentHealth += changeAmount;

        if (_currentHealth <= 0)
        {
            PlayerDead();
        }
        else
        {
            if (changeAmount < 0) {
                StartCoroutine(BloodyScreenEffect());
                playerHealthUI.text = "Health: ";
                float _currentHealthAsPersentage = (float)_currentHealth / MaxHealth;
                HealthChanged?.Invoke(_currentHealthAsPersentage);
            }
        }
    }

    private IEnumerator BloodyScreenEffect()
    {
        if (!bloodySreen.activeInHierarchy) 
        {
            bloodySreen.SetActive(true);
        }

        var image = bloodySreen.GetComponentInChildren<Image>();

        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 1f;
        float elapsedTime = 0f;
 
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
  
        if (bloodySreen.activeInHierarchy)
        {
            bloodySreen.SetActive(false);
        }
    }
    
    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);
    }

    private void PlayerDead() 
    {
        isDead = true;

        HealthChanged?.Invoke(0);

        GetComponent<PlayerController>().enabled = false;
        GetComponent<PersonLook>().enabled = false;
        
        GetComponentInChildren<Animator>().enabled = true;
        playerHealthUI.gameObject.SetActive(false);

        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOverUI());
    }
}
