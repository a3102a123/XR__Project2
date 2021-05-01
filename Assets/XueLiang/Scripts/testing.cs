using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public Transform rightHand;

    private void FixedUpdate()
    {
        transform.position = rightHand.position;

    }
}
