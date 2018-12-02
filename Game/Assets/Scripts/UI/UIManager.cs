// Date   : #CREATIONDATE#
// Project: #PROJECTNAME#
// Author : #AUTHOR#

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private UICharacterManager uiCharacterManager;


    [SerializeField]
    private UIInventoryManager uiInventoryManager;


    public void AddCharacter(PlayerMovement playerMovement)
    {
        uiCharacterManager.AddCharacter(playerMovement);
    }

    public void RemoveCharacter(PlayerMovement playerMovement) {
        uiCharacterManager.RemoveCharacter(playerMovement);
    }

    public UICharacter GetUICharacter(int characterId) {
        return uiCharacterManager.GetUICharacter(characterId);
    }

    public int SelectCharacter(int characterId)
    {
        return uiCharacterManager.SelectCharacter(characterId);
    }

    public void AddPickupObject(PickupObject pickupObject, PlayerMovement player) {
        uiInventoryManager.Add(pickupObject, GetUICharacter(player.CharacterId));
    }

    public void RemoveItemFromInventory(PickupObject pickupObject) {
        uiInventoryManager.Remove(pickupObject);
    }

}
