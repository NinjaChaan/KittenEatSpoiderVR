
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KittenScript : MonoBehaviour {
    
    public float timer = 0;
    public bool isGazed = false;
    float speed = 1;
    bool eating = false;

    Rigidbody rb;
    Animator a;
    Spawner s;
    public Text spoidersKilledText;
    int spoidersKilled = 0;

    public Camera cam;

	// Use this for initialization
	void Start ()
    {
        s = FindObjectOfType<Spawner>();
        rb = GetComponent<Rigidbody>();
        a = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (isGazed)
        //{
        //    timer += Time.deltaTime;
        //}
        //if(timer >= 2)
        //{
        //    Destroy(gameObject);
        //}

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            print("I'm looking at " + hit.point);
        else
            print("I'm looking at nothing!");
        hit.point = new Vector3(hit.point.x, 0, hit.point.z);
        float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
        if (Vector3.Distance(hit.point, transform.position) > 0.5f && !eating)
        {
            a.SetBool("walking", true);
            rb.velocity = (hit.point - transform.position).normalized * speed;
            //transform.LookAt(hit.point);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(hit.point - transform.position), Time.deltaTime * 3);
        }
        else
        {
            a.SetBool("walking", false);
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }

    public void SetGazeAt(bool gazeAt)
    {
        isGazed = gazeAt;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Spider" && !eating)
        {
            //transform.LookAt(col.transform);
            
            col.collider.enabled = false;
            col.gameObject.GetComponent<SpiderScript>().eaten = true;
            eating = true;
            rb.velocity = new Vector3(0, 0, 0);
            a.SetTrigger("eating");
            StartCoroutine(Eating(col));
            StartCoroutine(Rotating(col));

        }
    }

    IEnumerator Rotating(Collision col) {
        while (eating) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(col.transform.position - transform.position), Time.deltaTime * 2);
            yield return null;
        }

    }

    IEnumerator Eating(Collision col)
    {
        yield return new WaitForSeconds(2.5f);
        eating = false;
        s.spiderList.Remove(col.gameObject);
        Destroy(col.gameObject);
        spoidersKilled++;
        spoidersKilledText.text = "Spoiders killed: " + spoidersKilled;
    }
}
