// Date   : 02.12.2018 09:24
// Project: LD43
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class UIInventoryManager : MonoBehaviour {

    [SerializeField]
    private UIInventoryItem uiInventoryItemPrefab;

    private List<UIInventoryItem> uiItems = new List<UIInventoryItem>();

    public void Add(PickupObject pickupObject, UICharacter uiCharacter)
    {
        UIInventoryItem uIInventoryItem = Instantiate(uiInventoryItemPrefab);
        uIInventoryItem.Init(pickupObject, uiCharacter);
        uiItems.Add(uIInventoryItem);
    }

    public void Remove(PickupObject pickupObject)
    {
        UIInventoryItem uiItem = uiItems.First(item => item.PickupObject == pickupObject);
        uiItems.Remove(uiItem);
        uiItem.Kill();
    }

}
