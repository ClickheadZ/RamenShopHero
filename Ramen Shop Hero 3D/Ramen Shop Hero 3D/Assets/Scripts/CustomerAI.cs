using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*      CustomerAI
 *  
 *  This script governs the behaviour of a Customer entity in our game.
 *  
 *  Current Behaviour (update as this AI evolves) :
 *  The customer walks towards the nearest unoccupied chair and sits in it for IdleTime seconds.
 */

public class CustomerAI : MonoBehaviour
{
    [SerializeField] int IdleTime;

    private EntityTools customerTools;
    private ActionLocation entrance;
    private DataContainer dataContainer; //may not be needed?

    void Start()
    {
        customerTools = GetComponent<EntityTools>();
        entrance = GameObject.FindWithTag("Entrance").GetComponent<ActionLocation>();
        dataContainer = GameObject.FindWithTag("Data Container").GetComponent<DataContainer>();
        IdleTime = 5;

        //Look through list of chairs instead in future
        if(dataContainer.availableLocations.Count > 0)
        {
            customerTools.PathTo(customerTools.FindClosestLocation());
        }

        //customerEntity.WaitForTime(IdleTime);

        //customerEntity.PathTo(entrance);
    }
    
}
