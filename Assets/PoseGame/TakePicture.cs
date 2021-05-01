using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TakePicture : MonoBehaviour
{
    // using diff camera on same rnader texture
    // and set these camera false through open camera get picture
    public Camera P1_Camera;
    public Camera P2_Camera;
    // Start is called before the first frame update
    void Start()
    {
        Take(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Take(int PlayerID){
        if(PlayerID == 0){
            P1_Camera.enabled = true;
        }
        else if(PlayerID ==1){
            P2_Camera.enabled = true;
        }
    }
}
