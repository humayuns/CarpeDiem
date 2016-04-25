Imports System.Windows.Threading

Public Class Focus

    ReadOnly timer1 As New DispatcherTimer
    Dim score As Integer

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        AddHandler timer1.Tick, AddressOf timer1_Tick
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value += 1
        Else
            timer1.Stop()
        End If
    End Sub

    Private Sub buttonStart_Click(sender As Object, e As RoutedEventArgs) Handles buttonStart.Click

        If buttonStart.Content = "Stop" Then
            timer1.Stop()
            buttonStart.Content = "Start"
            If ProgressBar1.Value > 95 Then
                score += 1
                label.Content = "Score: " & score.ToString()
                PlaySound("C:\Windows\Media\tada.wav")
            End If
        Else
            timer1.Interval = New TimeSpan(0, 0, 0, 0, 1)
            timer1.IsEnabled = True
            timer1.Start()
            ProgressBar1.Value = 0
            buttonStart.Content = "Stop"
        End If
    End Sub

    Private Sub buttonReset_Click(sender As Object, e As RoutedEventArgs) Handles buttonReset.Click
        timer1.Stop()
        ProgressBar1.Value = 0
        score = 0
        label.Content = "Score: " & score.ToString()
    End Sub
End Class
