using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public GameObject CameraRig;
    Transform cma;
    Transform LeftHand;
    Transform RightHand;
    Transform Center;
    Direction left_dir;
    Direction right_dir;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor"));
        GetController();
        //close other player's camera
        if(!this.isLocalPlayer){
            cma.GetComponent<Camera>().enabled = false;
            cma.GetComponent<AudioListener>().enabled = false;
            Destroy(CameraRig);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DeterminePose(Direction.NONE,Direction.NONE);
        SendPosInfo();
    }

    void GetController(){
        cma = this.transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
        LeftHand = this.transform.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor");
        RightHand = this.transform.Find("OVRCameraRig/TrackingSpace/RightHandAnchor");
        Center = this.transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
    }
    void DeterminePose(Direction left,Direction right){
        left_dir = Direction.N;
        right_dir = Direction.SE;
    }
    // the info to debug (show on "Develop_scene" UI)
    void SendPosInfo(){
        TempUI.LeftPos = LeftHand.position;
        TempUI.RightPos = RightHand.position;
        TempUI.CenterPos = Center.position;
        TempUI.l_dir = left_dir;
        TempUI.r_dir = right_dir;
    }
}
