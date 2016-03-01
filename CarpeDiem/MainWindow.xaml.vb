﻿Imports System.Windows.Threading
Imports CarpeDiem.Core

Class MainWindow

    ReadOnly timer1 As New DispatcherTimer
    Dim timeDiff As Date
    Dim minutesDiff As Integer = 0

    Dim timeDiffFood As Date
    Dim minutesDiffFood As Integer = 0

    Dim remainingMinutes = 0

    Dim targetTime As DateTime
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        UpdateFoodClock()
    End Sub

    Private Sub UpdateFoodClock()
        timeDiffFood = TimeManagement.TimeAfterHours(3)
        minutesDiffFood = TimeManagement.TotalMinutesAfter(3) 'timeDiffFood.Subtract(Date.Now).TotalMinutes
    End Sub

    Private sub timer1_Tick(sender As Object, e As EventArgs)


        Progressbar1.Value = (100 * timeDiff.Subtract(Date.Now).TotalMinutes / minutesDiff)
        ProgressbarFood.Value = (100 * timeDiffFood.Subtract(Date.Now).TotalMinutes / minutesDiffFood)

        'Progressbar1.Value += 10
        labelHoursLeft.Content = Math.Round(targetTime.Subtract(Now).TotalHours, 2)

        If Progressbar1.Value >= 100 Then
            timer1.IsEnabled = False
            Progressbar1.Value = 0
        End If

        If Progressbar1.Value <= 0 Then
            timer1.IsEnabled = False
            MsgBox("Finished!", MsgBoxStyle.Information)
        End If

        labelTime.Content = Now.ToLongTimeString()


    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click

        ' set up timer and start it


        targetTime = Date.Now.AddHours(textBoxHours.Text)
        labelTarget.Content = targetTime.ToString()

        timeDiff = Date.Now.AddHours(textBoxHours.Text)

        Dim diff As TimeSpan = timeDiff.Subtract(Date.Now)
        'Debug.WriteLine(diff)

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
        targetTime = Date.Now.AddHours(Val(textBoxHours.Text))
        labelTarget.Content = targetTime.ToString()
    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If (e.ChangedButton = MouseButton.Left) Then Me.DragMove()
    End Sub

    Private Sub buttonSprite_Click(sender As Object, e As RoutedEventArgs) Handles buttonSprite.Click
        Dim s As New SpriteWindow
        s.Show()
    End Sub

    Private Sub buttonClock_Click(sender As Object, e As RoutedEventArgs) Handles buttonClock.Click
        Dim c As New ClockWindow
        c.Show()
    End Sub

    Private Sub buttonClose_Click(sender As Object, e As RoutedEventArgs) Handles buttonClose.Click
        Close()
    End Sub

    Private Sub buttonCountDown_Click(sender As Object, e As RoutedEventArgs) Handles buttonCountDown.Click
        MsgBox("Add CountDown Here!", MsgBoxStyle.Information)
    End Sub

    Private Sub buttonFood_Click(sender As Object, e As RoutedEventArgs) Handles buttonFood.Click
        UpdateFoodClock()
    End Sub
End Class
