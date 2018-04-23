using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{

    public GameObject Particles;

    private Collision2D lastColl;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Particles.gameObject.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject,1.5f);
        lastColl = coll;
    }

    void OnDestroy()
    {
        lastColl.gameObject.SendMessage("AddDamage", 25.0F);
    }

}
