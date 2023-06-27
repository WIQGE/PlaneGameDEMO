using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public UIButton btnClose;
    public UIScrollView svList;

    //专门用于存储单条数据控件
    private List<RankItem> itemList = new List<RankItem>();
    public override void Init()
    {
        btnClose.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
        }));
        HideMe();

        
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //更新面板信息

        //获取排行榜数据
        List<RankInfo> list = GameDataMgr.Instance.rankData.rankList;
        //根据 数据更新面板上 组件信息
        //组件 只会增加 不会减少
        for (int i = 0; i < list.Count; i++)
        {
            //如果面板上已经存在 组件 直接更新即可
            if (itemList.Count > i)
            {
                itemList[i].InitInfo(i + 1, list[i].name, list[i].time);
            }
            //如果面板上 组合控件不够多 就去实例化出来
            else
            {
                //创建预设体
                GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RankItem"));
                //设置父对象
                obj.transform.SetParent(svList.transform, false);
                //设置位置
                obj.transform.localPosition = new Vector3(0, 120 - 55 * i, 0);

                //设置数据
                //得到脚本
                RankItem item = obj.GetComponent<RankItem>();
                //调用设置数据方法
                item.InitInfo(i + 1, list[i].name, list[i].time);
                //记录
                itemList.Add(item);
            }
        }
    }
}
