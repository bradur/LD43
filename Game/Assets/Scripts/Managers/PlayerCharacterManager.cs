// Date   : 01.12.2018 12:33
// Project: LD43
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharacterManager : MonoBehaviour
{
    private List<PlayerMovement> characters = new List<PlayerMovement>();

    [SerializeField]
    private float movementInterval = 0.05f;

    private UIManager uiManager;

    [SerializeField]
    private FollowCamera followCamera;

    private void Start()
    {
        uiManager = GameManager.main.GetUIManager();
    }

    public void AddCharacter(PlayerMovement character)
    {
        character.SetMovementInterval(movementInterval);
        characters.Add(character);
        uiManager.AddCharacter(character);
    }

    public PlayerMovement SelectCharacter(int characterId)
    {
        if (characters.Count >= characterId)
        {
            PlayerMovement selectedCharacter = null;
            Debug.Log(string.Format("Selecting character #{0}.", characterId));
            foreach (PlayerMovement character in characters)
            {
                if (character.CharacterId == characterId)
                {
                    selectedCharacter = character;
                    Debug.Log(uiManager);
                    Debug.Log(character);
                    uiManager.SelectCharacter(character.CharacterId);
                    character.Select();
                    followCamera.SetTarget(selectedCharacter.transform);
                }
                else
                {
                    character.Deselect();
                }
            }
            return selectedCharacter;
        }
        return null;
    }

    private void Update()
    {
        if (KeyManager.main.GetKeyDown(Action.CharacterOne))
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
