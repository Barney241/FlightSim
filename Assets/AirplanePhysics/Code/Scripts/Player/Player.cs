using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("Player Properties")]
    public GameObject activePlane;


    private Transform playerPosition;
    private int playerHealth;
    private int playerMoney;
    #endregion

    #region Properties
    public Transform PlayerPosition
    {
        get { return playerPosition; }
    }
    public int PlayerHealth
    {
        get { return playerHealth; }
    }
    public int PlayerMoney
    {
        get { return playerMoney; }
    }
    #endregion
    void Start()
    {
        
    }

    void Update()
    {
        updatePlayerPosition();
    }

    void updatePlayerPosition()
    {
        playerPosition = activePlane.transform;
    }
}
