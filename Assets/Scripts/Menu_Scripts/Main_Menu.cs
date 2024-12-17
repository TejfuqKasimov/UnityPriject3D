using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour

{
    public void Play_Game()
    {   
        SceneManager.LoadScene("DevelopPlayerTEST");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public GameObject canvasGameObject;

    public void EnableCanvas()
    {
        canvasGameObject.SetActive(true);
    }

    public void DisableCanvas()
    {
        canvasGameObject.SetActive(false);
    }
    void Update() {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            EnableCanvas();
        }
        else {
            DisableCanvas();
        }
    }
}