Imports System.Windows.Threading

Public Class StopWatch

    ReadOnly stopwatch As New System.Diagnostics.Stopwatch
    ReadOnly timer1 As New DispatcherTimer With {.Interval = TimeSpan.FromMilliseconds(50)}

    Private Sub StopWatch_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        AddHandler timer1.Tick, Sub() UpdateDisplay()
    End Sub

    Private Sub UpdateDisplay()
        labelTime.Content = stopwatch.Elapsed.ToString("hh\:mm\:ss\.f")
    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            timer1.Stop()
            button.Content = "Start"
            UpdateDisplay()
        Else
            stopwatch.Start()
            timer1.Start()
            button.Content = "Pause"
        End If
    End Sub

    Private Sub buttonReset_Click(sender As Object, e As RoutedEventArgs) Handles buttonReset.Click
        stopwatch.Reset()
        timer1.Stop()
        button.Content = "Start"
        UpdateDisplay()
    End Sub

    Private Sub buttonClose_Click(sender As Object, e As RoutedEventArgs) Handles buttonClose.Click
        timer1.Stop()
        Me.Close()
    End Sub

End Class
