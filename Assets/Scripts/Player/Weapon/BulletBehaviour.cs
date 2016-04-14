using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    public float damage = 50;
    public PlayerWeaponBehaviour weapon;
    private BulletPool bulletPool;
    private TimeManager timeManager;
    private CamShake cameraShake;

    void Awake()
    {
        this.cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamShake>();
        this.timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        this.bulletPool = this.weapon.shootsBehaviour.bulletPool;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        string name = col.name;
        string tag = col.tag;
        //TODO Change to renderer.isVisible
        if ((tag == "Boundary" && (name == "Right" || name == "Left")) || tag.Contains("Enemy")) {
            if (tag.Contains("Enemy"))
            {
                if (!tag.Contains("Bullet")) { 
                    if (!col.GetComponent<EnemyBehaviour>().dead)
                    {
                        this.bulletPool.SaveBullet(this.gameObject);
                    }
                    this.cameraShake.ShakeCamera(10f);
                    timeManager.Micropause(0.017f);
                }
            } else
            { 
                this.bulletPool.SaveBullet(this.gameObject);
            }
        }
    }
}
