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
    private DataContainer dataContainer; //may not be needed?

    void Start()
    {
        customerTools = GetComponent<EntityTools>();
        dataContainer = GameObject.FindWithTag("Data Container").GetComponent<DataContainer>();

        if(dataContainer.availableChairs.Count > 0)
        {
            customerTools.PathTo(customerTools.FindClosestLocation(dataContainer.availableChairs));
        }

        //customerTools.WaitAndGo(IdleTime, PickRandomDespawner());
    }

    //Picks one of two available Customer Despawners at random
    private ActionLocation PickRandomDespawner()
    {
        int despawnerRng = Random.Range(0, 2);
        ActionLocation despawnerChoice;

        if (despawnerRng == 0)
        {
            despawnerChoice = GameObject.FindGameObjectsWithTag("Customer Despawner")[0].GetComponent<ActionLocation>();
        }
        else
        {
            despawnerChoice = GameObject.FindGameObjectsWithTag("Customer Despawner")[1].GetComponent<ActionLocation>();
        }

        return despawnerChoice;
    }

    void Update()
    {
        if(customerTools.atLocation)
        {
            customerTools.WaitAndGo(IdleTime, PickRandomDespawner());
        }
    }

}
