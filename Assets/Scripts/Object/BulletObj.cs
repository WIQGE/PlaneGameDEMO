using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    //�ӵ�ʹ�õ�����
    private BulletInfo info;

    //���������˶� �ļ�ʱ
    private float time;

    //private void Start()
    //{
    //    InitInfo(GameDataMgr.Instance.bulletData.bulletInfoList[Random.Range(0,20)]);
    //}

    /// <summary>
    /// ��ʼ���ӵ����ݷ���
    /// </summary>
    /// <param name="info">�����ӵ�����</param>
    public void InitInfo(BulletInfo info)
    {
        this.info = info;
        //�����������ں��� �����Լ�ʲôʱ���Ƴ���ʹ���ӳٺ��������գ�
        Invoke("DealyDestroy", info.lifeTime);
    }
    private void DealyDestroy()
    {
        //����
        Dead();
    }


    /// <summary>
    /// �����ӵ�
    /// </summary>
    public void Dead()
    {
        //����������Ч
        GameObject effObj = Instantiate(Resources.Load<GameObject>(info.deadEffRes));
        //������Чλ�� �����ڵ�ǰ�ӵ�λ��
        effObj.transform.position = this.transform.position;
        //1����ӳ�������Ч
        Destroy(effObj, 1f);

        //�����ӵ�����
        Destroy(this.gameObject);
    }

    //�Ͷ�����ײʱ��������
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //�õ���ҽű�
            PlayerObj obj = other.gameObject.GetComponent<PlayerObj>();
            //�������
            obj.Wound();
            //�����Լ�
            Dead();
        }
    }

    void Update()
    {
        //�����ӵ���ͬ�ƶ��ص� ���ǳ��Լ����泯���ƶ�
        this.transform.Translate(Vector3.forward * info.forwardSpeed *  Time.deltaTime);
        //���������� �������ƶ��߼�
        //1����ֻ�� �Լ��泯���ƶ� ֱ���ƶ�
        //2���� ����
        //3���� ��������
        //4���� ��������
        //5���� ���ٵ���
        switch(info.type)
        {
            case 2:
                time += Time.deltaTime;
                //sin����ֵ�仯�Ŀ��� ������ ���ұ仯��Ƶ��
                //���Ե��ٶ� �仯�Ĵ�С ������ ����λ�� �Ķ���
                //�����˶�ʱ roundSpeed ��ת�ٶ� ���ڿ��Ʊ仯��Ƶ��
                this.transform.Translate(Vector3.right * Mathf.Sin(time * info.roundSpeed) * info.rightSpeed * Time.deltaTime);
                break;
            case 3:
                //�������� �ı���ת�Ƕ� 
                this.transform.rotation *= Quaternion.AngleAxis(info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 4:
                //�������� �ı为��ת�Ƕ� 
                this.transform.rotation *= Quaternion.AngleAxis(-info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 5:
                //�����ƶ� ��ͣ���� ������ӵ�֮��ķ������� Ȼ��õ���Ԫ�� �ǶȲ�ͣ�ı仯Ϊ����Ԫ��
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                           Quaternion.LookRotation(PlayerObj.Instance.transform.position - this.transform.position),
                                                           info.roundSpeed * Time.deltaTime);
                break;
        }
    }
}
