using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class EnemyData : MonoBehaviour
{
    public static EnemyData Instance { get; private set; }

    public static int enemyId = 10000;

    public List<EnemyStatus> enemyList;

    private void Awake()
    {
        Instance = this;
    }

    public int GetEnemyID()
    {
        enemyId++;
        return enemyId;
    }
}
