// Date   : 08.11.2018 22:43
// Project: PortableLoopingMachine
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateAnimators : MonoBehaviour {

    [SerializeField]
    private List<Animator> animators;

    void Start () {
        gameObject.SetActive(false);
    }

    void OnEnable () {
        foreach (Animator animator in animators)
        {
            animator.enabled = true;
        }
    }
}
