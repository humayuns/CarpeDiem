Public Class Launcher

    Private Sub buttonCountDown_Click(sender As Object, e As RoutedEventArgs) Handles buttonCountDown.Click
        Dim w As New CountDown
        w.Show()
    End Sub

    Private Sub buttonStopWatch_Click(sender As Object, e As RoutedEventArgs) Handles buttonStopWatch.Click
        Dim w As New StopWatch
        w.Show()
    End Sub

    Private Sub buttonFocus_Click(sender As Object, e As RoutedEventArgs) Handles buttonFocus.Click
        Dim w As New Focus
        w.Show()
    End Sub

    Private Sub buttonBodyClock_Click(sender As Object, e As RoutedEventArgs) Handles buttonBodyClock.Click
        Dim w As New BodyClock
        w.Show()
    End Sub

    Private Sub buttonCalendar_Click(sender As Object, e As RoutedEventArgs) Handles buttonCalendar.Click
        Dim w As New Calendar
        w.Show()
    End Sub

    Private Sub buttonAbility_Click(sender As Object, e As RoutedEventArgs) Handles buttonAbility.Click
        Dim w As New Ability
        w.Show()
    End Sub

    Private Sub buttonDiary_Click(sender As Object, e As RoutedEventArgs) Handles buttonDiary.Click
        Dim w As New Diary
        w.Show()
    End Sub

    Private Sub buttonDiary2_Click(sender As Object, e As RoutedEventArgs) Handles buttonDiary2.Click
        Dim w As New Diary2
        w.Show()
    End Sub

    Private Sub buttonPomodoro_Click(sender As Object, e As RoutedEventArgs) Handles buttonPomodoro.Click
        Dim w As New Pomodoro
        w.Show()
    End Sub

    Private Sub buttonQuotes_Click(sender As Object, e As RoutedEventArgs) Handles buttonQuotes.Click
        Dim w As New Quotes
        w.Show()
    End Sub

End Class
