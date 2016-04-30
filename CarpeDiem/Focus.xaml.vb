Imports System.Windows.Threading

Public Class Focus

    ReadOnly timer1 As New DispatcherTimer
    Dim score As Integer

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        AddHandler timer1.Tick, AddressOf timer1_Tick
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value += 1
        Else
            ProgressBar1.Value = 0
            'timer1.Stop()
        End If
    End Sub

    Private Sub buttonStart_Click(sender As Object, e As RoutedEventArgs) Handles buttonStart.Click

        If buttonStart.Content = "Stop" Then
            timer1.Stop()
            buttonStart.Content = "Start"
            If ProgressBar1.Value > 95 Then
                Dim newscore = math.Abs(95 - ProgressBar1.Value)
                labelnewscore.Content = "+" + newscore.ToString()
                labelnewscore.Foreground = Brushes.Green
                score += newscore ' TODO: Add score streak for 5 points, add +5 on each streak hit.
                label.Content = "Score: " & score.ToString()
                Select Case newscore
                    Case 1
                        PlaySound("C:\Windows\Media\Windows Foreground.wav")
                    Case 2
                        PlaySound("C:\Windows\Media\Windows User Account Control.wav")
                    Case 3
                        PlaySound("C:\Windows\Media\Windows Hardware Insert.wav")
                    Case 4
                        PlaySound("C:\Windows\Media\Windows Unlock.wav")
                    Case 5
                        PlaySound("C:\Windows\Media\tada.wav")
                End Select
            Else
                Dim newscore = 0
                If ProgressBar1.Value < 10 Then
                    PlaySound("C:\Windows\Media\Windows Hardware Fail.wav")
                    newscore = -3
                Elseif ProgressBar1.Value < 50 then
                    PlaySound("C:\Windows\Media\Windows Battery Critical.wav")
                    newscore = -2
                Elseif ProgressBar1.Value < 95 then
                    PlaySound("C:\Windows\Media\Windows Battery Low.wav")
                    newscore = -1
                End If
                score += newscore
                labelnewscore.Content = newscore.ToString()
                labelnewscore.Foreground = Brushes.Red
                label.Content = "Score: " & score.ToString()
            End If

            If checkBoxAuto.IsChecked Then
                buttonStart_Click(Nothing, Nothing)
            End If
        Else
            timer1.Interval = New TimeSpan(0, 0, 0, 0, 1)
            timer1.IsEnabled = True
            timer1.Start()
            ProgressBar1.Value = 0
            buttonStart.Content = "Stop"
            If Not checkBoxAuto.IsChecked Then
                labelnewscore.Content = ""
            End If
        End If
    End Sub

    Private Sub buttonReset_Click(sender As Object, e As RoutedEventArgs) Handles buttonReset.Click
        timer1.Stop()
        ProgressBar1.Value = 0
        score = 0
        label.Content = "Score: " & score.ToString()
    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If (e.ChangedButton = MouseButton.Left) Then DragMove()
    End Sub
End Class
