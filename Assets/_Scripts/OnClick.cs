using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    GamController gameController;

    
    // Start is called before the first frame update
    void OnMouseDown()
    {

        if (gameController.whosTurn == 0)
        {
            gameController.whosTurn = 1;
            gameController.turnIcons[0].SetActive(false);
            gameController.turnIcons[1].SetActive(true);
        }
        else
        {
            gameController.whosTurn = 0;
            gameController.turnIcons[0].SetActive(true);
            gameController.turnIcons[1].SetActive(false);
        }
    }
}
