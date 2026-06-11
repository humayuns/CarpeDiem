Imports CarpeDiem.Core

Public Class Calendar

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Calendar1.SelectedDate = Date.Today
        Calendar1.DisplayDate = Date.Today
    End Sub

    Private Sub Calendar1_SelectedDatesChanged(sender As Object, e As SelectionChangedEventArgs) Handles Calendar1.SelectedDatesChanged
        If Not Calendar1.SelectedDate.HasValue Then Return
        Dim dt = Calendar1.SelectedDate.Value

        labelSelectedDate.Content = dt.ToString("dddd, MMMM d, yyyy")

        Dim daysInYear = If(Date.IsLeapYear(dt.Year), 366, 365)
        labelDayOfYear.Content = "Day " & dt.DayOfYear & " / " & daysInYear
        labelWeek.Content = "Week " & TimeManagement.GetIso8601WeekOfYear(dt)
        labelRelative.Content = DescribeRelativeToToday(dt.Date)

        Dim pct = dt.DayOfYear / daysInYear * 100
        progressYear.Value = pct
        textYear.Text = dt.Year & " - " & (pct / 100).ToString("p1")
    End Sub

    Private Function DescribeRelativeToToday(dt As Date) As String
        Dim diffDays = (dt - Date.Today).Days
        Select Case diffDays
            Case 0 : Return "Today"
            Case 1 : Return "Tomorrow"
            Case -1 : Return "Yesterday"
            Case Is > 0 : Return "In " & diffDays & " days"
            Case Else : Return Math.Abs(diffDays) & " days ago"
        End Select
    End Function

    Private Sub buttonToday_Click(sender As Object, e As RoutedEventArgs) Handles buttonToday.Click
        Calendar1.SelectedDate = Date.Today
        Calendar1.DisplayDate = Date.Today
    End Sub

End Class
