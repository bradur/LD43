// Date   : #CREATIONDATE#
// Project: #PROJECTNAME#
// Author : #AUTHOR#

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private UICharacterManager uiCharacterManager;

    public void AddCharacter(PlayerMovement playerMovement)
    {
        uiCharacterManager.AddCharacter(playerMovement);
    }

    public void SelectCharacter(int characterId)
    {
        uiCharacterManager.SelectCharacter(characterId);
    }

}
