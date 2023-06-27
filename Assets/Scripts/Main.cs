using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        //���ݿ�ʼ������ѡ���Ӣ�� ����̬���� �ɻ�
        RoleInfo info = GameDataMgr.Instance.GetNowSelHeroInfo();

        //���������е���Դ·�� ���ж�̬����
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.resName));
        PlayerObj playerObj = obj.AddComponent<PlayerObj>();
        playerObj.speed = info.speed * 20;
        playerObj.roundSpeed = 20;
        playerObj.maxHp = 10;
        playerObj.nowHp = info.hp;

        //���½�������ʾ��Ѫ��
        GamePanel.Instance.ChangeHp(info.hp);

    }

}
