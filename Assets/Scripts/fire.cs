using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    private float fireCooldownTime = 1.0f;
    public float fireCDdiscrement = 0.1f;
    public GameObject bulletPrefab;
    public bool firing = false;

    private void Start()
    {
        fireCooldownTime -= fireCDdiscrement;
        if (firing)
        {
            StartCoroutine("startFire");
            transform.tag = "collected";
        }
    }
    IEnumerator Fire()
    {
        while (true)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(fireCooldownTime);
        }
    }
    public void startFire()
    {
        fireCooldownTime -= fireCDdiscrement;
        if (fireCooldownTime<=0)
        {
            fireCooldownTime = 0.01f;
        }
        StartCoroutine("Fire");
    }

    public void stopFire()
    {
        StopCoroutine("Fire");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "collect" || other.transform.tag == "multiplex2left" || other.transform.tag == "multiplex2Right")
        {
            other.GetComponent<fire>().firing = true;
            if (other.GetComponent<fire>().fireCDdiscrement>=0.9)
            {
                other.GetComponent<fire>().fireCDdiscrement = 0.005f + other.GetComponent<fire>().fireCDdiscrement;
            }
            other.GetComponent<fire>().fireCDdiscrement = fireCDdiscrement+ other.GetComponent<fire>().fireCDdiscrement;
            other.GetComponent<fire>().startFire();
            stopFire();
            Destroy(this);
        }
    }
}
