using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float x, y;

    // Use this for initialization
    void Start()
    {
        var obj = GameObject.Find("Player");

        x = obj.transform.position.x;
        y = obj.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 2;
        var cam = GameObject.Find("Main Camera");
        
        if (Input.GetKey(KeyCode.RightArrow))
            x+= speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
            x -= speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow))
            y += speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow))
            y -= speed * Time.deltaTime;

        this.transform.SetPositionAndRotation(new Vector3(x, y), new Quaternion(x, y, 0, 0));
        cam.transform.SetPositionAndRotation(new Vector3(x, y,-10), new Quaternion(0, 0, 0, 0));

    }
}
