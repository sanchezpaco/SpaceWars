using UnityEngine;
using System.Collections;

public class EnemyTypeAngular : MonoBehaviour {

    public float faceDirection;
    public float speed;

    private EnemyBehaviour enemyBehaviour;
    private bool canMove;
    private float directionYCooldown;
    private float yDirection;

    // Use this for initialization
    void Awake()
    {
        this.yDirection = 1;
        this.enemyBehaviour = GetComponent<EnemyBehaviour>();
        this.faceDirection = this.enemyBehaviour.faceDirection;
        this.canMove = false;
        this.directionYCooldown = 0.7f;
    }

    public void SpawnEnemy(GameObject parent)
    {
        if (parent != null)
        {
            float parentX = parent.transform.localPosition.x,
                  parentY = parent.transform.localPosition.y;

            this.transform.localPosition = new Vector2(parentX + parent.transform.localScale.x / 3f, parentY);
        }
        else
        {
            this.transform.localPosition = new Vector2(10 * (this.faceDirection * -1), Random.Range(-3.5f, 3.5f));
        }

        this.canMove = true;
        this.ResetEnemy();
    }

    void ResetEnemy()
    {
        this.enemyBehaviour.dead = false;
        this.yDirection = 1;
        this.directionYCooldown = 0.7f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.canMove)
        {
            this.transform.Translate(new Vector3(speed * faceDirection, 2 * this.yDirection, 0) * Time.fixedDeltaTime);

            if (this.directionYCooldown <= 0)
            {
                this.yDirection *= -1;
                this.directionYCooldown = 0.7f;
            }

            this.directionYCooldown -= Time.fixedDeltaTime;
        }        
    }
}
