Imports System.Windows.Threading
Imports CarpeDiem.Core

Class MainWindow

    ReadOnly timer1 As New DispatcherTimer


    dim timeDiff as new TimeCountDown
    Dim timeDiffFood as new TimeCountDown
    Dim timeDiffMotivation as new TimeCountDown
    Dim timeDiffInspiration as new TimeCountDown


    Dim targetTime As DateTime
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        AddHandler timer1.Tick, AddressOf timer1_Tick
        UpdateFoodClock()
        RefreshMotivation()
        RefreshInspiration()
    End Sub

    Private Sub UpdateFoodClock()
        timeDiffFood.AddHours(3)
    End Sub

    Private sub RefreshMotivation()
        timeDiffMotivation.AddHours(1)
    End sub

    Private sub RefreshInspiration()
        timeDiffInspiration.AddHours(0.10)
    End sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        UpdateProgress()
    End Sub

    Private Sub UpdateProgress()

    ' Display current local and UTC time.
        labelTime.Content = TimeManagement.GetFormattedDateTime("hh:mm:ss tt")
        labelTimeUTC.Content = Date.Now.ToLongDateString()

        Progressbar1.Value = timeDiff.GetDifferencePercentage()
        ProgressbarFood.Value = timeDiffFood.GetDifferencePercentage()
        ProgressbarMotivation.Value = timeDiffMotivation.GetDifferencePercentage()
        ProgressbarInspiration.Value = timeDiffInspiration.GetDifferencePercentage()

        ProgressbarLT.Value = TimeManagement.GetDifferencePercentage(My.Settings.LTGoalStart, My.Settings.LTGoalEnd)
        ProgressbarMonth.Value = TimeManagement.GetDifferencePercentage(TimeManagement.GetFirstDayOfMonth(now), TimeManagement.GetLastDayOfMonth(now))
        
        ProgressbarWeek.Value = TimeManagement.GetDifferencePercentage(TimeManagement.GetFirstDayOfWeek(Now, DayOfWeek.Monday), TimeManagement.GetFirstDayOfWeek(Now, DayOfWeek.Monday).AddDays(7))

        labelHoursLeft.Content = Math.Round(targetTime.Subtract(Now).TotalHours, 2)

        If Progressbar1.Value >= 100 Then
            timer1.IsEnabled = False
            Progressbar1.Value = 0
        End If

        If Progressbar1.Value <= 0 Then
            timer1.IsEnabled = False
            MsgBox("Finished!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click

        ' set up timer and start it


        targetTime = Date.Now.AddHours(textBoxHours.Text)
        labelTarget.Content = targetTime.ToString()

        timeDiff.AddHours(textBoxHours.Text)

        timer1.Interval = New TimeSpan(0, 0, 1)
        timer1.Start()

    End Sub

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
        UpdateFoodClock()
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
End Class
