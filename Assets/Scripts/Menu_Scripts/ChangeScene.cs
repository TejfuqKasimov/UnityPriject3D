using UnityEngine;
using UnityEngine.EventSystems;

public class DisableEventSystem : MonoBehaviour
{
    void Start()
    {
        // Найти EventSystem в сцене
        EventSystem eventSystem = EventSystem.current;

        // Проверка, найден ли EventSystem
        if (eventSystem != null)
        {
            // Отключить EventSystem
            eventSystem.gameObject.SetActive(false);
            Debug.Log("Event System отключен.");
        }
        else
        {
            Debug.LogWarning("Event System не найден в сцене.");
        }
    }
}