using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*      CustomerDespawner
 * 
 *  The object with this script attached destroys a customer when it intersects it.
 */
public class CustomerDespawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Customer")
        {
            Destroy(collider.gameObject);
        }
    }
}
