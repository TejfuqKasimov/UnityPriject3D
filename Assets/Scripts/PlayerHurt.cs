using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerHurt : MonoBehaviour
{
    public int HP = 100;
    public GameObject bloodySreen;

    public bool isDead = false;
    public TextMeshProUGUI playerHealthUI;
    public GameObject gameOverUI;
    private void Start()
    {
        playerHealthUI.text = $"Health: {HP}";
    }
    public void TakeDamage(int damageAmount) 
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            PlayerDead();
        }
        else
        {
            StartCoroutine(BloodyScreenEffect());
            playerHealthUI.text = $"Health: {HP}";
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

        GetComponent<PlayerController>().enabled = false;
        GetComponent<PersonLook>().enabled = false;
        
        GetComponentInChildren<Animator>().enabled = true;
        playerHealthUI.gameObject.SetActive(false);

        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOverUI());
    }
}
