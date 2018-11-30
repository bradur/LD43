// Date   : 13.10.2018 14:42
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MapObjectList", menuName = "MapObjects")]
public class MapObjectList : ScriptableObject {

    [SerializeField]
    private string objectName = "MapObjectList";
    public string Name { get { return objectName; } }

    [SerializeField]
    private List<MapObject> mapObjects;
    public List<MapObject> MapObjects {get {return mapObjects; } }

}
