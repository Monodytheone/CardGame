using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    /// <summary>
    /// ��ʼ��
    /// </summary>
    public Vector2 StartPoint; 
    /// <summary>
    /// ��ֹ��
    /// </summary>
    private Vector2 EndingPoint;

    /// <summary>
    /// ��ͷ�Ǹ�Image��ֱ�ӻ�ȡ����Transform�ǻ�ȡ������
    /// </summary>
    private RectTransform arrow;

    /// <summary>
    /// ��ͷ����
    /// </summary>
    private float ArrowLength;
    /// <summary>
    /// ��ͷ����
    /// </summary>
    private float ArrowTheta;
    /// <summary>
    /// ��ͷ����
    /// </summary>
    private Vector2 ArrowPosition;









    // Start is called before the first frame update
    void Start()
    {
        arrow = transform.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // �������
        EndingPoint = Input.mousePosition - new Vector3(960f, 540f, 0f);  // ��ֹ�㼴ʱ����Ϊ���λ��
        ArrowPosition = new Vector2((EndingPoint.x + StartPoint.x) / 2, (EndingPoint.y + StartPoint.y) / 2);
        ArrowLength = Mathf.Sqrt((EndingPoint.x - StartPoint.x) * (EndingPoint.x - StartPoint.x) + (EndingPoint.y - StartPoint.y) * (EndingPoint.y - StartPoint.y));
        ArrowTheta = Mathf.Atan2(EndingPoint.y - StartPoint.y, EndingPoint.x - StartPoint.x); ;
         
        // �Լ�ͷ��RectTransform��ֵ
        arrow.localPosition = ArrowPosition;  // ����
        arrow.sizeDelta = new Vector2(ArrowLength, arrow.sizeDelta.y);  // �ߴ�
        // ��z����ת������ArrowThetaתΪ�Ƕ�Ҫ��180����PI
        arrow.localEulerAngles = new Vector3(0f, 0f, ArrowTheta * 180 / Mathf.PI);  // ŷ����


        //arrow.localPosition = EndingPoint;  // Test
    }





    /// <summary>
    /// ������ʼ��
    /// <para>�������½�Ϊ0��Ĳ���תΪ������Ϊ0��ģ�����StartPoint </para>
    /// </summary>
    /// <param name="_startPoint">�����½�Ϊԭ��Ķ�ά����</param>
    public void SetStartPoint(Vector2 _startPoint)
    {
        StartPoint = _startPoint - new Vector2(960f, 540f);  // ��������½ǵ�������� 
    }
}
