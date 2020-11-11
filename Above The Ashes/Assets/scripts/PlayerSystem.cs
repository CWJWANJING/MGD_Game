﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public double healthPoint = 10;
    public double ammo = 10;
    public double attackPoint = 5;
    public double range = 10;
    public double bulletSpeed = 20;

    public double shootSpeed = 1;
    private double shootTimer = 0;
    private double shootTimeInterval = 0;

    public Text PlayerUI;

    public GameSystem gs;
    public TPSCamera TPSC;
    public AudioSource ads;

    public Boolean isAttack = false;

    void Start()
    {
        PlayerUI.text = "";
        shootTimeInterval = 1 / shootSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerUI.text = "HP: " + healthPoint + "  Ammo:" + ammo + " Attack:" + isAttack;
        if (Input.GetMouseButtonDown(0) && ammo !=0) {
            isAttack = true;
        }
        if (Input.GetMouseButtonUp(0)) {
            isAttack = false;
        }
        shootTimer += Time.deltaTime;
        if ((shootTimer > shootTimeInterval) && isAttack && ammo != 0 && TPSC.isAiming) {
            fire();
            shootTimer = 0;
        }
    }

    private void fire() {
        //计算准星的位置
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * 5;
        RaycastHit hit;
        ammo -= 1;
        ads.Play();
        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, 5))
        {
            if (hit.collider.gameObject.tag == "Zombie") {
                gs.HitTo(hit.collider.gameObject);
            }
            targetPosition = hit.point;
        }
        
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        //if (gs.player.isAttack && other.tag == "Zombie")
        //{
           // gs.HitTo(gameObject);
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        
    }




}