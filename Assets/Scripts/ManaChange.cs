using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class Mana : MonoBehaviour
{
    [SerializeField]private int MaxMana = 100;
    private int _currentMana;
    public event Action<float> ManaChanged;
    private void Start()
    {
        _currentMana = MaxMana;
    }
    public void ChangeMana(int changeAmount) 
    {
        _currentMana += changeAmount;

        if (_currentMana <= 0)
        {
            _currentMana = 0;
            float _currentManaAsPersentage = (float)_currentMana/ MaxMana;
            ManaChanged?.Invoke(_currentManaAsPersentage);
        }
        else
        {
            if (changeAmount < 0) {
                float _currentManaAsPersentage = (float)_currentMana/ MaxMana;
                ManaChanged?.Invoke(_currentManaAsPersentage);
            }
        }
    }

        

}
