Imports System.Windows.Threading

Class MainWindow

    Dim timer1 As New DispatcherTimer
    Dim timeDiff As Date
    Dim minutesDiff As Integer = 0
    Dim remainingMinutes = 0

    Dim targetTime As DateTime
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private sub timer1_Tick(sender As Object, e As EventArgs)


        Progressbar1.Value = (100 * timeDiff.Subtract(Date.Now).TotalMinutes / minutesDiff)


        'Progressbar1.Value += 10


        If Progressbar1.Value >= 100 Then
            timer1.IsEnabled = False
            Progressbar1.Value = 0
        End If


    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click

        ' set up timer and start it


        targetTime = Date.Now.AddHours(textBoxHours.Text)
        labelTarget.Content = targetTime.ToString()

        timeDiff = Date.Now.AddHours(textBoxHours.Text)

        'MsgBox(timeDiff.ToString(), MsgBoxStyle.Information)

        Dim diff = timeDiff.Subtract(Date.Now)
        Debug.WriteLine(diff)
        'MsgBox(diff.TotalMinutes, MsgBoxStyle.Information)

        minutesDiff = diff.TotalMinutes

        AddHandler timer1.Tick, AddressOf timer1_Tick
        timer1.Interval = New TimeSpan(0, 0, 1)
        timer1.Start()
    End Sub

    Private Function GetHoursDifference() As Integer
        'get the number of hours
        Return 0
    End Function

    Private Sub textBoxHours_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxHours.TextChanged
        targetTime = Date.Now.AddHours(textBoxHours.Text)
        labelTarget.Content = targetTime.ToString()
    End Sub
End Class
