// Date   : 01.12.2018 14:19
// Project: LD43
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UICharacterManager : MonoBehaviour
{

    [SerializeField]
    private UICharacter uiCharacterPrefab;

    [SerializeField]
    private Color selectedCharacterBorderColor;

    [SerializeField]
    private Transform characterParent;

    private List<UICharacter> uiCharacters = new List<UICharacter>();

    [SerializeField]
    private ColorList colorList;

    public void AddCharacter(PlayerMovement playerMovement)
    {
        UICharacter uiCharacter = Instantiate(uiCharacterPrefab, characterParent);
        uiCharacter.Init(playerMovement, colorList.Colors[playerMovement.CharacterId].color, selectedCharacterBorderColor);
        uiCharacters.Add(uiCharacter);
    }

    public void RemoveCharacter(PlayerMovement playerMovement) {
        UICharacter uiCharacter = uiCharacters.First(character => character.CharacterId == playerMovement.CharacterId);
        if (uiCharacter != null) {
            uiCharacters.Remove(uiCharacter);
            uiCharacter.Kill();
        }
    }

    public int SelectCharacter(int characterId)
    {
        int previousCharacterId = -1;
        foreach (UICharacter uiCharacter in uiCharacters)
        {
            if (uiCharacter.CharacterId == characterId)
            {
                uiCharacter.Select();
            }
            else
            {
                if (uiCharacter.Selected) {
                    previousCharacterId = uiCharacter.CharacterId;
                }
                uiCharacter.Deselect();
            }
        }
        return previousCharacterId;
    }

    public UICharacter GetUICharacter(int characterId) {
        UICharacter searchedCharacter = null;
        foreach (UICharacter uiCharacter in uiCharacters)
        {
            if (uiCharacter.CharacterId == characterId)
            {
                searchedCharacter = uiCharacter;
                break;
            }
        }
        return searchedCharacter;
    }
}
