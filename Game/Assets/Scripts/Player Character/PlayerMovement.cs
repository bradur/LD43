// Date   : #CREATIONDATE#
// Project: #PROJECTNAME#
// Author : #AUTHOR#

using UnityEngine;
using System.Collections;

public enum MoveDirection
{
    None,
    Forward,
    Backward
}

public enum MoveAxis
{
    None,
    Horizontal,
    Vertical
}

public class PlayerMovement : MonoBehaviour
{

    private float moveInterval = 0.1f;
    private float moveTimer = 0f;
    private bool moving = false;
    private bool physicallyMoving = false;
    private Vector3 targetPosition;

    private float tileSize = 1f;

    private int characterId = -1;
    public int CharacterId { get { return characterId; } }

    private bool selectedCharacter = false;

    private int xPos;
    private int yPos;
    private MapGrid mapGrid;
    public void Init(int x, int y, MapGrid mapGrid, TiledSharp.PropertyDict properties)
    {
        characterId = Tools.IntParseFast(Tools.GetProperty(properties, "characterId"));
        PlayerCharacterManager characterManager = GameManager.main.GetCharacterManager();
        characterManager.AddCharacter(this);
        xPos = x;
        yPos = y;
        this.mapGrid = mapGrid;
    }

    public void SetMovementInterval(float interval)
    {
        moveInterval = interval;
    }

    void Update()
    {
        if (selectedCharacter)
        {
            if (KeyManager.main.GetKeyDown(Action.MoveUp))
            {
                AttemptToMove(MoveDirection.Forward, MoveAxis.Vertical);
            }
            else if (KeyManager.main.GetKeyDown(Action.MoveRight))
            {
                AttemptToMove(MoveDirection.Forward, MoveAxis.Horizontal);
            }
            else if (KeyManager.main.GetKeyDown(Action.MoveDown))
            {
                AttemptToMove(MoveDirection.Backward, MoveAxis.Vertical);
            }
            else if (KeyManager.main.GetKeyDown(Action.MoveLeft))
            {
                AttemptToMove(MoveDirection.Backward, MoveAxis.Horizontal);
            }
        }
        if (moving)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer > moveInterval)
            {
                moving = false;
                moveTimer = 0f;
            }
        }
    }

    void FixedUpdate ()
    {
        if (physicallyMoving)
        {
            transform.position = targetPosition;
            physicallyMoving = false;
        }
    }

    public void Deselect()
    {
        selectedCharacter = false;
    }

    public void Select()
    {
        selectedCharacter = true;
    }

    public void MoveToPosition(int newPosX, int newPosY)
    {
        // animate somehow?
        Vector3 oldPosition = transform.position;
        targetPosition = new Vector3(newPosX * tileSize, newPosY * tileSize, oldPosition.z);
        physicallyMoving = true;
        xPos = newPosX;
        yPos = newPosY;
    }

    public bool AttemptToMove(MoveDirection moveDirection, MoveAxis moveAxis)
    {
        if (!moving)
        {
            moving = true;
            int direction = moveDirection == MoveDirection.Forward ? 1 : -1;
            int axis = moveAxis == MoveAxis.Horizontal ? 0 : 1;
            int newPosX = xPos + (axis == 0 ? direction : 0);
            int newPosY = yPos + (axis == 1 ? direction : 0);
            if (mapGrid.CanMoveIntoPosition(newPosX, newPosY))
            {
                foreach (GridObject gridObject in mapGrid.Get(newPosX, newPosY))
                {
                    if (gridObject.CollisionType == CollisionType.Pickup)
                    {
                        gridObject.GetComponent<PickupObject>().Pickup();
                    }
                }
                MoveToPosition(newPosX, newPosY);
                return true;
            }
        }
        return false;
    }
}
