Imports System.Windows.Threading
Imports CarpeDiem.Core

Class MainWindow

    ReadOnly timer1 As New DispatcherTimer
    Dim timerSpeed = "fast"

    Dim timeDiff as new TimeCountDown
    Dim timeDiffFood as new TimeCountDown
    Dim timeDiffMotivation as new TimeCountDown
    Dim timeDiffInspiration as new TimeCountDown


    Dim targetTime As DateTime
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        AddHandler timer1.Tick, AddressOf timer1_Tick
        timer1.Interval = New TimeSpan(0, 0, 0, 0, 1)
        timer1.Start()

        UpdateEnergyClock()
        RefreshMotivation()
        RefreshInspiration()

    End Sub

    Private Sub UpdateEnergyClock()
        timeDiffFood.AddHours(16)
    End Sub

    Private Sub RefreshMotivation()
        timeDiffMotivation.AddHours(1)
    End Sub

    Private Sub RefreshInspiration()
        timeDiffInspiration.AddHours(0.1)
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        ' Need to add a feature where can restore energy from sleep.
        UpdateProgress()
    End Sub

    Private Sub UpdateProgress()

        ' Display current local and UTC time.
        labelTime.Content = TimeManagement.GetFormattedDateTime("hh:mm:ss tt")
        labelTimeUTC.Content = Date.Now.ToLongDateString()

        Try
            Progressbar1.Value = timeDiff.GetDifferencePercentage()
            labelHoursLeft.Content = Math.Round(targetTime.Subtract(Now).TotalHours, 2)
        Catch ex As Exception

        End Try

        ProgressbarFood.Value = timeDiffFood.GetDifferencePercentage()
        ProgressbarMotivation.Value = timeDiffMotivation.GetDifferencePercentage()
        ProgressbarInspiration.Value = timeDiffInspiration.GetDifferencePercentage()

        ProgressbarLT.Value = TimeManagement.GetDifferencePercentage(New Date(Now.Year, 1, 1), New Date(Now.Year, 12, 31, 23, 59, 59))  ' new year progress
        ProgressbarMonth.Value = TimeManagement.GetDifferencePercentage(TimeManagement.GetFirstDayOfMonth(Now), TimeManagement.GetLastDayOfMonth(Now))
        ProgressbarWeek.Value = TimeManagement.GetDifferencePercentage(TimeManagement.GetFirstDayOfWeek(Now, DayOfWeek.Monday), TimeManagement.GetFirstDayOfWeek(Now, DayOfWeek.Monday).AddDays(7))

        ProgressbarDay.Value = TimeManagement.GetDifferencePercentage(Now.Date, Now.Date.AddDays(1).AddTicks(-1))
        ProgressbarHour.Value = TimeManagement.GetDifferencePercentageHour(Now, Now.AddHours(1).AddTicks(-1))
        ProgressbarMinute.Value = TimeManagement.GetDifferencePercentageMinute(Now, Now.AddMinutes(1).AddTicks(-1))
        ProgressbarSecond.Value = TimeManagement.GetDifferencePercentageSecond(Now, Now.AddSeconds(1).AddTicks(-1))

        ProgressbarSecondText.Text = "Second " & Now.Second + 1 & " - " & (ProgressbarSecond.Value / 100).ToString("p")
        ProgressbarMinuteText.Text = "Minute " & Now.Minute + 1 & " - " & (ProgressbarMinute.Value / 100).ToString("p")
        ProgressbarHourText.Text = "Hour " & Now.Hour & " - " & (ProgressbarHour.Value / 100).ToString("p")
        Dim remainingTime = Now.Date.AddDays(1).AddTicks(-1).Subtract(Now)
        ProgressbarDayText.Text = Now.DayOfWeek.ToString() & " - " & Now.Day & " - " & (ProgressbarDay.Value / 100).ToString("p") & " - " & remainingTime.ToString()
        ProgressbarDay.ToolTip = ProgressbarDayText.Text
        ProgressbarWeekText.Text = TimeManagement.GetIso8601WeekOfYear(Now) & " - " & (ProgressbarWeek.Value / 100).ToString("p")
        ProgressbarMonthText.Text = TimeManagement.GetMonthName(Now.Month) & "(" & Now.Month & ")" & " - " & (ProgressbarMonth.Value / 100).ToString("p")
        ProgressbarLTText.Text = Now.Year & " - " & (ProgressbarLT.Value / 100).ToString("p")




        If Progressbar1.Value >= 100 Then
            timer1.IsEnabled = False
            Progressbar1.Value = 0
        End If

        If Progressbar1.Value <= 0 Then
            'timer1.IsEnabled = False
            'MsgBox("Finished!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click

        StartTimeProgress()

    End Sub

    Private Sub StartTimeProgress()
        ' set up timer and start it
        targetTime = Date.Now.AddHours(textBoxHours.Text)
        labelTarget.Content = targetTime.ToString()

        timeDiff.AddHours(textBoxHours.Text)

        timer1.Interval = getInterval()
        timer1.Start()
    End Sub

    Private Function getInterval() As TimeSpan
        If timerSpeed = "fast" Then
            Return New TimeSpan(0, 0, 0, 1, 0)
        Else
            Return New TimeSpan(0, 0, 0, 0, 1)
        End If
    End Function

    Private Sub textBoxHours_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxHours.TextChanged
        targetTime = Date.Now.AddHours(Val(textBoxHours.Text))
        labelTarget.Content = targetTime.ToString()
    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If (e.ChangedButton = MouseButton.Left) Then DragMove()
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
        End
    End Sub


    Private Sub buttonFood_Click(sender As Object, e As RoutedEventArgs) Handles buttonFood.Click
        UpdateEnergyClock()
    End Sub


    Private Sub buttonGo_Click(sender As Object, e As RoutedEventArgs) Handles buttonGo.Click
        Select Case ComboBox.Text
            Case "CountDown"
                Dim c As New CountDown
                c.Show()
            Case "Stop Watch"
                Dim s As New StopWatch
                s.Show()
            Case "Focus"
                Dim f As New Focus
                f.Show()
            Case "Body Clock"
                Dim b As New BodyClock
                b.Show()
            Case "Calendar"
                Dim c As New Calendar
                c.Show()
            Case "Ability"
                Dim a As New Ability
                a.Show()
            Case "Diary"
                Dim d As New Diary
                d.Show()
            Case "Diary2"
                Dim d2 As New Diary2
                d2.Show()
        End Select
    End Sub

    Private Sub Grid_Unloaded(sender As Object, e As RoutedEventArgs)
        End
    End Sub

    Private Sub buttonMotivation_Click(sender As Object, e As RoutedEventArgs) Handles buttonMotivation.Click
        RefreshMotivation()
    End Sub

    Private Sub buttonInspiration_Click(sender As Object, e As RoutedEventArgs) Handles buttonInspiration.Click
        RefreshInspiration()
    End Sub

    Private Sub buttonClock_Copy_Click(sender As Object, e As RoutedEventArgs) Handles buttonClock_Copy.Click
        Dim d As New Diary2
        d.Show()
    End Sub

    Private Sub textBoxHours_KeyDown(sender As Object, e As KeyEventArgs) Handles textBoxHours.KeyDown
        If textBoxHours.Text <> "" And e.Key = Key.Return Then
            StartTimeProgress()
        End If
    End Sub


    Private Sub Grid_MouseRightButtonUp(sender As Object, e As MouseButtonEventArgs)
        timer1.Interval = getInterval()
        If timerSpeed = "fast" Then
            timerSpeed = "slow"
        Else
            timerSpeed = "fast"
        End If
    End Sub

    Private Sub menuItemAlwaysOnTop_Click(sender As Object, e As RoutedEventArgs)
        Me.Topmost = menuItemAlwaysOnTop.IsChecked
    End Sub
End Class
