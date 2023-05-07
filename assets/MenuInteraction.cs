using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour
{
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void exitGame()
    {
        Debug.Log("Game has Quit");
        Application.Quit();
    }
}
