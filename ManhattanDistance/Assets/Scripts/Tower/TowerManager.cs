using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>{

    [SerializeField]private TowerButton towerPressed;
    [SerializeField] private LayerMask TowerPlacerMask;
    [SerializeField] private LayerMask maskHolder;
    [SerializeField] private GameObject selected;

    private bool hasSelected = false;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && hasSelected)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, TowerPlacerMask))
            {
                TowerPlacer place = hit.collider.gameObject.GetComponent<TowerPlacer>();

                if (!place.IsTurrentOnTop)
                {
                    if(selected.GetComponent<TurretIdle>() != null)
                        selected.GetComponent<TurretIdle>().enabled = true;

                    Instantiate(selected, hit.transform.position, hit.transform.rotation);
                    place.IsTurrentOnTop = true;
                }
            }

            hasSelected = false;
            Destroy(selected);
            selected = null;
        }
    }

    private void FixedUpdate()
    {
        if (hasSelected)
            followMouse();
    }

    public void selectTower(TowerButton selectedTower)
    {
        if (GameManager.Shared.Money < selectedTower.Price)
            return;

        print("read");
        GameManager.Shared.SubtractMoney(selectedTower.Price);
        towerPressed = selectedTower;
        selected = Instantiate(selectedTower.Turret) as GameObject;
        hasSelected = true;
    }

    private void followMouse()
    {
        if (selected.GetComponent<TurretIdle>() != null)
            selected.GetComponent<TurretIdle>().enabled = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, TowerPlacerMask))
        {
            selected.transform.position = hit.transform.position;
        }

    }
}
