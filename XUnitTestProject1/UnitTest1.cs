using System;
using Xunit;
using DotNetStandardSample;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
		[Fact]
        public void Test1()
        {
			TestedClass test = new TestedClass();

			Assert.Equal(test.Plus(1, 3), 4);
		}
    }
}
