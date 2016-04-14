using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour {

    public GameObject bullet;
    private List<GameObject> bulletPool;

    // Use this for initialization
    void Awake () {
        this.bulletPool = new List<GameObject>();
        this.CreateBulletsPool();
    }

    void CreateBulletsPool()
    {
        for (int i = 0; i <= 40; i++)
        {
            GameObject bulletIns = GameObject.Instantiate<GameObject>(this.bullet);
            bulletIns.transform.parent = this.transform;
            bulletIns.transform.localPosition = new Vector3(1, 0, 0);
            this.bulletPool.Add(bulletIns);
        }
    }

    public GameObject GetBullet()
    {
        GameObject bullet = this.bulletPool[0];
        this.bulletPool.Remove(bullet);

        return bullet;
    }

    public void SaveBullet(GameObject bulletToSave)
    {
        bulletToSave.SetActive(false);
        bulletToSave.transform.parent = this.transform;
        bulletToSave.transform.localPosition = new Vector3(1,0,0);
        this.bulletPool.Add(bulletToSave);
    }

    public bool AreBulletsAvailable()
    {
        return this.bulletPool.Count > 0;
    }
}
