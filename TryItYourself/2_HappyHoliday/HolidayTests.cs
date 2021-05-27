using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace TryItYourself._2_HappyHoliday
{
	class HolidayTests
	{
		const string EXPECTED_Merry_Xmas = "Merry Xmas";
		const string EXPECTED_Not_Xmas = "Today is not Xmas";

		[Test]
		public void today_is_xmas()
		{
			// arrange - Given Today = 12, 25
			var xmasProviderStub = Mock.Of<IDateTimeProvider>( p => p.GetToday() == new DateTime( 2021, 12, 25 ) );
			var holiday = new Holiday( xmasProviderStub );

			// act - When Holiday SayHello
			var greeting = holiday.SayHello();

			// assert - Then Response Should Be "Merry Xmas"
			greeting.Should().Be( EXPECTED_Merry_Xmas );
		}

		[Test]
		public void today_is_xmas_when_12_24()
		{
			// arrange - Given Today = 12, 24
			var xmasEveProviderStub = Mock.Of<IDateTimeProvider>( p => p.GetToday() == new DateTime( 2021, 12, 24 ) );
			var holiday = new Holiday( xmasEveProviderStub );

			// act - When Holiday SayHello
			var greeting = holiday.SayHello();

			// assert - Then Response Should Be "Merry Xmas"
			greeting.Should().Be( EXPECTED_Merry_Xmas );
		}

		[Test]
		public void today_is_not_xmas()
		{
			// arrange - Given Today = 11, 25
			var definitelyNotXmasProvider = Mock.Of<IDateTimeProvider>( p => p.GetToday() == new DateTime( 2021, 11, 25 ) );
			var holiday = new Holiday( definitelyNotXmasProvider );

			// act - When Holiday SayHello
			var greeting = holiday.SayHello();

			// assert - Then Response Should Be "Today is not Xmas"
			greeting.Should().Be( EXPECTED_Not_Xmas );
		}
	}
}
