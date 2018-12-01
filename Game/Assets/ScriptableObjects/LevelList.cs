// Date   : 13.10.2018 15:37
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New LevelList", menuName = "LevelList")]
public class LevelList : ScriptableObject
{

    [SerializeField]
    private string objectName = "ScriptableObject";
    public string Name { get { return objectName; } }

    [SerializeField]
    private List<TextAsset> levels;
    public List<TextAsset> Levels { get { return levels; } }
}
