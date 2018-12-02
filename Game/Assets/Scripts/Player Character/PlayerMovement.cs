// Date   : #CREATIONDATE#
// Project: #PROJECTNAME#
// Author : #AUTHOR#

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq; 

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

    float currentLerpTime = 0f;
    float lerpTime = 1f;

    private int characterId = -1;
    public int CharacterId { get { return characterId; } }

    [SerializeField]
    private bool canJump = false;

    private bool selectedCharacter = false;

    private int xPos;
    private int yPos;
    private MapGrid mapGrid;

    private float distanceToStopLerp;

    private Animator animator;

    public void Init(int x, int y, MapGrid mapGrid, TiledSharp.PropertyDict properties)
    {
        characterId = Tools.IntParseFast(Tools.GetProperty(properties, "characterId"));
        PlayerCharacterManager characterManager = GameManager.main.GetCharacterManager();
        characterManager.AddCharacter(this);
        xPos = x;
        yPos = y;
        animator = GetComponent<Animator>();
        this.mapGrid = mapGrid;
        lerpTime = moveInterval;
    }

    public void SetOptions(float interval, float distanceToStopLerp)
    {
        this.distanceToStopLerp = distanceToStopLerp;
        moveInterval = interval;
    }

    void Update()
    {
        if (selectedCharacter)
        {
            if (KeyManager.main.GetKey(Action.MoveUp))
            {
                AttemptToMove(MoveDirection.Forward, MoveAxis.Vertical);
            }
            else if (KeyManager.main.GetKey(Action.MoveRight))
            {
                AttemptToMove(MoveDirection.Forward, MoveAxis.Horizontal);
            }
            else if (KeyManager.main.GetKey(Action.MoveDown))
            {
                AttemptToMove(MoveDirection.Backward, MoveAxis.Vertical);
            }
            else if (KeyManager.main.GetKey(Action.MoveLeft))
            {
                AttemptToMove(MoveDirection.Backward, MoveAxis.Horizontal);
            }
            else if (!physicallyMoving) {
                animator.SetBool("walking", false);
            }
            if (KeyManager.main.GetKeyDown(Action.InteractWithObject)) {
                foreach(GridObject gridObject in mapGrid.Get(xPos, yPos)) {
                    gridObject.Interact();
                }
            }
        }

        /*if (moving)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer > moveInterval)
            {
                moving = false;
                moveTimer = 0f;
            }
        }*/
    }

    void LateUpdate ()
    {
        if (physicallyMoving)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
 
            float percentage = currentLerpTime / lerpTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, percentage);
            if (Vector3.Distance(targetPosition, transform.position) < distanceToStopLerp) {
                transform.position = targetPosition;
                currentLerpTime = 0f;
                physicallyMoving = false;
                animator.SetBool("walking", false);
            }
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
        mapGrid.MovingAwayFrom(xPos, yPos);
        Vector3 oldPosition = transform.position;
        targetPosition = new Vector3(newPosX * tileSize, newPosY * tileSize, oldPosition.z);
        physicallyMoving = true;
        xPos = newPosX;
        yPos = newPosY;
    }

    private bool isJumping = false;
    private bool physicallyJumping = false;

    private List<DoorKey> keys = new List<DoorKey>();

    public bool AttemptToMove(MoveDirection moveDirection, MoveAxis moveAxis)
    {
        //moving = true;
        animator.SetBool("walking", true);
        int direction = moveDirection == MoveDirection.Forward ? 1 : -1;
        int axis = moveAxis == MoveAxis.Horizontal ? 0 : 1;
        int newPosX = xPos + (axis == 0 ? direction : 0);
        int newPosY = yPos + (axis == 1 ? direction : 0);
        if (!physicallyMoving && mapGrid.CanMoveIntoPosition(newPosX, newPosY))
        {
            MoveToPosition(newPosX, newPosY);
            foreach (GridObject gridObject in mapGrid.GetCopy(xPos, yPos))
            {
                if (gridObject.CollisionType == CollisionType.Pickup)
                {
                    gridObject.GetComponent<PickupObject>().Pickup(this);
                    mapGrid.RemoveObject(xPos, yPos, gridObject);
                    DoorKey key = gridObject.GetComponent<DoorKey>();
                    if (key != null) {
                        key.Hide();
                        keys.Add(key);
                    }
                }
                else if (gridObject.CollisionType == CollisionType.LevelEnd) {
                    gridObject.Interact();
                }
            }
            return true;
        } else {
            JumpableGap gap = mapGrid.GetSpecificObject<JumpableGap>(newPosX, newPosY);
            if (gap != null && canJump) {
            
                if (gap != null) {
                    newPosX = newPosX + (axis == 0 ? direction : 0);
                    newPosY = newPosY + (axis == 1 ? direction : 0);
                    MoveToPosition(newPosX, newPosY);
                }
            }
            else if (keys.Count > 0) {
                Door door = mapGrid.GetSpecificObject<Door>(newPosX, newPosY);
                
                if (door != null) {
                    DoorKey doorKey = keys.First(key => key.KeyId == door.KeyId);
                    if (doorKey != null) {
                        keys.Remove(doorKey);
                        doorKey.RemoveFromInventory();
                        door.Unlock();
                        MoveToPosition(newPosX, newPosY);
                    }
                } 
            }
        }
        return false;
    }
}
