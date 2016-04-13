Public Class AbilityTransparent

    Public Property ParentWindow As Window

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If (e.ChangedButton = MouseButton.Left) Then DragMove()
    End Sub

    Private Sub image_MouseRightButtonUp(sender As Object, e As MouseButtonEventArgs) Handles image.MouseRightButtonUp
        Me.Hide()
        Me.ParentWindow.Show()
    End Sub
End Class
