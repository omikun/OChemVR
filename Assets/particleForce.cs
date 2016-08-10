using UnityEngine;
using System.Collections;

public class particleForce : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        GameObject[] hAtoms = GameObject.FindGameObjectsWithTag("h");
        GameObject[] cAtoms = GameObject.FindGameObjectsWithTag("c");
        Debug.Log("number of atoms attraction: " + hAtoms.Length);
        foreach(GameObject h1 in hAtoms)
        {
            foreach(GameObject c1 in cAtoms)
            {
                Vector3 diff = c1.transform.position - h1.transform.position;
                float magSqr = diff.sqrMagnitude;
                if (magSqr < .45f)
                {
                    float vel = 0.05f / magSqr / magSqr;
                    //Debug.Log("distance: " + magSqr + " vel: " + vel);
                    vel = (vel > 1) ? 1 : vel;
                    h1.GetComponent<Rigidbody>().velocity += vel * diff;
                    c1.GetComponent<Rigidbody>().velocity -= vel * diff;
                }
            }
            foreach (GameObject h2 in hAtoms)
            {
                if (h1 != h2)
                {
                    Vector3 diff = h1.transform.position - h2.transform.position;
                    float magSqr = diff.sqrMagnitude;
                    if (magSqr < .25f)
                    {
                        float vel = 0.01f / magSqr / magSqr;
                        //Debug.Log("distance: " + magSqr + " vel: " + vel);
                        vel = (vel > 1) ? 1 : vel;
                        h2.GetComponent<Rigidbody>().velocity -= vel * diff;
                    }
                }

            }

        }
	}
}
