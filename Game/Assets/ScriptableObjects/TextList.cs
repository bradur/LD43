// Date   : 13.10.2018 15:37
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New TextList", menuName = "TextList")]
public class TextList : ScriptableObject
{

    [SerializeField]
    private string objectName = "ScriptableObject";
    public string Name { get { return objectName; } }

    [SerializeField]
    private List<TextListText> texts;
    public List<TextListText> Texts { get { return texts; } }
}

[System.Serializable]
public class TextListText : System.Object {
    [TextArea]
    public string text;
    public string name;
}
