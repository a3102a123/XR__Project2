using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetManager : NetworkManager
{
    public static NetManager NM;

    public override void Awake() {
        NM = this;
        base.Awake();
    }
    //init server
    public override void OnStartServer()
    {
        base.OnStartServer();
    }
    // assign diff player prefab to diff client
    public override void OnServerAddPlayer(NetworkConnection conn){
        Debug.LogError(conn.authenticationData);
        if(GameObject.FindObjectOfType<CharacterManager>() == null)
        {
            Debug.LogError("Player NO." + GameManager.GM.GetPlayerID() + " ,Player total num : " + spawnPrefabs.Count);
            NM.playerPrefab = spawnPrefabs[GameManager.GM.GetPlayerID()];
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
