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
    private Color portraitColor;

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

    public void Init(PlayerMovement playerCharacter, Color selectedColor)
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
    }

    public void Select()
    {
        imgPortraitBorder.color = selectedColor;
        Debug.Log("<color=green>Select!</color>");
    }

    public void Deselect()
    {
        imgPortraitBorder.color = originalColor;
        Debug.Log("<color=red>Deselect!</color>");
    }
}
