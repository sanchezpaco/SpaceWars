using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
    
    public float facingDirection;
    public bool dead;
    public StarMovement starMovement;
    public PlayerLifeBehaviour playerLife;
    public SpriteRenderer spriteRenderer;
    public PlayerWeaponBehaviour weapon;
    public ParticleSystem starsLeft;
    public ParticleSystem starsRight;


    private ParticleSystem currentParticleSystem;
    private Animator animator;
    private string movingSide;
    private PowerUpsManager powerUpsManager;


    // Use this for initialization
    void Awake () {
        this.dead = false;
        this.facingDirection = 1;
        this.playerLife = GetComponent<PlayerLifeBehaviour>();
        this.starMovement = GetComponent<StarMovement>();
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.weapon = GetComponentInChildren<PlayerWeaponBehaviour>();
        this.powerUpsManager = GameObject.FindGameObjectWithTag("PowerUpsManager").GetComponent<PowerUpsManager>();
        this.currentParticleSystem = this.starsRight;
        this.starsLeft.Stop();
    }
	
	// Update is called once per frame
	void Update () {

        this.movingSide = this.starMovement.movingSide;

        if (Input.GetButtonDown("Jump"))
        {
            this.ChangeDirection();
        }

        if (this.movingSide.Contains("stop"))
        {
            this.animator.SetTrigger("Mid");
        }
        else
        {
            if (this.movingSide.Contains("down"))
            {
                this.animator.SetTrigger("Down");
            }

            if (this.movingSide.Contains("up"))
            {
                this.animator.SetTrigger("Up");
            }
        }

        if (this.currentParticleSystem.startSpeed < 50) { 
            this.currentParticleSystem.startSpeed += Time.deltaTime * 5;
        }
    }   

    void ChangeDirection()
    {
        this.facingDirection *= -1;
        Vector2 currentScale = this.transform.localScale;
        this.transform.localScale = new Vector2(currentScale.x * -1, currentScale.y);

        if (this.facingDirection == 1)
        {
            this.starsRight.Play();
            this.starsLeft.Stop();
            this.currentParticleSystem = this.starsRight;
        }
        else {
            this.starsLeft.Play();
            this.starsRight.Stop();
            this.currentParticleSystem = this.starsLeft;
        }

        this.currentParticleSystem.startSpeed = 20;

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Contains("Enemy"))
        {
            if (col.tag.Contains("Bullet")) {
                this.playerLife.ReduceLife(col.GetComponent<EnemyBulletBehaviour>().damage);
            } else { 
                EnemyBehaviour enemy = col.gameObject.GetComponent<EnemyBehaviour>();
                if (!enemy.dead) { 
                    this.playerLife.ReduceLife(enemy.damage);
                }
            }

            string weapon = this.powerUpsManager.DemotePlayer();
            this.weapon.weaponType = weapon;
        }

        if (col.tag == "PowerUp")
        {
            PowerUpBehaviour powerUp = col.gameObject.GetComponent<PowerUpBehaviour>();
            string weapon = this.powerUpsManager.UpgradePlayer(powerUp);
            this.weapon.weaponType = weapon;
        }
    }

    
}
