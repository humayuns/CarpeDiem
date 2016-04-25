Module Functions
    Sub PlaySound(path As String)
        Dim m = New MediaPlayer
        m.Open(New Uri(path))
        m.Play()
    End Sub
End Module
