using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePanel : BasePanel<ChoosePanel>
{
    //各个按钮
    public UIButton btnClose;
    public UIButton btnLeft;
    public UIButton btnRight;
    public UIButton btnStart;

    //模型父对象
    public Transform heroPos;

    //属性相关对象
    public List<GameObject> hpObjs;
    public List<GameObject> speedObjs;
    public List<GameObject> volumeObjs;

    //当前显示的飞机模型对象
    private GameObject airPlaneObj;

    public Camera UICamera;

    public override void Init()
    {
        //选择角色后 点击开始 切场景
        btnStart.onClick.Add(new EventDelegate(() =>
        {
            SceneManager.LoadScene("GameScene");
        }));
        //左按钮点击 逻辑
        btnLeft.onClick.Add(new EventDelegate(() =>
        {
            //左按钮 减索引
            GameDataMgr.Instance.nowSelHeroIndex--;
            //如果小于最小的索引 直接等于最后一个索引
            if (GameDataMgr.Instance.nowSelHeroIndex < 0)
                GameDataMgr.Instance.nowSelHeroIndex = GameDataMgr.Instance.roleData.roleList.Count - 1;

            ChangeNowHero();
        }));
        //右按钮点击 逻辑
        btnRight.onClick.Add(new EventDelegate(() =>
        {
            //右按钮 加索引
            GameDataMgr.Instance.nowSelHeroIndex++;
            //如果大于最大的索引 直接等于0
            if (GameDataMgr.Instance.nowSelHeroIndex > GameDataMgr.Instance.roleData.roleList.Count - 1)
                GameDataMgr.Instance.nowSelHeroIndex = 0;

            ChangeNowHero();
        }));
        //关闭按钮 逻辑
        btnClose.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
            BeginPanel.Instance.ShowMe();
        }));
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        GameDataMgr.Instance.nowSelHeroIndex = 0;
        ChangeNowHero() ;
    }
    public override void HideMe()
    {
        base.HideMe();
        DestorObj();
    }
    private void ChangeNowHero()
    {
        //得到当前选择的 角色数据
        RoleInfo info = GameDataMgr.Instance.GetNowSelHeroInfo();

        //更新模型
        //先删除上一次的飞机模型
        DestorObj();
        //再创建当前的飞机模型
        airPlaneObj = Instantiate(Resources.Load<GameObject>(info.resName));
        //设置父对象
        airPlaneObj.transform.SetParent(heroPos, false);
        //设置角度、位置、缩放
        airPlaneObj.transform.localPosition = Vector3.zero;
        airPlaneObj.transform.localRotation = Quaternion.identity;
        airPlaneObj.transform.localScale = Vector3.one * info.scale;
        //修改为UI层
        airPlaneObj.layer = LayerMask.NameToLayer("UI");


        //更新属性
        for (int i = 0; i < 10; i++)
        {
            hpObjs[i].SetActive(i < info.hp);
            speedObjs[i].SetActive(i < info.speed);
            volumeObjs[i].SetActive(i < info.volume);
        }
    }

    private void DestorObj()
    {
        if (airPlaneObj != null)
        {
            Destroy(airPlaneObj);
            airPlaneObj = null;
        }
    }

    private float time;
    //鼠标是否选中
    private bool isSel;
    void Update()
    {
        //让飞机上下浮动
        time += Time.deltaTime;
        heroPos.Translate(Vector3.up * Mathf.Sin(time) * 0.0001f, Space.World);

        //射线检测 让飞机 可以 左右转动
        if (Input.GetMouseButtonDown(0))
        {
            //如果点击到了 UI层碰撞器 认为需要 拖飞机
            if (Physics.Raycast(UICamera.ScreenPointToRay(Input.mousePosition),
                                1000, 1 << LayerMask.NameToLayer("UI")))
            {
                isSel = true;
            }
        }
        //抬起取消旋转
        if (Input.GetMouseButtonUp(0))
        {
            isSel = false;
        }
        //旋转对象
        if (Input.GetMouseButton(0) && isSel)
        {
            heroPos.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * -20, Vector3.up);
        }
    }
}
