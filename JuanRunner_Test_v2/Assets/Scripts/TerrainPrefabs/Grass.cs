using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : IBlock
{
    // Use this for initialization
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
