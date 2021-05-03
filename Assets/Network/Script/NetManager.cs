using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetManager : NetworkManager
{
    public static NetManager NM;

    [HideInInspector]
    public int[] connectionID;

    private int connectionCount;

    public override void Awake() {
        NM = this;
        connectionID = new int[2];
        for (int i = 0; i < 2; i++)
            connectionID[i] = -1;
        connectionCount = 0;
        base.Awake();
    }
    //init server
    public override void OnStartServer()
    {
        base.OnStartServer();
    }
    // assign diff player prefab to diff client
    public override void OnServerAddPlayer(NetworkConnection conn){
        if (connectionCount < 2)
        {
            connectionID[connectionCount] = conn.connectionId;
            connectionCount++;
        }
        if(GameObject.FindObjectOfType<CharacterManager>() == null)
        {
            if (conn.connectionId == 0)
            {
                Debug.LogError("Player NO." + GameManager.GM.GetPlayerID() + " ,Player total num : " + spawnPrefabs.Count);
                NM.playerPrefab = spawnPrefabs[GameManager.GM.GetPlayerID()];
            }
            else
            {
                int id = GameManager.GM.GetPlayerID();
                id = id == 0 ? 1 : 0;
                NM.playerPrefab = spawnPrefabs[id];
            }
        }
        //avoid overflow
        base.OnServerAddPlayer(conn);
    }

    // initial necessary value when scene change
    public override void OnServerChangeScene(string newSceneName){

    }
    public void Start_Server(){
        NM.StartHost();
        Debug.LogError("host");
    }   
    // Start client only
    public void Start_Cient(){
        Debug.Log("Connect to : " + networkAddress);
        NM.StartClient();
    }

    public void ChangeScene(string sceneName)
    {
        ServerChangeScene(sceneName);
    }
}
