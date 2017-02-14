Imports System.Windows.Threading
Imports CarpeDiem.Core
Public Class Ability

    ReadOnly timer1 As New DispatcherTimer
    Dim targetTime As New Date
    Dim totalSeconds As Integer
    Dim abilityTransparent As AbilityTransparent

    Private Sub Ability_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        AddHandler timer1.Tick, AddressOf timer1_Tick
        timer1.Interval = New TimeSpan(0, 0, 1)
        If abilityTransparent Is Nothing Then
            abilityTransparent = New AbilityTransparent
            abilityTransparent.ParentWindow = Me
        End If

        For Each file As String In IO.Directory.GetFiles(My.Application.Info.DirectoryPath & "/images/")
            comboBoxImage.Items.Add(IO.Path.GetFileName(file))
        Next

        Me.checkBoxTopMost.IsChecked = True
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        ' change progress bar value according to time difference
        ProgressBar1.Value = (100 * targetTime.Subtract(Date.Now).TotalSeconds / totalSeconds)
        abilityTransparent.ProgressBar1.Value = ProgressBar1.Value
        Debug.Print(ProgressBar1.Value)
        If ProgressBar1.Value = 0 Then
            timer1.Stop()
            'MsgBox("Finished!", MsgBoxStyle.Information)
            PlaySound("C:\Windows\Media\tada.wav")

            abilityTransparent.ProgressBar1.Visibility = Visibility.Hidden
            If not checkBoxRepeat.IsChecked Then
                Me.Close
            End If
        End If
    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If (e.ChangedButton = MouseButton.Left) Then DragMove()
    End Sub

    Private Sub buttonStart_Click(sender As Object, e As RoutedEventArgs) Handles buttonStart.Click
        StartAbility()
    End Sub

    Public Sub StartAbility()
        ' Get time difference
        If textBoxTime.Text = "" Then textBoxTime.Text = 1
        targetTime = TimeManagement.GetTimeAfterMinutes(textBoxTime.Text)
        totalSeconds = targetTime.Subtract(Date.Now).TotalSeconds
        ProgressBar1.Value = 100
        abilityTransparent.ProgressBar1.Visibility = Visibility.Visible
        PlaySound("C:\Windows\Media\Windows Unlock.wav")
        timer1.Start()
    End Sub

    Private Sub image_MouseRightButtonUp(sender As Object, e As MouseButtonEventArgs) Handles image.MouseRightButtonUp

        abilityTransparent.Left = Me.Left + 371
        abilityTransparent.Top = Me.Top + 50
        abilityTransparent.Show()
        Me.Hide()

    End Sub

    Private Sub checkBoxTopMost_Checked(sender As Object, e As RoutedEventArgs) Handles checkBoxTopMost.Checked
        abilityTransparent.Topmost = True
    End Sub

    Private Sub checkBoxTopMost_Unchecked(sender As Object, e As RoutedEventArgs) Handles checkBoxTopMost.Unchecked
        abilityTransparent.Topmost = False
    End Sub

    Private Sub textBoxImage_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxImage.TextChanged

        Try
            Dim imgpath = My.Application.Info.DirectoryPath & "/images/" & textBoxImage.Text & ".png"

            image.Source = New BitmapImage(New Uri(imgpath))
            abilityTransparent.image.Source = image.Source
        Catch
        End Try
    End Sub

    Private Sub comboBoxImage_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBoxImage.SelectionChanged
        Try
            Dim imgpath = My.Application.Info.DirectoryPath & "/images/" & e.AddedItems(0)
            Debug.Print(comboBoxImage.Text)
            image.Source = New BitmapImage(New Uri(imgpath))
            abilityTransparent.image.Source = image.Source
        Catch
        End Try
    End Sub

    Private Sub Ability_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        abilityTransparent.Close
    End Sub
End Class
