// Date   : 01.12.2018 12:33
// Project: LD43
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using System.Linq;

public class PlayerCharacterManager : MonoBehaviour
{
    private List<PlayerMovement> characters = new List<PlayerMovement>();

    [SerializeField]
    private float movementInterval = 0.05f;


    [SerializeField]
    private float distanceToStopLerp = 0.1f;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    private bool allowSwitching = true;

    private int previousCharacterId = -1;
    
    public void PreventSwitching () {
        allowSwitching = false;
    }
   
    public void PreventMovement () {
        foreach (PlayerMovement player in characters) {
            player.AllowMovement = false;
        }
    }

    public void KillAllIdleCharacters() {
        foreach (PlayerMovement player in characters) {
            if (!player.ReachedEnd) {
                player.GetComponent<GridObject>().AnimateAndKill();
            }
        }
    }

    public void AddCharacter(PlayerMovement character)
    {
        character.SetOptions(movementInterval, distanceToStopLerp);
        characters.Add(character);
        uiManager.AddCharacter(character);
    }

    public void RemoveCharacter(PlayerMovement character) {
        UICharacter uiCharacter = uiManager.GetUICharacter(character.CharacterId);
        bool selected = false;
        if (uiCharacter != null) {
            selected = uiCharacter.Selected;
        }
        characters.Remove(character);
        uiManager.RemoveCharacter(character);
        if (selected) {
            SelectPrevious();
        }
    }

    private int selectedCharacterId = -1;
    private void SelectNextCharacter() {
        bool selectNext = false;
        bool characterFound = false;
        foreach(PlayerMovement player in characters) {
            if (selectNext) {
                SelectCharacter(player.CharacterId);
                characterFound = true;
                break;
            }
            if (player.CharacterId == selectedCharacterId) {
                selectNext = true;
            }
        }
        if (!characterFound && characters.Count > 0) {
            PlayerMovement player = characters[0];
            SelectCharacter(player.CharacterId);
        }
    }

    public PlayerMovement SelectCharacter(int characterId)
    {
        Debug.Log(string.Format("Selecting character #{0}.", characterId));
        PlayerMovement selectedCharacter = null;
        PlayerMovement selectThisCharacter = null;
        foreach(PlayerMovement character in characters) {
            if (character.CharacterId == characterId) {
                selectThisCharacter = character;
            }
        }
        if (selectThisCharacter != null && !selectThisCharacter.Dying) {
            foreach (PlayerMovement character in characters)
            {
                if (character.CharacterId == characterId)
                {
                    previousCharacterId = uiManager.SelectCharacter(character.CharacterId);
                    if (previousCharacterId == -1) {
                        previousCharacterId = characterId;
                    }
                    if (previousCharacterId != characterId) {
                        SoundManager.main.PlaySound(SoundType.SwitchCharacter);
                    }
                    selectedCharacter = character;
                    selectedCharacterId = characterId;
                    character.Select();
                    virtualCamera.Follow = selectedCharacter.transform;
                }
                else
                {
                    character.Deselect();
                }
            }
        }
        return selectedCharacter;
    }

    public void SelectPrevious() {
        UICharacter uICharacter = uiManager.GetUICharacter(previousCharacterId);
        Debug.Log(string.Format("Selecting previous char: {0} id: {1}", uICharacter, previousCharacterId));
        if (uICharacter == null) {
            if (characters.Count > 0) {
                SelectCharacter(characters[0].CharacterId);
            }
        } else {
            SelectCharacter(previousCharacterId);
        }
    }

    private void Update()
    {
        if (allowSwitching) {
            if (KeyManager.main.GetKeyDown(Action.SwitchCharacter)) {
                SelectNextCharacter();
            }
            else if (KeyManager.main.GetKeyDown(Action.CharacterOne))
            {
                SelectCharacter(1);
            }
            else if (KeyManager.main.GetKeyDown(Action.CharacterTwo))
            {
                SelectCharacter(2);
            }
            else if (KeyManager.main.GetKeyDown(Action.CharacterThree))
            {
                SelectCharacter(3);
            }
            else if (KeyManager.main.GetKeyDown(Action.CharacterFour))
            {
                SelectCharacter(4);
            }
        }
    }
}
