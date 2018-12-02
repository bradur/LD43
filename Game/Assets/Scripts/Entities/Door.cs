// Date   : 02.12.2018 09:05
// Project: LD43
// Author : bradur

using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    [SerializeField]
    private Sprite spriteUnlocked;

    private GridObject gridObject;
    
    private int keyId;
    public int KeyId {get {return keyId;}}

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public void Init(TiledSharp.PropertyDict properties, ColorList colorList) {
        keyId = Tools.IntParseFast(Tools.GetProperty(properties, "keyId"));
        if (keyId == -1) {
            Debug.Log(string.Format("{0} is missing its keyId!"), this);
        }
        gridObject = GetComponent<GridObject>();
        spriteRenderer.color = colorList.Colors[keyId].color;
    }

    public void Unlock() {
        gridObject.SetCollisionType(CollisionType.None);
        spriteRenderer.sprite = spriteUnlocked;
    }
}
