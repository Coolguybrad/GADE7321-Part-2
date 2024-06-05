using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public void multiplayerScene()
    {
        SceneManager.LoadScene(1);
    }

    public void easySingleplayer()
    {
        SceneManager.LoadScene(2);
    }

    public void mediumSingleplayer()
    {
        SceneManager.LoadScene(3);
    }

    public void hardSingleplayer()
    {
        SceneManager.LoadScene(4);
    }
}
