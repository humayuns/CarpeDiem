Imports System.Windows.Threading

Public Class Pomodoro

    Private Enum PhaseKind
        Work
        ShortBreak
        LongBreak
    End Enum

    Const WorkMinutes As Double = 25
    Const ShortBreakMinutes As Double = 5
    Const LongBreakMinutes As Double = 15
    Const SessionsPerCycle As Integer = 4

    ReadOnly timer1 As New DispatcherTimer With {.Interval = TimeSpan.FromMilliseconds(250)}
    Dim phase As PhaseKind = PhaseKind.Work
    Dim sessionsDone As Integer = 0
    Dim phaseEnd As Date
    Dim remaining As TimeSpan = TimeSpan.FromMinutes(WorkMinutes)
    Dim running As Boolean = False

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        AddHandler timer1.Tick, AddressOf timer1_Tick
        UpdateUi()
    End Sub

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
            Case PhaseKind.ShortBreak : Return TimeSpan.FromMinutes(ShortBreakMinutes)
            Case PhaseKind.LongBreak : Return TimeSpan.FromMinutes(LongBreakMinutes)
            Case Else : Return TimeSpan.FromMinutes(WorkMinutes)
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
