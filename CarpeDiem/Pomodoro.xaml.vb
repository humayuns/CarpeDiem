Imports System.Windows.Threading

Public Class Pomodoro

    Private Enum PhaseKind
        Work
        ShortBreak
        LongBreak
    End Enum

    Const SessionsPerCycle As Integer = 4

    ' Durations are configurable in the Settings window.
    Dim workMinutes As Double = 25
    Dim shortBreakMinutes As Double = 5
    Dim longBreakMinutes As Double = 15

    ReadOnly timer1 As New DispatcherTimer With {.Interval = TimeSpan.FromMilliseconds(250)}
    Dim phase As PhaseKind = PhaseKind.Work
    Dim sessionsDone As Integer = 0
    Dim phaseEnd As Date
    Dim remaining As TimeSpan = TimeSpan.FromMinutes(25)
    Dim running As Boolean = False

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        workMinutes = ReadMinutesSetting("PomodoroWork", 25)
        shortBreakMinutes = ReadMinutesSetting("PomodoroShort", 5)
        longBreakMinutes = ReadMinutesSetting("PomodoroLong", 15)
        remaining = TimeSpan.FromMinutes(workMinutes)

        AddHandler timer1.Tick, AddressOf timer1_Tick
        UpdateUi()
    End Sub

    Private Function ReadMinutesSetting(key As String, fallback As Double) As Double
        Dim value As Double
        If Double.TryParse(SettingsStore.Read(key, fallback.ToString()), value) AndAlso value > 0 Then Return value
        Return fallback
    End Function

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        remaining = phaseEnd.Subtract(Now)
        If remaining.TotalSeconds <= 0 Then
            PlaySound("C:\Windows\Media\tada.wav")
            AdvancePhase()
        Else
            UpdateUi()
        End If
    End Sub

    Private Sub buttonStart_Click(sender As Object, e As RoutedEventArgs) Handles buttonStart.Click
        If running Then
            remaining = phaseEnd.Subtract(Now)
            If remaining < TimeSpan.Zero Then remaining = TimeSpan.Zero
            running = False
            timer1.Stop()
        Else
            phaseEnd = Now.Add(remaining)
            running = True
            timer1.Start()
        End If
        UpdateUi()
    End Sub

    Private Sub buttonReset_Click(sender As Object, e As RoutedEventArgs) Handles buttonReset.Click
        running = False
        timer1.Stop()
        phase = PhaseKind.Work
        sessionsDone = 0
        remaining = PhaseDuration(phase)
        UpdateUi()
    End Sub

    Private Sub buttonSkip_Click(sender As Object, e As RoutedEventArgs) Handles buttonSkip.Click
        AdvancePhase()
    End Sub

    Private Sub AdvancePhase()
        If phase = PhaseKind.Work Then
            sessionsDone += 1
            phase = If(sessionsDone Mod SessionsPerCycle = 0, PhaseKind.LongBreak, PhaseKind.ShortBreak)
        Else
            phase = PhaseKind.Work
        End If

        remaining = PhaseDuration(phase)
        If running Then phaseEnd = Now.Add(remaining)
        UpdateUi()
    End Sub

    Private Function PhaseDuration(kind As PhaseKind) As TimeSpan
        Select Case kind
            Case PhaseKind.ShortBreak : Return TimeSpan.FromMinutes(shortBreakMinutes)
            Case PhaseKind.LongBreak : Return TimeSpan.FromMinutes(longBreakMinutes)
            Case Else : Return TimeSpan.FromMinutes(workMinutes)
        End Select
    End Function

    Private Sub UpdateUi()
        labelTimeLeft.Content = remaining.ToString("mm\:ss")
        labelSessions.Content = "Done: " & sessionsDone
        buttonStart.Content = If(running, "Pause", "Start")

        Select Case phase
            Case PhaseKind.Work
                labelPhase.Content = "Work"
                ProgressBar1.SetResourceReference(ForegroundProperty, "ButtonCloseBrush")
            Case PhaseKind.ShortBreak
                labelPhase.Content = "Short Break"
                ProgressBar1.SetResourceReference(ForegroundProperty, "BarYearBrush")
            Case PhaseKind.LongBreak
                labelPhase.Content = "Long Break"
                ProgressBar1.SetResourceReference(ForegroundProperty, "BarYearBrush")
        End Select

        Dim total = PhaseDuration(phase).TotalSeconds
        ProgressBar1.Value = Math.Max(0, Math.Min(100, 100 * (1 - remaining.TotalSeconds / total)))
    End Sub

    Private Sub Pomodoro_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        timer1.Stop()
    End Sub

End Class
