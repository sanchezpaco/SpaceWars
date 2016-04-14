using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesDefinition : MonoBehaviour {

    private IDictionary<string, int> enemiesCompanionSpawn;

	// Use this for initialization
	void Awake () {

        this.enemiesCompanionSpawn = new Dictionary<string, int>();

        //MAX COMPANIONS BY ENEMY
        enemiesCompanionSpawn["AngularEnemy"] = 5;
        enemiesCompanionSpawn["StraightEnemy"] = 5;
        enemiesCompanionSpawn["EnemyShooter"] = 0;

    }

    public IDictionary<string, int> GetEnemiesDefinition ()
    {
        return this.enemiesCompanionSpawn;
    }
}
