Imports System.Windows.Threading
Imports CarpeDiem.Core

Public Class CountDown

    ReadOnly timer1 As New DispatcherTimer
    Private startTime As Date
    Private targetTime As Date
    Dim tspn As New TimeSpan()

    Private Sub button_Copy_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy.Click
        Me.Close()
    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        Dim targetSeconds As Double
        If Not Double.TryParse(textBoxTarget.Text, targetSeconds) OrElse targetSeconds <= 0 Then
            labelTime.Content = "Enter seconds"
            Return
        End If

        startTime = Now
        targetTime = Now.AddSeconds(targetSeconds)
        tspn = TimeSpan.FromSeconds(targetSeconds)

        ProgressBar1.Value = 0
        UpdateDisplay()
        timer1.Start()
        button.IsEnabled = False
    End Sub

    Private Sub textBoxTarget_KeyDown(sender As Object, e As KeyEventArgs) Handles textBoxTarget.KeyDown
        If e.Key = Key.Return AndAlso button.IsEnabled Then button_Click(Nothing, Nothing)
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        AddHandler timer1.Tick, AddressOf timer1_Tick
        timer1.Interval = New TimeSpan(0, 0, 1)
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        ' Compute the remainder from the target time so the countdown stays accurate
        ' even if ticks are delayed.
        tspn = targetTime.Subtract(Now)

        If tspn.TotalSeconds <= 0 Then
            tspn = TimeSpan.Zero
            timer1.Stop()
            ProgressBar1.Value = 100
            labelTime.Content = "Done!"
            button.IsEnabled = True
            PlaySound("C:\Windows\Media\tada.wav")
            Return
        End If

        ProgressBar1.Value = TimeManagement.GetDifferencePercentage(startTime, targetTime)
        UpdateDisplay()
    End Sub

    Private Sub UpdateDisplay()
        labelTime.Content = tspn.ToString("hh\:mm\:ss")
    End Sub

    Private Sub CountDown_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        timer1.Stop()
    End Sub

End Class
