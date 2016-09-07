Imports System.IO

Public Class Diary


    Private Sub Calendar1_SelectedDatesChanged(sender As Object, e As SelectionChangedEventArgs) Handles Calendar1.SelectedDatesChanged

        Dim dt = Calendar1.SelectedDate
        If File.Exists("Diary\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & ".txt") Then
            textBox.Text = File.ReadAllText("Diary\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & ".txt")
        Else
            textBox.Text = ""
        End If



    End Sub


    Private Sub textBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox.TextChanged

        Dim dt = Calendar1.SelectedDate


        Try
            If Not Directory.Exists("Diary") Then Directory.CreateDirectory("Diary")
            If Not Directory.Exists("Diary\" & dt.Value.Year) Then Directory.CreateDirectory("Diary\" & dt.Value.Year)
            If Not Directory.Exists("Diary\" & dt.Value.Year & "\" & dt.Value.Month) Then Directory.CreateDirectory("Diary\" & "\" & dt.Value.Year & "\" & dt.Value.Month)

            File.WriteAllText("Diary\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & ".txt", textBox.Text)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Calendar1.SelectedDate = Now

        If File.Exists("Diary\specialsymbols.txt") Then
            textBoxSpecial.Text = File.ReadAllText("Diary\specialsymbols.txt")
        End If
    End Sub

    Private Sub textBoxSpecial_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxSpecial.TextChanged
        If Not Directory.Exists("Diary") Then Directory.CreateDirectory("Diary")
        File.WriteAllText("Diary\specialsymbols.txt", textBoxSpecial.Text)
    End Sub
End Class
