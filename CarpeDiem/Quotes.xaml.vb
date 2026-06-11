Public Class Quotes

    Private Class Quote
        Public Text As String
        Public Author As String
        Public Sub New(text As String, author As String)
            Me.Text = text
            Me.Author = author
        End Sub
    End Class

    ReadOnly quotes As Quote() = {
        New Quote("Carpe diem. Seize the day, boys. Make your lives extraordinary.", "John Keating, Dead Poets Society"),
        New Quote("Gather ye rosebuds while ye may.", "Robert Herrick"),
        New Quote("It is not that we have a short time to live, but that we waste a lot of it.", "Seneca"),
        New Quote("Begin at once to live, and count each separate day as a separate life.", "Seneca"),
        New Quote("You may delay, but time will not.", "Benjamin Franklin"),
        New Quote("The two most powerful warriors are patience and time.", "Leo Tolstoy"),
        New Quote("Time is what we want most, but what we use worst.", "William Penn"),
        New Quote("If you love life, don't waste time, for time is what life is made up of.", "Bruce Lee"),
        New Quote("How we spend our days is, of course, how we spend our lives.", "Annie Dillard"),
        New Quote("Yesterday is gone. Tomorrow has not yet come. We have only today. Let us begin.", "Mother Teresa"),
        New Quote("The best time to plant a tree was twenty years ago. The second best time is now.", "Chinese proverb"),
        New Quote("Do not wait; the time will never be 'just right'.", "Napoleon Hill"),
        New Quote("Time flies over us, but leaves its shadow behind.", "Nathaniel Hawthorne"),
        New Quote("Nothing is ours, except time.", "Seneca")
    }

    Dim index As Integer

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        ' Quote of the day, then Next browses from there.
        index = Date.Now.DayOfYear Mod quotes.Length
        ShowQuote()
    End Sub

    Private Sub ShowQuote()
        textQuote.Text = """" & quotes(index).Text & """"
        labelAuthor.Content = "— " & quotes(index).Author
    End Sub

    Private Sub buttonNext_Click(sender As Object, e As RoutedEventArgs) Handles buttonNext.Click
        index = (index + 1) Mod quotes.Length
        ShowQuote()
    End Sub

    Private Sub buttonCopy_Click(sender As Object, e As RoutedEventArgs) Handles buttonCopy.Click
        Try
            Clipboard.SetText(quotes(index).Text & " — " & quotes(index).Author)
        Catch
        End Try
    End Sub

End Class
