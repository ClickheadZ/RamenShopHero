using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManipulator : MonoBehaviour
{
    public void DisableCanvas(Canvas canvas)
    {
        canvas.GetComponent<Canvas>().enabled = false;
    }

    public void EnableCanvas(Canvas canvas)
    {
        canvas.GetComponent<Canvas>().enabled = true;
    }
}
