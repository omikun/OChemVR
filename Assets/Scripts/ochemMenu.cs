using UnityEngine;
using VRTK;

public class ochemMenu : VRTK_InteractableObject
{
    public enum PrimitiveTypes
    {
        Cube,
        Sphere
    }

    public PrimitiveTypes shape;
    private Color selectedColor;
    public GameObject spawnMe;

    public void SetSelectedColor(Color color)
    {
        selectedColor = color;
        gameObject.GetComponent<MeshRenderer>().material.color = color;
        Debug.Log("my spawner: new color: " + color);
    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        
        if (shape == PrimitiveTypes.Cube)
        {
            CreateShape(PrimitiveType.Cube, selectedColor);
        } else if(shape == PrimitiveTypes.Sphere)
        {
            CreateShape(PrimitiveType.Sphere, selectedColor);
        }
        ResetMenuItems();
    }

    private void CreateShape(PrimitiveType shape, Color color)
    {
        if (true)
        {
            GameObject obj = Instantiate(spawnMe);
            obj.transform.position = transform.position;
            //obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            //obj.GetComponent<MeshRenderer>().material.color = color;
            //obj.AddComponent<Rigidbody>();
        } else
        {
            GameObject obj = GameObject.CreatePrimitive(shape);
            obj.transform.position = transform.position;
            obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            obj.GetComponent<MeshRenderer>().material.color = color;
            obj.AddComponent<Rigidbody>();
        }
    }

    private void ResetMenuItems()
    {
        foreach (ochemMenu menuObjectSpawner in FindObjectsOfType<ochemMenu>())
        {
            menuObjectSpawner.StopUsing(null);
        }
    }
}