using FluentAssertions;
using FluentAssertions.Primitives;

namespace NUnit.Samples.Syntax
{
	public class CustomerAssertions :
	ReferenceTypeAssertions<Customer, CustomerAssertions>
	{
		private readonly Customer customer;

		public CustomerAssertions( Customer customer )
		{
			this.customer = customer;
		}

		protected override string Identifier => nameof(CustomerAssertions);

		[CustomAssertion]
		public AndConstraint<CustomerAssertions> BeActive( string because = "", params object[] becauseArgs )
		{
			customer.Active.Should().BeTrue( because, becauseArgs );
			return new AndConstraint<CustomerAssertions>( this );
		}
	}
}
