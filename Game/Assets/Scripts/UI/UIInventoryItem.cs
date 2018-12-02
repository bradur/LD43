// Date   : 02.12.2018 09:25
// Project: LD43
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInventoryItem : MonoBehaviour {

    [SerializeField]
    private Text txtItemName;

    [SerializeField]
    private Image imgItem;

    RectTransform rt;

    private float verticalMargin = 5f;

    private PickupObject pickupObject;
    public PickupObject PickupObject {get { return pickupObject; }}
    private UICharacter uICharacter;

    public void Init(PickupObject pickupObject, UICharacter uiCharacter)
    {
        this.pickupObject = pickupObject;
        imgItem.color = pickupObject.Color;
        txtItemName.text = pickupObject.DisplayName;
        rt = GetComponent<RectTransform>();
        rt.SetParent(uiCharacter.transform);
        float height = rt.sizeDelta.y;
        this.uICharacter = uiCharacter;
        int index = uiCharacter.ItemCount + 1;
        uiCharacter.ItemCount += 1;
        rt.anchoredPosition = new Vector2(0, - (height * index + verticalMargin * index));
    }

    public void Kill() {
        uICharacter.ItemCount -= 1;
        Destroy(gameObject);
    }

}
