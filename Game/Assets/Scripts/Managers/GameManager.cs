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


    private int playerCount = 0;
    public void EditPlayerCount(int difference) {
        Debug.Log(string.Format("PlayerCount change: {0} -> {1}. Required: ",
            playerCount, playerCount + difference, levelLoader.PlayersRequired
        ));
        playerCount += difference;
        if (difference < 0) {
            if (playerCount < levelLoader.PlayersRequired) {
                Debug.Log(string.Format("FAILED! Need {0} players, you only have {1}!", levelLoader.PlayersRequired, playerCount));
            }
        }
    }

    public void LoadNextLevel()
    {
        levelLoader.OpenNextLevel();
    }

    private int activatedEnds = 0;
    public void ToggleEnd(bool activated) {
        if (activated) {
            activatedEnds += 1;
        } else {
            activatedEnds -= 1;
        }
        if (activatedEnds >= levelLoader.PlayersRequired) {
            Debug.Log("All players activated!");
            LoadNextLevel();
        }
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
