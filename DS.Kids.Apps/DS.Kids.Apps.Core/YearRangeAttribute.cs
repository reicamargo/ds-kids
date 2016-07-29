using System;

using BRFX.Core.Validation;

namespace DS.Kids.Apps.Core
{

	public class YearRangeAttribute : RangeAttribute
	{

		public YearRangeAttribute(int lowerBound, int upperBound, string validationMessage = "The value is not between minimum and maximum allowed value.")
			: base(typeof(DateTime),
				DateTime.Now.AddYears(lowerBound).ToString("d"),
				DateTime.Now.AddYears(upperBound).ToString("d"), validationMessage)
		{
		}

	}

}
