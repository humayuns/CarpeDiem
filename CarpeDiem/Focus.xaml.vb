﻿Imports System.Windows.Threading

Public Class Focus

    ReadOnly timer1 As New DispatcherTimer

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        AddHandler timer1.Tick, AddressOf timer1_Tick
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        If ProgressBar1.Value > 0 Then
            ProgressBar1.Value -= 1
        Else
            timer1.Stop()
        End If
    End Sub

    Private Sub buttonStart_Click(sender As Object, e As RoutedEventArgs) Handles buttonStart.Click
        timer1.Interval = New TimeSpan(0, 0, 1)
        timer1.Start()
        ProgressBar1.Value = 100
        buttonStart.Content = "Stop"
    End Sub
End Class