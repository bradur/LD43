using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour {

    public static GameManager main;

    private bool gameOver = false;
    public bool GameIsOver { get { return gameOver; } }

    [SerializeField]
    private PlayerCharacterManager characterManager;

    [SerializeField]
    private UIManager uiManager;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length == 0)
        {
            main = this;
            gameObject.tag = "GameManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    MapGrid mapGrid;

    public void SetMapGrid(MapGrid mapGrid)
    {
        this.mapGrid = mapGrid;
    }

    public UIManager GetUIManager()
    {
        Debug.Log(uiManager);
        return uiManager;
    }

    LevelLoader levelLoader;
    public void SetLevelLoader(LevelLoader levelLoader)
    {
        this.levelLoader = levelLoader;
    }

    /*public void StopPlayer()
    {
        playerMovement.Stop();
    }*/

    public void LoadNextLevel()
    {
        levelLoader.OpenNextLevel();
    }

    public MapGrid GetMapGrid()
    {
        return mapGrid;
    }

    public PlayerCharacterManager GetCharacterManager()
    {
        return characterManager;
    }

    private PlayerMovement playerMovement;
    public void SetUpPlayer(PlayerCharacter pc)
    {
        playerMovement = pc.GetComponent<PlayerMovement>();
        //followerCamera.SetTarget(pc.transform);
        //followerArea.SetTarget(pc.transform);
    }

    /*
    public void ShowNextLevelScreen()
    {
        uiManager.ShowNextLevelScreen();
    }

    public void TheEnd()
    {
        uiManager.ShowTheEndScreen();
    }*/

    void Start () {
        
    }

    void Update () {

    }
}
