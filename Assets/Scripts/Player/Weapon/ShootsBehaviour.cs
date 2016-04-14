using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class ShootsBehaviour : MonoBehaviour {

    public BulletPool bulletPool;
    public float shootCooldown;

    private PlayerWeaponBehaviour playerWeapon;
    private GameObject bulletUp;
    private GameObject bulletMid;
    private GameObject bulletDown;
    private SpriteRenderer bulletSpawn;
    private float bulletDirection;
    
    private Type type;
    private MethodInfo shootMethod;
    private Vector3 shootPosition;

    // Use this for initialization
    void Awake () {
        this.type = this.GetType();
        this.playerWeapon = GetComponent<PlayerWeaponBehaviour>();
        this.bulletPool = GetComponent<BulletPool>();
        this.bulletSpawn = GetComponentInChildren<SpriteRenderer>();
    }

    public void Shoot(string shootType, bool big, float bulletDirection)
    {
        this.bulletDirection = bulletDirection;
        if (this.bulletPool.AreBulletsAvailable()) { 
            this.shootPosition = new Vector3(this.transform.position.x + (1 * bulletDirection),
                                                            this.transform.position.y, 0);
            this.shootMethod = this.type.GetMethod(shootType, BindingFlags.NonPublic | BindingFlags.Instance);
            this.shootMethod.Invoke(this, new System.Object[] { big });
        }
    }

    private void NormalShoot(bool big)
    {
        this.playerWeapon.canShoot = false;
        this.bulletMid = this.bulletPool.GetBullet();

        this.bulletMid.transform.parent = null;
        this.bulletMid.transform.position = this.shootPosition;
        this.bulletMid.transform.localScale = new Vector3(3f * bulletDirection, 2f, 0);

        if (big)
        {
            this.bulletMid.transform.localScale *= this.playerWeapon.timeChargingShoot * 2;
        }

        this.bulletMid.GetComponent<BulletBehaviour>().weapon = this.playerWeapon;
        this.bulletMid.SetActive(true);
        this.bulletMid.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
        this.bulletMid.GetComponent<Rigidbody2D>().AddForce(new Vector2(30f * this.bulletDirection, 0f),
                                                                            ForceMode2D.Impulse);
        this.playerWeapon.audioShot.Play();

        this.StartCoroutine(this.ShootCooldown(this.shootCooldown));
        this.bulletSpawn.enabled = true;
    }

    private void DobleShoot(bool big)
    {

        this.playerWeapon.canShoot = false;

        this.bulletUp = this.bulletPool.GetBullet();
        this.bulletDown = this.bulletPool.GetBullet();

        this.bulletUp.transform.parent = null;
        this.bulletUp.transform.position = this.shootPosition;
        this.bulletUp.transform.localScale = new Vector3(3f * bulletDirection, 2f, 0);
        this.bulletUp.GetComponent<BulletBehaviour>().weapon = this.playerWeapon;
        this.bulletUp.SetActive(true);
        this.bulletUp.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
        this.bulletUp.GetComponent<Rigidbody2D>().AddForce(new Vector2(30f * this.bulletDirection, 8f),
                                                                            ForceMode2D.Impulse);        

        this.bulletDown.transform.parent = null;
        this.bulletDown.transform.position = this.shootPosition;
        this.bulletDown.transform.localScale = new Vector3(3f * bulletDirection, 2f, 0);
        this.bulletDown.GetComponent<BulletBehaviour>().weapon = this.playerWeapon;
        this.bulletDown.SetActive(true);
        this.bulletDown.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
        this.bulletDown.GetComponent<Rigidbody2D>().AddForce(new Vector2(30f * this.bulletDirection, -8f),
                                                                            ForceMode2D.Impulse);

        this.playerWeapon.audioShot.Play();

        this.StartCoroutine(this.ShootCooldown(this.shootCooldown));
        this.bulletSpawn.enabled = true;

    }

    private void TripleShoot(bool big)
    {

        this.playerWeapon.canShoot = false;

        this.bulletUp = this.bulletPool.GetBullet();
        this.bulletMid = this.bulletPool.GetBullet();
        this.bulletDown = this.bulletPool.GetBullet();

        this.bulletUp.transform.parent = null;
        this.bulletUp.transform.position = this.shootPosition;
        this.bulletUp.transform.localScale = new Vector3(3f * bulletDirection, 2f, 0);
        this.bulletUp.GetComponent<BulletBehaviour>().weapon = this.playerWeapon;
        this.bulletUp.SetActive(true);
        this.bulletUp.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
        this.bulletUp.GetComponent<Rigidbody2D>().AddForce(new Vector2(30f * this.bulletDirection, 8f),
                                                                            ForceMode2D.Impulse);

        this.bulletMid.transform.parent = null;
        this.bulletMid.transform.position = this.shootPosition;
        this.bulletMid.transform.localScale = new Vector3(3f * bulletDirection, 2f, 0);
        this.bulletMid.GetComponent<BulletBehaviour>().weapon = this.playerWeapon;
        this.bulletMid.SetActive(true);
        this.bulletMid.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
        this.bulletMid.GetComponent<Rigidbody2D>().AddForce(new Vector2(30f * this.bulletDirection, 0f),
                                                                            ForceMode2D.Impulse);

        this.bulletDown.transform.parent = null;
        this.bulletDown.transform.position = this.shootPosition;
        this.bulletDown.transform.localScale = new Vector3(3f * bulletDirection, 2f, 0);
        this.bulletDown.GetComponent<BulletBehaviour>().weapon = this.playerWeapon;
        this.bulletDown.SetActive(true);
        this.bulletDown.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
        this.bulletDown.GetComponent<Rigidbody2D>().AddForce(new Vector2(30f * this.bulletDirection, -8f),
                                                                            ForceMode2D.Impulse);

        this.playerWeapon.audioShot.Play();

        this.StartCoroutine(this.ShootCooldown(this.shootCooldown));
        this.bulletSpawn.enabled = true;

    }

    private void LaserShoot(bool big)
    {
        this.playerWeapon.canShoot = false;
        this.bulletMid = this.bulletPool.GetBullet();

        this.bulletMid.transform.parent = null;
        this.bulletMid.transform.position = this.shootPosition;
        this.bulletMid.transform.localScale = new Vector3(3f * bulletDirection, 2f, 0);

        if (big)
        {
            this.bulletMid.transform.localScale *= this.playerWeapon.timeChargingShoot * 2;
        }

        this.bulletMid.GetComponent<BulletBehaviour>().weapon = this.playerWeapon;
        this.bulletMid.SetActive(true);
        this.bulletMid.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
        this.bulletMid.GetComponent<Rigidbody2D>().AddForce(new Vector2(30f * this.bulletDirection, 0f),
                                                                            ForceMode2D.Impulse);
        this.playerWeapon.audioShot.Play();

        this.StartCoroutine(this.ShootCooldown(0));
        this.bulletSpawn.enabled = true;
    }

    IEnumerator ShootCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        this.playerWeapon.canShoot = true;
        this.bulletSpawn.enabled = false;
    }
}
