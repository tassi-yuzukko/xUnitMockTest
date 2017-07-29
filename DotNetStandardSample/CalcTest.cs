using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetStandardSample
{
	public interface ICalcTest
	{
		int Sub(int a, int b);
	}

    public class CalcTest : ICalcTest
    {
		public virtual int Add(int a, int b)
		{
			return 100;
		}

		public int Sub(int a, int b)
		{
			throw new NotImplementedException();
		}
    }
}
