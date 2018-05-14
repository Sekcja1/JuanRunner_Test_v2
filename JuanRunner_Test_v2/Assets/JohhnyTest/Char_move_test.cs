using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Char_move_test : MonoBehaviour
{


	Animator anim;

	private new GameObject gameObject;
	private Vector3 position;
    private Vector3 startPos;

	bool isgrounded = true;
	public float speed = 5;
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

    public bool grounded = true;

	public GameObject pistolBullet;
	public GameObject granade;
	public GameObject mele;
	private GameObject camera;

    private AudioSource shoot;

    private Vector3 prev, curr;

    public GameObject feet { get; set; }
	public GameObject Gun { get; set; }
	public GameObject Gun2 { get; set; }

	// Use this for initialization
	void Start()
	{
	    gameObject = GameObject.Find("RedBeret");
	    feet = GameObject.Find("RedBeretFeet");
        camera = GameObject.Find("Camera");
		anim = GetComponent<Animator>();
	    startPos = gameObject.transform.position;

	    shoot = this.gameObject.GetComponent<AudioSource>();

        UseWeapon = 0;
		cross = GameObject.Find("Aim");
		Gun = GameObject.Find("Gun");
		Gun2 = GameObject.Find("Gun2");

		rotation = gameObject.transform.rotation;

		weapons.Add(new Pistol() { AmmoCount = 50000, AmmoPrefab = pistolBullet });
		weapons.Add(new Granade() { AmmoCount = 20000, AmmoPrefab = granade });
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


		if (Input.GetKeyDown(KeyCode.Space))
		{
			anim.SetBool("char_normal_shoot", true);
			Normal_shoot = true;

            if(UseWeapon == 0)
		        shoot.Play();

		    weapons[UseWeapon].Shoot(Gun.transform.position, Gun2.transform.position, rotation);
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
    
	    if (Input.GetKey(KeyCode.UpArrow) && grounded)
	    {
	        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
	        grounded = false;

	    }

	    var cols = Physics2D.OverlapCircleAll(transform.position, 10F);

        if (cols.Any(a=>feet.GetComponent<BoxCollider2D>().IsTouching(a)))
        {
            grounded = true;

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

        if (gameObject.transform.position.y <= -30.0f)
        {
            gameObject.transform.position = startPos;
        }
    }

}
