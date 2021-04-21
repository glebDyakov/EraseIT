
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CyberCradle
{
	public class Tools
	{
		public static void GetFolders(List<DirectoryInfo> arr, DirectoryInfo di)
		{
			DirectoryInfo[] dx = di.GetDirectories();
			foreach (DirectoryInfo dd in dx)
			{
				if ((dd.Attributes & (FileAttributes.System | FileAttributes.Hidden)) == 0)
				{
					arr.Add (dd);
				}
			}
		}

	    public static void GetFiles(List<FileInfo> arr, DirectoryInfo di)
	    {
	        DirectoryInfo[] dx = di.GetDirectories();
	        foreach (DirectoryInfo dd in dx)
	        {
	            if ((dd.Attributes & (FileAttributes.System | FileAttributes.Hidden)) == 0)
	                GetFiles(arr, dd);
	        }

	        FileInfo[] fi = di.GetFiles();
	        foreach (FileInfo ff in fi)
	        {
	            if ((ff.Attributes & (FileAttributes.Hidden | FileAttributes.Directory |
	                FileAttributes.System)) == 0)
	            {
	                if (ff.Extension != ".meta" && ff.Extension != ".asset")
	                {
	                    arr.Add(ff);
	                }
	            }
	        }
	    }
	}
}
