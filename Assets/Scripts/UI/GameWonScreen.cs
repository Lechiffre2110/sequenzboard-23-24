using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWonScreen : MonoBehaviour
{   
    /// <summary>
    /// Custom SetActive method to set the game object active or inactive
    /// </summary>
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
