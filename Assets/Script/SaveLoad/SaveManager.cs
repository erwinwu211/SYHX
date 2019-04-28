using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class SaveLoadUtility
{
    private static string GetDirectory()
    {
        return "Save";
    }
    private static string ReadAndDecrypt(string name)
    {
        var path = PreparePath(name);

        if(File.Exists(path))
        {
            return null;
        }
        
        var text = File.ReadAllText(path);
        return StringEncryptor.Decrypt(text);
    }
    public static T GetSaveData<T>(string path)
    {
        var text = ReadAndDecrypt(path);
        return JsonUtility.FromJson<T>(text);

    }
    public static void SetSaveData<T>(T obj,string name)
    {
        var json = JsonUtility.ToJson(obj);
        var jsonEncrypt = StringEncryptor.Encrypt(json);

        var path = PreparePath(name);
        File.WriteAllText(path,jsonEncrypt);
    }

    private static string PreparePath(string name)
    {
        var path = GetDirectory();

        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return $"{path}/{name}";
    }
}
