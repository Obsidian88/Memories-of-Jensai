﻿using UnityEngine;
using System.Collections;
using System;

public class ApplyDamage : MonoBehaviour {

    public float force;
    public int damage;
    public string[] damages;

    public struct Parameter
    {
        public int damage;
        public float force;
        public Vector3 direction;
        public Transform transform;
        public Parameter(int x, float y, Vector3 v, Transform z)
        {
            damage = x;
            force = y;
            direction = v;
            transform = z;
        }
    }

    private Rigidbody body;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Array.IndexOf(damages, collision.gameObject.tag) > -1) {
            Parameter args = new Parameter(damage, force,body.velocity, transform);
            collision.gameObject.SendMessage("ApplyDamage", args);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (Array.IndexOf(damages, collision.gameObject.tag) > -1)
        {
            Parameter args = new Parameter(damage, force, body.velocity, transform);
            collision.gameObject.SendMessage("ApplyDamage", args);
        }
    }
}
