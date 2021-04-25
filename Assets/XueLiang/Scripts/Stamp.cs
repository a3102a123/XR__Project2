using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamp : MonoBehaviour
{
    public Sprite fullColorImage;

    [HideInInspector]
    public bool onHover;

    private void Start()
    {
        onHover = false;
    }

    public void OnHoverEnter()
    {
        onHover = true;
    }

    public void OnHoverExit()
    {
        onHover = false;
    }

    public void Success()
    {
        transform.GetComponent<Image>().sprite = fullColorImage;
    }
}