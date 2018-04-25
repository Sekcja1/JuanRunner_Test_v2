using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_move_test : MonoBehaviour
{


    Animator anim;

    private new GameObject gameObject;
    private Vector3 position;
    bool isgrounded = true;
    public float speed = 3;
    public int UseWeapon { get; set; }

    public bool move = false;
    private Quaternion rotation;
    bool DirRight = true;
    bool DirLeft = false;
    bool Dir_triggerL = true;
    bool Dir_triggerR = true;
    bool Dir_IDLE = true;
    bool Normal_shoot = false;
    bool crouch = false;
    private GameObject cross;
    private List<IWeapon> weapons = new List<IWeapon>();

    public GameObject pistolBullet;
    public GameObject granade;
    public GameObject mele;
    private GameObject camera;

    public GameObject Gun { get; set; }

    // Use this for initialization
    void Start()
    {
        gameObject = GameObject.Find("RedBeret");
        camera = GameObject.Find("Camera");
        anim = GetComponent<Animator>();

        UseWeapon = 0;
        cross = GameObject.Find("Aim");
        Gun = GameObject.Find("Gun");

        rotation = gameObject.transform.rotation;

        weapons.Add(new Pistol() { AmmoCount = 50, AmmoPrefab = pistolBullet });
        weapons.Add(new Granade() { AmmoCount = 20, AmmoPrefab = granade });
        weapons.Add(new Mele() { AmmoPrefab = mele });
    }

    // Update is called once per frame
    void Update()
    {
        float char_move = Input.GetAxis("Vertical");
        //anim.SetFloat("char_speed", );
        position = gameObject.transform.position;

        Dir_IDLE = true;

        camera.transform.position = new Vector3(position.x, position.y, camera.transform.position.z);


        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("char_normal_shoot", true);
            Normal_shoot = true;
        }
        else
        {
            anim.SetBool("char_normal_shoot", false);
            Normal_shoot = false;
        }

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.DownArrow) && Normal_shoot == false)
        {
            position.x += speed * Time.deltaTime;
            DirRight = true;
            Dir_triggerR = false;
            Dir_triggerL = true;
            DirLeft = false;
            Dir_IDLE = false;
            anim.SetBool("char_moving", true);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && Normal_shoot == false)
        {
            position.x -= speed * Time.deltaTime;
            DirRight = false;
            Dir_triggerR = true;
            Dir_triggerL = false;
            DirLeft = true;
            Dir_IDLE = false;
            anim.SetBool("char_moving", true);
        }
        if (Input.GetKey(KeyCode.UpArrow) && isgrounded && Normal_shoot == false)
        {
            //position.y += 2 * speed * Time.deltaTime;
            Dir_IDLE = false;
            //anim.SetBool("char_moving", true);
        }
        if (Input.GetKey(KeyCode.DownArrow) && Normal_shoot == false)
        {
            // position.y -= speed * Time.deltaTime;
            Dir_IDLE = false;
            crouch = true;
            anim.SetBool("char_crouch", true);


        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            UseWeapon = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            UseWeapon = 1;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            UseWeapon = 2;
        }

        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.C))
        {
            anim.SetBool("char_crouch_shoot", true);
        }

        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.C))
        {
            anim.SetBool("char_crouch_shoot", false);
        }

        if (!Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("char_crouch", false);
        }

        if (Dir_IDLE == true)
        {
            anim.SetBool("char_moving", false);
        }

        gameObject.transform.SetPositionAndRotation(position, rotation);


        #region Click
        var cameraloc = new Camera();
        var mousePos = new Vector3();
        var worldPos = new Vector3();
        cameraloc = GameObject.Find("Camera").GetComponent<Camera>();
        mousePos = Input.mousePosition;
        worldPos = cameraloc.ScreenToWorldPoint(mousePos);
        #endregion

        cross.transform.position = worldPos + new Vector3(0, 0, 10);

        if (Input.GetMouseButtonDown(0))
        {
            weapons[UseWeapon].Shoot(Gun.transform.position, worldPos, rotation);
        }

        Vector3 thescale = transform.localScale;
        if (DirRight == true)
        {
            thescale.x = 1;
        }

        else if (DirLeft == true)
        {
            thescale.x = -1;
        }
        transform.localScale = thescale;

    }

}
/*
 * 
 
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
	bool isgrounded = true;
	public GameObject particle;

	public GameObject bullet;

	private List<GameObject> Bulets = new List<GameObject>();

	public float speed = 2;
	private GameObject cross;

	private ParticleSystem emiter = new ParticleSystem();

	// Use this for initialization
	void Start()
	{
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
		if (Input.GetKey(KeyCode.UpArrow) && isgrounded)
		{
			position.y += 2 * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			position.y -= speed * Time.deltaTime;
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

			var bullet = Instantiate(this.bullet, position + a, rotation);

			bullet.GetComponent<Rigidbody2D>().AddForce(
				new Vector2(worldPos.x - position.x, worldPos.y - position.y),
				ForceMode2D.Impulse);

			//Bulets.Add(bullet);

			Destroy(bullet, 10.0f);

			Debug.Log(string.Format("Pressed secondary button. Coś: {0} | Coś drugiego: {1}", mousePos, worldPos));
		}

		//if (Input.GetMouseButtonDown(1))
		//if (Input.GetMouseButtonDown(2))
	}

}


	*/
