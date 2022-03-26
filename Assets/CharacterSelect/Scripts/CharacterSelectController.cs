using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectController : MonoBehaviour
{
    private int selectedCharacterIndex;
    private Color desiredColor;

    [Header("List of characters")]
    [SerializeField] private List<CharacterSelectObject> characterLists = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSplash;
    [SerializeField] private Image backgroundColor;

    [Header("Sounds")]
    [SerializeField] private AudioClip arrowClickSFX;
    [SerializeField] private AudioClip characterSelectMusic;

    [Header("Tweaks")]
    [SerializeField] private float backgroundColorTransitionSpeed = 10.0f;

    private void Start()
    {
        UpdateCharacterSelect();
        AudioManager.Instance.PlayMusicWithFade(characterSelectMusic);
    }
    private void Update()
    {
        backgroundColor.color = Color.Lerp(backgroundColor.color, desiredColor, Time.deltaTime * backgroundColorTransitionSpeed);
    }

    public void UpdateCharacterSelect()
    {
        characterSplash.sprite = characterLists[selectedCharacterIndex].splash;
        characterName.text = characterLists[selectedCharacterIndex].characterName;
        desiredColor = characterLists[selectedCharacterIndex].characterColor;
    }

    public void LeftArrow()
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
            selectedCharacterIndex = characterLists.Count-1;

        UpdateCharacterSelect();
        AudioManager.Instance.PlaySFX(arrowClickSFX);
    }
    public void RightArrow()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterLists.Count)
            selectedCharacterIndex = 0;

        UpdateCharacterSelect();
        AudioManager.Instance.PlaySFX(arrowClickSFX);
    }
    public void ConfirmSelection()
    {
        Debug.Log(string.Format("Character {0}:{1} has been chosen", selectedCharacterIndex, characterLists[selectedCharacterIndex].characterName));
    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string characterName;
        public Color characterColor;
    }
}