﻿using UnityEngine;
using System.Collections;
using System;

public class particleForce : MonoBehaviour {
    public float epsilon = 1.0f;
    public float sigma = 0.3f;
    public float maxSpeed = 20;
    public float maxRange = 30;
	// Use this for initialization
	void Start () {
	
	}
	
    Vector3 calculateForce(Vector3 a, Vector3 b, float threshold, float accel)
    {
        var diff = a - b;
        float magSqr = diff.sqrMagnitude;
        float mag = diff.magnitude;
        float vel = 0.0f;
        if (magSqr < threshold)
        {
            if (false)
                vel = accel / magSqr;
            else { 
                //vel = -1 * 4 * epsilon * (float)(Math.Pow(sigma / mag, 4) - Math.Pow(sigma / mag, 2));
                var r = mag;
                var force = -24 * (1 / r) * epsilon * (2 * Math.Pow((sigma / r), 12) - Math.Pow(sigma / r, 6)) ;
                vel = (float)force * 1.0f / 90.0f;
                //vel = (Math.Abs(vel) > 0.5f) ? 0.5f*Math.Sign(vel) : vel;
                //FIXME becomes nand (negative?) vel = (float)Math.Pow(vel, .5f);
                //if (vel < -0.1f)
                 //   vel = -0.1f;
            }
        }
        return vel / mag * diff;
    }
    // Update is called once per frame
    void FixedUpdate () {
        GameObject[] hAtoms = GameObject.FindGameObjectsWithTag("h");
        GameObject[] cAtoms = GameObject.FindGameObjectsWithTag("c");
        //Debug.Log("number of atoms attraction: " + hAtoms.Length);

        //carbon-carbon pairs
        foreach (GameObject c1 in cAtoms)
        {
            foreach (GameObject c2 in cAtoms)
            {
                if (c1 != c2)
                {
                    var vel = calculateForce(c1.transform.position, c2.transform.position, 0.45f, 0.05f);
                    c2.GetComponent<Rigidbody>().velocity += vel;
                    c1.GetComponent<Rigidbody>().velocity -= vel;
                }
            }
        }
        foreach (GameObject h1 in hAtoms)
        {
            //hydrogen-carbon pairs
            foreach(GameObject c1 in cAtoms)
            {
                var vel = calculateForce(c1.transform.position, h1.transform.position, 0.65f, 0.05f);
                h1.GetComponent<Rigidbody>().velocity += vel;
                c1.GetComponent<Rigidbody>().velocity -= vel;
            }
            //hydrogen-hydrogen pairs
            foreach (GameObject h2 in hAtoms)
            {
                if (h1 != h2)
                {
                    var vel = calculateForce(h2.transform.position, h1.transform.position, 0.65f, 0.01f);
                    h2.GetComponent<Rigidbody>().velocity += vel;
                }

            }
        }

        //apply max speed
#if true

        foreach(GameObject h1 in hAtoms)
        {
            var mag = h1.GetComponent<Rigidbody>().velocity.magnitude;
            if (mag > maxSpeed)
                h1.GetComponent<Rigidbody>().velocity /= mag/maxSpeed;
        }
        foreach (GameObject c1 in cAtoms)
        {
            var mag = c1.GetComponent<Rigidbody>().velocity.magnitude;
            if (mag > maxSpeed)
                c1.GetComponent<Rigidbody>().velocity /= mag/maxSpeed;
        }
#endif
        //apply bounding box (max dist from center)
        foreach (GameObject h1 in hAtoms)
        {
            if (h1.transform.position.sqrMagnitude > maxRange)
            {
                h1.GetComponent<Rigidbody>().velocity *= 0.2f;
            }
        }

       foreach (GameObject c1 in cAtoms)
        {
            if (c1.transform.position.sqrMagnitude > maxRange)
            {
                c1.GetComponent<Rigidbody>().velocity *= 0.2f;
            }
        }
	}
}
