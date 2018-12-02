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

    private GridObject gridObject;

    public void Init(int x, int y, MapGrid mapGrid, TiledSharp.PropertyDict properties, ColorList gridObjectColorList)
    {
        int activationId = Tools.IntParseFast(Tools.GetProperty(properties, "activationId"));
        gridObject = GetComponent<GridObject>();
        this.mapGrid = mapGrid;
        xPos = x;
        yPos = y;
        spriteRenderer.color = gridObjectColorList.Colors[activationId].color;
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
            foreach(GridObject mapGridObject in mapGrid.Get(xPos, yPos))
            {
                Debug.Log(string.Format("Portcullis coming down on: {0}", mapGridObject));
                mapGridObject.AnimateAndKill();
            }
        }
        gridObject.SetCollisionType(switchedOn ? CollisionType.Wall : CollisionType.Floor);
        boxCollider2D.enabled = switchedOn;
        spriteRenderer.sprite = switchedOn ? spriteOn : spriteOff;
    }
}
