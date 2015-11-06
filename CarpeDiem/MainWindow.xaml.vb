Imports System.Windows.Threading

Class MainWindow

    dim timer1 as New DispatcherTimer
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private sub timer1_Tick(sender As Object, e As EventArgs)
        Progressbar1.Value += 10
        If Progressbar1.Value >= 100 Then
            timer1.IsEnabled = False
            Progressbar1.Value = 0
        End If
    End sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        AddHandler timer1.Tick, AddressOf timer1_Tick
        timer1.Interval = new TimeSpan(0,0,1)
        timer1.Start()
    End Sub
End Class
