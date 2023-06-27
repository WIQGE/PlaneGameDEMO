using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPanel : BasePanel<BeginPanel>
{
    public UIButton btnBegin;
    public UIButton btnRank;
    public UIButton btnSetting;
    public UIButton btnQuit;
    public override void Init()
    {
        //监听按钮事件
        btnBegin.onClick.Add(new EventDelegate(() =>
        {
            //显示 选角面板
            ChoosePanel.Instance.ShowMe();
            //隐藏自己
            HideMe();
        }));

        btnRank.onClick.Add(new EventDelegate(() =>
        {
            //显示 排行榜
            RankPanel.Instance.ShowMe();
        }));

        btnSetting.onClick.Add(new EventDelegate(() =>
        {
            //显示 设置
            SettingPanel.Instance.ShowMe();
        }));

        btnQuit.onClick.Add(new EventDelegate(() =>
        {
            //退出游戏
            Application.Quit();
        }));
    }
}
