Imports System.IO
Imports System.Windows.Media.Animation
Imports System.Windows.Threading
Imports CarpeDiem.Core

Public Class Diary2

    ReadOnly timer1 As New DispatcherTimer
    Const DiaryFolder As String = "Diary2"
    Dim currentDate As Date?
    Dim todayDate As String = ""
    Dim fileChanged As Boolean = False
    Dim fsWatcher As New FileSystemWatcher()
    Private Sub Calendar1_SelectedDatesChanged(sender As Object, e As SelectionChangedEventArgs) Handles Calendar1.SelectedDatesChanged

        Dim dt = Calendar1.SelectedDate


        If dt.Value.ToShortDateString() = DateTime.Now.ToShortDateString() Then
            buttonCurrentDate.Background = Brushes.LightBlue
        Else
            buttonCurrentDate.Background = Brushes.Yellow
        End If

        currentDate = dt

        Dim dayFolder = DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day

        textBoxDay.Text = GetFileText(dayFolder & "\notes.txt")
        textBox1.Text = GetFileText(dayFolder & "\notes1.txt")
        textBox2.Text = GetFileText(dayFolder & "\notes2.txt")
        textBox3.Text = GetFileText(dayFolder & "\notes3.txt")
        textBoxMonoSpace.Text = GetFileText(dayFolder & "\monospace.txt")

        If File.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\ideas.rtf") Then
            Try
                Dim TextRange = New TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd)
                Using fs = New System.IO.FileStream(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\ideas.rtf", System.IO.FileMode.OpenOrCreate)
                    richTextBox.Document.Blocks.Clear()
                    TextRange.Load(fs, System.Windows.DataFormats.Rtf)
                End Using
            Catch ex As Exception

            End Try
        Else
            richTextBox.Document.Blocks.Clear()
        End If

        Dim ca = New ColorAnimation(Colors.White, New Duration(TimeSpan.FromMilliseconds(500)))
        textBox1.Background = New SolidColorBrush(Colors.Azure)
        textBox1.Background.BeginAnimation(SolidColorBrush.ColorProperty, ca)

        textBox1.Background = New SolidColorBrush(Colors.White)
        textBox2.Background = New SolidColorBrush(Colors.White)
        textBox3.Background = New SolidColorBrush(Colors.White)

        Select Case Core.TimeManagement.GetDaySection(Now)
            Case "1"
                textBox1.Background = New SolidColorBrush(Colors.Bisque)
            Case "2"
                textBox2.Background = New SolidColorBrush(Colors.Bisque)
            Case "3"
                textBox3.Background = New SolidColorBrush(Colors.Bisque)
        End Select

        Try
            fsWatcher.Path = My.Application.Info.DirectoryPath & "\" & DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day
        Catch ex As Exception

        End Try


    End Sub


    Private Function GetFileText(filePath As String) As String
        If File.Exists(filePath) Then
            Return File.ReadAllText(filePath)
        Else
            Return ""
        End If
    End Function

    Private Sub textBox1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox1.TextChanged

        Dim dt = Calendar1.SelectedDate


        Try
            If Not Directory.Exists(DiaryFolder) Then Directory.CreateDirectory(DiaryFolder)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year) Then Directory.CreateDirectory(DiaryFolder & "\" & dt.Value.Year)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month) Then Directory.CreateDirectory(DiaryFolder & "\" & "\" & dt.Value.Year & "\" & dt.Value.Month)
            If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day) Then Directory.CreateDirectory(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day)

            File.WriteAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes1.txt", textBox1.Text)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Calendar1.SelectedDate = Now

        If File.Exists(DiaryFolder & "\specialsymbols.txt") Then
            textBoxSpecial.Text = File.ReadAllText(DiaryFolder & "\specialsymbols.txt")
        End If

        comboBox.Text = "Segoe UI"
        AddHandler timer1.Tick, AddressOf timer1_Tick
        timer1.Interval = New TimeSpan(0, 0, 1)
        timer1.Start()

        todayDate = Now.ToShortDateString()

        Try
            SetupFileSystemWatcher()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetupFileSystemWatcher()

        ' add the handler to each event
        AddHandler fsWatcher.Changed, AddressOf fsWatcher_Changed
        AddHandler fsWatcher.Created, AddressOf fsWatcher_Changed
        AddHandler fsWatcher.Deleted, AddressOf fsWatcher_Changed

        'Set this property to true to start watching
        fsWatcher.EnableRaisingEvents = True
    End Sub

    Private Sub fsWatcher_Changed(sender As Object, e As FileSystemEventArgs)

        If e.ChangeType = WatcherChangeTypes.Changed Then
            fileChanged = True
        End If
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
        textBox1.Background = New SolidColorBrush(Colors.White)
        textBox2.Background = New SolidColorBrush(Colors.White)
        textBox3.Background = New SolidColorBrush(Colors.White)

        Dim sectionStartTime = TimeManagement.SectionStartingTime(Now)
        Dim sectionEndTime = TimeManagement.SectionEndingTime(Now)
        Dim diff = TimeManagement.GetDifferenceTimespan(Now, sectionEndTime)
        ProgressBar1.Value = TimeManagement.GetDifferencePercentage(sectionStartTime, sectionEndTime)
        Progressbar1Text.Text = (ProgressBar1.Value / 100).ToString("p")
        Dim timeString = $"{diff.Hours}:{diff.Minutes}:{diff.Seconds}"
        ProgressBar1.ToolTip = $"{Progressbar1Text.Text} - {timeString}"

        Select Case Core.TimeManagement.GetDaySection(Now)
            Case "1"
                textBox1.Background = New SolidColorBrush(Colors.Bisque)
            Case "2"
                textBox2.Background = New SolidColorBrush(Colors.Bisque)
            Case "3"
                textBox3.Background = New SolidColorBrush(Colors.Bisque)
        End Select


        If fileChanged Then
            Dim dt = currentDate


            Dim dayFolder = DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day

            textBoxDay.Text = GetFileText(dayFolder & "\notes.txt")
            textBox1.Text = GetFileText(dayFolder & "\notes1.txt")
            textBox2.Text = GetFileText(dayFolder & "\notes2.txt")
            textBox3.Text = GetFileText(dayFolder & "\notes3.txt")
            textBoxMonoSpace.Text = GetFileText(dayFolder & "\monospace.txt")

            Try

                If File.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\ideas.rtf") Then
                    Dim TextRange = New TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd)
                    Using fs = New System.IO.FileStream(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\ideas.rtf", System.IO.FileMode.OpenOrCreate)
                        richTextBox.Document.Blocks.Clear()
                        TextRange.Load(fs, System.Windows.DataFormats.Rtf)
                    End Using
                Else
                    richTextBox.Document.Blocks.Clear()
                End If

            Catch ex As Exception

            End Try

            fileChanged = False
        End If

        Me.Title = "Diary2 - " & Now.ToLongTimeString() & " - " & Now.ToLongDateString()

        ' Check for day change
        If todayDate <> Now.ToShortDateString() Then
            Calendar1.SelectedDate = Date.Now
            Calendar1.DisplayDate = Date.Now

            todayDate = Now.ToShortDateString()

            ' Automatically open smooth draw doc
            CopyTemplateAndStart("ideas.sddoc")
        End If

    End Sub

    Private Sub textBoxSpecial_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxSpecial.TextChanged
        If Not Directory.Exists(DiaryFolder) Then Directory.CreateDirectory(DiaryFolder)
        File.WriteAllText(DiaryFolder & "\specialsymbols.txt", textBoxSpecial.Text)
    End Sub

    Private Sub textBoxFontSize_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxFontSize.TextChanged
        Try
            textBox1.FontSize = textBoxFontSize.Text
            textBox2.FontSize = textBoxFontSize.Text
            textBox3.FontSize = textBoxFontSize.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub comboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBox.SelectionChanged
        Dim newFont As String = ""
        newFont = e.AddedItems(0).ToString()
        textBox1.FontFamily = New FontFamily(newFont)
        textBox2.FontFamily = New FontFamily(newFont)
        textBox3.FontFamily = New FontFamily(newFont)
    End Sub

    Private Sub buttonFolder_Click(sender As Object, e As RoutedEventArgs) Handles buttonFolder.Click
        Dim dt = Calendar1.SelectedDate
        CreateDirectoryIfNotExists()
        If Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day) Then
            Process.Start(AppDomain.CurrentDomain.BaseDirectory & "\" & DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day)
        End If
    End Sub

    Private Sub buttonCurrentDate_Click(sender As Object, e As RoutedEventArgs) Handles buttonCurrentDate.Click
        Calendar1.SelectedDate = Date.Now
        Calendar1.DisplayDate = Date.Now
    End Sub

    Private Sub textBox2_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox2.TextChanged
        Dim dt = Calendar1.SelectedDate


        Try
            CreateDirectoryIfNotExists()
            File.WriteAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes2.txt", textBox2.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub textBox3_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox3.TextChanged
        Dim dt = Calendar1.SelectedDate


        Try
            CreateDirectoryIfNotExists()
            File.WriteAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes3.txt", textBox3.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub buttonNextDate_Click(sender As Object, e As RoutedEventArgs) Handles buttonNextDate.Click
        Calendar1.SelectedDate = Calendar1.SelectedDate.Value.AddDays(1)
        Calendar1.DisplayDate = Calendar1.SelectedDate
    End Sub

    Private Sub buttonPreviousDate_Click(sender As Object, e As RoutedEventArgs) Handles buttonPreviousDate.Click
        Calendar1.SelectedDate = Calendar1.SelectedDate.Value.AddDays(-1)
        Calendar1.DisplayDate = Calendar1.SelectedDate
    End Sub

    Private Sub textBoxDay_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxDay.TextChanged

        Dim dt = Calendar1.SelectedDate

        Try
            CreateDirectoryIfNotExists()
            File.WriteAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\notes.txt", textBoxDay.Text)
        Catch ex As Exception

        End Try
    End Sub



    Private Sub btnSmoothDraw_Click(sender As Object, e As RoutedEventArgs) Handles btnSmoothDraw.Click
        CopyTemplateAndStart("ideas.sddoc")

    End Sub



    Private Sub btnSketchBook_Click(sender As Object, e As RoutedEventArgs) Handles btnSketchBook.Click
        MsgBox("Coming soon!")
    End Sub



    Private Sub CreateFileAndStart(fileName As String)
        Dim dt = Calendar1.SelectedDate
        Dim fpath = DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\" & fileName

        If Not File.Exists(fpath) Then
            File.WriteAllText(fpath, "")
        End If

        Process.Start(fpath)
    End Sub

    Private Sub CreateDirectoryIfNotExists()
        Dim dt = Calendar1.SelectedDate
        If Not Directory.Exists(DiaryFolder) Then Directory.CreateDirectory(DiaryFolder)
        If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year) Then Directory.CreateDirectory(DiaryFolder & "\" & dt.Value.Year)
        If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month) Then Directory.CreateDirectory(DiaryFolder & "\" & "\" & dt.Value.Year & "\" & dt.Value.Month)
        If Not Directory.Exists(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day) Then Directory.CreateDirectory(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day)
    End Sub

    Private Sub CopyTemplateAndStart(fileName As String)
        Dim dt = Calendar1.SelectedDate
        Dim fpath = DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\" & fileName
        CreateDirectoryIfNotExists()

        If Not File.Exists(fpath) Then
            File.Copy(DiaryFolder & "\templates\" & fileName, fpath)
        End If

        Process.Start(fpath)
    End Sub

    Private Sub btnWordDoc_Click(sender As Object, e As RoutedEventArgs) Handles btnWordDoc.Click
        CreateFileAndStart("ideas.docx")
    End Sub

    Private Sub btnWordPad_Click(sender As Object, e As RoutedEventArgs) Handles btnWordPad.Click
        CreateFileAndStart("ideas.rtf")
    End Sub

    Private Sub btnPhotoshop_Click(sender As Object, e As RoutedEventArgs) Handles btnPhotoshop.Click
        CopyTemplateAndStart("ideas.psd")
    End Sub

    Private Sub richTextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles richTextBox.TextChanged
        ' FIX: Fix the saving of RTF.
        'Try
        '    Dim dt = Calendar1.SelectedDate
        '    Dim TextRange = New TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd)
        '    Using fs = New FileStream(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\ideas.rtf", FileMode.OpenOrCreate)
        '        TextRange.Save(fs, System.Windows.DataFormats.Rtf)
        '    End Using

        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub textBoxMonoSpace_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBoxMonoSpace.TextChanged
        Dim dt = Calendar1.SelectedDate

        Try
            CreateDirectoryIfNotExists()
            File.WriteAllText(DiaryFolder & "\" & dt.Value.Year & "\" & dt.Value.Month & "\" & dt.Value.Day & "\monospace.txt", textBoxMonoSpace.Text)
        Catch ex As Exception

        End Try
    End Sub
End Class
