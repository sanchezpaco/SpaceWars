using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class EnemyBehaviour : MonoBehaviour {

    public float faceDirection;
    public SpriteRenderer enemyRenderer;
    public Animator anim;
    public EnemiesManager enemiesManager;
    public float life = 50;
    public bool dead;
    public EnemiesDefinition enemiesDefinition;
    public float damage;


    private PowerUpsManager powerUpsManager;
    private AudioSource audioExplosion;
    private Type type;
    private MethodInfo methodInfo;
    private float initialLife;
    private string boundaryName;


    // Use this for initialization

    void Awake () {

        this.enemiesManager = GameObject.FindGameObjectWithTag("EnemiesManager").GetComponent<EnemiesManager>();
        this.enemiesDefinition = this.enemiesManager.enemiesDefinition;
        this.enemyRenderer = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
        this.powerUpsManager = GameObject.FindGameObjectWithTag("PowerUpsManager").GetComponent<PowerUpsManager>();

        this.type = this.GetType();
        this.audioExplosion = GetComponent<AudioSource>();

        this.initialLife = this.life;
        this.dead = false;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Boundary" && col.name == this.boundaryName)
        {
            Hide();
        }

        if ((col.tag == "Bullet" || col.tag == "Player") && !this.dead)
        {
            if (col.tag == "Player")
            {
                this.life -= 50;
            } else
            {
                this.life -= col.gameObject.GetComponent<BulletBehaviour>().damage;
            }

            this.Flash();

            if (this.life <= 0) { 
                Destroy();
            }
        }
    }

    void Hide()
    {
        this.enemiesManager.EnemyKilled(this.gameObject);
    }

    void Destroy()
    {
        this.powerUpsManager.DropPowerUp(this);
        this.dead = true;
        this.transform.FindChild("ExplosionPixel").gameObject.SetActive(true);
        this.audioExplosion.Play();
        StartCoroutine(SaveEnemy());        
    }

    IEnumerator SaveEnemy()
    {
        yield return new WaitForSeconds(0.2f);
        this.life = this.initialLife;
        this.transform.FindChild("ExplosionPixel").gameObject.SetActive(false);
        this.enemiesManager.EnemyKilled(this.gameObject);
    }

    public void Spawn(GameObject parent, float spawnDirection)
    {
        this.faceDirection = spawnDirection;
        this.boundaryName = (faceDirection > 0) ? "Right" : "Left";
        
        this.methodInfo = this.type.GetMethod(this.tag+"Spawn");
        if (methodInfo != null)
        {
            this.methodInfo.Invoke(null, new object[] { this, parent });
        }
        this.transform.localScale = new Vector2(this.transform.localScale.x * (this.faceDirection * -1),
                                                    this.transform.localScale.y);
    }

    public static void StraightEnemySpawn(EnemyBehaviour me, GameObject parent)
    {
        me.GetComponent<EnemyTypeStraight>().faceDirection = me.faceDirection;
        me.GetComponent<EnemyTypeStraight>().SpawnEnemy(parent);
    }

    public static void AngularEnemySpawn(EnemyBehaviour me, GameObject parent)
    {
        me.GetComponent<EnemyTypeAngular>().faceDirection = me.faceDirection;
        me.GetComponent<EnemyTypeAngular>().SpawnEnemy(parent);
    }

    public static void EnemyShooterSpawn(EnemyBehaviour me, GameObject parent)
    {
        me.GetComponent<EnemyTypeStraight>().faceDirection = me.faceDirection;
        me.GetComponent<EnemyTypeStraight>().SpawnEnemy(null);
        me.GetComponentInChildren<EnemyWeaponBehaviour>().canShoot = true;
    }

    void Flash()
    {
        this.enemyRenderer.material.SetFloat("_FlashAmount",1);
        StartCoroutine(RemoveFlash(0.1f));
    }

    IEnumerator RemoveFlash(float time)
    {
        yield return new WaitForSeconds(time);
        this.enemyRenderer.material.SetFloat("_FlashAmount", 0);
    }
}
