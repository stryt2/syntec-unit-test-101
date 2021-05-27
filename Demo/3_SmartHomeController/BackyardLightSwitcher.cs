using System;

namespace Demo._3_SmartHomeController
{
#pragma warning disable S3453 // Classes should not have only "private" constructors
	internal class BackyardLightSwitcher
#pragma warning restore S3453 // Classes should not have only "private" constructors
	{
		public static BackyardLightSwitcher Instance => m_singleton;

		private static readonly BackyardLightSwitcher m_singleton = new();

		private BackyardLightSwitcher()
		{
		}

		internal void TurnOn()
		{
		}

		internal void TurnOff()
		{
		}
	}
}
