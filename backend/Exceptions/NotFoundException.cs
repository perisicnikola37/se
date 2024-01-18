using System;

namespace Vega.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string message)
			: base(message)
		{
		}

		public static NotFoundException Create(string message)
		{
			return new NotFoundException(message);
		}

		public override string ToString()
		{
			return $"{{ \"errors\": {{ \"ExpenseGroupId\": [\"Expense group not found.\"] }} }}";
		}
	}
}
