﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    public List<Ball> Balls;

    [SerializeField]
    private Ball ballPrefab;

    public int maxBAAALLSSSSSSSSS = 8;

    public Transform[] Cannons = new Transform[4];

    [SerializeField]
    float _firingInterval = 5f;
    float _ballTimer = 0f;
    [SerializeField]
    float _fireForce = 5f;

    bool canFire = false;


	// Use this for initialization
	void Start ()
    {
        for(int i = 0; i<maxBAAALLSSSSSSSSS; i++)
        {
            Balls.Add(Instantiate(ballPrefab, transform.position, transform.rotation, transform));
        }
		//foreach(Ball ball in Balls)
  //      {
  //          ball.transform.position = RandomizeCannon().position;
  //      }
	}

    private void Update()
    {
        if (_ballTimer >= 0)
        {
            _ballTimer -= Time.deltaTime;
        }
        else
        {
            canFire = true;
            _ballTimer = _firingInterval;
        }
    }

    Transform RandomizeCannon()
    {
        return Cannons[Random.Range(0, 4)];
    }
	
	// Physics related stuff in fixed update
	void FixedUpdate () {
        foreach(Ball ball in Balls)
        {
            if(ball.transform.position.y < -5)
            {
                ball.gameObject.SetActive(false);
                ball.Rb.velocity = Vector3.zero;
            }
            if (!ball.gameObject.activeSelf && canFire)
            {
                var cannon = RandomizeCannon();
                ball.transform.position = cannon.position;
                ball.gameObject.SetActive(true);
                ball.Rb.AddForce(cannon.up * _fireForce, ForceMode.Impulse);
                canFire = false;
            }
        }
    }
}