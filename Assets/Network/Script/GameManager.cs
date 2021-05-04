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
    // send another player who don't trigger the game to facility
    public void SendAnotherPlayer(int tirggerPlayerID,GameObject facility,Vector3 position){
        Player[] PlayerList = FindObjectsOfType<Player>();
        Player SendTarget = null;
        for(int i = 0 ; i < PlayerList.Length ; i++){
            if(PlayerList[i].PlayerID != tirggerPlayerID){
                SendTarget = PlayerList[i];
                break;
            }
        }
        Debug.Log("Who be send? : " + SendTarget);
        if(SendTarget != null){
            Debug.Log("Sending! " + facility.name + " " + position);
            SendTarget.CmdAttach(facility,position); 
        }
    }
    // send player back to ground
    public void SendPlayerBack(int targetPlayerID){
        Player[] PlayerList = FindObjectsOfType<Player>();
        Player SendTarget = null;
        for(int i = 0 ; i < PlayerList.Length ; i++){
            if(PlayerList[i].PlayerID == targetPlayerID){
                SendTarget = PlayerList[i];
                break;
            }
        }
        if(SendTarget != null){
            // set is_origin to true let player back to origin position
            SendTarget.CmdDetach(new Vector3(),true);
        }
    }
}
