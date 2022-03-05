using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // 为了鼠标响应的功能

/// <summary>
/// 鼠标移到卡牌上时将其放大
/// </summary>
public class ZoomUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// 放大的尺寸
    /// </summary>
    public float zoomSize;






    /// <summary>
    /// 鼠标进入时放大
    /// <para>鼠标进入的方法必须叫这个名字，不然class ZoomUI后继承的IPointerEnterHandler会划线报错，下同
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.localScale = new Vector3(zoomSize, zoomSize, 1.0f);  // x, y都扩大zoomSize倍
    }

    /// <summary>
    /// 鼠标退出时缩小
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.localScale = Vector3.one;
    }
    
}
