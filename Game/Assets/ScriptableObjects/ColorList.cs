// Date   : 13.10.2018 15:37
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New ColorList", menuName = "ColorList")]
public class ColorList : ScriptableObject
{

    [SerializeField]
    private string objectName = "ScriptableObject";
    public string Name { get { return objectName; } }

    [SerializeField]
    private List<ColorListColor> colors;
    public List<ColorListColor> Colors { get { return colors; } }
}

[System.Serializable]
public class ColorListColor : System.Object {
    public Color color;
    public string name;
}
