using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

using FluentAssertions;
using FluentAssertions.Common;
using FluentAssertions.Extensions;

using Newtonsoft.Json.Linq;

using NUnit.Framework;

namespace AssertSyntax
{
	class FluentAssertionsSyntaxTests
	{
		#region Object
		[Test]
		public void Object_Basic()
		{
			object theObject = null;
			theObject.Should().BeNull( "because the value is null" );
			theObject.Should().NotBeNull();

			theObject = "whatever";
			theObject.Should().BeOfType<string>( "because a {0} is set", typeof( string ) );
			theObject.Should().BeOfType( typeof( string ), "because a {0} is set", typeof( string ) );
		}

		[Test]
		public void Chaining_By_Which()
		{
			string message = "Other Message";
			object someObject = new Exception( message );
			someObject.Should().BeOfType<Exception>().Which.Message.Should().Be( message );
		}

		[Test]
		public void Object_Equal_Same()
		{
			object theObject = "whatever";
			string otherObject = "whatever";
			theObject.Should().Be( otherObject, "because they have the same values" );
			theObject.Should().NotBe( otherObject );

			theObject = otherObject;
			theObject.Should().BeSameAs( otherObject );
			theObject.Should().NotBeSameAs( otherObject );
		}

		[Test]
		public void Object_General_Assertions()
		{
			var ex = new ArgumentException();
			ex.Should().BeAssignableTo<Exception>( "because it is an exception" );
			ex.Should().NotBeAssignableTo<DateTime>( "because it is an exception" );

			var dummy = new Object();
			dummy.Should().Match( d => ( d.ToString() == "System.Object" ) );
			dummy.Should().Match<string>( d => ( d == "System.Object" ) );
			dummy.Should().Match( ( string d ) => ( d == "System.Object" ) );
		}

		#endregion Object

		#region Nullable
		[Test]
		public void Nullable()
		{
			short? theShort = null;
			theShort.Should().NotHaveValue();
			theShort.Should().BeNull();
			theShort.Should().Match( x => !x.HasValue || x > 0 );

			int? theInt = 3;
			theInt.Should().HaveValue();
			theInt.Should().NotBeNull();

			DateTime? theDate = null;
			theDate.Should().NotHaveValue();
			theDate.Should().BeNull();
		}
		#endregion Nullable

		#region Boolean
		[Test]
		public void Boolean_Basic()
		{
			bool theBoolean = false;
			theBoolean.Should().BeFalse( "it's set to false" );

			bool otherBoolean = true;
			theBoolean = true;
			theBoolean.Should().BeTrue();
			theBoolean.Should().Be( otherBoolean );
		}

		[Test]
		public void Boolean_Nullable_NotNull()
		{
			bool? theBoolean = null;
			theBoolean.Should().NotBeFalse();
			theBoolean.Should().NotBeTrue();
		}
		#endregion Boolean
		#region Strings
		[Test]
		public void Strings_Basic()
		{
			string theString = "";
			theString.Should().NotBeNull();
			theString.Should().BeNull();
			theString.Should().BeEmpty();
			theString.Should().NotBeEmpty( "because the string is not empty" );
			theString.Should().HaveLength( 0 );
			theString.Should().BeNullOrWhiteSpace(); // either null, empty or whitespace only
			theString.Should().NotBeNullOrWhiteSpace();
		}

		[Test]
		public void Strings_Value()
		{
			string theString = "This is a String";
			theString.Should().Be( "This is a String" );
			theString.Should().NotBe( "This is another String" );
			theString.Should().BeEquivalentTo( "THIS IS A STRING" );
			theString.Should().NotBeEquivalentTo( "THIS IS ANOTHER STRING" );

			theString.Should().BeOneOf(
				"That is a String",
				"This is a String"
			);

			theString.Should().Contain( "is a" );
			theString.Should().Contain( "is a", Exactly.Once() );
			theString.Should().Contain( "is a", AtLeast.Twice() );
			theString.Should().Contain( "is a", MoreThan.Thrice() );
			theString.Should().Contain( "is a", AtMost.Times( 5 ) );
			theString.Should().Contain( "is a", LessThan.Twice() );
			theString.Should().ContainAll( "should", "contain", "all", "of", "these" );
			theString.Should().ContainAny( "any", "of", "these", "will", "do" );
			theString.Should().NotContain( "is a" );
			theString.Should().NotContainAll( "can", "contain", "some", "but", "not", "all" );
			theString.Should().NotContainAny( "can't", "contain", "any", "of", "these" );
			theString.Should().ContainEquivalentOf( "WE DONT CARE ABOUT THE CASING" );
			theString.Should().ContainEquivalentOf( "WE DONT CARE ABOUT THE CASING", Exactly.Once() );
			theString.Should().ContainEquivalentOf( "WE DONT CARE ABOUT THE CASING", AtLeast.Twice() );
			theString.Should().ContainEquivalentOf( "WE DONT CARE ABOUT THE CASING", MoreThan.Thrice() );
			theString.Should().ContainEquivalentOf( "WE DONT CARE ABOUT THE CASING", AtMost.Times( 5 ) );
			theString.Should().ContainEquivalentOf( "WE DONT CARE ABOUT THE CASING", LessThan.Twice() );
			theString.Should().NotContainEquivalentOf( "HeRe ThE CaSiNg Is IgNoReD As WeLl" );

			theString.Should().StartWith( "This" );
			theString.Should().NotStartWith( "This" );
			theString.Should().StartWithEquivalent( "this" );
			theString.Should().NotStartWithEquivalentOf( "this" );

			theString.Should().EndWith( "a String" );
			theString.Should().NotEndWith( "a String" );
			theString.Should().EndWithEquivalent( "a string" );
			theString.Should().NotEndWithEquivalentOf( "a string" );
		}

		[Test]
		public void Strings_Match()
		{
			string emailAddress = "syntec@syntec.com";
			string homeAddress = "syntec@gmail.com";
			emailAddress.Should().Match( "*@*.com" );
			homeAddress.Should().NotMatch( "*@*.com" );
			emailAddress.Should().MatchEquivalentOf( "*@*.COM" );
			emailAddress.Should().NotMatchEquivalentOf( "*@*.COM" );
		}

		[Test]
		public void Strings_Regex()
		{
			string someString = "hello world";
			someString.Should().MatchRegex( "h.*\\sworld.$" );
			someString.Should().NotMatchRegex( ".*earth.*" );
		}
		#endregion Strings

		#region Numeric
		[Test]
		public void Numeric_Basic()
		{
			int theInt = 5;
			theInt.Should().BeGreaterOrEqualTo( 5 );
			theInt.Should().BeGreaterOrEqualTo( 3 );
			theInt.Should().BeGreaterThan( 4 );
			theInt.Should().BeLessOrEqualTo( 5 );
			theInt.Should().BeLessThan( 6 );
			theInt.Should().BePositive();
			theInt.Should().Be( 5 );
			theInt.Should().NotBe( 10 );
			theInt.Should().BeInRange( 1, 10 );
			theInt.Should().NotBeInRange( 6, 10 );
			theInt.Should().Match( x => x % 2 == 1 );

			theInt = 0;
			//theInt.Should().BePositive(); => Expected positive value, but found 0
			//theInt.Should().BeNegative(); => Expected negative value, but found 0

			theInt = -8;
			theInt.Should().BeNegative();
			int? nullableInt = 3;
			nullableInt.Should().Be( 3 );

			double theDouble = 5.1;
			theDouble.Should().BeGreaterThan( 5 );
			byte theByte = 2;
			theByte.Should().Be( 2 );
		}

		[Test]
		public void Numeric_Inaccurate()
		{
			float value = 3.1415927F;
			value.Should().BeApproximately( 3.14F, 0.01F );
			value.Should().NotBeApproximately( 2.5F, 0.5F );
			value.Should().BeInRange( 3.14F, 3.15F );
		}
		#endregion Numeric

		#region Dates & Times
		[Test]
		public void Dates()
		{
			var theDatetime = 1.March( 2010 ).At( 22, 15 ).AsLocal();

			theDatetime.Should().Be( 1.March( 2010 ).At( 22, 15 ) );
			theDatetime.Should().BeAfter( 1.February( 2010 ) );
			theDatetime.Should().BeBefore( 2.March( 2010 ) );
			theDatetime.Should().BeOnOrAfter( 1.March( 2010 ) );
			theDatetime.Should().BeOnOrBefore( 1.March( 2010 ) );
			theDatetime.Should().BeSameDateAs( 1.March( 2010 ).At( 22, 16 ) );
			theDatetime.Should().BeIn( DateTimeKind.Local );

			theDatetime.Should().NotBe( 1.March( 2010 ).At( 22, 16 ) );
			theDatetime.Should().NotBeAfter( 2.March( 2010 ) );
			theDatetime.Should().NotBeBefore( 1.February( 2010 ) );
			theDatetime.Should().NotBeOnOrAfter( 2.March( 2010 ) );
			theDatetime.Should().NotBeOnOrBefore( 1.February( 2010 ) );
			theDatetime.Should().NotBeSameDateAs( 2.March( 2010 ) );

			theDatetime.Should().BeOneOf(
				1.March( 2010 ).At( 21, 15 ),
				1.March( 2010 ).At( 22, 15 ),
				1.March( 2010 ).At( 23, 15 )
			);

			theDatetime.Should().HaveDay( 1 );
			theDatetime.Should().HaveMonth( 3 );
			theDatetime.Should().HaveYear( 2010 );
			theDatetime.Should().HaveHour( 22 );
			theDatetime.Should().HaveMinute( 15 );
			theDatetime.Should().HaveSecond( 0 );

			theDatetime.Should().NotHaveDay( 2 );
			theDatetime.Should().NotHaveMonth( 4 );
			theDatetime.Should().NotHaveYear( 2011 );
			theDatetime.Should().NotHaveHour( 23 );
			theDatetime.Should().NotHaveMinute( 16 );
			theDatetime.Should().NotHaveSecond( 1 );

			theDatetime.Should().BeCloseTo( 1.March( 2010 ).At( 22, 15 ), 2000 ); // 2000 milliseconds
			theDatetime.Should().BeCloseTo( 1.March( 2010 ).At( 22, 15 ) );       // default is 20 milliseconds
			theDatetime.Should().BeCloseTo( 1.March( 2010 ).At( 22, 15 ), 2.Seconds() );

			theDatetime.Should().NotBeCloseTo( 2.March( 2010 ), 1.Hours() );
		}

		[Test]
		public void Timespans()
		{
			var timeSpan = new TimeSpan( 12, 59, 59 );
			var someOtherTimeSpan = new TimeSpan( 12, 12, 12 );
			timeSpan.Should().BePositive();
			timeSpan.Should().BeNegative();
			timeSpan.Should().Be( 12.Hours() );
			timeSpan.Should().NotBe( 1.Days() );
			timeSpan.Should().BeLessThan( someOtherTimeSpan );
			timeSpan.Should().BeLessOrEqualTo( someOtherTimeSpan );
			timeSpan.Should().BeGreaterThan( someOtherTimeSpan );
			timeSpan.Should().BeGreaterOrEqualTo( someOtherTimeSpan );
		}
		#endregion Dates & Times

		#region Collection
		[Test]
		public void Collections_Basic()
		{
			IEnumerable collection = new[] { 1, 2, 5, 8 };

			collection.Should().NotBeEmpty()
				.And.HaveCount( 4 )
				.And.ContainInOrder( new[] { 2, 5 } )
				.And.ContainItemsAssignableTo<int>();

			collection.Should().Equal( new List<int> { 1, 2, 5, 8 } );
			collection.Should().Equal( 1, 2, 5, 8 );
			collection.Should().BeEquivalentTo( 8, 2, 1, 5 );
			collection.Should().NotBeEquivalentTo( new[] { 8, 2, 3, 5 } );

			collection.Should().HaveCount( c => c > 3 )
			  .And.OnlyHaveUniqueItems();

			collection.Should().HaveCountGreaterThan( 3 );
			collection.Should().HaveCountGreaterOrEqualTo( 4 );
			collection.Should().HaveCountLessOrEqualTo( 4 );
			collection.Should().HaveCountLessThan( 5 );
			collection.Should().NotHaveCount( 3 );
			collection.Should().HaveSameCount( new[] { 6, 2, 0, 5 } );
			collection.Should().NotHaveSameCount( new[] { 6, 2, 0 } );

			collection.Should().StartWith( 1 );
			collection.Should().StartWith( new[] { 1, 2 } );
			collection.Should().EndWith( 8 );
			collection.Should().EndWith( new[] { 5, 8 } );

			collection.Should().BeSubsetOf( new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, } );

			collection.Should().Contain( 8 )
			  .And.HaveElementAt( 2, 5 )
			  .And.NotBeSubsetOf( new[] { 11, 56 } );

			collection.Should().Contain( collection, "", 5, 6 ); // It should contain the original items, plus 5 and 6.

			collection.Should().ContainItemsAssignableTo<int>();

			collection.Should().ContainInOrder( new[] { 1, 5, 8 } );

			collection.Should().NotContain( 82 );
			collection.Should().NotContain( new[] { 82, 83 } );
			collection.Should().NotContainNulls();

			object boxedValue = 2;
			collection.Should().ContainEquivalentOf( boxedValue ); // Compared by object equivalence

			collection.Should().BeEmpty();
			collection.Should().BeNullOrEmpty();
			collection.Should().NotBeNullOrEmpty();

			IEnumerable otherCollection = new[] { 1, 2, 5, 8, 1 };
			IEnumerable anotherCollection = new[] { 10, 20, 50, 80, 10 };
			collection.Should().IntersectWith( otherCollection );
			collection.Should().NotIntersectWith( anotherCollection );

			collection.Should().BeInAscendingOrder();
			collection.Should().BeInDescendingOrder();
			collection.Should().NotBeInAscendingOrder();
			collection.Should().NotBeInDescendingOrder();

			IEnumerable<string> stringCollection = new[] { "build succeded", "test failed" };
			stringCollection.Should().ContainMatch( "* failed" );
		}

		[Test]
		public void Collection_Preceding_Succeeding()
		{
			IEnumerable collection = new[] { 1, 2, 5, 8 };
			const int successor = 5;
			const int predecessor = 5;
			collection.Should().HaveElementPreceding( successor, 2 );
			collection.Should().HaveElementSucceeding( predecessor, 8 );

		}
		#endregion Collection

		#region Dictionary
		[Test]
		public void Dictionary()
		{
			Dictionary<int, string> dictionary = null;
			dictionary.Should().BeNull();

			dictionary = new Dictionary<int, string>();
			dictionary.Should().NotBeNull();
			dictionary.Should().BeEmpty();
			dictionary.Add( 1, "first element" );
			dictionary.Should().NotBeEmpty();

			dictionary.Should().ContainKey( 1 );
			dictionary.Should().ContainKeys( 1, 2 );
			dictionary.Should().NotContainKey( 9 );
			dictionary.Should().NotContainKeys( 9, 10 );
			dictionary.Should().ContainValue( "One" );
			dictionary.Should().ContainValues( "One", "Two" );
			dictionary.Should().NotContainValue( "Nine" );
			dictionary.Should().NotContainValues( "Nine", "Ten" );

			var dictionary1 = new Dictionary<int, string>
{
	{ 1, "One" },
	{ 2, "Two" }
};

			var dictionary2 = new Dictionary<int, string>
{
	{ 1, "One" },
	{ 2, "Two" }
};

			var dictionary3 = new Dictionary<int, string>
{
	{ 3, "Three" },
};

			dictionary1.Should().Equal( dictionary2 );
			dictionary1.Should().NotEqual( dictionary3 );

			dictionary.Should().HaveCount( 2 );
			dictionary.Should().NotHaveCount( 3 );

			dictionary1.Should().HaveSameCount( dictionary2 );
			dictionary1.Should().NotHaveSameCount( dictionary3 );

			dictionary1.Should().HaveSameCount( dictionary2.Keys );
			dictionary1.Should().NotHaveSameCount( dictionary3.Keys );

			KeyValuePair<int, string> item1 = new KeyValuePair<int, string>( 1, "One" );
			KeyValuePair<int, string> item2 = new KeyValuePair<int, string>( 2, "Two" );

			dictionary.Should().Contain( item1 );
			dictionary.Should().Contain( item1, item2 );
			dictionary.Should().Contain( 2, "Two" );
			dictionary.Should().NotContain( item1 );
			dictionary.Should().NotContain( item1, item2 );
			dictionary.Should().NotContain( 9, "Nine" );


		}
		#endregion Dictionary

		#region Enum
		enum MyEnum
		{
			One, Two, Three
		}

		[Test]
		public void Enum()
		{
			MyEnum myEnum = MyEnum.One;
			myEnum.Should().Be( MyEnum.One );
		}
		#endregion Enum

		#region GUID
		[Test]
		public void GUID()
		{
			Guid theGuid = Guid.NewGuid();
			Guid sameGuid = theGuid;
			Guid otherGuid = Guid.NewGuid();

			theGuid.Should().Be( sameGuid );
			theGuid.Should().NotBe( otherGuid );
			theGuid.Should().NotBeEmpty();

			Guid.Empty.Should().BeEmpty();
		}
		#endregion GUID

		#region Exceptions
		[Test]
		public void Exception()
		{
			var subject = new exp();
			Action act = () => subject.Foo2( "Hello" );

			act.Should().Throw<InvalidOperationException>()
				.WithInnerException<ArgumentException>()
				.WithMessage( "whatever" );
			act.Should().NotThrowAfter( 10.Seconds(), 100.Milliseconds() );

			Action act2 = () => subject.Foo( null );
			act2.Should().Throw<ArgumentNullException>().Where( e => e.Message.StartsWith( "did" ) );
		}
		class exp
		{
			public void Foo( object obj )
			{
				throw new InvalidOperationException();
			}

			public void Foo2(string str)
			{
				throw new InvalidOperationException();
			}
		}
		#endregion Exceptions

		#region Execution Time
		public class SomePotentiallyVerySlowClass
		{
			public void ExpensiveMethod()
			{
				for( short i = 0; i < short.MaxValue; i++ ) {
					string tmp = " ";
					if( !string.IsNullOrEmpty( tmp ) ) {
						tmp += " ";
					}
				}
			}
		}
		[Test]
		public void Execution_Time()
		{
			var subject = new SomePotentiallyVerySlowClass();
			subject.ExecutionTimeOf( s => s.ExpensiveMethod() ).Should().BeLessOrEqualTo( 500.Milliseconds() );

			Action someAction = () => Thread.Sleep( 100 );
			someAction.ExecutionTime().Should().BeLessOrEqualTo( 200.Milliseconds() );

			someAction.ExecutionTime().Should().BeLessOrEqualTo( 200.Milliseconds() );
			someAction.ExecutionTime().Should().BeLessThan( 200.Milliseconds() );
			someAction.ExecutionTime().Should().BeGreaterThan( 100.Milliseconds() );
			someAction.ExecutionTime().Should().BeGreaterOrEqualTo( 100.Milliseconds() );
			someAction.ExecutionTime().Should().BeCloseTo( 150.Milliseconds(), 50.Milliseconds() );
		}

		[Test]
		public async void Tasks()
		{
			Func<Task> someAsyncWork = () => SomethingReturningATask();
			someAsyncWork.Should().CompleteWithin( 100.Milliseconds() );
			await someAsyncWork.Should().CompleteWithinAsync( 100.Milliseconds() );

			Func<Task<int>> someAsyncFunc = null;
			someAsyncFunc.Should().CompleteWithin( 100.Milliseconds() ).Which.Should().Be( 42 );
		}

		private Task SomethingReturningATask()
		{
			throw new NotImplementedException();
		}
		#endregion Execution Time
	}
}
