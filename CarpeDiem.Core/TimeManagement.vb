Public Class TimeManagement


    Public Shared Function TimeAfterHours(hours As Integer) As Date
        Return Date.Now.AddHours(hours)
    End Function

    Public Shared Function TotalMinutesAfter(hours As Integer) As Integer
        Return TimeAfterHours(hours).Subtract(Date.Now).TotalMinutes
    End Function


End Class
