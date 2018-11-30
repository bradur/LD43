// Date   : 29.07.2017 20:07
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TextMeshLayerOrderer : MonoBehaviour {

    [SerializeField]
    private int layerOrder;

    void Start () {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingOrder = layerOrder;
    }

}
