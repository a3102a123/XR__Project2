using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public int playerID = -1;
    public static GameManager GM;
    public NetManager networkManager;

    [SyncVar]
    public bool loadScene;
    [SyncVar]
    public string sceneName;

    private void Start()
    {
        DontDestroyOnLoad(this);
        GM = this;
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

    public void SetPlayerID(int ID)
    {
        playerID = ID;
    }
    public int GetPlayerID()
    {
        return playerID;
    }
}
