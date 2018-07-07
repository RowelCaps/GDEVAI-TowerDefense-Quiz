using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    [SerializeField] float speed = 3f;
    [SerializeField] float distance = 1.5f;

    private int currentNodeIndex = 0;
    private Grid gridReference;
    public bool hasEscaped;

	// Use this for initialization
	void Start () {
        gridReference = GameObject.Find("GameManager").GetComponent<Grid>();
	}

    private void FixedUpdate()
    {
        if(hasEscaped)
        {
            GameManager.Shared.registerEnemy();
            GameManager.Shared.enemyNotActive(this.transform);
            Destroy(this.gameObject);
        }
        move();
    }

    void move()
    {
        if (gridReference.FinalPath == null || hasEscaped)
            return;

        Vector3 targetPos = gridReference.FinalPath[currentNodeIndex].Position;
        this.transform.position = Vector3.MoveTowards(this.transform.position,targetPos, speed * Time.deltaTime);

        if(gridReference.FinalPath[currentNodeIndex] == gridReference.FinalPath[gridReference.FinalPath.Count - 1])
        {
            hasEscaped = true;
            GameManager.Shared.addEscapedEnemies();
            return;
        }

        if (Vector3.Distance(transform.position, gridReference.FinalPath[currentNodeIndex].Position) < distance)
            currentNodeIndex++;

    }
}
