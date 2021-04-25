using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    Transform cma;
    // Start is called before the first frame update
    void Start()
    {
        //close other player's camera
        if(!this.isLocalPlayer){
            cma = this.transform.Find("TrackingSpace").Find("CenterEyeAnchor");
            cma.GetComponent<Camera>().enabled = false;
            cma.GetComponent<AudioListener>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
