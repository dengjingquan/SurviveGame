using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*********************************
 @Description   :   控制敌人移动
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.1
********************************/

public class EnemyMoveController : MonoBehaviour {
    GameObject player;                               // 游戏玩家.
    PlayerHealthController playerHealthController;            // 玩家生命.
    EnemyHealthController enemyHealthController;    // 敌人生命（自身）.
    NavMeshAgent nav;                               // 导航网格代理（寻路）.


    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealthController = player.GetComponent<PlayerHealthController>();
        enemyHealthController = GetComponent<EnemyHealthController>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        //敌人自身与玩家均没有死亡 更新目的地 继续自动寻路
        if (enemyHealthController.currHealth > 0 && playerHealthController.currHealth > 0)
        {
            nav.SetDestination(player.transform.position);
        }
        else
        {
            // 关闭寻路
            nav.enabled = false;
        }
    }
}
