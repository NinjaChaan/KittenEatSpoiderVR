using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    Spawner s;
    float speed = 5;
    public bool eaten = false;

    // Use this for initialization
    void Start()
    {
        s = FindObjectOfType<Spawner>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        if (!eaten)
            rb.velocity = transform.forward * (Time.deltaTime * speed);
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
        }

        //transform.Translate(Vector3.forward * Time.deltaTime * 0.2f);
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
        //anim.SetTrigger("Jump");
        //StartCoroutine(Scale(Random.Range(0.01f, 0.5f)));
        //transform.localScale = new Vector3 (rand, rand, rand);
        //transform.parent.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
    }

    IEnumerator ChangeDirection()
    {
        if (!eaten)
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                if(!eaten)
                    transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));

            }
        }
    }

    
}
