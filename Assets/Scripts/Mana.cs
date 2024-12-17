using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField]private Image Manafilling;

    [SerializeField] Mana mana;



    private void Awake() 
    {
        mana.ManaChanged += OnManaChanged;

    }

    private void OnDestroy()
    {
        mana.ManaChanged -= OnManaChanged;
    }
    private void OnManaChanged(float valueAsPerventage) 
    {  
        Manafilling.fillAmount = valueAsPerventage;
    }

}