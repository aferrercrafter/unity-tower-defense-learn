using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money { get; set; }
    public int startMoney = 400;

    private void Start()
    {
        Money = startMoney;
    }
}
