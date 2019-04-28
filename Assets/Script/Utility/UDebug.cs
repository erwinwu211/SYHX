using System.Diagnostics;

public static class UDebug
{

    /// <summary>
    /// 以防正式发布时有log，Debug.Log全部替换为这个
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Log(object obj)
    {
        UnityEngine.Debug.Log(obj);
    }
}
