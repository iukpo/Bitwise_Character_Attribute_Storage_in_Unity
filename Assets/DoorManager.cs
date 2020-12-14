/**
@file       DoorManager.cs
@brief      Manages and updates state of global state of application
@author     Ihimu Ukpo
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
	int doorType = AttributeManager.MAGIC;
	
	void OnCollisionEnter(Collision collision)
	{
		//Bitmask test. If zero, failed mask. Otherwise, indicate that a collision has occurred, and process result in AttributeManager.
		if ( (collision.gameObject.GetComponent<AttributeManager>().attributes & doorType) !=0)
		{
			this.GetComponent<BoxCollider>().isTrigger = true;
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		//Make the object "solid" again.
		this.GetComponent<BoxCollider>().isTrigger = false;
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
