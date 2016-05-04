Imports System.Windows.Threading

Public Class CountDown

    ReadOnly timer1 As New DispatcherTimer
    Private targetSeconds As Integer
    Private targetTime As Date
    Dim tspn As New TimeSpan()

    Private Sub button_Copy_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy.Click
        Me.Close()
    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        targetSeconds = textBoxTarget.Text
        targetTime = Now.AddSeconds(targetSeconds)

        tspn = New TimeSpan(0, 0, targetSeconds)

        Timer1.Start
        button.IsEnabled = False

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        AddHandler timer1.Tick, AddressOf timer1_Tick
        timer1.Interval = New TimeSpan(0, 0, 1)
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        tspn = tspn.Subtract(New TimeSpan(0, 0, 1))

        labelTime.Content = String.Format(" {0} Mins : {1} Secs", tspn.Minutes, tspn.Seconds)
        If tspn.Minutes = 0 AndAlso tspn.Seconds = 0 Then
            Timer1.Stop()
            button.IsEnabled = True
        End If

    End Sub
End Class
