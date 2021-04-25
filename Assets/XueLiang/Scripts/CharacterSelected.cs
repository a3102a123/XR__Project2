using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelected : MonoBehaviour
{
    public GameObject selectedBoard;

    [HideInInspector]
    public bool selected;

    private void Start()
    {
        selectedBoard.SetActive(false);
        selected = false;
    }

    public void SelectionChecked()
    {
        if (selected == false)
        {
            selectedBoard.SetActive(true);
            selected = true;
        }
        else
        {
            selectedBoard.SetActive(false);
            selected = false;
        }
    }
}