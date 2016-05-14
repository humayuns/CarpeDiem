Public Class TimeCountDown

    Public timeDiff As Date
    Public minutesDiff As Integer = 0

    Public Function GetDifferencePercentage() As Double
        Return (100 * timeDiff.Subtract(Date.Now).TotalMinutes / minutesDiff)
    End Function

    Public sub AddHours(hours As Double)
        timeDiff = TimeManagement.TimeAfterHours(hours)
        minutesDiff = TimeManagement.TotalMinutesAfter(hours)
    End sub
   
End Class
