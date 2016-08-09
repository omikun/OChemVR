using UnityEngine;
using System.Collections;

public class particleForce : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        GameObject[] atoms = GameObject.FindGameObjectsWithTag("atom");
        foreach(GameObject atom in atoms)
        {
            foreach(GameObject atom2 in atoms)
            {
                if (atom != atom2)
                {
                    Vector3 diff = atom.transform.position - atom2.transform.position;
                    float magSqr = diff.sqrMagnitude;
                    float vel = 0.1f / magSqr / magSqr;
                    //Debug.Log("distance: " + magSqr + " vel: " + vel);
                    vel = (vel > 1) ? 1 : vel;
                    atom2.GetComponent<Rigidbody>().velocity += vel * diff;
                }

            }

        }
	}
}
