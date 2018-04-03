using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    //类名和文件名需要一样 不然会报错 
    // Use this for initialization  
    void Start()
    { 

    }

    // Update is called once per frame  
    void Update()
    {

        GameObject.Find("Sun").transform.Rotate(Vector3.up * Time.deltaTime * 8);

        GameObject.Find("Mercury").transform.RotateAround(Vector3.zero, new Vector3(0.2f, 1, 0), 100 * Time.deltaTime);
        GameObject.Find("Mercury").transform.Rotate(Vector3.up * Time.deltaTime * 100 / 58);

        GameObject.Find("Venus").transform.RotateAround(Vector3.zero, new Vector3(0, 1, -0.1f), 75 * Time.deltaTime);
        GameObject.Find("Venus").transform.Rotate(Vector3.up * Time.deltaTime * 100 / 243);

        GameObject.Find("Earth").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 55 * Time.deltaTime);
        GameObject.Find("Earth").transform.Rotate(Vector3.up * Time.deltaTime * 100);

        GameObject.Find("Moon").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 5 * Time.deltaTime);
        GameObject.Find("Moon").transform.Rotate(Vector3.up * Time.deltaTime * 100 / 27);

        GameObject.Find("Mars").transform.RotateAround(Vector3.zero, new Vector3(0.2f, 1, 0.1f), 40 * Time.deltaTime);
        GameObject.Find("Mars").transform.Rotate(Vector3.up * Time.deltaTime * 100);

        GameObject.Find("Jupiter").transform.RotateAround(Vector3.zero, new Vector3(0.1f, 2, 0.1f), 25 * Time.deltaTime);
        GameObject.Find("Jupiter").transform.Rotate(Vector3.up * Time.deltaTime * 100 / 0.3f);

        GameObject.Find("Saturn").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0.2f), 15 * Time.deltaTime);
        GameObject.Find("Saturn").transform.Rotate(Vector3.up * Time.deltaTime * 100 / 0.4f);

        GameObject.Find("Uranus").transform.RotateAround(Vector3.zero, new Vector3(0, 2, -0.1f), 10 * Time.deltaTime);
        GameObject.Find("Uranus").transform.Rotate(Vector3.up * Time.deltaTime * 100 / 0.6f);

        GameObject.Find("Neptune").transform.RotateAround(Vector3.zero, new Vector3(0.1f, 1, 0.1f), 5 * Time.deltaTime);
        GameObject.Find("Neptune").transform.Rotate(Vector3.up * Time.deltaTime * 100 / 0.7f);

    }
}