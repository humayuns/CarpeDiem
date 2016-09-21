Public Class TimeManagement


    Public Shared Function TimeAfterHours(hours As Double) As Date
        Return Date.Now.AddHours(hours)
    End Function

    Public Shared Function TotalMinutesAfter(hours As Double) As Integer
        Return TimeAfterHours(hours).Subtract(Date.Now).TotalMinutes
    End Function

    Public Shared Function GetTimeAfterMinutes(minutes As Double) As Date
        Return now.AddMinutes(minutes)
    End Function

    Public Shared Function GetFormattedDateTime(format As String) As String
        ' Example: "hh:mm:ss tt"
        Return Date.Now.ToString(format)
    End Function

    ''' <summary>
    ''' Gets progress percentage between a startDate and endDate in context of current time.
    ''' </summary>
    ''' <param name="startDate"></param>
    ''' <param name="endDate"></param>
    ''' <returns>a value between 0 to 100</returns>
    Public Shared Function GetDifferencePercentage(startDate As Date, endDate As Date) As Decimal
        Return 100 * (Now.Subtract(endDate).TotalMinutes / startDate.Subtract(endDate).TotalMinutes)
    End Function

    Public Shared Function GetFirstDayOfMonth(DateTime as Date) As DateTime
        Return new DateTime(dateTime.Year, dateTime.Month, 1)
    End Function

    Public Shared Function GetLastDayOfMonth(dateTime As DateTime) As DateTime
	    Dim firstDayOfTheMonth As New DateTime(dateTime.Year, dateTime.Month, 1)
	    Return firstDayOfTheMonth.AddMonths(1).AddDays(-1)
    End Function

    ''' <summary>
    ''' Get first day of week according to provided date.
    ''' </summary>
    ''' <param name="dt">Date</param>
    ''' <param name="startOfWeek">Week Start Day</param>
    ''' <returns></returns>
    Public Shared Function GetFirstDayOfWeek(dt As DateTime, startOfWeek As DayOfWeek) As DateTime
	    Dim diff As Integer = dt.DayOfWeek - startOfWeek
	    If diff < 0 Then
		    diff += 7
	    End If
	    Return dt.AddDays(-1 * diff).[Date]
    End Function


    Public Shared Function GetDaySection(time As Date) As String
        Dim result As String = ""
        If time.Hour >= 0 And time.Hour < 8 Then
            result = "1"
        ElseIf time.Hour >= 8 And time.Hour < 16 Then
            result = "2"
        ElseIf time.Hour >= 16 And time.Hour > 0 Then
            result = "3"
        End If
        Return result
    End Function

End Class
