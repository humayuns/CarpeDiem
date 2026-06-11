Imports System.Runtime.InteropServices
Imports System.Windows.Interop

''' <summary>
''' Switches the application-wide color theme (Original / Soft / Dark) and keeps
''' every window's title bar in sync with it.
''' </summary>
Public Module ThemeManager

    Public ReadOnly ThemeOrder As String() = {"Original", "Soft", "Dark"}

    Private _currentTheme As String = "Original"

    Public ReadOnly Property CurrentTheme As String
        Get
            Return _currentTheme
        End Get
    End Property

    Public Function NextTheme() As String
        Return ThemeOrder((Array.IndexOf(ThemeOrder, _currentTheme) + 1) Mod ThemeOrder.Length)
    End Function

    Public Sub ApplyTheme(theme As String)
        _currentTheme = theme
        Dim themeDict As New ResourceDictionary With {
            .Source = New Uri("Themes/" & theme & "Theme.xaml", UriKind.Relative)
        }
        Application.Current.Resources.MergedDictionaries(0) = themeDict

        For Each w As Window In Application.Current.Windows
            ApplyTitleBar(w)
        Next
    End Sub

    ' DWMWA_USE_IMMERSIVE_DARK_MODE — supported on Windows 10 1809+ / Windows 11.
    <DllImport("dwmapi.dll")>
    Private Function DwmSetWindowAttribute(hwnd As IntPtr, attribute As Integer, ByRef value As Integer, size As Integer) As Integer
    End Function

    Public Sub ApplyTitleBar(w As Window)
        Try
            Dim hwnd As IntPtr = New WindowInteropHelper(w).Handle
            If hwnd = IntPtr.Zero Then Return
            Dim value As Integer = If(_currentTheme = "Dark", 1, 0)
            DwmSetWindowAttribute(hwnd, 20, value, 4)
        Catch
        End Try
    End Sub

End Module
