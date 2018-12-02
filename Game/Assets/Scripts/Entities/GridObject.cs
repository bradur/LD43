// Date   : 01.12.2018 10:22
// Project: LD43
// Author : bradur

using UnityEngine;
using System.Collections;

public enum CollisionType
{
    None,
    Floor,
    Wall,
    Pickup,
    Switch,
    Gap,
    LevelEnd,
    PressurePlate
}

public class GridObject : MonoBehaviour
{

    private Animator animator;
    private int activationId;
    public int ActivationId { get { return activationId; } }

    [SerializeField]
    private CollisionType collisionType;
    public CollisionType CollisionType { get { return collisionType; } }

    [SerializeField]
    private bool killable = false;

    [SerializeField]
    private GameObject gridObjectParticleSystem;

    public void Init(int x, int y, MapGrid mapGrid, TiledSharp.PropertyDict properties, ColorList colorList, ColorList characterColorList, ColorList gridObjectColorList)
    {
        animator = GetComponent<Animator>();
        activationId = Tools.IntParseFast(Tools.GetProperty(properties, "activationId"));
        foreach (Portcullis portcullis in GetComponents<Portcullis>()) {
            portcullis.Init(x, y, mapGrid, properties, gridObjectColorList);
        }
        foreach(Switch switchObject in GetComponents<Switch>()) {
            switchObject.Init(x, y, mapGrid, properties, gridObjectColorList);
        }
        foreach(PressurePlate pressurePlate in GetComponents<PressurePlate>()) {
            pressurePlate.Init(x, y, mapGrid, properties, gridObjectColorList);
        }
        foreach(PlayerMovement playerMovement in GetComponents<PlayerMovement>())
        {
            playerMovement.Init(x, y, mapGrid, properties, characterColorList);
        }
        foreach(DoorKey doorKey in GetComponents<DoorKey>()) {
            doorKey.Init(properties, gridObjectColorList);
        }
        foreach(Door door in GetComponents<Door>()) {
            door.Init(properties, gridObjectColorList);
        }
    }

    public void Interact(PlayerMovement player) {

        foreach(Switch switchObject in GetComponents<Switch>()) {
            switchObject.Activate();
        }
        foreach (LevelEnd endObject in GetComponents<LevelEnd>()) {
            endObject.Toggle(player);
        }

    }

    public void SetCollisionType(CollisionType collisionType) {
        this.collisionType = collisionType;
    }

    public void ActivateParticleSystem() {
        gridObjectParticleSystem.SetActive(true);
    }

    private bool dying = false;
    public void AnimateAndKill()
    {
        if (killable && !dying)
        {
            dying = true;
            if (animator != null)
            {
                animator.SetTrigger("Kill");
                foreach(PlayerMovement playerMovement in GetComponents<PlayerMovement>())
                {
                    playerMovement.StartDying();
                }
            }
            else
            {
                KillGridObject();
            }
        }
    }

    public void KillGridObject()
    {
        foreach(PlayerMovement playerMovement in GetComponents<PlayerMovement>())
        {
            playerMovement.Kill();
        }
        foreach(DoorKey doorKey in GetComponents<DoorKey>()) {
            doorKey.Kill();
        }
        //Destroy(gameObject);
    }
}
