using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityExtention
{
    /// <summary>
    /// JObject를 원하는 타입으로 변환하여 반환하는 제네릭 메서드
    /// </summary>
    /// <typeparam name="T">변환할 타입</typeparam>
    /// <param name="path">불러올 JObject의 이름</param>
    /// <returns>변환된 데이터</returns>
    public static T GetValue<T>(this JToken obj, T defaultValue = default)
    {
        try
        {
            if (obj == null || obj.Type == JTokenType.Null)
                return defaultValue;

            return obj.Value<T>();
        }
        catch
        {
            Debug.LogWarning($"JToken 변환 실패: {obj} → {typeof(T).Name}");
            return defaultValue;
        }
    }

    /// <summary>
    /// Fisher-Yates Shuffle, List용
    /// </summary>
    /// <typeparam name="T">모든 변수 타입</typeparam>
    /// <param name="values"></param>
    /// <returns>List</returns>
    public static List<T> Shuffle<T>(this List<T> values)
    {
        for (int i = values.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (values[i], values[j]) = (values[j], values[i]);
        }

        return values;
    }

    /// <summary>
    /// Fisher-Yates Shuffle, Array용
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns>Array</returns>
    public static T[] Shuffle<T>(this T[] values)
    {
        for (int i = values.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (values[i], values[j]) = (values[j], values[i]);
        }

        return values;
    }
}

