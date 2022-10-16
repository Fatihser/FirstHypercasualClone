using System.Collections.Generic;
using UnityEngine;

public class stack : MonoBehaviour
{
    public static List<GameObject> cubes=new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="collect")
        {
            Destroy(this);
            other.transform.tag = "collected";
            other.transform.parent = transform.parent;
            other.transform.localPosition = new Vector3(transform.localPosition.x+ 0.26f,transform.localPosition.y-0.15f,transform.localPosition.z);
            other.gameObject.AddComponent<stack>();
            cubes.Add(other.gameObject);
            Debug.Log(cubes.Count);
        }
        else if (other.transform.tag=="multiplex2Left")
        {
            Destroy(this);
            other.transform.parent = transform.parent;
            other.gameObject.GetComponent<x2>().bro.transform.parent = transform.parent;
            other.transform.tag = "collected";
            other.gameObject.GetComponent<x2>().bro.transform.tag = "collected";
            other.transform.localPosition = new Vector3(transform.localPosition.x + 0.26f, transform.localPosition.y - 0.15f, transform.localPosition.z);
            other.gameObject.GetComponent<x2>().bro.transform.localPosition= new Vector3(transform.localPosition.x + 0.10f, transform.localPosition.y - 0.43f, transform.localPosition.z);
            other.gameObject.AddComponent<stack>();
            other.gameObject.GetComponent<x2>().bro.AddComponent<stack>();
            cubes.Add(other.gameObject);
            if (other.gameObject.GetComponent<x2>().bro.GetComponent<fire>().firing==false)
            {
                other.gameObject.GetComponent<x2>().bro.GetComponent<fire>().firing = true;
                other.gameObject.GetComponent<x2>().bro.GetComponent<fire>().startFire();
                cubes.Add(other.gameObject.GetComponent<x2>().bro);
            }
            Debug.Log(cubes.Count);
        }
        else if (other.transform.tag=="multiplex2Right")
        {
            Destroy(this);
            other.transform.parent = transform.parent;
            other.gameObject.GetComponent<x2>().bro.transform.parent = transform.parent;
            other.transform.tag = "collected";
            other.gameObject.GetComponent<x2>().bro.transform.tag = "collected";
            other.transform.localPosition = new Vector3(transform.localPosition.x + 0.26f, transform.localPosition.y - 0.15f, transform.localPosition.z);
            other.gameObject.GetComponent<x2>().bro.transform.localPosition = new Vector3(transform.localPosition.x + 0.42f, transform.localPosition.y +0.08f, transform.localPosition.z);
            other.gameObject.AddComponent<stack>();
            other.gameObject.GetComponent<x2>().bro.AddComponent<stack>();
            cubes.Add(other.gameObject);
            if (other.gameObject.GetComponent<x2>().bro.GetComponent<fire>().firing == false)
            {
                other.gameObject.GetComponent<x2>().bro.GetComponent<fire>().firing = true;
                other.gameObject.GetComponent<x2>().bro.GetComponent<fire>().startFire();
                cubes.Add(other.gameObject.GetComponent<x2>().bro);
            }
            Debug.Log(cubes.Count);
        }
    }
}
