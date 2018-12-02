// Date   : 01.12.2018 10:59
// Project: LD43
// Author : bradur

using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    [SerializeField]
    private Sprite spriteOn;

    [SerializeField]
    private Sprite spriteOff;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private bool activated = false;

    private int xPos;
    private int yPos;
    private MapGrid mapGrid;
    private int activationId;

    public void Init(int x, int y, MapGrid mapGrid, TiledSharp.PropertyDict properties, ColorList gridObjectColorList)
    {
        activationId = Tools.IntParseFast(Tools.GetProperty(properties, "activationId"));
        spriteRenderer.color = gridObjectColorList.Colors[activationId].color;
        this.mapGrid = mapGrid;
        xPos = x;
        yPos = y;
        ProcessActivation();
    }

    public void Activate()
    {
        activated = !activated;
        SoundManager.main.PlaySound(activated ? SoundType.SwitchOn : SoundType.SwitchOff);
        ProcessActivation();
    }

    

    private void ProcessActivation()
    {
        foreach (GridObject gridObject in mapGrid.GetAllWithActivationId(activationId))
        {
            //Debug.Log(string.Format("Found portcullis {0} with activationId {1}.", gridObject, activationId));
            Portcullis portCullis = gridObject.GetComponent<Portcullis>();
            if (portCullis != null)
            {
                portCullis.Switch();
            }
        }
        spriteRenderer.sprite = activated ? spriteOn : spriteOff;
    }
}
