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

    public EnemyStatus GetEnemyData()
    {
        int index= Random.Range(0, enemyList.Count);
        return enemyList[index];
    }

    public int GetEnemyID()
    {
        enemyId++;
        return enemyId;
    }
}
