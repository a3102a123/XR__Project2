using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetManager : NetworkManager
{
    private int player_count = 0;
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
        Debug.Log("Player NO." + player_count + " ,Player total num : " + spawnPrefabs.Count);
        NM.playerPrefab = spawnPrefabs[player_count++];
        //avoid overflow
        player_count %= spawnPrefabs.Count;
        base.OnServerAddPlayer(conn);
    }
    // initial necessary value when scene change
    public override void OnServerChangeScene(string newSceneName){
        player_count = 0;
    }
    public void Start_Server(){
        NM.StartHost();
    }
    // Start client only
    public void Start_Cient(){
        Debug.Log("Connect to : " + networkAddress);
        NM.StartClient();
    }
}
