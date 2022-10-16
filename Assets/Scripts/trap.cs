using UnityEngine;

public class trap : MonoBehaviour
{
    private bool onceTime = true;
    void Update()
    {
        transform.Rotate(0,0, 40 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="collected")
        {
            if (onceTime)
            {
                onceTime = false;
                other.gameObject.transform.parent.GetComponent<Player>().setPistolAgain();

            }
            foreach (GameObject item in stack.cubes)
            {
                item.GetComponent<Animator>().enabled = true;
                if (item.GetComponent<fire>()!=null)
                {
                    item.GetComponent<fire>().stopFire();
                    Destroy(item.GetComponent<fire>());
                }
                item.gameObject.transform.parent = null;
            }
            stack.cubes.Clear();
        }
        if (other.tag=="Player")
        {
            other.GetComponent<Rigidbody>().AddForce(0, 0, -500);
        }
    }
}
