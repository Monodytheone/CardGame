using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    /// <summary>
    /// 起始点
    /// </summary>
    public Vector2 StartPoint; 
    /// <summary>
    /// 终止点
    /// </summary>
    private Vector2 EndingPoint;

    /// <summary>
    /// 箭头是个Image，直接获取它的Transform是获取不到的
    /// </summary>
    private RectTransform arrow;

    /// <summary>
    /// 箭头长度
    /// </summary>
    private float ArrowLength;
    /// <summary>
    /// 箭头弧度
    /// </summary>
    private float ArrowTheta;
    /// <summary>
    /// 箭头坐标
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
        // 计算变量
        EndingPoint = Input.mousePosition - new Vector3(960f, 540f, 0f);  // 终止点即时更新为鼠标位置
        ArrowPosition = new Vector2((EndingPoint.x + StartPoint.x) / 2, (EndingPoint.y + StartPoint.y) / 2);
        ArrowLength = Mathf.Sqrt((EndingPoint.x - StartPoint.x) * (EndingPoint.x - StartPoint.x) + (EndingPoint.y - StartPoint.y) * (EndingPoint.y - StartPoint.y));
        ArrowTheta = Mathf.Atan2(EndingPoint.y - StartPoint.y, EndingPoint.x - StartPoint.x); ;
         
        // 对箭头的RectTransform赋值
        arrow.localPosition = ArrowPosition;  // 坐标
        arrow.sizeDelta = new Vector2(ArrowLength, arrow.sizeDelta.y);  // 尺寸
        // 绕z轴旋转，弧度ArrowTheta转为角度要乘180除以PI
        arrow.localEulerAngles = new Vector3(0f, 0f, ArrowTheta * 180 / Mathf.PI);  // 欧拉角


        //arrow.localPosition = EndingPoint;  // Test
    }





    /// <summary>
    /// 设置起始点
    /// <para>把以左下角为0点的参数转为以中心为0点的，赋给StartPoint </para>
    /// </summary>
    /// <param name="_startPoint">以左下角为原点的二维向量</param>
    public void SetStartPoint(Vector2 _startPoint)
    {
        StartPoint = _startPoint - new Vector2(960f, 540f);  // 从相对左下角到相对中心 
    }
}
