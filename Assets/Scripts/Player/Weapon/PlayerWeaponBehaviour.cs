using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class PlayerWeaponBehaviour : MonoBehaviour {

    public GameObject bullet;
    public bool canShoot;
    public AudioSource audioShot;

    public float timeChargingShootLimit;
    public float timeToBigShoot;
    public string weaponType = "Normal";

    public ShootsBehaviour shootsBehaviour;
    public float timeChargingShoot;
    private PlayerBehaviour player;
    private string currentShoot;    
    private float tapsToShoot;
    private bool bigShoot;
    private CamShake cameraShake;

    // Use this for initialization
    void Awake () {
        this.player = GetComponentInParent<PlayerBehaviour>();
        this.canShoot = true;
        this.timeToBigShoot = 0;
        this.audioShot = GetComponent<AudioSource>();
        this.shootsBehaviour = GetComponent<ShootsBehaviour>();
        this.cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamShake>();
    }
    

    void FixedUpdate()
    {
        if (this.canShoot) { 

            if (this.currentShoot != this.weaponType + "Shoot")
            {
                this.currentShoot = this.weaponType + "Shoot";
            }
        
            if (Input.GetButton("Fire1"))
            {
                if (!this.bigShoot)
                {
                    this.cameraShake.ShakeCamera(5f);
                    this.shootsBehaviour.Shoot(this.currentShoot, false, this.player.facingDirection);
                }
                else
                {
                    this.ChargeBigShoot();
                }
            }

            if (Input.GetButtonUp("Fire1") || this.timeChargingShoot >= this.timeChargingShootLimit)
            {
                if (this.bigShoot)
                {
                    this.ReleaseBigShoot();
                    this.cameraShake.ShakeCamera(10f);
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                this.tapsToShoot++;
                this.timeToBigShoot = 0.2f;
            }

            this.CheckBigShoot();
        }
    }

    void CheckBigShoot() {
        if (!this.bigShoot)
        {
            if (this.tapsToShoot > 0)
            {
                this.timeToBigShoot -= Time.fixedDeltaTime;
            }

            if (this.timeToBigShoot <= 0)
            {
                this.tapsToShoot = 0;
                this.timeToBigShoot = 1f;
            }
            else
            {
                if (this.tapsToShoot == 2)
                {
                    this.bigShoot = true;
                }
            }
        }
    }

    void ChargeBigShoot()
    {
        this.timeChargingShoot += Time.deltaTime;
    }

    void ReleaseBigShoot()
    {
        this.shootsBehaviour.Shoot(this.currentShoot, true, this.player.facingDirection);
        this.bigShoot = false;
        this.timeChargingShoot = 0f;
        this.tapsToShoot = 0;
        this.timeToBigShoot = 0f;        
    }

	

	
}
