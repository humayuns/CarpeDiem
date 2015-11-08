Public Class SpriteWindow


    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If (e.ChangedButton = MouseButton.Left) Then Me.DragMove()
    End Sub

End Class
