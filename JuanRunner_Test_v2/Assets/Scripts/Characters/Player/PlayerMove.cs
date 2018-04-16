using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private GameObject gameObject;
    private Vector3 position;
    private Quaternion rotation;
    bool isgrounded = true;
    public GameObject particle;

    public float speed = 2;

    private ParticleSystem emiter = new ParticleSystem();

    // Use this for initialization
    void Start()
    {
        gameObject = GameObject.Find("Player");
        rotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        position = gameObject.transform.position;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow) && isgrounded)
        {
            position.y += 2 * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            position.y -= speed * Time.deltaTime;
        }

        gameObject.transform.SetPositionAndRotation(position, rotation);


        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Pressed primary button.");
            //Debug.Log(Input.mousePosition.ToString());
        }

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed secondary button.");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");

        if (Input.GetMouseButtonDown(0))
        {
            emiter.emission.SetBurst(1, new ParticleSystem.Burst(1000,500,750));
            emiter.Play();
        }
        else
        {
            emiter.Stop();
            //emiter.emit = false;
        }
    }

}
