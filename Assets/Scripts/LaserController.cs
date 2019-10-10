using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

    public Rigidbody2D rb2;
    public Vector2 speed;
	// Use this for initialization

	void Start () 
    {
        rb2 = GetComponent<Rigidbody2D>();
        rb2.velocity = speed;
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        rb2.velocity = speed;
    }
}
