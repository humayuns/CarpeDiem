Module Functions

    ''' <summary>Global mute switch, controlled from the Settings window.</summary>
    Public Property SoundsEnabled As Boolean = True

    Sub PlaySound(path As String)
        If Not SoundsEnabled Then Return
        Dim m = New MediaPlayer
        m.Open(New Uri(path))
        m.Play()
    End Sub
End Module
