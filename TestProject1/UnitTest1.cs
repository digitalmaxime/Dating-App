using API.Extensions;

namespace TestProject1;

public class DateTimeTests
{
    [Theory]
    [InlineData(2020, 1, 1, 5)]
    [InlineData(2019, 7, 15, 5)]
    [InlineData(2020, 7, 15, 4)]
    public void GetYearTest(int year, int month, int day, int result)
    {
        DateTime date = new DateTime(year, month, day);   
        var res = date.CalculateAge();
        
        Assert.Equal(result, res);
    }
}