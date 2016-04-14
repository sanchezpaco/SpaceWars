using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesManager : MonoBehaviour {
    
    public List<GameObject> firstPhaseEnemies;
    public List<int> enemiesNumberFirstPhase;
    public List<GameObject> secondPhaseEnemies;
    public List<int> enemiesNumberSecondPhase;
    public List<GameObject> thirdPhaseEnemies;
    public List<int> enemiesNumberThirdPhase;

    public EnemiesDefinition enemiesDefinition;
    public float spawnDirection = -1;
    
    
    private IDictionary<string, int> enemiesCompanionSpawn;
    private List<GameObject> firstPhaseEnemyPool;
    private List<GameObject> secondPhaseEnemyPool;
    private List<GameObject> thirdPhaseEnemyPool;
    private GameObject enemyToSpawn;    
    private float spawnCooldown;
    private float enemiesKilled;
    private float enemiesToKill;
    private GameObject currentAlert;
    private GameObject leftAlert;
    private GameObject rightAlert;
    private List<GameObject> currentEnemyPool;
    private int currentPhase;
    private int currentPhaseEnemiesToKill;


    // Use this for initialization
    void Awake () {

        this.currentPhaseEnemiesToKill = 0;
        this.enemiesToKill = 0;
        this.spawnCooldown = 1f;

        this.firstPhaseEnemyPool = new List<GameObject>();
        this.secondPhaseEnemyPool = new List<GameObject>();
        this.thirdPhaseEnemyPool = new List<GameObject>();
        
        this.leftAlert = GameObject.FindGameObjectWithTag("LeftAlert");
        this.rightAlert = GameObject.FindGameObjectWithTag("RightAlert");
        this.CreateEnemiesPool(firstPhaseEnemies, ref firstPhaseEnemyPool, enemiesNumberFirstPhase);
        this.CreateEnemiesPool(secondPhaseEnemies, ref secondPhaseEnemyPool, enemiesNumberSecondPhase);
        this.CreateEnemiesPool(thirdPhaseEnemies, ref thirdPhaseEnemyPool, enemiesNumberThirdPhase);
        
        this.currentPhase = 0;
        this.ChangePhase();
}

    void Start()
    {
        this.enemiesDefinition = GetComponent<EnemiesDefinition>();
        this.enemiesCompanionSpawn = this.enemiesDefinition.GetEnemiesDefinition();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

	    if (this.spawnCooldown <= 0 && this.enemiesToKill >= this.enemiesKilled)
        {
            this.SpawnEnemy();
            this.SpawnEnemy();
        }

        this.spawnCooldown -= Time.fixedDeltaTime;
	}

    public void SpawnEnemy()
    {
        this.enemyToSpawn = this.currentEnemyPool[0];
        this.currentEnemyPool.Remove(this.enemyToSpawn);

        this.enemyToSpawn.SetActive(true);
        this.enemyToSpawn.GetComponent<EnemyBehaviour>().Spawn(null, this.spawnDirection);
        int maxCompanionsToSpawn = this.enemiesCompanionSpawn[this.enemyToSpawn.tag];

        if (maxCompanionsToSpawn > 0)
        {
            if (Random.Range(0, 10) > 6)
            {
                this.SpawnCompanions(this.enemyToSpawn, maxCompanionsToSpawn);
            }
        }
        
        this.spawnCooldown = Random.Range(0.6f, 1);
    }

    void SpawnCompanions(GameObject parent, int maxCompanionsToSpawn) {

        int companionsToSpawn = Random.Range(1, maxCompanionsToSpawn);
        GameObject companion = null;

        for(int i = companionsToSpawn; i >= 0; i--)
        {
            for (int j = this.currentEnemyPool.Count -1; j >= 0; j--)
            {
                if (currentEnemyPool[j].tag == parent.tag)
                {
                    companion = this.currentEnemyPool[j];
                    break;
                }
            }

            if (companion != null) { 
                this.currentEnemyPool.Remove(companion);
                companion.SetActive(true);
                companion.GetComponent<EnemyBehaviour>().Spawn(parent, this.spawnDirection);
                parent = companion;
            }
        }
    }

    public void EnemyKilled (GameObject enemyKilled)
    {
        
        enemyKilled.SetActive(false);
        this.currentEnemyPool.Add(enemyKilled);
        this.enemiesKilled++;

        if (enemiesKilled == (this.enemiesToKill/2))
        {
            this.spawnDirection *= -1;
            this.ActivateAlert();
            this.spawnCooldown = 3f;
        }

        if (this.currentPhase < 3 && this.enemiesKilled == this.currentPhaseEnemiesToKill)
        {
            this.spawnCooldown = 2f;
            this.ChangePhase();
        }
    }

    void ActivateAlert()
    {
        this.currentAlert = (this.spawnDirection == -1) ? rightAlert : leftAlert;
        this.currentAlert.GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(this.UnactivateAlert());    
    }

    IEnumerator UnactivateAlert()
    {
        yield return new WaitForSeconds(1f);
        this.currentAlert.GetComponent<SpriteRenderer>().enabled = false;
        this.currentAlert = null;
    }

    void CreateEnemiesPool(List<GameObject> phaseEnemies, ref List<GameObject>phaseEnemyPool, List<int> phaseEnemiesNumber)
    {

        for (int i = phaseEnemies.Count -1; i >= 0; i--)
        {
            this.enemiesToKill += phaseEnemiesNumber[i];
            for (int j = 0; j <= phaseEnemiesNumber[i] * 3; j++)
            {
                GameObject enemyIns = GameObject.Instantiate<GameObject>(
                                        phaseEnemies[i]
                                        );
                enemyIns.transform.parent = this.transform;
                enemyIns.transform.localPosition = Vector3.zero;
                phaseEnemyPool.Add(enemyIns);
            }            
        }

        for (int i = 0; i < phaseEnemyPool.Count; i++)
        {
            GameObject temp = phaseEnemyPool[i];
            int randomIndex = Random.Range(i, phaseEnemyPool.Count);
            phaseEnemyPool[i] = phaseEnemyPool[randomIndex];
            phaseEnemyPool[randomIndex] = temp;
        }

    }

    void ChangePhase()
    {
        this.currentPhase++;

        Debug.Log("CHANGING PHASE: " + this.currentPhase);
        switch (this.currentPhase)
        {
            case 1:
                this.currentEnemyPool = firstPhaseEnemyPool;
                break;
            case 2:
                this.currentEnemyPool = secondPhaseEnemyPool;
                break;
            case 3:
                this.currentEnemyPool = thirdPhaseEnemyPool;
                break;
        }

        this.currentPhaseEnemiesToKill += this.GetPhaseEnemiesToKill();
    }

    int GetPhaseEnemiesToKill()
    {

        List<int> enemiesToKillList = null;
        int numberToKill = 0;

        switch(this.currentPhase)
        {
            case 1:
                enemiesToKillList = enemiesNumberFirstPhase;
                break;
            case 2:
                enemiesToKillList = enemiesNumberSecondPhase;
                break;
            case 3:
                enemiesToKillList = enemiesNumberThirdPhase;
                break;
        }

        for (int i = enemiesToKillList.Count - 1; i >= 0; i--)
        {
            numberToKill += enemiesToKillList[i];
        }

        return numberToKill;
    }

}
