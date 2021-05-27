using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Demo._3_SmartHomeController
{
	class SmartHomeControllerTests
	{
		static DateTime SampleDate = new( 2015, 12, 31, 23, 59, 59 );

		[Test]
		public void ActuateLights_MotionDetected_SavesTimeOfMotion()
		{
			// Arrange

			// Act

			// Assert
		}

		[Test]
		public void ActuateLights_MotionDetectedAtNight_TurnsOnTheLight()
		{
			// Arrange: create a pair of actions that change boolean variable
			// instead of really turning the light on or off.

			// Act

			// Assert
		}
	}
}
