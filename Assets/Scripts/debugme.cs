using UnityEngine;
using System.Collections;

public class debugme : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("controller OnTriggerEnter occurs fine");
        transform.parent.gameObject.GetComponent<PickupParent>().OnTriggerEnter(col);
    }
    void OnTriggerExit(Collider col)
    {
        Debug.Log("controller OnTriggerExit occurs fine");
        transform.parent.gameObject.GetComponent<PickupParent>().OnTriggerExit(col);
    }
    
}
