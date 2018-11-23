using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [HideInInspector]
    public int whichTile;

	public void Victory() {

    }

    public void Death(int type) {
        switch (type) {
            default:
                Debug.LogError("No Death Type Recorded! Check MovementStats collisions and GameController.Death() for possible problems.");
                
                break;

            case 0:
                //fall
                break;

            case 1:
                //enemy
                break;

            case 2:
                //trap
                break;

            case 3:
                //time
                break;
        }
    }
}
