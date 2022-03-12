using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ģ��
/// <para>�˽ű�������ʮ�ֵ��ͣ���VS����ĺ�һ����ѧϰ!</para>
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour  // ����TҲ�Ǽ̳���MonoBehavour��
{
    private static T instance;
    public static T Instance  // ������ͷһ����дһ��Сд
    {
        get  // �ⲿ���� Instance ʱ���ڲ��ͻ�� instance����
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
        if(instance != null)  // ����տ�ʼinstance���Ѿ�������ش���������������֮
        {
            Destroy(gameObject);  // ���԰�������"gameObject"�Ͽ�������
        }
    }

   
}
