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

    public void SelectCharacter(int characterId)
    {
        uiCharacterManager.SelectCharacter(characterId);
    }

    public void AddPickupObject(PickupObject pickupObject, PlayerMovement player) {
        uiInventoryManager.Add(pickupObject, uiCharacterManager.GetUICharacter(player.CharacterId));
    }

    public void RemoveItemFromInventory(PickupObject pickupObject) {
        uiInventoryManager.Remove(pickupObject);
    }

}
