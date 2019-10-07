using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    
    public  void RetrunMain()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame1()
    {
        SceneManager.LoadScene(1);
    }
    public void StartGame2()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        //gameObject.GetComponent<AudioSource>().Play();
    }
}
