using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using DotNetStandardSample;
using Xunit.Abstractions;

namespace XUnitTestProject1
{
    public class MoqTest
	{
		private readonly ITestOutputHelper _output;

		public MoqTest(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact(DisplayName = "引数をすべて指定するモック")]
		public void TestStubExactMatch()
		{
			// Calcクラスのモック作成
			var calcMock = new Mock<CalcTest>();

			// Addメソッドに、引数１，２を与えられると３を返すように設定する
			calcMock.Setup(m => m.Add(1, 2)).Returns(3);

			// Clacクラスのモックされたインスタンスを取り出す
			CalcTest c = calcMock.Object;

			Assert.Equal(3, c.Add(1, 2));
			// Assert.Equal(3, c.Add(2, 1));	
		}

		[Fact(DisplayName = "引数はなんでもよくても動くモック => It.IsAny句")]
		public void TestStubExactMatch2()
		{
			// Calcクラスのモック作成
			var calcMock = new Mock<ICalcTest>();

			// Subメソッドに、引数はなんでもよく，２を与えられると３を返すように設定する
			calcMock.Setup(m => m.Sub(It.IsAny<int>(), 2)).Returns(3);

			// Clacクラスのモックされたインスタンスを取り出す
			ICalcTest c = calcMock.Object;

			Assert.Equal(3, c.Sub(1, 2));
			// Assert.Equal(3, c.Add(2, 1));	// 設定されていないので、例外を返す
		}

		[Fact(DisplayName = "引数以外の条件を使用できるモック => When句")]
		public void TestStubExactMatch3()
		{
			int count = 0;
			var mock = new Mock<CalcTest>();

			// countが0の時は引数の値に関係なく、Addメソッドは０を返す
			mock.When(() => count.Equals(0))
				.Setup(m => m.Add(It.IsAny<int>(), It.IsAny<int>()))
				.Returns(0);

			// countが0以外の場合は引数の値に関係なく、Addメソッドは１を返す
			mock.When(() => !count.Equals(0))
				.Setup(m => m.Add(It.IsAny<int>(), It.IsAny<int>()))
				.Returns(1);

			CalcTest c = mock.Object;

			count = 0;
			Assert.Equal(0, c.Add(3333, 44));

			count = 11111110;
			Assert.Equal(1, c.Add(3333, 44));
		}

		[Theory(DisplayName = "WhenとInlineDataの合わせ技")]
		[InlineData(0,0)]
		[InlineData(11, 1)]
		[InlineData(-10, 1)]
		[InlineData(-11, 1)]
		public void TestStubExactMatch4(int count, int expected)
		{
			var mock = new Mock<CalcTest>();

			// countが0の時は引数の値に関係なく、Addメソッドは０を返す
			mock.When(() => count.Equals(0))
				.Setup(m => m.Add(It.IsAny<int>(), It.IsAny<int>()))
				.Returns(0);

			// countが0以外の場合は引数の値に関係なく、Addメソッドは１を返す
			mock.When(() => !count.Equals(0))
				.Setup(m => m.Add(It.IsAny<int>(), It.IsAny<int>()))
				.Returns(1);

			Assert.Equal(expected, mock.Object.Add(222453, 44421));
		}

		[Fact(DisplayName = "例外を呼び出すモック")]
		public void TestStubExactMatch5()
		{
			var mock = new Mock<CalcTest>();

			mock.Setup(m => m.Add(0, 0)).Throws(new ArgumentException());

			Assert.ThrowsAny<ArgumentException>(() =>
			{
				mock.Object.Add(0, 0);
			});
		}

		[Theory(DisplayName = "Callbackの使用")]
		[InlineData(0, 0)]
		[InlineData(11, 1)]
		[InlineData(-10, 1)]
		[InlineData(-11, 1)]
		public void TestStubExactMatch6(int a, int b)
		{
			var mock = new Mock<CalcTest>();

			// countが0の時は引数の値に関係なく、Addメソッドは０を返す
			mock.Setup(m => m.Add(a, b))
				.Returns(a + b)
				.Callback<int, int>((x, y) => _output.WriteLine("{0}+{1}", x, y));

			Assert.Equal(a+b, mock.Object.Add(a, b));
		}
	}
}
