using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVImportExample : ScriptableObject
{
    [System.Serializable]
    public class Sample
    {
        public int year;
        public string make;
        public string model;
        public string description;
        public float price;
    }
    public Sample[] m_Sample;
}
