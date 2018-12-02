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


    [SerializeField]
    private TextList quotes;

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


    [SerializeField]
    private Transform uiPopupContainer;

    [SerializeField]
    private UIPopup uiPopupPrefab;

    private UIPopup currentUIPopup = null;
    public void ShowPopup(string title, string message, string info) {
        if (currentUIPopup != null) {
            currentUIPopup.Kill();
        }
        Debug.Log(string.Format("Open menu with title: {0}, message: {1}", title, message));
        UIPopup uiPopup = Instantiate(uiPopupPrefab, uiPopupContainer);
        uiPopup.Init(title, message, info);
        currentUIPopup = uiPopup;
    }

    public string GetRandomQuote() {
        Debug.Log(quotes.Texts.Count);
        return quotes.Texts[Random.Range(0, quotes.Texts.Count - 1)].text;
    }

    public void ShowLevelEndMenu() {

        string info = string.Format(
            "> Press {0} to continue\n> Press {1} to quit\n> Press {2} to restart level",
            KeyManager.main.GetKeyString(Action.NextLevel),
            KeyManager.main.GetKeyString(Action.Quit),
            KeyManager.main.GetKeyString(Action.Restart)
        );

        ShowPopup("Great!", GetRandomQuote(), info);
    }

    public void ShowTheEndMenu() {

        string info = string.Format(
            "> Press {0} to quit\n> Press {1} to restart level",
            KeyManager.main.GetKeyString(Action.Quit),
            KeyManager.main.GetKeyString(Action.Restart)
        );

        ShowPopup("The end!", "That's all, folks!", info);
    }

    public void OpenPauseMenu() {
        string info = string.Format(
            "> Press {0} to close this dialog\n> Press {1} to restart level",
            KeyManager.main.GetKeyString(Action.CloseMenu),
            KeyManager.main.GetKeyString(Action.Restart)
        );
        ShowPopup("Paused", "You've paused the game.", info);
    }

    public void ClosePauseMenu() {
        currentUIPopup.Kill();
    }

}
