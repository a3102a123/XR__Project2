using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerID = -1;
    public static GameManager GM;
    private void Start()
    {
        DontDestroyOnLoad(this);
        GM = this;
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
