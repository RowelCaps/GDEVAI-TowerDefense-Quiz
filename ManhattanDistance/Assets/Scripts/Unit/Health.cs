using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [SerializeField] private int maxHealth = 20;
    [SerializeField] private int moneyDrop = 2;
    [SerializeField] private Image health;
    private int currentHealth;

    public bool isDead
    {
        get { return currentHealth <= 0; }
    }

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

        if (this.isDead)
            die();

        health.fillAmount = (float)currentHealth / (float)maxHealth;
		
	}

    private void die()
    {
        GameManager.Shared.registerEnemy();
        GameManager.Shared.AddMoney(moneyDrop);
        GameManager.Shared.enemyNotActive(this.transform);

        Destroy(this.gameObject);
    }

    public void takeDamage(int damage)
    {
        currentHealth = currentHealth - damage < 0 ? 0 : currentHealth - damage;
    }

}
