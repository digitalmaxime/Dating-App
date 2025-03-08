namespace API.Extensions;

public static class DateTimeExtensions
{
    private const int NumberOfDaysInYear = 365;

    public static int CalculateAge(this DateTime dateTime) {
        var delta = DateTime.UtcNow - dateTime; 
        var divisionDoubleResult = delta.TotalDays / NumberOfDaysInYear;
        var divisionIntResult = (int)divisionDoubleResult;
        return divisionIntResult;
    }
    
    public static int CalculateAge(this DateOnly dateTime) {
        var delta = DateTime.UtcNow - dateTime.ToDateTime(TimeOnly.MinValue); 
        var divisionDoubleResult = delta.TotalDays / NumberOfDaysInYear;
        var divisionIntResult = (int)divisionDoubleResult;
        return divisionIntResult;
    }
}
