using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class ManaChange : MonoBehaviour
{
    [SerializeField]private float MaxMana = 100f;
    private float _currentMana;
    public event Action<float> ManaChanged;
    private void Start()
    {
        _currentMana = MaxMana;
    }
    public void ChangeMana(float changeAmount) 
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
            float _currentManaAsPersentage = (float)_currentMana/ MaxMana;
            ManaChanged?.Invoke(_currentManaAsPersentage);
        }
    }

        

}
