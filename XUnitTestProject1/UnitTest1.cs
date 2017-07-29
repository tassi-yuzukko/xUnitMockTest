using System.Collections.Generic;
using Xunit;
using DotNetStandardSample;
using Xunit.Abstractions;
using Xunit.Extensions;
using System;

namespace XUnitTestProject1
{
    public class UnitTest1 : IDisposable
	{

		private readonly ITestOutputHelper _output;
		int val;

		public UnitTest1(ITestOutputHelper output)
		{
			_output = output;
			val = 3;
			_output.WriteLine(val.ToString());
		}

		public void Dispose()
		{
			val = 0;
			_output.WriteLine(val.ToString());
		}

		[Fact]
        public void Test1()
        {
			TestedClass test = new TestedClass();

			Assert.Equal(test.Plus(1, 3), 4);
		}

		[Theory]
		[InlineData(1,2,"3")]
		[InlineData(13, 24, "37")]
		public void Test2(int a, int b, string str)
		{
			Assert.Equal(str, (a + b).ToString());

			_output.WriteLine("Test2 {0}", str);
		}

		[Theory, MemberData("MyTestData")]
		public void Test3(string x, string y, bool z)
		{
			Assert.Equal(x == y, z);
		}

		public static IEnumerable<object[]> MyTestData
		{
			get
			{
				yield return new object[] { "aaa", "aaa", true };
				yield return new object[] { "aaa", "bbb", false };
			}
		}

		[Fact]
		public void Test4()
		{
			Assert.ThrowsAny<ArgumentException>(() =>
			{
				throw new ArgumentNullException();
			});
		}
    }
}
