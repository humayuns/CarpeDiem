Public Class SettingsWindow

    Private suppressEvents As Boolean = False

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        suppressEvents = True
        comboTheme.Items.Clear()
        For Each themeName In ThemeManager.ThemeOrder
            comboTheme.Items.Add(themeName)
        Next
        comboTheme.SelectedItem = ThemeManager.CurrentTheme
        checkAlwaysOnTop.IsChecked = SettingsStore.Read("AlwaysOnTop", "False") = "True"
        suppressEvents = False
    End Sub

    Private Sub comboTheme_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboTheme.SelectionChanged
        If suppressEvents OrElse comboTheme.SelectedItem Is Nothing Then Return
        ThemeManager.ApplyTheme(comboTheme.SelectedItem.ToString())
        Dim mw = TryCast(Application.Current.MainWindow, MainWindow)
        If mw IsNot Nothing Then mw.UpdateThemeButton()
    End Sub

    Private Sub checkAlwaysOnTop_Changed(sender As Object, e As RoutedEventArgs) Handles checkAlwaysOnTop.Checked, checkAlwaysOnTop.Unchecked
        If suppressEvents Then Return
        Dim onTop = (checkAlwaysOnTop.IsChecked = True)
        SettingsStore.Write("AlwaysOnTop", onTop.ToString())
        Dim mw = TryCast(Application.Current.MainWindow, MainWindow)
        If mw IsNot Nothing Then
            mw.Topmost = onTop
            mw.menuItemAlwaysOnTop.IsChecked = onTop
        End If
    End Sub

    Private Sub buttonOpenThemes_Click(sender As Object, e As RoutedEventArgs) Handles buttonOpenThemes.Click
        Dim w As New ThemeManagerWindow
        w.Show()
    End Sub

End Class
