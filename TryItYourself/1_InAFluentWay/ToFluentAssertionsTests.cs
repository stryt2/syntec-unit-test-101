using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TryItYourself._1_InAFluentWay
{
	class ToFluentAssertionsTests
	{
		[Test]
		public void IsNull()
		{
			object nada = null;

			//Assert.That( nada, Is.Null );
			nada.Should().BeNull();
		}

		[Test]
		public void IsFalse()
		{
			//Assert.That( 2 + 2 == 5, Is.False );
			( 2 + 2 == 5 ).Should().BeFalse();
		}

		[Test]
		public void EmptyCollectionTests()
		{
			//Assert.That( new bool[ 0 ], Is.Empty );
			( new bool[ 0 ] ).Should().BeEmpty();

			//Assert.That( new int[] { 1, 2, 3 }, Is.Not.Empty );
			( new int[] { 1, 2, 3 } ).Should().NotBeEmpty();
		}

		public void ExactTypeTests()
		{
			//Assert.That( "Hello", Is.TypeOf( typeof( string ) ) );
			( "Hello" ).Should().BeOfType<string>();

			//Assert.That( "Hello", Is.Not.TypeOf( typeof( int ) ) );
			( "Hello" ).Should().NotBeOfType<int>();
		}

		[Test]
		public void StartsWithTests()
		{
			string phrase = "Hello World!";
			string[] greetings = new string[] { "Hello!", "Hi!", "Hola!" };

			//Assert.That( phrase, Does.Not.StartWith( "Hi!" ) );
			phrase.Should().NotStartWith( "Hi!" );

			//Assert.That( phrase, Does.StartWith( "HeLLo" ).IgnoreCase );
			phrase.Should().StartWithEquivalent( "HeLLo" );

			//Assert.That( greetings, Is.All.StartsWith( "h" ).IgnoreCase );
			greetings
				.Where( g => g.StartsWith( "h", StringComparison.CurrentCultureIgnoreCase ) )
				.Should().BeEquivalentTo( greetings );
		}

		[Test]
		public void EqualityTestsWithTolerance()
		{
			//Assert.That( 4.99d, Is.EqualTo( 5.0d ).Within( 0.05d ) );
			( 4.99d ).Should().BeApproximately( 5.0d, 0.05d );

			//Assert.That( 4.0d, Is.Not.EqualTo( 5.0d ).Within( 0.5d ) );
			( 4.0d ).Should().NotBeApproximately( 5.0d, 0.5d );

			//Assert.That( 4.99f, Is.EqualTo( 5.0f ).Within( 0.05f ) );
			( 4.99f ).Should().BeApproximately( 5.0f, 0.05f );
		}

		[Test]
		public void ComparisonTests()
		{
			//Assert.That( 7, Is.GreaterThan( 3 ) );
			( 7 ).Should().BeGreaterThan( 3 );

			//Assert.That( 7, Is.AtLeast( 7 ) );
			( 7 ).Should().BeGreaterOrEqualTo( 7 );

			//Assert.That( 3, Is.LessThanOrEqualTo( 3 ) );
			( 3 ).Should().BeLessOrEqualTo( 3 );
		}

		[Test]
		public void CollectionContainsTests()
		{
			int[] iarray = new int[] { 1, 2, 3 };
			string[] sarray = new string[] { "a", "b", "c" };

			//Assert.That( iarray, Has.Member( 3 ) );
			iarray.Should().Contain( 3 );

			//Assert.That( sarray, Has.Member( "b" ) );
			sarray.Should().Contain( "b" );

			//Assert.That( iarray, Has.Some.EqualTo( 3 ) );
			iarray
				.Where( i => i.Equals( 3 ) )
				.Should().NotBeEmpty();
		}

		[Test]
		public void PropertyTests()
		{
			string[] array = { "abc", "bca", "xyz", "qrs" };
			string[] array2 = { "a", "ab", "abc" };
			ArrayList list = new ArrayList( array );

			//Assert.That( list, Has.Property( "Count" ) );
			list.GetType().GetProperties()
				.Select( p => p.Name )
				.Should().Contain( "Count" );

			//Assert.That( "Hello", Has.Length.EqualTo( 5 ) );
			( "Hello" ).Should().HaveLength( 5 );

			//Assert.That( array2, Has.No.Property( "Length" ).GreaterThan( 3 ) );
			array2.Should().HaveCountLessOrEqualTo( 3 );
		}

		[Test]
		public void ComplexTests()
		{
			//Assert.That( 7, Is.Not.Null & Is.Not.LessThan( 5 ) & Is.Not.GreaterThan( 10 ) );
			( 7 ).Should().NotBe( null )
				.And.BeGreaterOrEqualTo( 5 )
				.And.BeLessOrEqualTo( 10 );

			//Assert.That( 7, !Is.Null & !Is.LessThan( 5 ) & !Is.GreaterThan( 10 ) );
			( 7 ).Should().NotBe( null )
				.And.BeGreaterOrEqualTo( 5 )
				.And.BeLessOrEqualTo( 10 );
		}

		[Test]
		public void ExceptionTest_Message()
		{
			// arrange
			var ex = new Example();

			// act
			Action act = () => ex.Worktime( 15 );

			// assert
			//Assert.Throws<AngryException>( () => ex.Worktime( 15 ), "我森77!!" );
			act.Should().Throw<AngryException>()
				.WithMessage( "我森77!!" );
		}

		class Example
		{
			public string Worktime( int hours )
			{
				if( hours > 12 ) {
					throw new AngryException( "我森77!!" );
				}

				return "正常";
			}
		}

		public class AngryException : Exception
		{
			public AngryException( string message ) : base( message )
			{
			}
			//public override string Message
			//{
			//	get
			//	{
			//		return "AngryException";
			//	}
			//}
		}
	}
}
