using UnityEngine;
using System.Collections;

public class PowerUpBehaviour : MonoBehaviour {

    public PowerUpsManager powerUpsManager;
    private float faceDirection;
    private string boundaryName;

    void Awake()
    {
        this.powerUpsManager = GameObject.FindGameObjectWithTag("PowerUpsManager").GetComponent<PowerUpsManager>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        this.transform.Translate(new Vector3(3 * this.faceDirection, 0, 0) * Time.fixedDeltaTime);
    }

    public void Activate(float faceDirection, Vector3 position)
    {
        this.faceDirection = faceDirection;
        this.transform.position = position;
        this.gameObject.SetActive(true);
    }

    public void Unactive()
    {
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        this.boundaryName = (faceDirection > 0) ? "Right" : "Left";
        if (col.tag == "Boundary" && col.name == this.boundaryName)
        {
            this.powerUpsManager.RemovePowerUp(this);
        }
    }
}
