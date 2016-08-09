using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class spherescripttest : MonoBehaviour {
    public float frictionCoefficient = 0.9f;
    public List<GameObject> freeList, usedList;
	// Use this for initialization
	void dontrunme() { //starts an infinite loop if changed to Awake
        var tempInstance = Instantiate(this.gameObject);
        tempInstance.tag = "atom";
        for (int i=0; i < 10; i++)
        {
            freeList.Add(Instantiate(tempInstance));
        }
        Debug.Log(" free: " + freeList.Count() + " used: " + usedList.Count());
        Destroy(tempInstance);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("sphere OnTriggerEnter occurs fine");
    }
    void OnTriggerExit(Collider col)
    {
        //Debug.Log("sphere OnTriggerExit occurs fine");
    }
    
}
