// Date   : 23.04.2017 11:09
// Project: Out of This Small World
// Author : bradur

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum Action
{
    None,
    Quit,
    Restart,
    CloseMenu,
    OpenMenu,
    Jump,
    InteractWithObject,
    NextLevel,
    MoveUp,
    MoveRight,
    MoveDown,
    MoveLeft,
    CharacterOne,
    CharacterTwo,
    CharacterThree,
    CharacterFour,
    SwitchCharacter
}

[System.Serializable]
public class GameKey : System.Object
{
    public KeyCode key;
    public Action action;
}

public class KeyManager : MonoBehaviour
{

    [SerializeField]
    private bool debug;

    public static KeyManager main;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("KeyManager").Length == 0)
        {
            main = this;
            gameObject.tag = "KeyManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private List<GameKey> gameKeys = new List<GameKey>();

    public bool GetKeyDown(Action action)
    {
        foreach (KeyCode keyCode in GetKeyCodes(action))
        {
            if (Input.GetKeyDown(keyCode))
            {
                if (debug)
                {
                    Debug.Log(string.Format("Key {0} pressed down to perform {1}.", GetKeyString(action), action.ToString()));
                }
                return true;
            }
        }
        return false;
    }

    public bool GetKeyUp(Action action)
    {
        foreach (KeyCode keyCode in GetKeyCodes(action))
        {
            if (Input.GetKeyUp(keyCode))
            {
                if (debug)
                {
                    Debug.Log(string.Format("Key {0} let up to perform {1}.", GetKeyString(action), action.ToString()));
                }
                return true;
            }
        }
        return false;
    }

    public bool GetKey(Action action)
    {
        foreach (KeyCode keyCode in GetKeyCodes(action))
        {
            if (Input.GetKey(keyCode))
            {
                if (debug)
                {
                    Debug.Log(string.Format("Key {0} held to perform {1}.", GetKeyString(action), action.ToString()));
                }
                return true;
            }
        }
        return false;
    }

    public KeyCode GetKeyCode(Action action)
    {
        foreach (GameKey gameKey in gameKeys)
        {
            if (gameKey.action == action)
            {
                return gameKey.key;
            }
        }
        return KeyCode.None;
    }


    public List<KeyCode> GetKeyCodes(Action action)
    {
        List<KeyCode> keyCodes = new List<KeyCode>();
        foreach (GameKey gameKey in gameKeys)
        {
            if (gameKey.action == action)
            {
                keyCodes.Add(gameKey.key);
            }
        }
        return keyCodes;
    }


    public string GetKeyString(Action action)
    {
        List<string> keyStrings = new List<string>();
        foreach (GameKey gameKey in gameKeys)
        {
            if (gameKey.action == action)
            {
                
                if (gameKey.key == KeyCode.Return)
                {
                    keyStrings.Add("Enter");
                }
                else if (gameKey.key == KeyCode.RightControl)
                {
                    keyStrings.Add("Right Ctrl");
                } else if (gameKey.key == KeyCode.Escape) {
                    keyStrings.Add("Esc");
                } else {
                    keyStrings.Add(gameKey.key.ToString());
                }
            }
        }
        return string.Join(" / ", keyStrings.ToArray());
    }
}
