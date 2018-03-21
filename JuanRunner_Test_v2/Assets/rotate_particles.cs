using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_particles : MonoBehaviour {

    Quaternion rotacja;
    Vector3 pozycja;
    int mnoznik = 1;

    public string name;
	// Use this for initialization
	void Start () {
        var obj = GameObject.Find(name);
        rotacja = obj.transform.rotation;
        pozycja = obj.transform.position;

        //var obj = GameObject.Find("Player");

        //x = obj.transform.position.x;
        //y = obj.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () { 

        if(pozycja.x >= 10)
        {
            mnoznik = -1;
        }
        else if(pozycja.x <= -10)
        {
            mnoznik = 1;
        }

        pozycja.x += mnoznik * Time.deltaTime * 2;
        // Rotate the object around its local X axis at 1 degree per second
        this.transform.SetPositionAndRotation(pozycja, rotacja);
        
        // transform.Rotate(Vector3.up * Time.deltaTime);
        //   transform.Rotate(Vector3.forward * Time.deltaTime);
        // ...also rotate around the World's Y axis
        // transform.Rotate(Vector3.up * Time.deltaTime, Space.World);



        //rotacja.z -= 30.0F;

        //this.transform.SetPositionAndRotation(new Vector3(-8, 21, 2.8f), new Quaternion(0,0,rotacja.z, 0));
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotacja, Time.deltaTime * 2f);
        ////float speed = 2;
        //var cam = GameObject.Find("Main Camera");

        //if (Input.GetKey(KeyCode.RightArrow))
        //    x += speed * Time.deltaTime;
        //if (Input.GetKey(KeyCode.LeftArrow))
        //    x -= speed * Time.deltaTime;
        //if (Input.GetKey(KeyCode.UpArrow))
        //    y += speed * Time.deltaTime;
        //if (Input.GetKey(KeyCode.DownArrow))
        //    y -= speed * Time.deltaTime;

        //this.transform.SetPositionAndRotation(new Vector3(x, y), new Quaternion(x, y, 0, 0));
        //cam.transform.SetPositionAndRotation(new Vector3(x, y, -10), new Quaternion(0, 0, 0, 0));



    }
}
