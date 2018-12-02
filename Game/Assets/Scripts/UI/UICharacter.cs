// Date   : 01.12.2018 14:20
// Project: LD43
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICharacter : MonoBehaviour {

    [SerializeField]
    private Text txtCharacterId;

    [SerializeField]
    private Image imgPortrait;

    [SerializeField]
    private Image imgPortraitBorder;

    private PlayerMovement character;

    public int CharacterId { get { return character.CharacterId; } }

    private Color selectedColor;
    private Color originalColor;

    RectTransform rt;

    private float horizontalMargin = 10f;

    private int itemCount;

    [SerializeField]
    private GameObject canJumpIndicator;

    public int ItemCount {get {return itemCount;} set {itemCount = value;}}

    public void Init(PlayerMovement playerCharacter, Color portraitColor, Color selectedColor)
    {
        this.selectedColor = selectedColor;
        originalColor = imgPortraitBorder.color;
        character = playerCharacter;
        imgPortrait.color = portraitColor;
        txtCharacterId.text = playerCharacter.CharacterId.ToString();
        rt = GetComponent<RectTransform>();
        float width = rt.sizeDelta.x;
        float index = character.CharacterId - 1;
        rt.anchoredPosition = new Vector2(width * index + horizontalMargin * index, 0);
        if (playerCharacter.CanJump) {
            canJumpIndicator.SetActive(true);
        } else {
            canJumpIndicator.SetActive(false);
        }
    }

    private bool selected = false;
    public bool Selected {get {return selected;}}
    public void Select()
    {
        selected = true;
        imgPortraitBorder.color = selectedColor;
        //Debug.Log("<color=green>Select!</color>");
    }

    public void Deselect()
    {
        selected = false;
        imgPortraitBorder.color = originalColor;
        //Debug.Log("<color=red>Deselect!</color>");
    }

    public void Kill() {
        Destroy(gameObject);
    }
}
