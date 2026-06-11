Imports System.Diagnostics
Imports Microsoft.Win32

Public Class SettingsWindow

    Private suppressEvents As Boolean = False

    Const RunKeyPath As String = "Software\Microsoft\Windows\CurrentVersion\Run"
    Const RunValueName As String = "CarpeDiem"

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        suppressEvents = True

        comboTheme.Items.Clear()
        For Each themeName In ThemeManager.ThemeOrder
            comboTheme.Items.Add(themeName)
        Next
        comboTheme.SelectedItem = ThemeManager.CurrentTheme

        checkAlwaysOnTop.IsChecked = SettingsStore.Read("AlwaysOnTop", "False") = "True"
        check24Hour.IsChecked = SettingsStore.Read("Use24HourClock", "False") = "True"
        checkSounds.IsChecked = SettingsStore.Read("PlaySounds", "True") = "True"
        checkFastBars.IsChecked = SettingsStore.Read("FastProgressBars", "True") = "True"
        checkStartup.IsChecked = IsStartupEnabled()

        textPlanHours.Text = SettingsStore.Read("DefaultPlanHours", "")
        textFoodHours.Text = SettingsStore.Read("FoodHours", "16")
        textPomodoroWork.Text = SettingsStore.Read("PomodoroWork", "25")
        textPomodoroShort.Text = SettingsStore.Read("PomodoroShort", "5")
        textPomodoroLong.Text = SettingsStore.Read("PomodoroLong", "15")

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

    Private Sub check24Hour_Changed(sender As Object, e As RoutedEventArgs) Handles check24Hour.Checked, check24Hour.Unchecked
        If suppressEvents Then Return
        SettingsStore.Write("Use24HourClock", (check24Hour.IsChecked = True).ToString())
    End Sub

    Private Sub checkSounds_Changed(sender As Object, e As RoutedEventArgs) Handles checkSounds.Checked, checkSounds.Unchecked
        If suppressEvents Then Return
        Dim enabled = (checkSounds.IsChecked = True)
        SettingsStore.Write("PlaySounds", enabled.ToString())
        Functions.SoundsEnabled = enabled
    End Sub

    Private Sub checkFastBars_Changed(sender As Object, e As RoutedEventArgs) Handles checkFastBars.Checked, checkFastBars.Unchecked
        If suppressEvents Then Return
        SettingsStore.Write("FastProgressBars", (checkFastBars.IsChecked = True).ToString())
        Dim mw = TryCast(Application.Current.MainWindow, MainWindow)
        If mw IsNot Nothing Then mw.ApplyTimerInterval()
    End Sub

    Private Sub checkStartup_Changed(sender As Object, e As RoutedEventArgs) Handles checkStartup.Checked, checkStartup.Unchecked
        If suppressEvents Then Return
        SetStartupEnabled(checkStartup.IsChecked = True)
    End Sub

    Private Sub textPlanHours_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textPlanHours.TextChanged
        If suppressEvents Then Return
        SettingsStore.Write("DefaultPlanHours", textPlanHours.Text.Trim())
    End Sub

    Private Sub textFoodHours_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textFoodHours.TextChanged
        If suppressEvents Then Return
        SettingsStore.Write("FoodHours", textFoodHours.Text.Trim())
    End Sub

    Private Sub textPomodoroWork_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textPomodoroWork.TextChanged
        If suppressEvents Then Return
        SettingsStore.Write("PomodoroWork", textPomodoroWork.Text.Trim())
    End Sub

    Private Sub textPomodoroShort_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textPomodoroShort.TextChanged
        If suppressEvents Then Return
        SettingsStore.Write("PomodoroShort", textPomodoroShort.Text.Trim())
    End Sub

    Private Sub textPomodoroLong_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textPomodoroLong.TextChanged
        If suppressEvents Then Return
        SettingsStore.Write("PomodoroLong", textPomodoroLong.Text.Trim())
    End Sub

    Private Sub buttonOpenThemes_Click(sender As Object, e As RoutedEventArgs) Handles buttonOpenThemes.Click
        Dim w As New ThemeManagerWindow
        w.Show()
    End Sub

    Private Function IsStartupEnabled() As Boolean
        Try
            Using key = Registry.CurrentUser.OpenSubKey(RunKeyPath)
                Return key IsNot Nothing AndAlso key.GetValue(RunValueName) IsNot Nothing
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Sub SetStartupEnabled(enabled As Boolean)
        Try
            Using key = Registry.CurrentUser.OpenSubKey(RunKeyPath, writable:=True)
                If key Is Nothing Then Return
                If enabled Then
                    key.SetValue(RunValueName, """" & Process.GetCurrentProcess().MainModule.FileName & """")
                Else
                    key.DeleteValue(RunValueName, throwOnMissingValue:=False)
                End If
            End Using
        Catch
        End Try
    End Sub

End Class
