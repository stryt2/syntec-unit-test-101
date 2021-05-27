using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace TryItYourself._2_HappyHoliday
{
    class HolidayTests
    {
        [Test]
        public void today_is_xmas()
        {
            // Given Today = 12, 25
            // When Holiday SayHello
            // Then Response Should Be "Merry Xmas"
            Assert.Fail();
        }

        [Test]
        public void today_is_xmas_when_12_24()
        {
            // Given Today = 12, 24
            // When Holiday SayHello
            // Then Response Should Be "Merry Xmas"
            Assert.Fail();
        }

        [Test]
        public void today_is_not_xmas()
        {
            // Given Today = 11, 25
            // When Holiday SayHello
            // Then Response Should Be "Today is not Xmas"
            Assert.Fail();
        }
    }
}
