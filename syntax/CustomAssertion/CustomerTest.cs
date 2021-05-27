using FluentAssertions;

using NUnit.Framework;

namespace NUnit.Samples.Syntax
{
	public class CustomerTest
	{
		[Test]
		public void Customer_Status_BeActive()
		{
			Customer myClient = new();
			myClient.Should().BeActive( "because we don't work with old clients" );
		}
	}
}
