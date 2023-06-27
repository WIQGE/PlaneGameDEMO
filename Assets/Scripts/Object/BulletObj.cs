using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    //子弹使用的数据
    private BulletInfo info;

    //用于曲线运动 的计时
    private float time;

    //private void Start()
    //{
    //    InitInfo(GameDataMgr.Instance.bulletData.bulletInfoList[Random.Range(0,20)]);
    //}

    /// <summary>
    /// 初始化子弹数据方法
    /// </summary>
    /// <param name="info">单条子弹数据</param>
    public void InitInfo(BulletInfo info)
    {
        this.info = info;
        //根据生命周期函数 决定自己什么时候移除（使用延迟函数更保险）
        Invoke("DealyDestroy", info.lifeTime);
    }
    private void DealyDestroy()
    {
        //死亡
        Dead();
    }


    /// <summary>
    /// 销毁子弹
    /// </summary>
    public void Dead()
    {
        //创建死亡特效
        GameObject effObj = Instantiate(Resources.Load<GameObject>(info.deadEffRes));
        //设置特效位置 创建在当前子弹位置
        effObj.transform.position = this.transform.position;
        //1秒后延迟消除特效
        Destroy(effObj, 1f);

        //销毁子弹对象
        Destroy(this.gameObject);
    }

    //和对象碰撞时（触发）
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //得到玩家脚本
            PlayerObj obj = other.gameObject.GetComponent<PlayerObj>();
            //玩家受伤
            obj.Wound();
            //销毁自己
            Dead();
        }
    }

    void Update()
    {
        //所有子弹共同移动特点 都是朝自己的面朝向移动
        this.transform.Translate(Vector3.forward * info.forwardSpeed *  Time.deltaTime);
        //接着来处理 其他的移动逻辑
        //1代表只朝 自己面朝向移动 直线移动
        //2代表 曲线
        //3代表 右抛物线
        //4代表 左抛物线
        //5代表 跟踪导弹
        switch(info.type)
        {
            case 2:
                time += Time.deltaTime;
                //sin里面值变化的快慢 决定了 左右变化的频率
                //乘以的速度 变化的大小 决定了 左右位移 的多少
                //曲线运动时 roundSpeed 旋转速度 用于控制变化的频率
                this.transform.Translate(Vector3.right * Mathf.Sin(time * info.roundSpeed) * info.rightSpeed * Time.deltaTime);
                break;
            case 3:
                //右抛物线 改变旋转角度 
                this.transform.rotation *= Quaternion.AngleAxis(info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 4:
                //左抛物线 改变负旋转角度 
                this.transform.rotation *= Quaternion.AngleAxis(-info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 5:
                //跟踪移动 不停计算 玩家与子弹之间的方向向量 然后得到四元数 角度不停的变化为该四元素
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                           Quaternion.LookRotation(PlayerObj.Instance.transform.position - this.transform.position),
                                                           info.roundSpeed * Time.deltaTime);
                break;
        }
    }
}
