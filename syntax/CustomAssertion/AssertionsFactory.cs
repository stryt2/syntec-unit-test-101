namespace NUnit.Samples.Syntax
{
	public static class AssertionsFactory
	{
		public static CustomerAssertions Should( this Customer instance )
		{
			return new CustomerAssertions( instance );
		}
	}
}
