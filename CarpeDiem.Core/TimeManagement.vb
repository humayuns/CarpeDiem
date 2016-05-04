﻿Public Class TimeManagement


    Public Shared Function TimeAfterHours(hours As Integer) As Date
        Return Date.Now.AddHours(hours)
    End Function

    Public Shared Function TotalMinutesAfter(hours As Integer) As Integer
        Return TimeAfterHours(hours).Subtract(Date.Now).TotalMinutes
    End Function

    Public Shared Function GetTimeAfterMinutes(minutes As Integer) As Date
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

End Class
