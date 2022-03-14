using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomSummonBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(gameObject.GetComponent<Block>().SummonBlock.activeInHierarchy || 
            gameObject.GetComponent<Block>().AttackBlock.activeInHierarchy)  // 如果是高亮
        {
            transform.localScale = new Vector3(1.3f, 1.3f, 1.0f);  // x, y都扩大到原来的1.2倍
        }
    }

    /// <summary>
    /// 这个方法即使格子原本没有高亮也会执行，以后要找办法，让只有鼠标已经进入之后，才能触发该方法的判定
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("鼠标离开Block");
        transform.localScale = Vector3.one;
    }    
}
