using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollision : MonoBehaviour {

    // Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    private void OnTriggerEnter(Collider other)
    {
        transform.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.5f);
    }

    private void OnTriggerExit(Collider other)
    {
        transform.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 0.5f);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag.Equals("MyCube"))
            transform.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.5f);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag.Equals("MyCube"))
            transform.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.5f);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag.Equals("MyCube"))
            transform.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 0.5f);
    }*/
}
