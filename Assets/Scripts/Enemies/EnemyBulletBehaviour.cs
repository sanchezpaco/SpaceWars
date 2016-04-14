using UnityEngine;
using System.Collections;

public class EnemyBulletBehaviour : MonoBehaviour
{

    public float damage = 50;
    public EnemyWeaponBehaviour weapon;
    public EnemyBehaviour enemyBehaviour;

    private string colliderName;
    private string colliderTag;

    void OnTriggerEnter2D(Collider2D col)
    {
        colliderName = col.name;
        colliderTag = col.tag;

        //TODO Change to renderer.isVisible
        if ((colliderTag == "Boundary" && (colliderName == "Right" || colliderName == "Left")) || colliderTag.Contains("Player"))
        {
            if (colliderTag.Contains("Player"))
            {
                if (!col.GetComponent<PlayerBehaviour>().dead)
                {
                    this.weapon.SaveBullet(this.gameObject);
                }
            }
            else
            {
                if (colliderTag == "Boundary")
                {
                    if ((colliderName == "Right" && this.enemyBehaviour.faceDirection == 1) || (colliderName == "Left" && this.enemyBehaviour.faceDirection == -1))
                    {
                        this.weapon.SaveBullet(this.gameObject);
                    }
                } else {
                    this.weapon.SaveBullet(this.gameObject);
                }
            }
        }
    }
}
