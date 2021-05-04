using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public int playerID = -1;
    public int connectionId;
    public static GameManager GM;
    public NetManager networkManager;
    public SyncDictionary<int, int> connection = new SyncDictionary<int, int>();

    [SyncVar]
    public bool loadScene;
    [SyncVar]
    public string sceneName;

    private List<int> keys;

    private void Start()
    {
        DontDestroyOnLoad(this);
        GM = this;
        connectionId = -1;
        loadScene = false;
    }

    private void Update()
    {
        if (loadScene && isServer)
        {
            LoadSceneByName(sceneName);
        }
    }

    public void LoadSceneByName(string name)
    {
        loadScene = false;
        networkManager.ChangeScene(name);
    }

    
    public int GetPlayerID()
    {
        return playerID;
    }

    public void SetPlayerID(int ID)
    {
        playerID = ID;
    }

    [Command(requiresAuthority = false)]
    public void SetPlayerIDByConnection(int conn, int id)
    {
        if (connection.ContainsKey(conn) == false)
        {
            connection.Add(conn, id);
        }
        else
        {
            connection[conn] = id;
        }
    }

    public void SetConnectionID()
    {
        keys = new List<int>(connection.Keys);
        connectionId = keys[keys.Count - 1];
    }
}
