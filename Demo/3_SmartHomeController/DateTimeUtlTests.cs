using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Demo._3_SmartHomeController
{
	class DateTimeUtlTests
	{
		[Test]
		public void GetTimeOfDay_For6AM_ReturnsMorning()
		{
			// Arrange
			DateTime sampleDate = new( 2015, 12, 31, 06, 00, 00 );

			// Act
			string timeOfDay = DateTimeUtl.GetTimeOfDay();

			// Assert
			Assert.AreEqual( "Morning", timeOfDay );
		}
	}
}
