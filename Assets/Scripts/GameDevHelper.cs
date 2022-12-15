using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class GameDevHelper : MonoBehaviour
{
    public static Vector3 ClampedVector(Vector3 vector, Vector3 minRange, Vector3 maxRange)
    {
        Vector3 clamped = vector;
        clamped.x = Mathf.Clamp(clamped.x, minRange.x, maxRange.x);
        clamped.y = Mathf.Clamp(clamped.y, minRange.y, maxRange.y);
        clamped.z = Mathf.Clamp(clamped.z, minRange.z, maxRange.z);
        return clamped;
    }

    public static Vector2 RandomVector(Vector2 range)
    {
        Vector2 random;
        random.x = Random.Range(-range.x, range.x);
        random.y = Random.Range(-range.y, range.y);
        return random;
    }

    public static Vector2Int ToVector2Int(Vector2 vector)
    {
        return new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
    }

    public static Color RandomColor()
    {
        Color randomColor = new Color(
          Random.Range(0f, 1f),
          Random.Range(0f, 1f),
          Random.Range(0f, 1f)
            );

        return randomColor;
    }

    public static T RandomEnum<T>()
    {
        System.Array array = System.Enum.GetValues(typeof(T));
        T randomBiome = (T)array.GetValue(Random.Range(0, array.Length));
        return randomBiome;
    }
}
