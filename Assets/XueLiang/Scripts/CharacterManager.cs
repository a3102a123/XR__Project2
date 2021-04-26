﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterSelected[] characters;

    private CharacterSelected lastSelected;

    private void Start()
    {
        lastSelected = null;
    }

    private void Update()
    {
        SelectionManagement();
    }

    private void SelectionManagement()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].selected && characters[i] != lastSelected)
            {
                if (lastSelected == null)
                {
                    lastSelected = characters[i];
                }
                else
                {
                    lastSelected.selectedBoard.SetActive(false);
                    lastSelected.selected = false;
                    lastSelected = characters[i];
                }
            }
        }
    }
}