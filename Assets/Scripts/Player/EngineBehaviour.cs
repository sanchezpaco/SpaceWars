using UnityEngine;
using System.Collections;

public class EngineBehaviour : MonoBehaviour {
    
    private PlayerBehaviour player;
    private Vector2 playerDirection;
    private Vector3 initSize;
    private float playerX;
    private string currentForm;
    private float playerInitialSpeed;
    
    // Use this for initialization
    void Start () {
        this.currentForm = "initial";
        this.initSize = this.transform.localScale;
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        this.playerInitialSpeed = this.player.starMovement.speed;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.Log(this.player);
        playerDirection = this.player.starMovement.movingXY;
        playerX = playerDirection.x * player.facingDirection;

        if (playerX == 0)
        {
            MakeInitialSize();
        }
        else if (playerX > 0)
        {
            MakeBig();
        } else
        {
            MakeSmall();
        }

	}

    void MakeBig()
    {
        if (this.currentForm != "big") {
            this.transform.localScale = this.initSize;
            this.transform.localScale *= 1.5f;
            this.currentForm = "big";
            this.player.starMovement.speed = this.playerInitialSpeed;
            this.player.starMovement.speed += 2;
        }
    }

    void MakeSmall()
    {
        if (this.currentForm != "small")
        {
            this.transform.localScale = this.initSize;
            this.transform.localScale /= 1.5f;
            this.currentForm = "small";
            this.player.starMovement.speed = this.playerInitialSpeed;
            this.player.starMovement.speed -= 2;
        }
    }

    void MakeInitialSize()
    {
        if (this.currentForm != "initial")
        {
            this.transform.localScale = this.initSize;
            this.currentForm = "initial";
            this.player.starMovement.speed = this.playerInitialSpeed;
        }
    }
}
