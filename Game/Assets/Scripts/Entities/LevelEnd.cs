// Date   : 01.12.2018 23:03
// Project: LD43
// Author : bradur

using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private Color activeColor;

    [SerializeField]
    private Color originalColor;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private bool activated = false;

    void Start () {
        originalColor = spriteRenderer.color;
    }

    public void Toggle(PlayerMovement player) {
        activated = !activated;
        spriteRenderer.color = activated ? activeColor : originalColor;
        GameManager.main.ToggleEnd(activated);
        player.ReachedEnd = activated;
    }

}
