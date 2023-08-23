using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    
   public void Quit()
    {
        Application.Quit(); 
    }
    public void SoundOn()
    {
        
    }
    public void SoundOff()
    {

    }
    public void Settings()
    {

    }
    public void GenrateCode()
    {

    }
    public void SendInvatation()
    {

    }
    public void Card1()
    {
        SceneManager.LoadScene(3);
    }
    public void Card2()
    {
        SceneManager.LoadScene(4);
    }
    public void Card3()
    {
        SceneManager.LoadScene(5);
    }
    public void Card4()
    {
        SceneManager.LoadScene(6);
    }
}
