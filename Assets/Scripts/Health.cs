using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]private Image HpBarfilling;

    [SerializeField] PlayerHurt health;



    private void Awake() 
    {
        health.HealthChanged += OnHealthChanged;

    }

    private void OnDestroy()
    {
        health.HealthChanged -= OnHealthChanged;
    }
    private void OnHealthChanged(float valueAsPerventage) 
    {  
        HpBarfilling.fillAmount = valueAsPerventage;
    }

}