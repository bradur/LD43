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
    Pickup
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

    public void Init(int x, int y, MapGrid mapGrid, TiledSharp.PropertyDict properties)
    {
        animator = GetComponent<Animator>();
        foreach (Portcullis portcullis in GetComponents<Portcullis>()) {
            portcullis.Init(x, y, mapGrid, properties);
        }
        foreach(Switch switchObject in GetComponents<Switch>()) {
            switchObject.Init(x, y, mapGrid, properties);
        }
        foreach(PlayerMovement playerMovement in GetComponents<PlayerMovement>())
        {
            playerMovement.Init(x, y, mapGrid, properties);
        }
    }

    public void AnimateAndKill()
    {
        if (killable)
        {
            if (animator != null)
            {
                animator.SetTrigger("Kill");
            }
            else
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
