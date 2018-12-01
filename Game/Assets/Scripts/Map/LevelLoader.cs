// Date   : 22.04.2017 08:44
// Project: Out of This Small World
// Author : bradur

using UnityEngine;
using System.Collections.Generic;
using TiledSharp;
using System.Xml.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

enum LayerType
{
    None,
    Ground,
    Wall,
    Player
}

[System.Serializable]
public class MapObject : System.Object
{
    public string name;
    public GridObject prefab;
    public string containerTag;
}

public class LevelLoader : MonoBehaviour
{

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("LevelLoader").Length == 0)
        {
            gameObject.tag = "LevelLoader";
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            GameManager.main.SetLevelLoader(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private TiledMap tiledMapPrefab;

    [SerializeField]
    private MapGrid mapGrid;


    [SerializeField]
    private LevelList levelList;

    [SerializeField]
    private MapObjectList mapObjectList;

    [SerializeField]
    private FollowCamera followCamera;

    [SerializeField]
    private int nextLevel = 0;

    void Start()
    {
        Init(levelList.Levels[nextLevel]);
    }

    void Update()
    {
    }

    public MapObject GetMapObject(string name)
    {
        return mapObjectList.MapObjects.Find(element => element.name == name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Time.timeScale = 1f;
        if (nextLevel < levelList.Levels.Count)
        {
            //Init(levelList.Levels[nextLevel]);
        }
        else
        {
            //GameManager.main.TheEnd();
        }
    }

    public void OpenNextLevel()
    {
        nextLevel += 1;
        if (nextLevel < levelList.Levels.Count)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            //GameManager.main.TheEnd();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Init(TextAsset mapFile)
    {
        mapGrid.Clear();
        GameManager.main.SetMapGrid(mapGrid);
        GameManager.main.SetLevelLoader(this);
        XDocument mapX = XDocument.Parse(mapFile.text);
        TmxMap map = new TmxMap(mapX);

        Debug.Log(string.Format("Opening {0}", mapFile.name));
        TiledMap tiledMap = Instantiate(tiledMapPrefab);
        tiledMap.Init(map, this, mapGrid);
        PlayerCharacterManager characterManager = GameManager.main.GetCharacterManager();
        characterManager.SelectCharacter(1);
    }

}

[System.Serializable]
public class Level : System.Object
{
    public TextAsset file;
}
