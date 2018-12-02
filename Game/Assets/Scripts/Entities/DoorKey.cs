// Date   : 02.12.2018 08:53
// Project: LD43
// Author : bradur

using UnityEngine;
using System.Collections;

public class DoorKey : MonoBehaviour {


    private int keyId;
    public int KeyId {get {return keyId;}}

    private PickupObject pickupObject;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public void Init(TiledSharp.PropertyDict properties, ColorList colors) {
        keyId = Tools.IntParseFast(Tools.GetProperty(properties, "keyId"));
        pickupObject = GetComponent<PickupObject>();
        ColorListColor color = colors.Colors[keyId];
        spriteRenderer.color = color.color;
        if (pickupObject != null) {
            pickupObject.Init(spriteRenderer.sprite, color.color, string.Format("{0} key", color.name));
        }
    }

    public void Hide() {
        spriteRenderer.enabled = false;
    }

    public void RemoveFromInventory() {
        if (pickupObject != null) {
            pickupObject.RemoveFromInventory();
        }
    }
}
