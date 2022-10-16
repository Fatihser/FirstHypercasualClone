using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float swipeSpeed = 8.0f;
    public Transform spawnPoint;
    private float fireCDdiscrement = 0.1f;
    private float fireCooldownTime = 1.0f;
    public GameObject bulletPrefab;
    public bool stackIsEmpty = true;
    public float speedUpTime = 0.1f;
    private bool inFinis = false;
    public float speedUpValue = 0.05f;
    public GameObject winPanel;
    public GameObject losePanel;
    Vector3 endTouchPos;
    private bool crash = false;
    private void Start()
    {
        Time.timeScale = 1.0f;
        Application.targetFrameRate = 120;
        startFire();
    }

    private void FixedUpdate()
    {
        move();
    }


    private void move()
    {
        //transform translate'e cevirebilirim.
        //kamera takip scripti eklenebilir.
        transform.position += new Vector3(0, 0, speed * Time.deltaTime);

        if (Input.touchCount > 0)
        {
            if (endTouchPos.x==0)
            {
                endTouchPos.x = Input.GetTouch(0).position.x;
                return;
            }
            if (Input.GetTouch(0).position.x > endTouchPos.x)
            {
                endTouchPos.x = Input.GetTouch(0).position.x;
                transform.position += new Vector3(swipeSpeed * Time.deltaTime, 0, 0);
                if (transform.position.x > 1.90f) { transform.position = new Vector3(1.90f, transform.position.y, transform.position.z); }

            }
            else if(Input.GetTouch(0).position.x < endTouchPos.x)
            {
                endTouchPos.x = Input.GetTouch(0).position.x;
                transform.position += new Vector3(-swipeSpeed * Time.deltaTime, 0, 0);
                if (transform.position.x < -1.95f) { transform.position = new Vector3(-1.90f, transform.position.y, transform.position.z); }
            }
            if (Input.GetTouch(0).phase==TouchPhase.Ended)
            {
                endTouchPos.x = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stackIsEmpty&&other.transform.tag == "collected")
        {
            if (other.GetComponent<fire>()!=null)
            {
                stackIsEmpty = false;
                other.GetComponent<fire>().firing = true;
                other.GetComponent<fire>().fireCDdiscrement = fireCDdiscrement + other.GetComponent<fire>().fireCDdiscrement;
                other.GetComponent<fire>().startFire();
                stopFire();
            }
        }
        else if (other.transform.tag=="finish")
        {
            StartCoroutine("speedUp");
            inFinis = true;
        }
        else if (other.transform.tag=="barrel"&&inFinis)
        {
            this.enabled = false;
            Invoke("getWinPanel",1.5f);
        }
        else if (other.transform.tag=="barrel"&&!inFinis)
        {
            this.enabled = false;
            Invoke("getlosePanel", 1.5f);
        }
    }

    private void getlosePanel()
    { 
        losePanel.SetActive(true);
    }   
    private void getWinPanel()
    {
        winPanel.SetActive(true);
    }
    public void startFire()
    {
        StartCoroutine("Fire");
    }

    public void stopFire()
    {
        StopCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        while (true)
        {
            Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(fireCooldownTime);
        }
    }

    IEnumerator speedUp()
    {
        while (true)
        {
            speed += speedUpValue;   
            yield return new WaitForSeconds(speedUpTime);
        }
    }

    public void setPistolAgain()
    {
        stackIsEmpty = true;
        spawnPoint.gameObject.AddComponent<stack>();
        startFire();
    }
}
