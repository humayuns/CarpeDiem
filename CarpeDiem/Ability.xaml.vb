Imports System.Windows.Threading
Imports CarpeDiem.Core
Public Class Ability

    ReadOnly timer1 As New DispatcherTimer
    Dim targetTime As New Date
    Dim totalSeconds As Integer
    Dim abilityTransparent As AbilityTransparent
    Dim counter As Integer = 0

    Private Sub Ability_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        AddHandler timer1.Tick, AddressOf timer1_Tick
        timer1.Interval = New TimeSpan(0, 0, 1)
        If abilityTransparent Is Nothing Then
            abilityTransparent = New AbilityTransparent
            abilityTransparent.ParentWindow = Me
        End If

        For Each file As String In IO.Directory.GetFiles(My.Application.Info.DirectoryPath & "/images/")
            comboBoxImage.Items.Add(IO.Path.GetFileName(file))
        Next

        Me.checkBoxTopMost.IsChecked = True
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        ' change progress bar value according to time difference
        ProgressBar1.Value = (100 * targetTime.Subtract(Date.Now).TotalSeconds / totalSeconds)
        abilityTransparent.ProgressBar1.Value = ProgressBar1.Value
        Dim remainingTime = targetTime.Subtract(Date.Now)
        abilityTransparent.ToolTip = remainingTime.ToString("hh\:mm\:ss")

        Debug.Print(ProgressBar1.Value)
        If ProgressBar1.Value = 0 Then

            timer1.Stop()
            PlaySound("C:\Windows\Media\tada.wav")
            abilityTransparent.ProgressBar1.Visibility = Visibility.Hidden
            counter += 1
            Me.Title = "Ability (+" & counter & ")"
            If checkBoxAuto.IsChecked Then StartAbility()
            If Not checkBoxRepeat.IsChecked Then Close()

        End If
    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If (e.ChangedButton = MouseButton.Left) Then DragMove()
    End Sub

    Private Sub buttonStart_Click(sender As Object, e As RoutedEventArgs) Handles buttonStart.Click
        StartAbility()
    End Sub

    Public Sub StartAbility()
        ' Get time difference
        If textBoxTime.Text = "" Then textBoxTime.Text = 1
        targetTime = TimeManagement.GetTimeAfterMinutes(textBoxTime.Text)
        totalSeconds = targetTime.Subtract(Date.Now).TotalSeconds
        ProgressBar1.Value = 100
        abilityTransparent.ProgressBar1.Visibility = Visibility.Visible
        PlaySound("C:\Windows\Media\Windows Unlock.wav")
        timer1.Start()
    End Sub

    ' Offset of the icon preview within the window (including chrome), measured each
    ' time the window floats so the icon and window swap in exactly the same spot.
    Private iconOffsetX As Double = 350
    Private iconOffsetY As Double = 40

    Private Sub image_MouseRightButtonUp(sender As Object, e As MouseButtonEventArgs) Handles image.MouseRightButtonUp
        ShowFloatingIcon()
    End Sub

    Private Sub buttonFloat_Click(sender As Object, e As RoutedEventArgs) Handles buttonFloat.Click
        ShowFloatingIcon()
    End Sub

    Public Sub ShowFloatingIcon()
        Try
            Dim source = PresentationSource.FromVisual(Me)
            If source IsNot Nothing Then
                Dim devicePos = image.PointToScreen(New Point(0, 0))
                Dim dipPos = source.CompositionTarget.TransformFromDevice.Transform(devicePos)
                iconOffsetX = dipPos.X - Me.Left
                iconOffsetY = dipPos.Y - Me.Top
            End If
        Catch
        End Try

        abilityTransparent.Left = Me.Left + iconOffsetX
        abilityTransparent.Top = Me.Top + iconOffsetY
        abilityTransparent.Show()
        Me.Hide()
    End Sub

    ''' <summary>
    ''' Shows the window positioned so its icon preview sits exactly where the
    ''' floating icon was, even after the icon has been dragged around.
    ''' </summary>
    Public Sub ShowAtIcon(iconLeft As Double, iconTop As Double)
        Dim newLeft = iconLeft - iconOffsetX
        Dim newTop = iconTop - iconOffsetY

        ' Keep the window on the working area.
        Dim work = SystemParameters.WorkArea
        newLeft = Math.Max(work.Left, Math.Min(newLeft, work.Right - Me.Width))
        newTop = Math.Max(work.Top, Math.Min(newTop, work.Bottom - Me.Height))

        Me.Left = newLeft
        Me.Top = newTop
        Me.Show()
        Me.Activate()
    End Sub

    Private Sub checkBoxTopMost_Checked(sender As Object, e As RoutedEventArgs) Handles checkBoxTopMost.Checked
        abilityTransparent.Topmost = True
    End Sub

    Private Sub checkBoxTopMost_Unchecked(sender As Object, e As RoutedEventArgs) Handles checkBoxTopMost.Unchecked
        abilityTransparent.Topmost = False
    End Sub

    Private Sub textBoxImage_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxImage.TextChanged

        Try
            Dim imgpath = My.Application.Info.DirectoryPath & "/images/" & textBoxImage.Text & ".png"

            image.Source = New BitmapImage(New Uri(imgpath))
            abilityTransparent.image.Source = image.Source
        Catch
        End Try
    End Sub

    Private Sub comboBoxImage_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBoxImage.SelectionChanged
        UpdateAbilityIcon(e.AddedItems(0), abilityTransparent)
    End Sub

    Public Sub UpdateAbilityIcon(imgName As String, at As AbilityTransparent)
        Try
            Dim imgpath = My.Application.Info.DirectoryPath & "/images/" & imgName
            Debug.Print(comboBoxImage.Text)
            image.Source = New BitmapImage(New Uri(imgpath))
            at.image.Source = image.Source
        Catch
        End Try
    End Sub

    Private Sub Ability_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        abilityTransparent.Close()
    End Sub


    Public Sub DuplicateAbility()
        ' Create a new instance of the Ability window
        Dim newAbility As New Ability

        ' Set the position of the new window similar to the current one
        newAbility.Left = Me.Left + 20 ' Offset slightly for visibility
        newAbility.Top = Me.Top + 20

        ' Set the same time duration
        newAbility.textBoxTime.Text = Me.textBoxTime.Text

        ' Create the associated transparent window
        If newAbility.abilityTransparent Is Nothing Then
            newAbility.abilityTransparent = New AbilityTransparent With {
                .ParentWindow = newAbility,
                .Topmost = True
            }
        End If

        ' Set the same selected image
        If Not String.IsNullOrEmpty(comboBoxImage.Text) Then
            newAbility.textBoxImage.Text = comboBoxImage.Text
            newAbility.comboBoxImage.Text = comboBoxImage.Text
            newAbility.UpdateAbilityIcon(comboBoxImage.Text, newAbility.abilityTransparent)

        End If



        ' Show the duplicate, then collapse it to its floating icon in the same spot.
        newAbility.Show()
        newAbility.ShowFloatingIcon()
        'newAbility.StartAbility()
    End Sub

End Class
