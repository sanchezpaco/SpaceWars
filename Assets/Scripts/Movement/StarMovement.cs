using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarMovement : MonoBehaviour
{

    public string movingSide;
    public Vector2 movingXY;
    public float speed;
    public bool canMove;
    public bool rolling;
    public Vector2 facingDirection;

    private Rigidbody2D rigidBody;
    private float moveX;
    private float moveY;
    private string lastDirection;
    private IDictionary<string, string> moveDirections = new Dictionary<string, string>();


    void Awake()
    {
        this.movingXY = new Vector2(0, 0);
        this.facingDirection = new Vector2(0, -1);
        this.rolling = false;
    }


    // Use this for initialization
    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();

        moveDirections["0,1"] = "right";
        moveDirections["1,1"] = "upright";
        moveDirections["-1,1"] = "downright";

        moveDirections["0,-1"] = "left";
        moveDirections["1,-1"] = "upleft";
        moveDirections["-1,-1"] = "downleft";

        moveDirections["1,0"] = "up";
        moveDirections["-1,0"] = "down";
        moveDirections["0,0"] = "stopped";

        lastDirection = "";
        canMove = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!rolling)
        {
            if (canMove)
            {
                moveY = Input.GetAxisRaw("Vertical");
                moveX = Input.GetAxisRaw("Horizontal");
                movingSide = moveDirections[moveY + "," + moveX];

                if (!movingSide.Contains("stop"))
                {
                    lastDirection = movingSide;
                }

                movingSide += (movingSide == "stopped") ? lastDirection : "";

                if (!movingSide.Contains("stopped"))
                {
                    this.facingDirection = this.movingXY;
                }

                movingXY = new Vector2(moveX, moveY);

                this.rigidBody.velocity = new Vector2(moveX, moveY).normalized * speed;
            }
            else
            {
                this.rigidBody.velocity = new Vector2(0, 0);
                movingSide = "stopped";
                movingSide += (movingSide == "stopped") ? lastDirection : "";
            }
        }
    }
}
