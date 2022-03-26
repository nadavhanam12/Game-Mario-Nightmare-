using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoCharacterController : MonoBehaviour
{
     /*private GameMaster gm;*/
    private int selectedCharacterIndex;

    [Header("List of characters")]
    [SerializeField] private List<CharacterSelectObject> characterLists = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSplash1;
    [SerializeField] private Image characterSplash2;
    /*[SerializeField] private Image backgroundColor;*/

    [Header("Sounds")]
    [SerializeField] private AudioClip arrowClickSFX;
    [SerializeField] private AudioClip characterSelectMusic;

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        /*public string characterName;*/
        /* public Color characterColor;*/
    }
    // Start is called before the first frame update
    void Start()
    {
        
        /*GameMaster.is2Players = true;*/
        /*print(GameMaster.is2Players);*/
        UpdateCharacterSelect();
        /*print(GameMaster.isMarioCheck());*/
        
        /*AudioManager.Instance.PlayMusicWithFade(characterSelectMusic);*/
    }

   /* private void OnEnable()
    {
        print("twocharacters");
    }*/

    // Update is called once per frame
    void Update()
    {

    }
    public void LeftArrow()
    {
        /*UpdateCharacterSelect(characterSplash1);
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
            selectedCharacterIndex = characterLists.Count - 1;*/
        GameMaster.marioFirst = !GameMaster.marioFirst;
        UpdateCharacterSelect();
        AudioManager.Instance.PlaySFX(arrowClickSFX);
       /* GameMaster.setMarioCheck(!GameMaster.isMarioCheck());
        print(GameMaster.isMarioCheck());*/


    }
    public void RightArrow()
    {
        GameMaster.marioFirst = !GameMaster.marioFirst;
        UpdateCharacterSelect();
        /*selectedCharacterIndex++;
        if (selectedCharacterIndex == characterLists.Count)
            selectedCharacterIndex = 0;

        UpdateCharacterSelect(characterSplash2);*/
        AudioManager.Instance.PlaySFX(arrowClickSFX);
        /*GameMaster.setMarioCheck(!GameMaster.isMarioCheck());
        print(GameMaster.isMarioCheck());*/

    }

    public void playButtonSelection()
    {
        SceneManager.LoadScene("Level 3");
        /*print(gm.isMarioCheck());*/
        //logic to choose the players!
    }

    public void UpdateCharacterSelect()
    {
        characterSplash1.sprite = characterLists[selectedCharacterIndex].splash;

        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterLists.Count)
            selectedCharacterIndex = 0;

        characterSplash2.sprite = characterLists[selectedCharacterIndex].splash;
        /*characterName.text = characterLists[selectedCharacterIndex].characterName;*/
        /*desiredColor = characterLists[selectedCharacterIndex].characterColor;*/

    }
}
