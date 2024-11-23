Public Class AbilityTransparent

    Public Property ParentWindow As Ability

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If (e.ChangedButton = MouseButton.Left) Then DragMove()
    End Sub

    Private Sub image_MouseRightButtonUp(sender As Object, e As MouseButtonEventArgs) Handles image.MouseRightButtonUp
        Me.Hide()
        Me.ParentWindow.Show()
    End Sub


    Private Sub Window_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.Key
            Case Key.OemPlus
                Me.Width += 10
                Me.Height += 10
                Me.image.Width += 10
                Me.image.Height += 10
            Case Key.OemMinus
                Me.Width -= 10
                Me.Height -= 10
                Me.image.Width -= 10
                Me.image.Height -= 10
            Case Key.Enter
                Me.ParentWindow.StartAbility()
            Case Key.D
                Me.ParentWindow.DuplicateAbility()
            Case Key.Q
                ToggleProgressBarSize()
        End Select
    End Sub

    Private Sub ToggleProgressBarSize()
        If ProgressBar1.Height = 10 Then
            ProgressBar1.Height = 2
        Else
            ProgressBar1.Height = 10
        End If
    End Sub
End Class
