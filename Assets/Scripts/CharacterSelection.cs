using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class CharacterSelection : MonoBehaviour
{
    /*private GameMaster gm;*/
    private int selectedCharacterIndex;

    [Header("List of characters")]
    [SerializeField] private List<CharacterSelectObject> characterLists = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSplash;
    /*[SerializeField] private Image backgroundColor;*/

    [Header("Sounds")]
    [SerializeField] private AudioClip arrowClickSFX;
    [SerializeField] private AudioClip characterSelectMusic;

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string characterName;
       /* public Color characterColor;*/
    }
    // Start is called before the first frame update
    void Start()
    {
        
        UpdateCharacterSelect();
        AudioManager.Instance.SetSFXVolume(0.3f);
        /*print(GameMaster.isMarioCheck());*/
        
        /*AudioManager.Instance.PlayMusicWithFade(characterSelectMusic);*/
    }

 /*   private void OnEnable()
    {
        print("onecharacters");
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LeftArrow()
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
            selectedCharacterIndex = characterLists.Count - 1;

        UpdateCharacterSelect();
        AudioManager.Instance.PlaySFX(arrowClickSFX);
        GameMaster.setMarioCheck(!GameMaster.isMarioCheck());
        /*print(GameMaster.isMarioCheck());*/
        

    }
    public void RightArrow()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterLists.Count)
            selectedCharacterIndex = 0;

        UpdateCharacterSelect();
        AudioManager.Instance.PlaySFX(arrowClickSFX);
        GameMaster.setMarioCheck(!GameMaster.isMarioCheck());
        /*print(GameMaster.isMarioCheck());*/

    }

    public void playButtonSelection()
    {
        SceneManager.LoadScene("Level 3");
        /*print(gm.isMarioCheck());*/
        //logic to choose the players!
    }

    public void UpdateCharacterSelect()
    {
        characterSplash.sprite = characterLists[selectedCharacterIndex].splash;
        characterName.text = characterLists[selectedCharacterIndex].characterName;
        /*desiredColor = characterLists[selectedCharacterIndex].characterColor;*/
    }
}
