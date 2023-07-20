using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonCustomUtility
{
    public class JsonParser:MonoBehaviour
    {
        public static string pathdefault = Application.persistentDataPath;
        public static string pathfull { get; set; }
        public int maxPathLength;
        public static void JsonParseAndLoad()
        {
           
        }
        public enum JsonParsingType { Default, List, DictionaryStrKey, DictionaryIntKey, DictionaryFloatKey }
        public class JsonParsingPrepare
        {
            
        }
        public class JsonEmptyTempleteCreate
        { 
        
        }
    }
   
}
