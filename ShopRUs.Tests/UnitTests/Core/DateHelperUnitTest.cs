using ShopRUs.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ShopRUs.Tests.UnitTests.Core
{
    public class DateHelperUnitTest
    {
        [Fact]
        public void DateDifferenceInYears_Return2Years()
        {
            var date1 = new DateTime(2019, 01, 01);
            var date2 = new DateTime(2021, 01, 01);

            var result = DateHelper.GetDateDifferenceInYears(date1, date2);

            Assert.True(result > 2, "Difference between both dates is less than or equal to 2 years");
        }
    }
}
