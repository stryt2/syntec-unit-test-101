using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace TryItYourself._1_InAFluentWay
{
	class ToFluentAssertionsTests
	{
		[Test]
		public void IsNull()
		{
			object nada = null;

			Assert.That( nada, Is.Null );
		}

		[Test]
		public void IsFalse()
		{
			Assert.That( 2 + 2 == 5, Is.False );
		}

		[Test]
		public void EmptyCollectionTests()
		{
			Assert.That( new bool[ 0 ], Is.Empty );
			Assert.That( new int[] { 1, 2, 3 }, Is.Not.Empty );
		}

		public void ExactTypeTests()
		{
			Assert.That( "Hello", Is.TypeOf( typeof( string ) ) );
			Assert.That( "Hello", Is.Not.TypeOf( typeof( int ) ) );
		}

		[Test]
		public void StartsWithTests()
		{
			string phrase = "Hello World!";
			string[] greetings = new string[] { "Hello!", "Hi!", "Hola!" };

			Assert.That( phrase, Does.Not.StartWith( "Hi!" ) );
			Assert.That( phrase, Does.StartWith( "HeLLo" ).IgnoreCase );
			Assert.That( greetings, Is.All.StartsWith( "h" ).IgnoreCase );
		}

		[Test]
		public void EqualityTestsWithTolerance()
		{
			Assert.That( 4.99d, Is.EqualTo( 5.0d ).Within( 0.05d ) );
			Assert.That( 4.0d, Is.Not.EqualTo( 5.0d ).Within( 0.5d ) );
			Assert.That( 4.99f, Is.EqualTo( 5.0f ).Within( 0.05f ) );
		}

		[Test]
		public void ComparisonTests()
		{
			Assert.That( 7, Is.GreaterThan( 3 ) );
			Assert.That( 7, Is.AtLeast( 7 ) );
			Assert.That( 3, Is.LessThanOrEqualTo( 3 ) );
		}

		[Test]
		public void CollectionContainsTests()
		{
			int[] iarray = new int[] { 1, 2, 3 };
			string[] sarray = new string[] { "a", "b", "c" };

			Assert.That( iarray, Has.Member( 3 ) );
			Assert.That( sarray, Has.Member( "b" ) );
			Assert.That( iarray, Has.Some.EqualTo( 3 ) );
		}

		[Test]
		public void PropertyTests()
		{
			string[] array = { "abc", "bca", "xyz", "qrs" };
			string[] array2 = { "a", "ab", "abc" };
			ArrayList list = new ArrayList( array );

			Assert.That( list, Has.Property( "Count" ) );
			Assert.That( "Hello", Has.Length.EqualTo( 5 ) );
			Assert.That( array2, Has.No.Property( "Length" ).GreaterThan( 3 ) );
		}

		[Test]
		public void ComplexTests()
		{
			Assert.That( 7, Is.Not.Null & Is.Not.LessThan( 5 ) & Is.Not.GreaterThan( 10 ) );
			Assert.That( 7, !Is.Null & !Is.LessThan( 5 ) & !Is.GreaterThan( 10 ) );
		}

		[Test]
		public void ExceptionTest_Message()
		{
			var ex = new Example();
			Assert.Throws<AngryException>( () => ex.Worktime( 15 ), "我森77!!" );
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
			public override string Message
			{
				get
				{
					return "AngryException";
				}
			}
		}
	}
}
