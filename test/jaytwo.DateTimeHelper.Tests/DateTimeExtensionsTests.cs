using System;
using System.Collections.Generic;
using Xunit;

namespace jaytwo.DateTimeHelper.Tests
{
    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData("2020-05-15", 0, "2020-05-15")] // friday
        [InlineData("2020-05-15", 1, "2020-05-18")]
        [InlineData("2020-05-15", 2, "2020-05-19")]
        [InlineData("2020-05-15", 3, "2020-05-20")]
        [InlineData("2020-05-15", 4, "2020-05-21")]
        [InlineData("2020-05-15", 5, "2020-05-22")]
        [InlineData("2020-05-15", 10, "2020-05-29")]
        [InlineData("2020-05-15", -5, "2020-05-08")]
        [InlineData("2020-05-15", -10, "2020-05-01")]
        public void AddWeekdays(string startDateString, int weekdays, string expectedDateString)
        {
            // arrange
            var startDate = DateTime.Parse(startDateString);
            var expectedDate = DateTime.Parse(expectedDateString);

            // act
            var actual = startDate.AddWeekdays(weekdays);

            // assert
            Assert.Equal(expectedDate, actual);
        }

        [Fact]
        public void AsLocal()
        {
            // arrange
            var time = new DateTime(2014, 1, 1, 12, 23, 34, 45, DateTimeKind.Unspecified);
            var expected = new DateTime(2014, 1, 1, 12, 23, 34, 45, DateTimeKind.Local);

            // act
            var actual = time.AsLocal();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AsUtc()
        {
            // arrange
            var time = new DateTime(2014, 1, 1, 12, 23, 34, 45, DateTimeKind.Unspecified);
            var expected = new DateTime(2014, 1, 1, 12, 23, 34, 45, DateTimeKind.Utc);

            // act
            var actual = time.AsUtc();

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2020-05-20", "2020-05-20", true)]
        [InlineData("2020-05-20", "2020-05-19", false)]
        [InlineData("2020-05-19", "2020-05-20", false)]
        [InlineData("2020-05-20 00:00:00", "2020-05-20 00:00:01", true)]
        [InlineData("2020-05-20 00:00:01", "2020-05-20 00:00:00", true)]
        [InlineData("2020-05-20 00:00:00", "2020-05-20 23:59:59", true)]
        [InlineData("2020-05-20 23:59:59", "2020-05-20 00:00:00", true)]
        [InlineData("2020-05-20 00:00:00", "2020-05-19 23:59:59", false)]
        [InlineData("2020-05-19 23:59:59", "2020-05-20 00:00:00", false)]
        public void IsSameDayAs(string a, string b, bool expected)
        {
            // arrange
            var aDate = DateTime.Parse(a);
            var bDate = DateTime.Parse(b);

            // act
            var a_to_b = aDate.IsSameDayAs(bDate);
            var b_to_a = bDate.IsSameDayAs(aDate);

            // assert
            Assert.Equal(expected, a_to_b);
            Assert.Equal(expected, b_to_a);
        }

        [Theory]
        [InlineData("2020-05-20", "2020-05-20", true)]
        [InlineData("2020-05-20", "2020-05-19", true)]
        [InlineData("2020-05-19", "2020-05-20", false)]
        [InlineData("2020-05-20 00:00:00", "2020-05-20 00:00:01", true)]
        [InlineData("2020-05-20 00:00:01", "2020-05-20 00:00:00", true)]
        [InlineData("2020-05-20 00:00:00", "2020-05-20 23:59:59", true)]
        [InlineData("2020-05-20 23:59:59", "2020-05-20 00:00:00", true)]
        [InlineData("2020-05-20 00:00:00", "2020-05-19 23:59:59", true)]
        [InlineData("2020-05-19 23:59:59", "2020-05-20 00:00:00", false)]
        public void IsSameDayOrAfter(string a, string b, bool expected)
        {
            // arrange
            var aDate = DateTime.Parse(a);
            var bDate = DateTime.Parse(b);

            // act
            var actual = aDate.IsSameDayOrAfter(bDate);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2020-05-20", "2020-05-20", true)]
        [InlineData("2020-05-20", "2020-05-19", false)]
        [InlineData("2020-05-19", "2020-05-20", true)]
        [InlineData("2020-05-20 00:00:00", "2020-05-20 00:00:01", true)]
        [InlineData("2020-05-20 00:00:01", "2020-05-20 00:00:00", true)]
        [InlineData("2020-05-20 00:00:00", "2020-05-20 23:59:59", true)]
        [InlineData("2020-05-20 23:59:59", "2020-05-20 00:00:00", true)]
        [InlineData("2020-05-20 00:00:00", "2020-05-19 23:59:59", false)]
        [InlineData("2020-05-19 23:59:59", "2020-05-20 00:00:00", true)]
        public void IsSameDayOrBefore(string a, string b, bool expected)
        {
            // arrange
            var aDate = DateTime.Parse(a);
            var bDate = DateTime.Parse(b);

            // act
            var actual = aDate.IsSameDayOrBefore(bDate);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2020-05-20", "2020-05-20", false)]
        [InlineData("2020-05-20", "2020-05-19", true)]
        [InlineData("2020-05-19", "2020-05-20", false)]
        [InlineData("2020-05-20 00:00:00", "2020-05-20 00:00:01", false)]
        [InlineData("2020-05-20 00:00:01", "2020-05-20 00:00:00", true)]
        [InlineData("2020-05-20 00:00:00", "2020-05-20 23:59:59", false)]
        [InlineData("2020-05-20 23:59:59", "2020-05-20 00:00:00", true)]
        [InlineData("2020-05-20 00:00:00", "2020-05-19 23:59:59", true)]
        [InlineData("2020-05-19 23:59:59", "2020-05-20 00:00:00", false)]
        public void IsAfter(string a, string b, bool expected)
        {
            // arrange
            var aDate = DateTime.Parse(a);
            var bDate = DateTime.Parse(b);

            // act
            var actual = aDate.IsAfter(bDate);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2020-05-20", "2020-05-20", false)]
        [InlineData("2020-05-20", "2020-05-19", false)]
        [InlineData("2020-05-19", "2020-05-20", true)]
        [InlineData("2020-05-20 00:00:00", "2020-05-20 00:00:01", true)]
        [InlineData("2020-05-20 00:00:01", "2020-05-20 00:00:00", false)]
        [InlineData("2020-05-20 00:00:00", "2020-05-20 23:59:59", true)]
        [InlineData("2020-05-20 23:59:59", "2020-05-20 00:00:00", false)]
        [InlineData("2020-05-20 00:00:00", "2020-05-19 23:59:59", false)]
        [InlineData("2020-05-19 23:59:59", "2020-05-20 00:00:00", true)]
        public void IsBefore(string a, string b, bool expected)
        {
            // arrange
            var aDate = DateTime.Parse(a);
            var bDate = DateTime.Parse(b);

            // act
            var actual = aDate.IsBefore(bDate);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(DayOfWeek.Sunday, false)]
        [InlineData(DayOfWeek.Monday, true)]
        [InlineData(DayOfWeek.Tuesday, true)]
        [InlineData(DayOfWeek.Wednesday, true)]
        [InlineData(DayOfWeek.Thursday, true)]
        [InlineData(DayOfWeek.Friday, true)]
        [InlineData(DayOfWeek.Saturday, false)]
        public void IsWeekday(DayOfWeek dayOfWeek, bool expected)
        {
            // arrange
            var knownSunday = new DateTime(2020, 5, 31, 0, 0, 0);
            var time = knownSunday.AddDays((int)dayOfWeek);

            // act
            var actual = time.IsWeekday();

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(DayOfWeek.Sunday, true)]
        [InlineData(DayOfWeek.Monday, false)]
        [InlineData(DayOfWeek.Tuesday, false)]
        [InlineData(DayOfWeek.Wednesday, false)]
        [InlineData(DayOfWeek.Thursday, false)]
        [InlineData(DayOfWeek.Friday, false)]
        [InlineData(DayOfWeek.Saturday, true)]
        public void IsWeekend(DayOfWeek dayOfWeek, bool expected)
        {
            // arrange
            var knownSunday = new DateTime(2020, 5, 31, 0, 0, 0);
            var time = knownSunday.AddDays((int)dayOfWeek);

            // act
            var actual = time.IsWeekend();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToIso8601String_Utc()
        {
            // arrange
            var date = new DateTime(2015, 1, 25, 1, 12, 25, 123, DateTimeKind.Utc);

            // act
            var actual = date.ToIso8601String();

            //assert
            Assert.Equal("2015-01-25T01:12:25.1230000Z", actual);
        }

        [Fact]
        public void ToIso8601String_Local()
        {
            // arrange
            var date = new DateTime(2015, 1, 25, 1, 12, 25, 123, DateTimeKind.Local);
            var localOffset = TimeZoneInfo.Local.GetUtcOffset(date);
            var expected = $"2015-01-25T01:12:25.1230000{(localOffset >= TimeSpan.Zero ? "+" : string.Empty)}{localOffset.Hours:00}:{localOffset.Minutes:00}";

            // act
            var actual = date.ToIso8601String();

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToIso8601String_Unspecified()
        {
            // arrange
            var date = new DateTime(2015, 1, 25, 1, 12, 25, 123, DateTimeKind.Unspecified);

            // act
            var actual = date.ToIso8601String();

            //assert
            Assert.Equal("2015-01-25T01:12:25.1230000", actual);
        }

        [Fact]
        public void ToLdapTime()
        {
            // http://www.epochconverter.com/epoch/ldap-timestamp.php

            // arrange
            var date = new DateTime(2015, 1, 25, 1, 12, 25, 123, DateTimeKind.Utc);

            // act
            var actual = date.ToLdapTime();

            //assert
            Assert.Equal(130666219451230000, actual);
        }

        [Fact]
        public void ToSortableString()
        {
            // arrange
            var date = new DateTime(2015, 1, 25, 1, 12, 25, 123, DateTimeKind.Utc);

            // act
            var actual = date.ToSortableString();

            //assert
            Assert.Equal("2015-01-25T01:12:25", actual);
        }

        [Fact]
        public void ToUnixTimeMilliseconds()
        {
            // arrange
            var now = DateTime.UtcNow;
            var expected = new DateTimeOffset(now).ToUnixTimeMilliseconds();

            // act
            var actual = now.ToUnixTimeMilliseconds();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToUnixTimeSeconds()
        {
            // arrange
            var now = DateTime.UtcNow;
            var expected = new DateTimeOffset(now).ToUnixTimeSeconds();

            // act
            var actual = now.ToUnixTimeSeconds();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TruncateToMinute()
        {
            // arrange
            var time = new DateTime(2014, 1, 1, 12, 23, 34, 45, DateTimeKind.Unspecified);
            var expected = new DateTime(2014, 1, 1, 12, 23, 0, 0, DateTimeKind.Unspecified);

            // act
            var actual = time.TruncateToMinute();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TruncateToSecond()
        {
            // arrange
            var time = new DateTime(2014, 1, 1, 12, 23, 34, 45, DateTimeKind.Unspecified);
            var expected = new DateTime(2014, 1, 1, 12, 23, 34, 0, DateTimeKind.Unspecified);

            // act
            var actual = time.TruncateToSecond();

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
