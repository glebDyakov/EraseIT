using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyberCradle
{
	public class MultiMap<TKey, TValue> : Dictionary<TKey, HashSet<TValue>>
	{
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
			}
			
			HashSet<TValue> container = null;
			if (!base.TryGetValue(key, out container))
			{
				container = new HashSet<TValue>();
				base.Add(key, container);
			}
			
			container.Add(value);
		}

		public bool ContainsValue(TKey key, TValue value)
		{
			HashSet<TValue> values = null;
			var toReturn = false;
			
			if (base.TryGetValue(key, out values))
			{
				toReturn = values.Contains(value);
			}
			
			return toReturn;
		}

		public void Remove(TKey key, TValue value)
		{
			if (key == null)
			{
			}
			
			HashSet<TValue> container = null;
			
			if (base.TryGetValue(key, out container))
			{
				container.Remove(value);
				if (container.Count <= 0)
				{
					base.Remove(key);
				}
			}
		}

		public void Merge(MultiMap<TKey, TValue> toMergeWith)
		{
			if (toMergeWith == null)
			{
				return;
			}
			
			foreach (var pair in toMergeWith)
			{
				foreach (TValue value in pair.Value)
				{
					this.Add(pair.Key, value);
				}
			}
		}

		public HashSet<TValue> GetValues(TKey key, bool returnEmptySet)
		{
			HashSet<TValue> toReturn = null;
			
			if (!base.TryGetValue(key, out toReturn) && returnEmptySet)
			{
				toReturn = new HashSet<TValue>();
			}
			
			return toReturn;
		}

		public MultiMap(IEqualityComparer<TKey> comparer)
			: base(comparer)
		{
		}

		public MultiMap()
		{
		}
	}
}