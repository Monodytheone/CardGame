using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例模板
/// <para>此脚本的内容十分典型，跟VS建议的很一样，学习!</para>
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour  // 泛型T也是继承自MonoBehavour的
{
    private static T instance;
    public static T Instance  // 这俩开头一个大写一个小写
    {
        get  // 外部请求 Instance 时，内部就会把 instance给它
        {
            if(instance == null)
            {
                instance  = FindObjectOfType<T>();
            }
            return instance;
        }
    }







    private void Awake()
    {
        if(instance != null)  // 如果刚开始instance就已经被错误地创建出来，就销毁之
        {
            Destroy(gameObject);  // 可以把鼠标放在"gameObject"上看看解释
        }
    }

   
}
