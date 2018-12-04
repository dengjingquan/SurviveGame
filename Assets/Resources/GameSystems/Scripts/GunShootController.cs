using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*********************************
 @Description   :   激光枪射击控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.11.31
********************************/

public class GunShootController : MonoBehaviour
{
    public Rigidbody shootCollider;                 // 击退碰撞器（类似子弹...）
    public GameObject player;                       // 玩家游戏对象.
    public int damagePerShot = 20;                  // 每枪伤害.
    public float gunShotCD = 0.15f;                 // 射击冷却时间.
    public float shootRange = 100f;                 // 射击范围.
    public float shootForceBase = 1000f;            // 击中敌人冲击力.
    public LineRenderer aimLine;                    // 瞄准射线特效.

    float gunShotLessCD;                            // 射击剩余冷却时间.
    RaycastHit shootHit;                            // 射线碰撞
    Ray shootRay;                                   // 射线投射对象.
    ParticleSystem gunParticles;                    // 开火粒子系统.
    LineRenderer gunLine;                           // 子弹射线特效.
    AudioSource gunAudio;                           // 射击音效.
    Light gunLight;                                 // 开火灯光.                                              
    void Start()
    {
        // Set up the references.
        shootRay = new Ray();
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        if ( gunShotLessCD > 0 )
        {
            gunShotLessCD -= Time.deltaTime;
        }
        // 监听鼠标点击开火
        else if (Input.GetMouseButton(0) && gunShotLessCD <= 0)
        {
            // 射击
            Shoot();
            // 进入CD
            gunShotLessCD = gunShotCD;
        }

        else
        {
            // 关闭特效
            DisableEffects();

        }
        // 更新射线和瞄准射线位置方向
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        aimLine.SetPosition(0, transform.position);
        aimLine.SetPosition(1, shootRay.origin + shootRay.direction * shootRange);
    }


    public void DisableEffects()
    {
        // 关闭开火特效.
        gunLine.enabled = false;
        gunLight.enabled = false;

    }


    void Shoot()
    {
        // 播放枪声
        gunAudio.Play();
        // 播放开火特效
        gunParticles.Play();
        gunLight.enabled = true;
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        if (Physics.Raycast(shootRay, out shootHit, shootRange))
        {
            EnemyHealthController enemyHealth =  shootHit.collider.GetComponent<EnemyHealthController>();
            // 射线与敌人产生碰撞
            if (enemyHealth != null)
            {
                // 造成伤害和获取分值
                enemyHealth.Attacked(damagePerShot, shootHit.point);
                player.GetComponent<PlayerScoreController>().score += enemyHealth.scoreValue;
            }
            Rigidbody rigidbody = shootHit.collider.GetComponent<Rigidbody>();

            // 直接对敌人施加力 （没有效果 原因暂时不明...）
            //if (rigidbody != null)
            //{
            //    rigidbody.AddForce(...);   
            //}

            // 碰撞体撞击敌人产生击退效果
            shootCollider.gameObject.SetActive(true);
            shootCollider.transform.position = shootHit.collider.transform.position + Vector3.back;
            // 根据距离设计击退力度
            float distance = (transform.position - shootHit.collider.transform.position).magnitude;
            float shootForce = (shootRange > distance) ? (shootForceBase * ((shootRange - distance) / shootRange)) : 0;
            Debug.Log(shootForce);
            shootCollider.AddForce(Vector3.forward*shootForce);
            // 射击特效投射到敌人
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            // 射击特效投射最大距离
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * shootRange);
        }

    }
} 
    

