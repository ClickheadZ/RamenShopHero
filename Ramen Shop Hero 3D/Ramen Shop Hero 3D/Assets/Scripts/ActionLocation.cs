using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*      ActionLocation
 *  
 *  This script represents a location where an EntityAI will simulate an action, such as
 *  sitting in a chair and eating, or preparing ingredients, or handling cash, etc. 
 */
public class ActionLocation : MonoBehaviour
{
    public bool isOccupied;

    public Vector2 position2d; //the position of this entity on a 2D plane, disregarding the Y axis (height)

    //Add enum here? Then in Start we can do if(this is a chair) {add to chair list} for more precise control.

    private DataContainer dataContainer;

    void Awake()
    {
        dataContainer = GameObject.FindWithTag("Data Container").GetComponent<DataContainer>();
        SetLocationAvailable();
        position2d = new Vector2(transform.position.x, transform.position.z);
    }

    public void SetLocationOccupied()
    {
        Debug.Log(this.name + " is now occupied");
        isOccupied = true;
        dataContainer.availableLocations.Remove(this);
    }

    public void SetLocationAvailable()
    {
        Debug.Log(this.name + " is now available");
        isOccupied = false;
        dataContainer.availableLocations.Add(this);
    }
}
