using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public string Level1 = "Level1";
    public string Level2 = "Level2";
    public string Level3 = "Level3";
    public string Mainmenu = "Mainmenu";

    public void Exit()
    {
        Application.Quit();
    }

    public void CambiarEscena1()
    {
        SceneManager.LoadScene(Level1);
    }


    public void CambiarEscena2()
    {
        SceneManager.LoadScene(Level2);
    }


    public void CambiarEscena3()
    {
        SceneManager.LoadScene(Level3);
    }

    public void CambiarEscenaMenu()
    {
        SceneManager.LoadScene(Mainmenu);
    }
}
