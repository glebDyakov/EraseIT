using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class StringUtils
{
	public static string UppercaseFirst (string s)
	{
		if (string.IsNullOrEmpty (s)) {
			return string.Empty;
		}
		char[] a = s.ToCharArray ();
		a [0] = char.ToUpper (a [0]);
		return new string (a);
	}

	public static string Reverse(string s)
	{
		if (string.IsNullOrEmpty (s)) {
			return string.Empty;
		}

		char[] array = s.ToCharArray();
		Array.Reverse(array);

		return new string(array);
	}

	public static string RemoveFirst (string s)
	{
		if (string.IsNullOrEmpty (s)) {
			return string.Empty;
		}

		return s.Substring(1);
	}
	
    public static bool CompareWithPattern(string pattern, string text)
    {
		bool caseSensitive = false;
        pattern = pattern.Replace(".", @"\.");
        pattern = pattern.Replace("?", ".");
        pattern = pattern.Replace("*", ".*?");
        pattern = pattern.Replace(@"\", @"\\"); 
        pattern = pattern.Replace(" ", @"\s");
        return new Regex(pattern, caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase).IsMatch(text);
    }

	public static string ReplaceWithPattern(string pattern, string replace, string text)
	{
		bool caseSensitive = false;
		pattern = pattern.Replace(".", @"\.");
		pattern = pattern.Replace("?", ".");
		pattern = pattern.Replace("*", ".*?");
		pattern = pattern.Replace(@"\", @"\\");
		pattern = pattern.Replace(" ", @"\s");
		return new Regex(pattern, caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase).Replace(text, replace);
	}
}

