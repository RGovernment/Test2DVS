using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class EnemyData : MonoBehaviour
{
    public static EnemyData Instance { get; private set; }

    public static int enemyId = 10000;

    public List<JObject> enemyList = new();

    public JObject firstEnemyData = new()
    {
        [NAME] = "더미",
        [HP] = 100,
        [SPEED] = 3,
        [DAMAGE] = 10,
        [ARMOR] = 0
    };

    private void Awake()
    {
        Instance = this;
        enemyList.Add(firstEnemyData);
    }

    public int GetEnemyID()
    {
        enemyId++;
        return enemyId;
        
    }
}
