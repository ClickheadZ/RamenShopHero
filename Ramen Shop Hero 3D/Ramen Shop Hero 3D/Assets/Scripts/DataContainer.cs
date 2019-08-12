using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    //Use different lists for different types of locations? Like availableChairs, availableCounters, etc.
    public List<ActionLocation> availableLocations;
    public List<ActionLocation> availableChairs;
}
