// Date   : 01.12.2018 12:27
// Project: LD43
// Author : bradur

using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {

    void Start () {
    
    }

    void Update () {
    
    }

    private Sprite sprite;
    public Sprite Sprite {get {return sprite;}}
    private Color color;
    public Color Color {get {return color;}}
    private string displayName;
    public string DisplayName {get {return displayName;}}
    public void Init(Sprite sprite, Color color, string displayName) {
        this.sprite = sprite;
        this.color = color;
        this.displayName = displayName;
    }

    private UIManager uiManager;
    public void Pickup(PlayerMovement player)
    {
        // uimanager stuff...
        if (uiManager == null) {
            uiManager = GameManager.main.GetUIManager();
        }
        uiManager.AddPickupObject(this, player);
    }

    public void RemoveFromInventory() {
        // uimanager stuff...
        if (uiManager == null) {
            uiManager = GameManager.main.GetUIManager();
        }
        uiManager.RemoveItemFromInventory(this);
    }
}
