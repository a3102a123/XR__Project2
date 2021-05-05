using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp_UI_Control : MonoBehaviour
{
    public GameObject ship_c;
    public GameObject droptower_c;
    public GameObject wheel_c;
    public GameObject circustent_c;
    public GameObject castle_c;
    public GameObject carousel_c;

    public GameObject ship_wb;
    public GameObject droptower_wb;
    public GameObject wheel_wb;
    public GameObject circustent_wb;
    public GameObject castle_wb;
    public GameObject carousel_wb;

    public GameObject final;

    public OVRGrabbable ship_reward;
    public OVRGrabbable droptower_reward;
    public OVRGrabbable wheel_reward;
    public OVRGrabbable circustent_reward;
    public OVRGrabbable castle_reward;
    public OVRGrabbable carousel_reward;

    private int ship_active;
    private int droptower_active;
    private int wheel_active; 
    private int circustent_active;
    private int castle_active;
    private int carousel_active;
    // Start is called before the first frame update
    void Start()
    {
        ship_active=0;
        droptower_active=0;
        wheel_active=0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ship_reward.isGrabbed == true)
        {
            ship_c.SetActive(true);
            ship_wb.SetActive(false);
            ship_active = 1;
        }
        if (droptower_reward.isGrabbed == true)
        {
            droptower_c.SetActive(true);
            droptower_wb.SetActive(false);
            droptower_active = 1;
        }
        if (wheel_reward.isGrabbed == true)
        {
            wheel_c.SetActive(true);
            wheel_wb.SetActive(false);
            wheel_active = 1;
        }
        if (circustent_reward.isGrabbed == true)
        {
            circustent_c.SetActive(true);
            circustent_wb.SetActive(false);
            circustent_active = 1;
        }
        if (castle_reward.isGrabbed == true)
        {
            castle_c.SetActive(true);
            castle_wb.SetActive(false);
            castle_active = 1;
        }
        if (carousel_reward.isGrabbed == true)
        {
            carousel_c.SetActive(true);
            carousel_wb.SetActive(false);
            carousel_active = 1;
        }

        if (wheel_active ==1 && ship_active ==1 && droptower_active == 1 && circustent_active == 1 && castle_active == 1 && carousel_active == 1)
        {
            final.SetActive(true);
        }

    }
}
