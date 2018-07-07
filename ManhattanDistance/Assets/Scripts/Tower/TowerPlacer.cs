using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour {

    bool isTurretOnTop = false;

	public bool IsTurrentOnTop
    {
        get { return isTurretOnTop; }
        set { isTurretOnTop = value; }
    }
}
