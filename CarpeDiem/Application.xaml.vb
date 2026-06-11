Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        ' Keep every window's title bar in sync with the active theme,
        ' including windows opened after a theme switch.
        EventManager.RegisterClassHandler(GetType(Window), FrameworkElement.LoadedEvent,
                                          New RoutedEventHandler(AddressOf OnAnyWindowLoaded))
    End Sub

    Private Sub OnAnyWindowLoaded(sender As Object, e As RoutedEventArgs)
        Dim w = TryCast(sender, Window)
        If w IsNot Nothing Then ThemeManager.ApplyTitleBar(w)
    End Sub

End Class
