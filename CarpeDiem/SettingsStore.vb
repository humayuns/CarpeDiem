Imports System.IO

''' <summary>
''' Tiny key=value settings file persisted next to the executable
''' (matching how the diary folders are stored).
''' </summary>
Public Module SettingsStore

    Private ReadOnly SettingsPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CarpeDiem.settings")
    Private ReadOnly values As New Dictionary(Of String, String)
    Private loaded As Boolean = False

    Private Sub EnsureLoaded()
        If loaded Then Return
        loaded = True
        Try
            If File.Exists(SettingsPath) Then
                For Each line In File.ReadAllLines(SettingsPath)
                    Dim idx = line.IndexOf("="c)
                    If idx > 0 Then values(line.Substring(0, idx)) = line.Substring(idx + 1)
                Next
            End If
        Catch
        End Try
    End Sub

    Public Function Read(key As String, defaultValue As String) As String
        EnsureLoaded()
        If values.ContainsKey(key) Then Return values(key)
        Return defaultValue
    End Function

    Public Sub Write(key As String, value As String)
        EnsureLoaded()
        values(key) = value
        Try
            File.WriteAllLines(SettingsPath, values.Select(Function(kv) kv.Key & "=" & kv.Value))
        Catch
        End Try
    End Sub

End Module
