using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public UIButton btnClose;
    public UIScrollView svList;

    //ר�����ڴ洢�������ݿؼ�
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
        //���������Ϣ

        //��ȡ���а�����
        List<RankInfo> list = GameDataMgr.Instance.rankData.rankList;
        //���� ���ݸ�������� �����Ϣ
        //��� ֻ������ �������
        for (int i = 0; i < list.Count; i++)
        {
            //���������Ѿ����� ��� ֱ�Ӹ��¼���
            if (itemList.Count > i)
            {
                itemList[i].InitInfo(i + 1, list[i].name, list[i].time);
            }
            //�������� ��Ͽؼ������� ��ȥʵ��������
            else
            {
                //����Ԥ����
                GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RankItem"));
                //���ø�����
                obj.transform.SetParent(svList.transform, false);
                //����λ��
                obj.transform.localPosition = new Vector3(0, 120 - 55 * i, 0);

                //��������
                //�õ��ű�
                RankItem item = obj.GetComponent<RankItem>();
                //�����������ݷ���
                item.InitInfo(i + 1, list[i].name, list[i].time);
                //��¼
                itemList.Add(item);
            }
        }
    }
}
