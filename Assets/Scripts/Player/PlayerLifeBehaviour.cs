using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerLifeBehaviour : MonoBehaviour {

    [Range(0, 100)]public float life;
    public ColorCorrectionCurves cameraColor;

    private float timeSinceLastLifeReduce;
    private PlayerBehaviour player;

	// Use this for initialization
	void Start () {
        this.player = GetComponent<PlayerBehaviour>();
        this.life = 100;
        this.cameraColor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorCorrectionCurves>();
	}
	

    void FixedUpdate()
    {
        if (this.timeSinceLastLifeReduce > 2f && this.life < 100)
        {
            this.RecoverLife();
        }



        if (this.life <= 0)
        {
            this.player.dead = true;
        } else
        {
            this.player.dead = false;
        }

        this.timeSinceLastLifeReduce += Time.fixedDeltaTime;
    }

	// Update is called once per frame
	void Update () {
        this.cameraColor.saturation = this.life / 100;
	}

    public void ReduceLife(float lifeToReduce)
    {
        this.Flash();
        if (this.life > 0) { 
            this.life -= lifeToReduce;
            this.timeSinceLastLifeReduce = 0f;
        }

        if (this.life < 0)
        {
            this.life = 0;
        }
    }

    void RecoverLife()
    {
        this.life += Time.fixedDeltaTime * 40;
    }

    void Flash()
    {
        this.player.spriteRenderer.material.SetFloat("_FlashAmount", 1);
        StartCoroutine(RemoveFlash(0.1f));
    }

    IEnumerator RemoveFlash(float time)
    {
        yield return new WaitForSeconds(time);
        this.player.spriteRenderer.material.SetFloat("_FlashAmount", 0);
    }
}
