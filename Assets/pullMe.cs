using UnityEngine;
using VRTK;

public class pullMe : VRTK_InteractableObject
{
    GameObject destObj;
    public float pullSpeed = 10;

    public override void StartUsing(GameObject usingObject)
    {
        destObj = usingObject;
        Debug.Log("start using me");
        base.StartUsing(usingObject);
    }

    public override void StopUsing(GameObject usingObject)
    {
        destObj = null;
        Debug.Log("stop using me");
        base.StopUsing(usingObject);
    }

    protected override void Start()
    {
        base.Start();
        Debug.Log("pullMe just started");
    }

    Vector3 calculateForce(Vector3 a, Vector3 b, float threshold, float vel)
    {
        var diff = a - b;
        float mag = diff.magnitude;
        if (mag < threshold)
            return Vector3.zero;
        else
            return diff * vel;
    }
    protected override void Update()
    {
        if (destObj != null)
        {
            //Debug.Log("prev pos: " + transform.position + " new pos: " + destObj.transform.position);
            //transform.position = destObj.transform.position;
            GetComponent<Rigidbody>().velocity = calculateForce(destObj.transform.position, transform.position, 0.2f, pullSpeed);
        }
    }
}