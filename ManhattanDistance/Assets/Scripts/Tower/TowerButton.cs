using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour {

    [SerializeField]private GameObject turret;
    [SerializeField] private int price;

    public GameObject Turret { get { return turret; } } 
    public int Price { get { return price; } }
}
