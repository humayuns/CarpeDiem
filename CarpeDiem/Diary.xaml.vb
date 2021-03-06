﻿Imports System.IO
Imports System.Windows.Media.Animation

Public Class Diary


    Const DiaryFolder As String = "Diary"
    Private Sub Calendar1_SelectedDatesChanged(sender As Object, e As SelectionChangedEventArgs) Handles Calendar1.SelectedDatesChanged

        Dim dt = Calendar1.SelectedDate
        If File.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes.txt") Then
            textBox.Text = File.ReadAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes.txt")
        Else
            textBox.Text = ""
        End If

        Dim ca = New ColorAnimation(Colors.White, New Duration(TimeSpan.FromMilliseconds(500)))
        textBox.Background = New SolidColorBrush(Colors.Azure)
        textBox.Background.BeginAnimation(SolidColorBrush.ColorProperty, ca)


    End Sub


    Private Sub textBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox.TextChanged

        Dim dt = Calendar1.SelectedDate


        Try
            If Not Directory.Exists(DiaryFolder) Then Directory.CreateDirectory(DiaryFolder)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year) Then Directory.CreateDirectory(DiaryFolder & "\" & dt.Value.Year)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month) Then Directory.CreateDirectory(DiaryFolder & "\" & "\" & dt.Value.Year & "\" & dt.Value.Month)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day) Then Directory.CreateDirectory(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day)

            File.WriteAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes.txt", textBox.Text)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Calendar1.SelectedDate = Now

        If File.Exists(DiaryFolder & "\specialsymbols.txt") Then
            textBoxSpecial.Text = File.ReadAllText(DiaryFolder & "\specialsymbols.txt")
        End If

        comboBox.Text = "Segoe UI"
    End Sub

    Private Sub textBoxSpecial_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxSpecial.TextChanged
        If Not Directory.Exists(DiaryFolder) Then Directory.CreateDirectory(DiaryFolder)
        File.WriteAllText(DiaryFolder & "\specialsymbols.txt", textBoxSpecial.Text)
    End Sub

    Private Sub textBoxFontSize_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxFontSize.TextChanged
        textBox.FontSize = textBoxFontSize.Text
    End Sub

    Private Sub comboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBox.SelectionChanged
        textBox.FontFamily = New FontFamily(comboBox.Text)
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
End Class
