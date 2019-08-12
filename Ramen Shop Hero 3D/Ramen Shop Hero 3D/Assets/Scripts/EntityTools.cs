using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*      EntityAI
 * 
 *  This script should be attached as a component to any object in our project that needs pathing from
 *  location to location.
 *  
 *  The object needs to have a NavMeshAgent component.
 * 
 *  This script defines base functions that a NavMeshAgent will use to navigate through our
 *  levels, such as moving and standing still for some time.
 */

public class EntityTools : MonoBehaviour
{
    private NavMeshAgent agent;
    public bool atLocation;
    private ActionLocation location;
    private DataContainer dataContainer;
    private Vector2 position2d; //the position of this entity on a 2D plane, disregarding the Y axis (height)

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        atLocation = false;
        dataContainer = GameObject.FindWithTag("Data Container").GetComponent<DataContainer>();

        position2d = new Vector2(transform.position.x, transform.position.z);
    }

    public void PathTo(ActionLocation destination)
    {
        if (location != null && position2d == location.position2d)
        {
            location.SetLocationAvailable();
        }

        location = destination;
        atLocation = false;
        agent.SetDestination(location.transform.position);
    }

    //Finds the closest location within a list of locations
    public ActionLocation FindClosestLocation(List<ActionLocation> locations)
    {
        ActionLocation closestLocation = null;
        float sqrClosestDistance = Mathf.Infinity;

        for (int i = 0; i < locations.Count; ++i)
        {
            ActionLocation nextLocation = locations[i];

            Vector3 nextLocationPosition = nextLocation.transform.position;
            Vector3 vectorToLocation = nextLocationPosition - transform.position;
            float sqrDistanceToTarget = vectorToLocation.sqrMagnitude;

            if (sqrDistanceToTarget < sqrClosestDistance)
            {
                closestLocation = nextLocation;
                sqrClosestDistance = sqrDistanceToTarget;
            }
        }

        Debug.Log("The closest available location to " + this.name + " is " + closestLocation.gameObject.name);
        return closestLocation;
    }

    public void WaitAndGo(int idleTime, ActionLocation destination)
    {
        StartCoroutine(WaitThenGo());

        IEnumerator WaitThenGo()
        {
            yield return new WaitForSecondsRealtime(idleTime);

            PathTo(destination);
        }
        
    }
    
    void Update()
    {
        position2d.Set(transform.position.x, transform.position.z);

        if(location != null)
        {
            if (!atLocation)
            {
                
                if(location.isOccupied)
                {
                    Debug.Log(this.name + " is recalculating path");
                    PathTo(FindClosestLocation(dataContainer.availableChairs)); //this works for customer only, will have to refactor a bit for player and assistant cooks
                }

                if(position2d == location.position2d && location.gameObject.tag != "Customer Despawner")
                {
                    atLocation = true;
                    location.SetLocationOccupied();
                }
            }
        }
    }
    
}
