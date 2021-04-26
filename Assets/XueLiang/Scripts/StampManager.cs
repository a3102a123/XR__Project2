﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StampManager : MonoBehaviour
{
    public GameObject[] stamps;
    public string[] stampNames;
    public TextMeshProUGUI stampName;

    private List<Stamp> stampHovers = new List<Stamp>();
    private bool nothingHover;

    private void Start()
    {
        for (int i = 0; i < stamps.Length; i++)
        {
            stampHovers.Add(stamps[i].GetComponent<Stamp>());
        }
        nothingHover = true;
    }

    private void Update()
    {
        nothingHover = true;
        for (int i = 0; i < stampHovers.Count; i++)
        {
            if (stampHovers[i].onHover)
            {
                stampName.SetText(stampNames[i]);
                nothingHover = false;
                break;
            }
        }
        if (nothingHover && stampName.text != "")
        {
            stampName.SetText(" ");
        }
    }
}