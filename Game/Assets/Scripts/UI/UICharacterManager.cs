// Date   : 01.12.2018 14:19
// Project: LD43
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UICharacterManager : MonoBehaviour
{

    [SerializeField]
    private UICharacter uiCharacterPrefab;

    [SerializeField]
    private Color selectedCharacterBorderColor;

    [SerializeField]
    private Transform characterParent;

    private List<UICharacter> uiCharacters = new List<UICharacter>();

    public void AddCharacter(PlayerMovement playerMovement)
    {
        UICharacter uiCharacter = Instantiate(uiCharacterPrefab, characterParent);
        uiCharacter.Init(playerMovement, selectedCharacterBorderColor);
        uiCharacters.Add(uiCharacter);
    }

    public void SelectCharacter(int characterId)
    {
        foreach (UICharacter uiCharacter in uiCharacters)
        {
            if (uiCharacter.CharacterId == characterId)
            {
                uiCharacter.Select();
            }
            else
            {
                uiCharacter.Deselect();
            }
        }
    }
}
