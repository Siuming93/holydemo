using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using UnityEngine;

namespace Monster.BaseSystem
{
    public class BundleTool
    {
    }

    [Serializable]
    public class AssetRefrenceNode
    {
        public List<string> depenceOnMe = new List<string>();
        public List<string> depence = new List<string>();
        public string path;
        public string groupName;
        public List<string> incluedDepReference = new List<string>();
    }

    public class DictionaryStrategy<TKey, TValue> : PocoJsonSerializerStrategy
    {
        public override object DeserializeObject(object value, Type type)
        {
            IDictionary<TKey, TValue> map = new Dictionary<TKey, TValue>();
            IList<object> valueAsList = value as IList<object>;
            if (valueAsList != null)
            {
                foreach (object o in valueAsList)
                {
                    map.Add(DeserializedObject<TKey>("Key", o), DeserializedObject<TValue>("Value", o));
                }
            }
            return map;
        }

        public T DeserializedObject<T>(string key, object obj)
        {
            JsonObject jsonObject = obj as JsonObject;;
            var content = jsonObject[key].ToString();
            return SimpleJson.SimpleJson.DeserializeObject<T>(content);
        }
    }
}
