using System.Collections.Generic;
using UnityEngine;
using SF = UnityEngine.SerializeField;
public class PlayerAttackArea : MonoBehaviour
{
    public List<Collider2D> enemyList;

    private void Start()
    {
        enemyList = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !enemyList.Contains(collision))
            enemyList.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")&& enemyList.Contains(collision))
            enemyList.Remove(collision);
        
    }

    /// <summary>
    /// 리스트 내의 적 중 가장 가까운 대상을 찾아 반환합니다.
    /// </summary>
    public Transform GetClosestTarget(out Transform closestTarget)
    {
        if (enemyList.Count <= 0) return closestTarget = null;

        closestTarget = null;
        float minDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            // 순회중 비활성화 / 삭제 등의 변수 발생시 제거후 순회
            if (enemyList[i] == null || !enemyList[i].gameObject.activeInHierarchy)
            {
                enemyList.RemoveAt(i);
                continue;
            }

            Vector3 directionToTarget = enemyList[i].transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < minDistanceSqr)
            {
                minDistanceSqr = dSqrToTarget;
                closestTarget = enemyList[i].transform;
            }
        }
        return closestTarget;
    }
}
