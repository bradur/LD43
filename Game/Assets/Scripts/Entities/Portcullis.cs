// Date   : 01.12.2018 10:13
// Project: LD43
// Author : bradur

using UnityEngine;
using System.Collections;

public class Portcullis : MonoBehaviour
{

    [SerializeField]
    private Sprite spriteOn;

    [SerializeField]
    private Sprite spriteOff;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private BoxCollider2D boxCollider2D;

    [SerializeField]
    private bool switchedOn = false;

    private int xPos;
    private int yPos;
    private MapGrid mapGrid;

    public void Init(int x, int y, MapGrid mapGrid, TiledSharp.PropertyDict properties)
    {
        this.mapGrid = mapGrid;
        xPos = x;
        yPos = y;
        ProcessSwitch();
    }

    public void Switch()
    {
        switchedOn = !switchedOn;
        SoundManager.main.PlaySound(switchedOn ? SoundType.PortcullisOn : SoundType.PortcullisOff);
        ProcessSwitch();
    }

    private void ProcessSwitch()
    {
        if (switchedOn)
        {
            foreach(GridObject gridObject in mapGrid.Get(xPos, yPos))
            {
                gridObject.AnimateAndKill();
            }
        }
        boxCollider2D.enabled = switchedOn;
        spriteRenderer.sprite = switchedOn ? spriteOn : spriteOff;
    }
}
