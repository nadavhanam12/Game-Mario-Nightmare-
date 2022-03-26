using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBtnScripts : MonoBehaviour
{
    public GameObject playerSelection;
    public GameObject TwoPlayerSelection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        
    }
    
    public void active2PlayerSelection()
    {
        GameMaster.is2Players = true;
        TwoPlayerSelection.SetActive(true);
        playerSelection.SetActive(false);
    }

    public void activePlayerSelectionOnePlayer()
    {
        GameMaster.is2Players = false;
        playerSelection.SetActive(true);
        TwoPlayerSelection.SetActive(false);
    }
}
