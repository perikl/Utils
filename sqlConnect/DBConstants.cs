using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AutoCADTest
{
    public static class DBConstants
    {
        public static string ConnectionString;
        public static string DataBase;

        public static string EventNameTable;
        public static string FacilityNameTable;

        public static string ExpositionTable;
        public static string EventTable;
        //Exposition:
        public static string LengthPlan;
        public static string WidthPlan;
        public static string AreaPlan;
        public static string Addressing;
        public static string TypeLayoutName;
        public static string CarpetName;
        public static string ExpositionTableEventName;
        public static string ExpositionTableEventId;
        public static string EventNameTableNameRus;
        public static string AccountName;
        public static string AccountPhone;
        public static string ConactName;
        public static string ContactPhones;
        public static string ContactEmail;
        public static string TSFacility;
        public static string EmployeeName;
        public static string CreatedOn;
        public static string AutocadXMLData;
        public static string AutocadFileData;
        //Event:
        //public static string 
        public static void fillConstants(string configFile)
        {
            XDocument xDoc = XDocument.Load(configFile);
            ConnectionString = xDoc.Root.Element("ConnectionString").Value.Replace("\n", "").Trim();
            DataBase = xDoc.Root.Element("DataBase").Value.Replace("\n", "").Trim();
            EventNameTable = xDoc.Root.Element("EventNameTable").Value.Replace("\n", "").Trim();
            FacilityNameTable = xDoc.Root.Element("FacilityNameTable").Value.Replace("\n", "").Trim();
            ExpositionTable = xDoc.Root.Element("ExpositionTable").Value.Replace("\n", "").Trim();
            EventTable = xDoc.Root.Element("EventTable").Value.Replace("\n", "").Trim();
            LengthPlan = xDoc.Root.Element("Fields").Element("LengthPlan").Value.Replace("\n", "").Trim();
            WidthPlan = xDoc.Root.Element("Fields").Element("WidthPlan").Value.Replace("\n", "").Trim();
            AreaPlan = xDoc.Root.Element("Fields").Element("AreaPlan").Value.Replace("\n", "").Trim();
            Addressing = xDoc.Root.Element("Fields").Element("Addressing").Value.Replace("\n", "").Trim();
            TypeLayoutName = xDoc.Root.Element("Fields").Element("TypeLayoutName").Value.Replace("\n", "").Trim();
            CarpetName = xDoc.Root.Element("Fields").Element("CarpetName").Value.Replace("\n", "").Trim();
            ExpositionTableEventName = xDoc.Root.Element("Fields").Element("ExpositionTableEventName").Value.Replace("\n", "").Trim();
            ExpositionTableEventId = xDoc.Root.Element("Fields").Element("ExpositionTableEventId").Value.Replace("\n", "").Trim();
            EventNameTableNameRus = xDoc.Root.Element("Fields").Element("EventNameTableNameRus").Value.Replace("\n", "").Trim();
            AccountName = xDoc.Root.Element("Fields").Element("AccountName").Value.Replace("\n", "").Trim();
            AccountPhone = xDoc.Root.Element("Fields").Element("AccountPhone").Value.Replace("\n", "").Trim();
            ConactName = xDoc.Root.Element("Fields").Element("ConactName").Value.Replace("\n", "").Trim();
            ContactPhones = xDoc.Root.Element("Fields").Element("ContactPhones").Value.Replace("\n", "").Trim();
            ContactEmail = xDoc.Root.Element("Fields").Element("ContactEmail").Value.Replace("\n", "").Trim();
            TSFacility = xDoc.Root.Element("Fields").Element("TSFacility").Value.Replace("\n", "").Trim();
            EmployeeName = xDoc.Root.Element("Fields").Element("EmployeeName").Value.Replace("\n", "").Trim();
            CreatedOn = xDoc.Root.Element("Fields").Element("CreatedOn").Value.Replace("\n", "").Trim();
            AutocadXMLData = xDoc.Root.Element("Fields").Element("AutocadXMLData").Value.Replace("\n", "").Trim();
            AutocadFileData = xDoc.Root.Element("Fields").Element("AutocadFileData").Value.Replace("\n", "").Trim();
        }

    }
}
