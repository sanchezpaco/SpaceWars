using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpsManager : MonoBehaviour {

    public GameObject weaponPowerUp;
    public GameObject upgradePlayerAnim;
    public GameObject demotePlayerAnim;

    private int powerUpQuantity;
    private int currentPowerUp;
    private IDictionary<int, string> powerUps;
    private bool canDrop;

	// Use this for initialization
	void Start () {
        this.powerUpQuantity = 3;
        this.currentPowerUp = 0;
        this.powerUps = new Dictionary<int, string>();
        this.canDrop = true;
        this.CreatePowerUps();
	}


    public void DropPowerUp(EnemyBehaviour enemy)
    {
        if (this.canDrop) { 
            var probability = 5 - currentPowerUp;
            var random = Random.Range(0, 40);
            if (random == probability)
            {
                this.canDrop = false;
                this.weaponPowerUp.GetComponent<PowerUpBehaviour>().Activate(enemy.faceDirection, enemy.transform.position);
            }
        }
    }

    public string UpgradePlayer(PowerUpBehaviour powerUp)
    {
        this.currentPowerUp++;
        this.RemovePowerUp(powerUp);
        this.canDrop = this.currentPowerUp < this.powerUpQuantity;
        this.upgradePlayerAnim.SetActive(false);
        this.upgradePlayerAnim.SetActive(true);
        return this.powerUps[this.currentPowerUp];
    }

    public string DemotePlayer()
    {
        if (this.currentPowerUp > 0) { 
            this.currentPowerUp--;
            this.demotePlayerAnim.SetActive(false);
            this.demotePlayerAnim.SetActive(true);
            return this.powerUps[this.currentPowerUp];
        }

        return "Normal";
    }

    public void RemovePowerUp(PowerUpBehaviour powerUp)
    {
        this.canDrop = true;
        powerUp.GetComponent<PowerUpBehaviour>().Unactive();
    }

    void CreatePowerUps()
    {
        powerUps[0] = "Normal";
        powerUps[1] = "Doble";
        powerUps[2] = "Triple";
        powerUps[3] = "Laser";
    }
}
