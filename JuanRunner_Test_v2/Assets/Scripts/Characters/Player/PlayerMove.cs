using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PlayerMove : MonoBehaviour
{
    private GameObject gameObject;
    private Vector3 position;
    private Quaternion rotation;

    public int UseWeapon { get; set; }
    public GameObject bullet;
    public GameObject granade;

    public float speed = 2;
    private GameObject cross;


    // Use this for initialization
    void Start()
    {
        UseWeapon = 1;
        gameObject = GameObject.Find("Player");
        cross = GameObject.Find("Aim");
        rotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        position = gameObject.transform.position;

        #region Sterowanie
        if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            position.y += 2 * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            position.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            UseWeapon = 1;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            UseWeapon = 2;
        }
        #endregion

        gameObject.transform.SetPositionAndRotation(position, rotation);

        #region Click
        var cameraloc = new Camera();
        var mousePos = new Vector3();
        var worldPos = new Vector3();
        cameraloc = GameObject.Find("Camera").GetComponent<Camera>();
        mousePos = Input.mousePosition;
        worldPos = cameraloc.ScreenToWorldPoint(mousePos);
        #endregion
        cross.transform.position = worldPos + new Vector3(0,0,10);

        if (Input.GetMouseButtonDown(0))
        {
            var a = worldPos;
            a.z = position.z;
            a.Normalize() ;
            a *= 1.5F;

            if (UseWeapon == 1)
            {
                var bullet = Instantiate(this.bullet, position + a, rotation);

                bullet.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(worldPos.x*1.2F, worldPos.y * 1.2F),
                    ForceMode2D.Impulse);
            }else if (UseWeapon == 2)
            {
                var bullet = Instantiate(this.granade, position + a, rotation);

                bullet.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(worldPos.x - position.x, worldPos.y - position.y),
                    ForceMode2D.Impulse);
            }


        }

        //if (Input.GetMouseButtonDown(1))
        //if (Input.GetMouseButtonDown(2))
    }

}
