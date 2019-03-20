using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadSQL : MonoBehaviour {


    void Start()
    {

        SqliteDatabase sqlDB = new SqliteDatabase("1.db");
        string query = string.Format("select * from Card");
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        int id = 0;
        string name = "";
        int atk = 0;
        int def = 0;
        string effect = "";

        foreach (DataRow dr in dataTable.Rows)
        {
            name = (string)dr["Name"];
            id = (int)dr["ID"];
            atk = (int)dr["ATK"];
            def = (int)dr["DEF"];
            effect = (string)dr["EFC"];
            Debug.Log(id + ":" + name + ":" + atk + ":" + def );

        }
    }
}
