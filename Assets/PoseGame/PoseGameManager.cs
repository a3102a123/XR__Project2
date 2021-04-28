using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public enum Direction{
    N,
    NE,
    E,
    SE,
    S,
    SW,
    W,
    NW,
    NONE
};

public class PoseGameManager : NetworkBehaviour
{
    // below value set public for debug 
    // (init in start no matter what value is setted)
    /************************************************/
    // this flag means whether the players finish whole pose game
    public bool is_complete = false;
    // trigger game start. prevent palyer complete game immediately after enter game scene
    public bool is_start = false;
    // Start is called before the first frame update
    void Start()
    {
        is_complete = false;
        is_start = false;
    }

    // Update is called once per frame
    void Update()
    {
        JudgePlayerPose();
        CompleteGame();
    }

    public bool CompleteGame(){
        int i;
        PoseGame[] GameList = FindObjectsOfType<PoseGame>();
        Debug.Log("Game num : " + GameList.Length);
        if(!is_complete){
            for(i = 0 ; i < GameList.Length ; i++){
                if(!GameList[i].is_pass)
                    break;
            }
            if(i == GameList.Length){
                Debug.Log("Whole Game Pass!");
                is_complete = true;
            }
        }
        return is_complete;
    }
    void JudgePlayerPose(){
        if(!is_start)
            return ;
        Player[] PlayerList = FindObjectsOfType<Player>();
        for(int i = 0 ; i < PlayerList.Length ; i++ ){
            PlayerList[i].DeterminePose();
        }
    }
    // Starting the game which can be call by any player
    [Command(requiresAuthority = false)]
    public void StartGame(){
        is_start = true;
    }
}
