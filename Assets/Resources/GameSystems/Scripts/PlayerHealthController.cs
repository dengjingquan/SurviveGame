using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/*********************************
 @Description   :   玩家生命控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.2
********************************/

public class PlayerHealthController : MonoBehaviour {
    public float maxHealth;         // 最大生命值        
    public float currHealth;        // 当前生命值
    bool isDead;                    // 是否死亡
    Animator playerAnim;            // 角色动画
    public GameObject deathCermera; // 上帝相机
    public GameObject gameOverText; // 游戏结束文本
    public Text userNameText;       // 用户名
    public Text scoreText;          // 分数文本
    public string FilePath;


    // Use this for initialization
    void Start () {
        playerAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        // 角色死亡
        if (currHealth <= 0)
        {
            deathCermera.gameObject.SetActive(true);
            deathCermera.transform.position = new Vector3(this.transform.position.x,15,this.transform.position.z);
            gameOverText.gameObject.SetActive(true);
            // 将成绩写入文件
           
            string scoreRecord = "\r\n"
                + userNameText.text.ToString()
                + " "
                + scoreText.text.ToString();
            File.AppendAllText(FilePath, scoreRecord);
            this.gameObject.SetActive(false);
        }
    }



    // 遭受攻击
    public void Attacked(float demage)
    {
        if (currHealth > 0)
        {
            currHealth -= demage;
        }
    }
}
