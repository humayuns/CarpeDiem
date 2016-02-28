Public Class TimeManagement


    Public Shared Function TimeAfterHours(hours As Integer) As Date


        Return Date.Now.AddHours(hours)

    End Function


End Class
