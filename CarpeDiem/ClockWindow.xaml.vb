Imports System.Windows.Threading

Public Class ClockWindow

    Dim timer1 As New DispatcherTimer
    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Me.DragMove()
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        AddHandler timer1.Tick, AddressOf timer1_Tick
        timer1.Interval = New TimeSpan(0, 0, 1)
        timer1.Start()
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        SecondsAngle.Angle = DateTime.Now.Second * 6
        MinutesAngle.Angle = DateTime.Now.Minute * 6
        HoursAngle.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5)
    End Sub
End Class
