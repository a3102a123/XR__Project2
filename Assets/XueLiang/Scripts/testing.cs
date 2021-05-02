using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public NetManager net;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            net.Start_Server();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            net.Start_Cient();
        }
    }
}
