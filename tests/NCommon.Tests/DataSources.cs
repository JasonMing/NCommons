using System;
using System.Collections.Generic;
using System.Linq;

namespace NCommon
{
	static class DataSources
	{
		private static readonly IDictionary<Object, Object> Mapping = new Dictionary<Object, Object>();

		static DataSources()
		{
			Mapping.Add(PlaceHolder.EmptyCollection, Enumerable.Empty<Object>());
			Mapping.Add(PlaceHolder.BlankCollection, new Object[] { null, null });
			Mapping.Add(PlaceHolder.NonBlankCollection, new Object[] { 0, String.Empty });
		}

		public static IEnumerable<Object[]> GetData(params Object[] args)
		{
			yield return args.Select(arg => Mapping.ContainsKey(arg) ? Mapping[arg] : arg).ToArray();
		}

		public enum PlaceHolder
		{
			None,

			EmptyCollection,
			BlankCollection,
			NonBlankCollection,
		}
	}
}
