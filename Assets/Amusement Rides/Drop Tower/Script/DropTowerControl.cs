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
    private int visible;
    // Start is called before the first frame update
    void Start()
    {
        start_position = new Vector3(chair.transform.position.x, chair.transform.position.y, chair.transform.position.z);
        energybarlength = new Vector3(energybar.transform.localScale.x, energybar.transform.localScale.y, energybar.transform.localScale.z);
        visible = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (chair.transform.position.y > 15)
        {
            chair.transform.position = start_position;
            energybar.transform.localScale = energybarlength;
        }

        if (OVRInput.Get(OVRInput.Button.Two) && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.2f)
        {
            chair.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
            energybar.transform.localScale += new Vector3(0, 0.05f, 0);
            visible = 0;
            energybar.SetActive(true);
            emptybar.SetActive(true);
        }

        else if (OVRInput.GetUp(OVRInput.Button.Two))
        {
            visible = 1;
        }

        if (visible == 1 && energybar.transform.localScale.y > 0.0f)
        {
            energybar.transform.localScale -= new Vector3(0, 0.15f, 0);
        }
        if (visible == 1 && chair.transform.position.y > start_position.y)
        {
            chair.transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }
    }

}