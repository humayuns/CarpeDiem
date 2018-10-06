Imports System.Globalization

Public Class TimeManagement


    Public Shared Function TimeAfterHours(hours As Double) As Date
        Return Date.Now.AddHours(hours)
    End Function

    Public Shared Function TotalMinutesAfter(hours As Double) As Integer
        Return TimeAfterHours(hours).Subtract(Date.Now).TotalMinutes
    End Function

    Public Shared Function GetTimeAfterMinutes(minutes As Double) As Date
        Return Now.AddMinutes(minutes)
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

    Public Shared Function GetDifferencePercentageHour(startDate As Date, endDate As Date) As Decimal

        Dim target = New Date(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, 0, 0).AddHours(1)
        Dim remaining = target.Subtract(startDate).TotalMinutes
        Dim total = 60 'startDate.Subtract(endDate).TotalMinutes

        Return 100 * (remaining / total)

    End Function

    Public Shared Function GetDifferencePercentageMinute(startDate As Date, endDate As Date) As Decimal

        Dim target = New Date(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, 0).AddMinutes(1)
        Dim remaining = target.Subtract(startDate).TotalSeconds
        Dim total = 60 'startDate.Subtract(endDate).TotalMinutes

        Return 100 * (remaining / total)

    End Function

    Public Shared Function GetDifferencePercentageSecond(startDate As Date, endDate As Date) As Decimal

        Dim target = New Date(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, startDate.Second)
        Dim remaining = 1000 + target.Subtract(startDate).TotalMilliseconds
        Dim total = 1000 'startDate.Subtract(endDate).TotalMinutes

        Return 100 * (remaining / total)

    End Function

    Public Shared Function GetFirstDayOfMonth(DateTime As Date) As DateTime
        Return New DateTime(DateTime.Year, DateTime.Month, 1)
    End Function

    Public Shared Function GetLastDayOfMonth(dateTime As DateTime) As DateTime
        Dim firstDayOfTheMonth As New DateTime(dateTime.Year, dateTime.Month, 1)
        Return firstDayOfTheMonth.AddMonths(1).AddMinutes(-1)
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
    ''' <summary>
    ''' Gets month name for provided month number.
    ''' </summary>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Shared Function GetMonthName(month As Integer) As String
        Return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)
    End Function

    ''' <summary>
    ''' Gets the section number of day according to given time.
    ''' </summary>
    ''' <param name="time"></param>
    ''' <returns></returns>
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

    Public Shared Function SectionEndingTime(time As Date) As Date
        Dim result As Date

        Select Case GetDaySection(time)
            Case "1"
                result = Date.Parse(time.ToShortDateString() & " 8:00")
            Case "2"
                result = Date.Parse(time.ToShortDateString() & " 16:00")
            Case "3"
                result = Date.Parse(time.ToShortDateString() & " 23:59")
        End Select
        Return result
    End Function

    Public Shared Function SectionStartingTime(time As Date) As Date
        Dim result As Date

        Select Case GetDaySection(time)
            Case "1"
                result = Date.Parse(time.ToShortDateString() & " 0:00")
            Case "2"
                result = Date.Parse(time.ToShortDateString() & " 8:00")
            Case "3"
                result = Date.Parse(time.ToShortDateString() & " 16:00")
        End Select
        Return result
    End Function


    Public Shared Function GetIso8601WeekOfYear(time As DateTime) As Integer
        ' Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
        ' be the same week# as whatever Thursday, Friday or Saturday are,
        ' and we always get those right
        Dim day As DayOfWeek = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time)
        If day >= DayOfWeek.Monday AndAlso day <= DayOfWeek.Wednesday Then
            time = time.AddDays(3)
        End If

        ' Return the week of our adjusted day
        Return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)
    End Function


    Public Shared Function GetDifferenceTimespan(startTime As DateTime, endTime As DateTime) As TimeSpan
        Return endTime.Subtract(startTime)
    End Function

End Class
