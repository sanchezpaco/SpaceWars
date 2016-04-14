using UnityEngine;
using System.Collections;

public class EnemyTypeStraight : MonoBehaviour {

    public float faceDirection;
    public float speed;

    private EnemyBehaviour enemyBehaviour;
    private bool canMove;

    // Use this for initialization
    void Awake () {
        this.enemyBehaviour = GetComponent<EnemyBehaviour>();
        this.faceDirection = this.enemyBehaviour.faceDirection;
        this.canMove = false;
    }
    
    public void SpawnEnemy(GameObject parent)
    {
        if (parent != null) {
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
    }
    
	
	// Update is called once per frame
	void FixedUpdate () {
        if (this.canMove) { 
            this.transform.Translate(new Vector3(speed * faceDirection, 0, 0) * Time.fixedDeltaTime);
        }
    }    

}
