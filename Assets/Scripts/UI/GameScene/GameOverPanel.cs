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
            //���浱ǰ��ҳɼ������а���
            GameDataMgr.Instance.AddRankData(inputName.value, endTime);
            //�лؿ�ʼ����
            SceneManager.LoadScene("BeginScene");
        }));
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //��ʾ���ʱ Ӧ�ü�¼ ��ǰʱ��
        endTime = (int)GamePanel.Instance.nowTime;
        //�õ���Ϸ��������ʾ��ʱ��
        labTime.text = GamePanel.Instance.labTime.text;
    }

}
