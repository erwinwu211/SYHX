using System;
using System.Reflection;
using System.Collections.Generic;

public interface Source { }
public interface Content { }

//source生成简化
public static class SourceExtension
{
    public static Dictionary<string, PropertyInfo> InitDescOption<Ts, Tc>(this Ts source)
    where Ts : Source
    where Tc : Content
    {
        // UDebug.Log("==============");
        // UDebug.Log("In Start");
        var dictionary = new Dictionary<string, PropertyInfo>();
        var properties = typeof(Tc).GetProperties();
        foreach (var property in properties)
        {
            var att = (CustomDescAttribute)Attribute.GetCustomAttribute(property, typeof(CustomDescAttribute));
            if (att != null)
            {
                dictionary.Add(att.descName, property);
                UDebug.Log(att.descName);
            }
        }
        // UDebug.Log("===============");
        return dictionary;
    }

    public static Tc GenerateContent<Ts, Tc>(this Ts source, Tc origin)
    where Ts : Source
    where Tc : Content, new()
    {
        var tc = new Tc();
        var fields = typeof(Tc).GetFields();
        foreach (var field in fields)
        {
            if (field.IsDefined(typeof(CloneFieldAttribute), false))
            {
                var obj = field.GetValue(origin);
                field.SetValue(tc, obj);
            }
        }
        return tc;
    }

}
