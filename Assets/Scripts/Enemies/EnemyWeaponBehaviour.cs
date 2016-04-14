using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyWeaponBehaviour : MonoBehaviour {

    public Transform player;
    public EnemyBehaviour enemy;
    public GameObject bullet;
    public bool canShoot;
    public float shootCooldown;

    private List<GameObject> bulletPool;
    private GameObject bulletShoot;

    private SpriteRenderer bulletSpawn;
    private AudioSource audioShot;

    // Use this for initialization
    void Awake () {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        this.enemy = GetComponentInParent<EnemyBehaviour>();
        this.bulletSpawn = GetComponentInChildren<SpriteRenderer>();
        this.bulletPool = new List<GameObject>();
        this.CreateBulletsPool();
        this.canShoot = true;
        //this.audioShot = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (canShoot)
        {
            this.Shoot();
        }
	}

    void Shoot()
    {
        if (this.bulletPool.Count > 0)
        {
            this.canShoot = false;
            this.bulletShoot = bulletPool[0];
            this.bulletPool.Remove(bulletShoot);

            this.bulletShoot.transform.parent = null;
            this.bulletShoot.transform.position = new Vector3(this.transform.position.x,
                                                                this.transform.position.y, 0);
            this.bulletShoot.transform.localScale = new Vector3(4f, 4f, 0);
            this.bulletShoot.GetComponent<EnemyBulletBehaviour>().weapon = this;
            this.bulletShoot.GetComponent<EnemyBulletBehaviour>().enemyBehaviour = this.enemy;
            this.bulletShoot.SetActive(true);
            this.bulletShoot.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, this.player.localPosition.y);
            this.bulletShoot.GetComponent<Rigidbody2D>().AddForce(new Vector2(10f * this.enemy.faceDirection,0 ),
                                                                                ForceMode2D.Impulse);
            //this.audioShot.Play();

            StartCoroutine(ShootCooldown(this.shootCooldown));
            this.bulletSpawn.enabled = true;
        }
        
    }

    void CreateBulletsPool()
    {
        for (int i = 0; i <= 7; i++)
        {
            GameObject bulletIns = GameObject.Instantiate<GameObject>(this.bullet);
            bulletIns.transform.parent = this.transform;
            bulletIns.transform.localPosition = Vector3.zero;
            this.bulletPool.Add(bulletIns);
        }
    }

    public void SaveBullet(GameObject bulletToSave)
    {
        bulletToSave.SetActive(false);
        bulletToSave.transform.parent = this.transform;
        bulletToSave.transform.localPosition = Vector3.zero;
        this.bulletPool.Add(bulletToSave);
    }

    IEnumerator ShootCooldown(float time)
    {
        yield return new WaitForSeconds(0.13f);
        this.bulletSpawn.enabled = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}
