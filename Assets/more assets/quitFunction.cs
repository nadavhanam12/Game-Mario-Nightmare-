using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quitFunction : MonoBehaviour
{
private GameMaster gm;
private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    public void doExitGame()
    {
        Application.Quit();
    }
    public void doBackToMenu()
    {
        gm.lastCheckPointPosPlayerOne = new Vector2(4, 2);
        gm.lastCheckPointPosPlayerTwo = new Vector3(6, 2);
        SceneManager.LoadScene("OpeningScene");
    }
}
