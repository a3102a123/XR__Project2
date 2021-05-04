using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Mirror;
using System.Linq;

public class Player : NetworkBehaviour
{
    public GameObject CameraRig;
    // player ID which needs to be setted uniquely between diff palyer
    public int PlayerID;
    // the player arm length to determine how long player should stretch in pose game
    Transform cma;
    Transform LeftHand;
    Transform RightHand;
    Transform Center;
    Vector3 OriginPosition;
    // variable use for tempUI to debug
    Direction left_dir;
    Direction right_dir;
    float angle = 0;
    void Start()
    {
        GetController();
        //close other player's camera
        if(!this.isLocalPlayer){
            cma.GetComponent<Camera>().enabled = false;
            cma.GetComponent<AudioListener>().enabled = false;
            Destroy(CameraRig);
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ButtonEvent();
        SendPosInfo();
    }
    void ButtonEvent(){
        OVRInput.Update();
        OVRInput.FixedUpdate();
        // Debug.Log("Buttton : " + OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger));
        if(OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.5f){
            Debug.Log("Start !!!");
            FindObjectOfType<PoseGameManager>().StartGame();
        }
    }

    void GetController(){
        cma = this.transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
        LeftHand = this.transform.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor");
        RightHand = this.transform.Find("OVRCameraRig/TrackingSpace/RightHandAnchor");
        Center = this.transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
    }
    // determine whther the player make a right pose
    public bool DeterminePose(PoseGame Game){
        // using squre to eliminate calc
        double r = Math.Pow(Game.ArmLength,2);
        Debug.Log("Player " + PlayerID + " left hand : " + LeftHand);
        Vector3 L_Vector3 = LeftHand.position - Center.position;
        Vector3 R_Vector3 = RightHand.position - Center.position;
        Vector2 L = new Vector2(L_Vector3.x,L_Vector3.y);
        Vector2 R = new Vector2(R_Vector3.x,R_Vector3.y);
        Vector2 Origin = new Vector2(1,0);
        float L_angle = CalcAngle(L);
        float R_angle = CalcAngle(R);
        bool is_L_stretch = false;
        bool is_R_stretch = false;
        // use to determine whether the controller is show on player vision
        Transform L_controller = this.transform.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor/LeftControllerAnchor/OVRControllerPrefab");
        Transform R_controller = this.transform.Find("OVRCameraRig/TrackingSpace/RightHandAnchor/RightControllerAnchor/OVRControllerPrefab");
        // Debug.Log("L : " + CheckController(L_controller) + " ,R : " + CheckController(R_controller));
        if( CheckController(L_controller) && L.sqrMagnitude > r )
            is_L_stretch = true;
        if( CheckController(R_controller) && R.sqrMagnitude > r )
            is_R_stretch = true;
        // Debug.Log("Arm cmp : " + R.sqrMagnitude.ToString() + " , " + r);
        angle = R_angle;
        left_dir = Game.DetermineDir(L_angle,is_L_stretch);
        right_dir = Game.DetermineDir(R_angle,is_R_stretch);
        if (left_dir == Game.left && right_dir == Game.right){
            Debug.Log("Yes!Yes!Yes!");
            Game.PassGame();
            return true;
        }
        else{
            Debug.Log("NO!NO!NO!");
            return false;
        }
    }
    // 0 degree means controller on the top of head
    float CalcAngle(Vector2 v){
        float value = (float)((Mathf.Atan2(v.x, v.y) / Math.PI) * 180f);
        if(value < 0) value += 360f;
        return value;
    }
    bool CheckController(Transform controller){
        for(int i = 0 ; i < controller.childCount ; i++)
            if(controller.GetChild(i).gameObject.activeInHierarchy)
                return true;
        return false;
    }
    // the info to debug (show on "Develop_scene" UI)
    void SendPosInfo(){
        TempUI.LeftPos = LeftHand.position;
        TempUI.RightPos = RightHand.position;
        TempUI.CenterPos = Center.position;
        TempUI.l_dir = left_dir;
        TempUI.r_dir = right_dir;
        TempUI.theta = angle;
    }
    // Attach player to target GameObject's local position
    [Command]
    public void CmdAttach(GameObject target,Vector3 position){
        if(target == null){
            Debug.Log("[Player:Attach]:Target net exist can't attach to");
            return;
        }
        Debug.Log("Attach : "+OriginPosition);
        RpcAttach(target,position);
    }
    [ClientRpc]
    private void RpcAttach(GameObject target,Vector3 position){
        OriginPosition = transform.position;
        transform.SetParent(target.transform);
        transform.localPosition = position;
    }
    // Detach player to target position(if set is_origin flag palyer is set to origin position where trigger Attach)
    // when is_origin set true, ignore input position
    [Command]
    public void CmdDetach(Vector3 position,bool is_origin){
        if(gameObject.transform.parent == null){
            Debug.Log("[Player:Detaach]:Player's parent doesn't exict.Player don't need to detach");
            return;
        }
        transform.SetParent(null);
        RpcDetach(position,is_origin);
    }
    [ClientRpc]
    public void RpcDetach(Vector3 position,bool is_origin){
        transform.SetParent(null);
        if(is_origin){
            transform.position = OriginPosition;
        }
        else{
            transform.position = position;
        }
    }
}
