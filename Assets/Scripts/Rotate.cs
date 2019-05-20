using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float centerX = 16f;
    public float centerZ = 9f;

    public float speed = 10.0f;

    protected float fDistance = 1;
    protected float fSpeed = 1;


    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        //transform.LookAt(new Vector3(centerX, 0.0f, centerY));

        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow)) OrbitHorizonal(false);
        if (Input.GetKey(KeyCode.LeftArrow)) OrbitHorizonal(true);

        #region hide
        //if (Input.GetKey(KeyCode.DownArrow)) OrbitVertical(false);
        //if (Input.GetKey(KeyCode.UpArrow)) OrbitVertical(true);
        //if (Input.GetKey(KeyCode.UpArrow)) MoveInOrOut(false);
        //if (Input.GetKey(KeyCode.DownArrow)) MoveInOrOut(true);
        #endregion
    }

    protected void OrbitHorizonal(bool bLeft)
    {
        float step = fSpeed * Time.deltaTime;
        float fOrbitCircumfrance = 2F * fDistance * Mathf.PI;
        float fDistanceDegrees = (fSpeed / fOrbitCircumfrance) * 360;
        float fDistanceRadians = (fSpeed / fOrbitCircumfrance) * 2 * Mathf.PI;
        if (bLeft)
        {
            transform.RotateAround(new Vector3(centerX/2, 0.0f, centerZ/2), Vector3.up, -fDistanceRadians);
        }
        else
            transform.RotateAround(new Vector3(centerX/2, 0.0f, centerZ/2), Vector3.up, fDistanceRadians);
    }



    #region HIDE
    //protected void OrbitVertical(bool bLeft)
    //{
    //    float step = fSpeed * Time.deltaTime;
    //    float fOrbitCircumfrance = 2F * fDistance * Mathf.PI;
    //    float fDistanceDegrees = (fSpeed / fOrbitCircumfrance) * 360;
    //    float fDistanceRadians = (fSpeed / fOrbitCircumfrance) * 2 * Mathf.PI;
    //    if (bLeft)
    //    {
    //        transform.RotateAround(new Vector3(centerX / 2, 0.0f, centerZ / 2), Vector3.right, -fDistanceRadians);
    //    }
    //    else
    //        transform.RotateAround(new Vector3(centerX / 2, 0.0f, centerZ / 2), Vector3.right, fDistanceRadians);
    //}

    //protected void MoveInOrOut(bool bLeft)
    //{
    //    float step = fSpeed * Time.deltaTime;
    //    float fOrbitCircumfrance = 2F * fDistance * Mathf.PI;
    //    float fDistanceDegrees = (fSpeed / fOrbitCircumfrance) * 360;
    //    float fDistanceRadians = (fSpeed / fOrbitCircumfrance) * 2 * Mathf.PI;
    //    if (bLeft)
    //    {
    //        transform.RotateAround(new Vector3(centerX / 2, 0.0f, centerZ / 2), new Vector3(1, 0, 0), -fDistanceRadians);
    //        transform.RotateAround(new Vector3(centerX / 2, 0.0f, centerZ / 2), new Vector3(0, 0, 1), -fDistanceRadians);
    //    }
    //    else
    //    {
    //        transform.RotateAround(new Vector3(centerX / 2, 0.0f, centerZ / 2), new Vector3(1, 0, 1), fDistanceRadians);
    //        transform.RotateAround(new Vector3(centerX / 2, 0.0f, centerZ / 2), new Vector3(0, 0, 1), fDistanceRadians);
    //    }
    //}
    #endregion
}
