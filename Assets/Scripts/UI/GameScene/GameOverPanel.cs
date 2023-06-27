using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : BasePanel<GameOverPanel>
{
    public UILabel labTime;
    public UIInput inputName;
    public UIButton btnSure;

    private int endTime;
    public override void Init()
    {
        btnSure.onClick.Add(new EventDelegate(() =>
        {
            //保存当前玩家成绩到排行榜中
            GameDataMgr.Instance.AddRankData(inputName.value, endTime);
            //切回开始界面
            SceneManager.LoadScene("BeginScene");
        }));
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //显示面板时 应该记录 当前时间
        endTime = (int)GamePanel.Instance.nowTime;
        //得到游戏界面中显示的时间
        labTime.text = GamePanel.Instance.labTime.text;
    }

}
