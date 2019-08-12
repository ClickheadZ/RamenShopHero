using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*      CustomerSpawner
 * 
 * The object with this script attached spawns customers at a certain rate.
 * 
 */
public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] int SpawnTime;
    [SerializeField] GameObject Customer;
    private bool keepSpawning;

    void Start()
    {
        keepSpawning = true;

        StartCoroutine(SpawnLoop(SpawnTime));
    }

    private IEnumerator SpawnLoop(int spawnInterval)
    {
        while(keepSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);
            Instantiate(Customer, this.transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
