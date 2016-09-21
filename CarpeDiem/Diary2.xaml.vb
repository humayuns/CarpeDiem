Imports System.IO
Imports System.Windows.Media.Animation
Imports System.Windows.Threading

Public Class Diary2

    ReadOnly timer1 As New DispatcherTimer
    Const DiaryFolder As String = "Diary2"
    Private Sub Calendar1_SelectedDatesChanged(sender As Object, e As SelectionChangedEventArgs) Handles Calendar1.SelectedDatesChanged

        Dim dt = Calendar1.SelectedDate
        If File.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes1.txt") Then
            textBox1.Text = File.ReadAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes1.txt")
        Else
            textBox1.Text = ""
        End If

        If File.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes2.txt") Then
            textBox2.Text = File.ReadAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes2.txt")
        Else
            textBox2.Text = ""
        End If

        If File.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes3.txt") Then
            textBox3.Text = File.ReadAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes3.txt")
        Else
            textBox3.Text = ""
        End If

        Dim ca = New ColorAnimation(Colors.White, New Duration(TimeSpan.FromMilliseconds(500)))
        textBox1.Background = New SolidColorBrush(Colors.Azure)
        textBox1.Background.BeginAnimation(SolidColorBrush.ColorProperty, ca)

        textBox1.Background = New SolidColorBrush(Colors.White)
        textBox2.Background = New SolidColorBrush(Colors.White)
        textBox3.Background = New SolidColorBrush(Colors.White)

        Select Case Core.TimeManagement.GetDaySection(Now)
            Case "1"
                textBox1.Background = New SolidColorBrush(Colors.Bisque)
            Case "2"
                textBox2.Background = New SolidColorBrush(Colors.Bisque)
            Case "3"
                textBox3.Background = New SolidColorBrush(Colors.Bisque)
        End Select


    End Sub


    Private Sub textBox1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox1.TextChanged

        Dim dt = Calendar1.SelectedDate


        Try
            If Not Directory.Exists(DiaryFolder) Then Directory.CreateDirectory(DiaryFolder)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year) Then Directory.CreateDirectory(DiaryFolder & "\" & dt.Value.Year)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month) Then Directory.CreateDirectory(DiaryFolder & "\" & "\" & dt.Value.Year & "\" & dt.Value.Month)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day) Then Directory.CreateDirectory(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day)

            File.WriteAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes1.txt", textBox1.Text)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Calendar1.SelectedDate = Now

        If File.Exists(DiaryFolder & "\specialsymbols.txt") Then
            textBoxSpecial.Text = File.ReadAllText(DiaryFolder & "\specialsymbols.txt")
        End If

        comboBox.Text = "Segoe UI"
        AddHandler timer1.Tick, AddressOf timer1_Tick
        timer1.Interval = New TimeSpan(0, 0, 1)
        timer1.Start()
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        textBox1.Background = New SolidColorBrush(Colors.White)
        textBox2.Background = New SolidColorBrush(Colors.White)
        textBox3.Background = New SolidColorBrush(Colors.White)

        Select Case Core.TimeManagement.GetDaySection(Now)
            Case "1"
                textBox1.Background = New SolidColorBrush(Colors.Bisque)
            Case "2"
                textBox2.Background = New SolidColorBrush(Colors.Bisque)
            Case "3"
                textBox3.Background = New SolidColorBrush(Colors.Bisque)
        End Select
    End Sub

    Private Sub textBoxSpecial_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxSpecial.TextChanged
        If Not Directory.Exists(DiaryFolder) Then Directory.CreateDirectory(DiaryFolder)
        File.WriteAllText(DiaryFolder & "\specialsymbols.txt", textBoxSpecial.Text)
    End Sub

    Private Sub textBoxFontSize_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxFontSize.TextChanged
        textBox1.FontSize = textBoxFontSize.Text
    End Sub

    Private Sub comboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBox.SelectionChanged
        textBox1.FontFamily = New FontFamily(comboBox.Text)
    End Sub

    Private Sub buttonFolder_Click(sender As Object, e As RoutedEventArgs) Handles buttonFolder.Click
        Dim dt = Calendar1.SelectedDate
        If Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day) Then
            Process.Start(System.AppDomain.CurrentDomain.BaseDirectory & "\" & DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day)
        End If
    End Sub

    Private Sub buttonCurrentDate_Click(sender As Object, e As RoutedEventArgs) Handles buttonCurrentDate.Click
        Calendar1.SelectedDate = Date.Now
        Calendar1.DisplayDate = Date.Now
    End Sub

    Private Sub textBox2_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox2.TextChanged
        Dim dt = Calendar1.SelectedDate


        Try
            If Not Directory.Exists(DiaryFolder) Then Directory.CreateDirectory(DiaryFolder)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year) Then Directory.CreateDirectory(DiaryFolder & "\" & dt.Value.Year)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month) Then Directory.CreateDirectory(DiaryFolder & "\" & "\" & dt.Value.Year & "\" & dt.Value.Month)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day) Then Directory.CreateDirectory(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day)

            File.WriteAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes2.txt", textBox2.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub textBox3_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox3.TextChanged
        Dim dt = Calendar1.SelectedDate


        Try
            If Not Directory.Exists(DiaryFolder) Then Directory.CreateDirectory(DiaryFolder)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year) Then Directory.CreateDirectory(DiaryFolder & "\" & dt.Value.Year)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month) Then Directory.CreateDirectory(DiaryFolder & "\" & "\" & dt.Value.Year & "\" & dt.Value.Month)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day) Then Directory.CreateDirectory(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day)

            File.WriteAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes3.txt", textBox3.Text)
        Catch ex As Exception

        End Try
    End Sub
End Class
