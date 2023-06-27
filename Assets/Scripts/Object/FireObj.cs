using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��ʾ �����λ�õ� ����
/// </summary>
public enum E_Pos_Type
{
    TopLeft,
    Top,
    TopRight,

    Left,
    Right,

    BottomLeft,
    Bottom,
    BottomRight,
}

public class FireObj : MonoBehaviour
{
    public E_Pos_Type type;

    //��ǰ������������Ϣ
    private FireInfo fireInfo;
    private int nowNum;
    private float nowCD;
    private float nowDelay;
    //��ǰ�鿪��� ʹ�õ��ӵ���Ϣ
    private BulletInfo nowBulletInfo;
    //ɢ��ʱ ÿ���ӵ��ļ���Ƕ�
    private float changeAngle;

    //��ʾ��Ļ�ϵĵ�
    private Vector3 screenPos;
    //��ʼ�����ӵ��ķ��� ��Ҫ������Ϊɢ���ĳ�ʼ���� ���ڼ���
    private Vector3 initDir;
    //��������ڷ���ɢ��ʱ ��¼��һ�εķ����
    private Vector3 nowDir;

    // Update is called once per frame
    void Update()
    {
        //���ڲ��Եõ� ���ת��Ļ����� ������ z��ֵ
        //print(Camera.main.WorldToScreenPoint(PlayerObject.Instance.transform.position));
        //����λ��
        UpdatePos();
        //ÿһ�� ����� �Ƿ���Ҫ ���� ����� ����
        ResetFireInfo();
        //�����ӵ�
        UpdateFire();
    }

    //���ݵ������ ����������λ��
    private void UpdatePos()
    {
        //��������Ϊ�������λ��ת��Ļ������ zλ��һ�� Ŀ���� �õ����� ���ڵ� �������һ�µ�
        screenPos.z = 351;
        switch (type)
        {
            case E_Pos_Type.TopLeft:
                screenPos.x = 0;
                screenPos.y = Screen.height;

                initDir = Vector3.right;
                break;
            case E_Pos_Type.Top:
                screenPos.x = Screen.width / 2;
                screenPos.y = Screen.height;

                initDir = Vector3.right;
                break;
            case E_Pos_Type.TopRight:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;

                initDir = Vector3.back;
                break;
            case E_Pos_Type.Left:
                screenPos.x = 0;
                screenPos.y = Screen.height / 2;

                initDir = Vector3.forward;
                break;
            case E_Pos_Type.Right:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height / 2;

                initDir = Vector3.back;
                break;
            case E_Pos_Type.BottomLeft:
                screenPos.x = 0;
                screenPos.y = 0;

                initDir = Vector3.forward;
                break;
            case E_Pos_Type.Bottom:
                screenPos.x = Screen.width / 2;
                screenPos.y = 0;

                initDir = Vector3.left;
                break;
            case E_Pos_Type.BottomRight:
                screenPos.x = Screen.width;
                screenPos.y = 0;

                initDir = Vector3.left;
                break;
        }
        //�ٰ���Ļ�� ת�����ǵ� ��������� �ǵõ��� ���� ������Ҫ������
        this.transform.position = Camera.main.ScreenToWorldPoint(screenPos);
    }

    //���õ�ǰҪ�������̨����
    private void ResetFireInfo()
    {
        //�Լ���һ������ ֻ�е�cd��������Ϊ0ʱ ����Ϊ��Ҫ���»�ȡ���ǵ� ���������
        if (nowCD != 0 && nowNum != 0)
            return;
        //�����Ϣʱ���ж�
        if (fireInfo != null)
        {
            nowDelay -= Time.deltaTime;
            //���������Ϣ 
            if (nowDelay > 0)
                return;
        }

        //�����������ȡ��һ�� �����չ��� �����ӵ�
        List<FireInfo> list = GameDataMgr.Instance.fireData.fireInfoList;
        fireInfo = list[Random.Range(0, list.Count)];
        //���ǲ���ֱ�Ӹı����ݵ��е����� ����Ӧ���ñ��� ��ʱ�洢���� �����Ͳ���Ӱ���������ݱ���
        nowNum = fireInfo.num;
        nowCD = fireInfo.cd;
        nowDelay = fireInfo.delay;

        //ͨ�� ��������� ȡ�� ��ǰҪʹ�õ��ӵ�������Ϣ
        //�õ���ʼid�ͽ���id �������ȡ�ӵ�����Ϣ
        string[] strs = fireInfo.ids.Split(',');
        int beginID = int.Parse(strs[0]);
        int endID = int.Parse(strs[1]);
        int randomBulletID = Random.Range(beginID, endID + 1);
        nowBulletInfo = GameDataMgr.Instance.bulletData.bulletInfoList[randomBulletID - 1];

        //�����ɢ�� ����Ҫ���� ���ǵ� ����Ƕ�
        if (fireInfo.type == 2)
        {
            switch (type)
            {
                case E_Pos_Type.TopLeft:
                case E_Pos_Type.TopRight:
                case E_Pos_Type.BottomLeft:
                case E_Pos_Type.BottomRight:
                    changeAngle = 90f / (nowNum + 1);
                    break;
                case E_Pos_Type.Top:
                case E_Pos_Type.Left:
                case E_Pos_Type.Right:
                case E_Pos_Type.Bottom:
                    changeAngle = 180f / (nowNum + 1);
                    break;
            }
        }
    }

    //��⿪��
    private void UpdateFire()
    {
        //��ǰ״̬ �ǲ���Ҫ�����ӵ��� 
        if (nowCD == 0 && nowNum == 0)
            return;

        //cd����
        nowCD -= Time.deltaTime;
        if (nowCD > 0)
            return;
        GameObject bullet;
        BulletObj bulletObj;
        switch (fireInfo.type)
        {
            //һ��һ���ķ����ӵ� �������
            case 1:
                //��̬���� �ӵ�����
                bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                //��̬��� �ӵ��ű�
                bulletObj = bullet.AddComponent<BulletObj>();
                //�ѵ�ǰ���ӵ����ݴ����ӵ��ű� ���г�ʼ��
                bulletObj.InitInfo(nowBulletInfo);

                //�����ӵ���λ�� �ͳ���
                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = Quaternion.LookRotation(PlayerObj.Instance.transform.position - this.transform.position);

                //��ʾ�Ѿ�����һ���ӵ�
                --nowNum;
                //����cd 
                nowCD = nowNum == 0 ? 0 : fireInfo.cd;
                break;
            //����ɢ��
            case 2:
                //��cd һ˲�� �������е�ɢ��
                if (nowCD == 0)
                {
                    for (int i = 0; i < nowNum; i++)
                    {
                        //��̬���� �ӵ�����
                        bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                        //��̬��� �ӵ��ű�
                        bulletObj = bullet.AddComponent<BulletObj>();
                        //�ѵ�ǰ���ӵ����ݴ����ӵ��ű� ���г�ʼ��
                        bulletObj.InitInfo(nowBulletInfo);

                        //�����ӵ���λ�� �ͳ���
                        bullet.transform.position = this.transform.position;
                        //ÿ�ζ�����תһ���Ƕ� �õ�һ���µķ���
                        nowDir = Quaternion.AngleAxis(changeAngle * i, Vector3.up) * initDir;
                        bullet.transform.rotation = Quaternion.LookRotation(nowDir);
                    }
                    //��Ϊ��˲�䴴���������ӵ� ������������
                    nowCD = nowNum = 0;
                }
                else
                {
                    //��̬���� �ӵ�����
                    bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                    //��̬��� �ӵ��ű�
                    bulletObj = bullet.AddComponent<BulletObj>();
                    //�ѵ�ǰ���ӵ����ݴ����ӵ��ű� ���г�ʼ��
                    bulletObj.InitInfo(nowBulletInfo);

                    //�����ӵ���λ�� �ͳ���
                    bullet.transform.position = this.transform.position;
                    //ÿ�ζ�����תһ���Ƕ� �õ�һ���µķ���
                    nowDir = Quaternion.AngleAxis(changeAngle * (fireInfo.num - nowNum), Vector3.up) * initDir;
                    bullet.transform.rotation = Quaternion.LookRotation(nowDir);

                    //��ʾ�Ѿ�����һ���ӵ�
                    --nowNum;
                    //����cd 
                    nowCD = nowNum == 0 ? 0 : fireInfo.cd;
                }
                break;
        }
    }
}

