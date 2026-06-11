Imports System.Windows.Media

Public Class ThemeManagerWindow

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        BuildCards()
    End Sub

    Private Sub BuildCards()
        panelThemes.Children.Clear()
        For Each themeName In ThemeManager.ThemeOrder
            panelThemes.Children.Add(CreateCard(themeName))
        Next
    End Sub

    ''' <summary>
    ''' A clickable preview card rendered with the theme's own colors,
    ''' so every card shows what you would get.
    ''' </summary>
    Private Function CreateCard(themeName As String) As Border
        Dim dict As New ResourceDictionary With {
            .Source = New Uri("Themes/" & themeName & "Theme.xaml", UriKind.Relative)
        }

        Dim isCurrent = (themeName = ThemeManager.CurrentTheme)
        Dim card As New Border With {
            .CornerRadius = New CornerRadius(6),
            .Padding = New Thickness(10, 8, 10, 8),
            .Margin = New Thickness(0, 0, 0, 6),
            .BorderThickness = New Thickness(2),
            .Cursor = Cursors.Hand,
            .Background = CType(dict("WindowBackgroundBrush"), Brush),
            .BorderBrush = If(isCurrent,
                              CType(dict("InputFocusBrush"), Brush),
                              CType(dict("SurfaceBorderBrush"), Brush)),
            .ToolTip = "Apply the " & themeName & " theme"
        }

        Dim root As New Grid
        root.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
        root.ColumnDefinitions.Add(New ColumnDefinition With {.Width = GridLength.Auto})

        Dim info As New StackPanel
        info.Children.Add(New TextBlock With {
            .Text = themeName & If(isCurrent, "   ✓", ""),
            .FontWeight = FontWeights.Bold,
            .FontSize = 13,
            .Foreground = CType(dict("TextPrimaryBrush"), Brush)
        })
        info.Children.Add(New TextBlock With {
            .Text = If(ThemeManager.IsDarkTheme(themeName), "Dark", "Light"),
            .FontSize = 10,
            .Foreground = CType(dict("TextSecondaryBrush"), Brush)
        })

        Dim swatches As New StackPanel With {.Orientation = Orientation.Horizontal, .VerticalAlignment = VerticalAlignment.Center}
        For Each key In {"TitleBrush", "ButtonStartBrush", "ButtonClockBrush", "BarDayBrush", "ButtonCloseBrush", "SurfaceBrush"}
            swatches.Children.Add(New Border With {
                .Width = 16, .Height = 16,
                .CornerRadius = New CornerRadius(4),
                .Margin = New Thickness(3, 0, 0, 0),
                .Background = CType(dict(key), Brush),
                .BorderBrush = CType(dict("SurfaceBorderBrush"), Brush),
                .BorderThickness = New Thickness(1)
            })
        Next

        Grid.SetColumn(info, 0)
        Grid.SetColumn(swatches, 1)
        root.Children.Add(info)
        root.Children.Add(swatches)
        card.Child = root

        AddHandler card.MouseLeftButtonUp,
            Sub()
                ThemeManager.ApplyTheme(themeName)
                Dim mw = TryCast(Application.Current.MainWindow, MainWindow)
                If mw IsNot Nothing Then mw.UpdateThemeButton()
                BuildCards()
            End Sub

        Return card
    End Function

End Class
