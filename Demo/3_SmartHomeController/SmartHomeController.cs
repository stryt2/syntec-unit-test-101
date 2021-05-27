using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo._3_SmartHomeController
{
	class SmartHomeController
	{
		public DateTime LastMotionTime
		{
			get;
			private set;
		}

		public void ActuateLights( bool motionDetected )
		{
			DateTime time = DateTime.Now; // Ouch!

			// Update the time of last motion.
			if( motionDetected ) {
				LastMotionTime = time;
			}

			// If motion was detected in the evening or at night, turn the light on.
			string timeOfDay = DateTimeUtl.GetTimeOfDay();
			if( motionDetected && ( timeOfDay == "Evening" || timeOfDay == "Night" ) ) {
				BackyardLightSwitcher.Instance.TurnOn();
			}
			// If no motion is detected for one minute, or if it is morning or day, turn the light off.
			else if( time.Subtract( LastMotionTime ) > TimeSpan.FromMinutes( 1 ) || ( timeOfDay == "Morning" || timeOfDay == "Noon" ) ) {
				BackyardLightSwitcher.Instance.TurnOff();
			}
		}
	}
}
