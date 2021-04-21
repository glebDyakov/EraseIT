using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel;


namespace CyberCradle
{

   public class ParseUtils
    {


       static public T Parse<T>( string key , Dictionary<string , object> dic , T defaultValue )
        {
            if (!dic.ContainsKey(key))
                return defaultValue;
            //Debug.LogError( "------------" + dic[ key ].GetType( ) + " >> " + defaultValue.GetType( ) ); ;

            return (T)dic[key];
        }

       static public T ParseEnum<T>( string key , Dictionary<string , object> dic , T defaultValue )
        {
            if (!dic.ContainsKey(key))
                return defaultValue;

            string valueString = ParseUtils.Parse<string>(key, dic, "");
            if (!string.IsNullOrEmpty(valueString))
            {
                T value = (T)Enum.Parse(typeof(T), valueString);
                return value;
            }
            
            return defaultValue;
        }

		static public List<T> ParseList<T>( string key , Dictionary<string , object> dic , List<T> defaultValue )
		{
			if (!dic.ContainsKey(key)) return defaultValue;
			
			List<object> list = dic[key] as List<object>;

			if (list == null) return defaultValue;

			List<T> result = new List<T>();
			foreach (T value in list)
			{
				result.Add(value);
			}

			return result;
		}
    }
}