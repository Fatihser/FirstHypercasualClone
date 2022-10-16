using UnityEngine;
using TMPro;

public class barrel : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI hpText;
    private Material mat;

    private void Start()
    {
        hpText.text = health.ToString();
        mat = GetComponent<Renderer>().material;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="bullet")
        {
            turnEmissionOn();
            Invoke("turnEmissionOff", 0.12f);
            Destroy(other.gameObject);
            vibration.vibrate();
            if (health<=1)
            {
                Destroy(gameObject);
            }
            hpText.text = (--health).ToString();
        }
        else if (other.tag == "collected")
        {
            foreach (GameObject item in stack.cubes)
            {
                item.GetComponentInParent<Rigidbody>().AddForce(0, 0, -10);
                item.GetComponentInParent<Rigidbody>().useGravity = true;
                Camera.main.GetComponent<Animator>().enabled = true;
                item.GetComponent<Animator>().enabled = true;
                if (item.GetComponent<fire>() != null)
                {
                    Destroy(item.GetComponent<fire>());
                }
            }
            other.GetComponentInParent<Player>().speed = 0.0f;
            other.GetComponentInParent<Player>().speedUpValue = 0.0f;
        }
    }

    private void turnEmissionOn()
    {
        mat.EnableKeyword("_EMISSION");
    }

    private void turnEmissionOff()
    {
        mat.DisableKeyword("_EMISSION");
    }
}
