using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickupParent : MonoBehaviour
{
    public String name;
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
    Boolean IsTriggerStay = false;
    Collider _col;
    Boolean spawnAtom = true;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Debug.Log("You are holding 'Touch' on the Trigger");
        }

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Debug.Log("You activated TouchDown on the Trigger");
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Debug.Log("You activated TouchUp on the Trigger");
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Debug.Log("You are holding 'Press' on the Trigger");
        } else
        {
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Debug.Log("You activated PressDown on the Trigger");
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Debug.Log("You activated PressUp on the Trigger");
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Debug.Log("You activated PressUp on the " + name + " Touchpad");
            //sphere.transform.position = new Vector3(0, 0.5f, 0);
            //sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if (IsTriggerStay)
        {
            OnTriggerStay(_col);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        IsTriggerStay = true;
        _col = col;
    }
    public void OnTriggerExit(Collider col)
    {
        IsTriggerStay = false;
    }
    void OnTriggerStay(Collider col)
    {
        
        //Debug.Log("You have collided with " + col.name + " and activated OnTriggerStay");
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            //   Debug.Log("You have collided with " + col.name + " while holding down Touch");
            Debug.Log("spawnAtom? " + spawnAtom);
            if (false)//spawnAtom) //not really spawn, just grab from a list
            {
                spawnAtom = false;
                spherescripttest scrpt = col.gameObject.GetComponent<spherescripttest>();
                if (scrpt.freeList.Count() > 0)
                {
                    var newAtom = scrpt.freeList[0];
                    scrpt.usedList.Add(newAtom);
                    scrpt.freeList.Remove(newAtom);
                    newAtom.GetComponent<Rigidbody>().isKinematic = true;
                    newAtom.transform.SetParent(gameObject.transform);
                    _col = newAtom.GetComponent<Collider>();
                }
                Debug.Log(name + "free: " + scrpt.usedList.Count() + " used: " + scrpt.usedList.Count());
            } 
            col.attachedRigidbody.isKinematic = true;
            col.gameObject.transform.SetParent(gameObject.transform);
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have released Touch while colliding with " + col.name);
            spawnAtom = true;
            Debug.Log("spawnAtom? " + spawnAtom);
            col.gameObject.transform.SetParent(null);
            col.attachedRigidbody.isKinematic = false;

            tossObject(col.attachedRigidbody);
        }
    }

    void tossObject(Rigidbody rigidBody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
        {
            rigidBody.velocity = origin.TransformVector(device.velocity);
            rigidBody.angularVelocity = origin.TransformVector(device.angularVelocity);
        }
        else
        {
            rigidBody.velocity = device.velocity;
            rigidBody.angularVelocity = device.angularVelocity;
        }

    }
}
