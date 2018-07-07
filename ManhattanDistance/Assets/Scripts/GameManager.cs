using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    [SerializeField] GameObject[] enemies;
    [SerializeField] int maxWave = 5;
    [SerializeField] int maxEscapeEnemies = 10;
    [SerializeField] private int money = 15;

    private int currentWave = 1;
    private int escapedEnemies = 0;

    private Grid gridReference;
    private Text waveText;
   [SerializeField] private Text escapedEnemiesText;
    private Text moneyText;

    private int enemyOnScene = 0;
    private int enemylist = 0;
    private List<Transform> enemiesCurrentlyActive = new List<Transform>();

    public List<Transform> EnemiesCurrentlyActive { get { return enemiesCurrentlyActive; } }
    public int Money { get { return money; } }

	// Use this for initialization
	void Start () {

        gridReference = GameObject.Find("GameManager").GetComponent<Grid>();
        waveText = GameObject.Find("HUD").transform.Find("Wave_Text").GetComponent<Text>();
        escapedEnemiesText = GameObject.Find("HUD").transform.Find("EscapedEnemies_Text").GetComponent<Text>();
        moneyText = GameObject.Find("HUD").transform.Find("Money_Text").GetComponent<Text>();

        StartCoroutine(startWave());
	}
	
	// Update is called once per frame
	void Update () {

        waveText.text = "Wave " + currentWave.ToString();
        escapedEnemiesText.text = "Escaped Enemies: " + escapedEnemies.ToString() + " / " + maxEscapeEnemies.ToString();
        moneyText.text = "Money: " + money.ToString();
	}

    IEnumerator startWave()
    {
        for(int i = 0; i < currentWave * 2; i++)
        {
            int indexSpawn = Random.Range(0, enemies.Length - 1);

            GameObject enemy = Instantiate(enemies[indexSpawn]) as GameObject;
            enemy.transform.position = gridReference.StartPosition.position;

            enemyOnScene++;
            enemiesCurrentlyActive.Add(enemy.GetComponent<Transform>());

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitUntil(() => checkEndWave());

        enemyOnScene = 0;
        enemylist = 0;
        currentWave++;
        enemiesCurrentlyActive.Clear();

        yield return new WaitForSeconds(10f);

        if(currentWave < maxWave && escapedEnemies < maxEscapeEnemies)
            StartCoroutine(startWave());
    }

    private bool checkEndWave()
    {
        return enemylist >= enemyOnScene;
    }

    public void registerEnemy()
    {
        enemylist++;
    }

    public void enemyNotActive(Transform enemy)
    {
        enemiesCurrentlyActive.Remove(enemy);
    }

    public void AddMoney(int moneyDrop)
    {
        money += moneyDrop;
    }

    public void SubtractMoney(int price)
    {
        money -= price;
    }

    public void addEscapedEnemies()
    {
        escapedEnemies++;
    }
}
