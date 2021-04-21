using System;

namespace CyberCradle
{
	public interface IPsdParser
	{
		string GetDisplayName ();
		PsdDocument Parse (string path);
	}
}

