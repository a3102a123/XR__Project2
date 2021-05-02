using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTowerControl : MonoBehaviour
{
    public GameObject controller;
    public GameObject chair;
    public GameObject energybar;
    public GameObject emptybar;
    private Vector3 start_position;
    private Vector3 energybarlength;
    private int release;
    private int toomuchpower;
    // Start is called before the first frame update
    void Start()
    {
        start_position = new Vector3(chair.transform.position.x, chair.transform.position.y, chair.transform.position.z);
        energybarlength = new Vector3(energybar.transform.localScale.x, energybar.transform.localScale.y, energybar.transform.localScale.z);
        release = 0;
        toomuchpower = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (energybar.transform.localScale.y > 2*emptybar.transform.localScale.y)
        {
            energybar.transform.localScale = energybarlength;
            chair.transform.Translate(Vector3.down * Time.deltaTime * 10.0f, Space.World);
            toomuchpower = 1;
        }

        if (OVRInput.Get(OVRInput.Button.Two) && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.2f  && chair.transform.position.y < 2.0f && toomuchpower ==0)
        {
            chair.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
            energybar.transform.localScale += new Vector3(0, 0.15f, 0);
            release = 0;
            
            energybar.SetActive(true);
            emptybar.SetActive(true);
        }

        else if (OVRInput.GetUp(OVRInput.Button.Two))
        {
            release = 1;
            toomuchpower = 0;
        }

        if (release == 1 && energybar.transform.localScale.y > 0.0f )
        {
            energybar.transform.localScale -= new Vector3(0, 0.3f, 0);
        }
        if ((release == 1 || toomuchpower == 1) && chair.transform.position.y > start_position.y)
        {
            chair.transform.Translate(Vector3.down * Time.deltaTime *2.0f, Space.World);
        }
    }

}
