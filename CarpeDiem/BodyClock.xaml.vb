Imports System.Windows.Threading

Public Class BodyClock

    Private Class Phase
        Public Name As String
        Public Tip As String
        Public StartHour As Double
        Public EndHour As Double      ' may exceed 24 when the phase crosses midnight
        Public BrushKey As String
        Public Segment As Shapes.Path
    End Class

    ReadOnly timer1 As New DispatcherTimer
    ReadOnly phases As New List(Of Phase)

    Const CenterX As Double = 150
    Const CenterY As Double = 150
    Const OuterRadius As Double = 118
    Const InnerRadius As Double = 76
    Const SegmentGapDegrees As Double = 1.2

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        BuildPhases()
        DrawDial()
        UpdateNow()

        AddHandler timer1.Tick, Sub() UpdateNow()
        timer1.Interval = New TimeSpan(0, 0, 1)
        timer1.Start()
    End Sub

    Private Sub BuildPhases()
        phases.Add(NewPhase("Sleep", 23, 31, "BarMonthBrush", "Rest and recovery — melatonin is high."))
        phases.Add(NewPhase("Morning Rise", 7, 9, "BarDayBrush", "Cortisol peaks — get light, hydrate and plan the day."))
        phases.Add(NewPhase("Peak Focus", 9, 12, "BarYearBrush", "Alertness is highest — do the deep, important work."))
        phases.Add(NewPhase("Lunch", 12, 13, "ButtonRestoreBrush", "Refuel and step away from the desk."))
        phases.Add(NewPhase("Afternoon Dip", 13, 15, "ButtonNeutralBrush", "Energy dips — light tasks, a walk or a short nap."))
        phases.Add(NewPhase("Second Wind", 15, 18, "BarSecondBrush", "Coordination and reaction time peak — meetings or exercise."))
        phases.Add(NewPhase("Evening", 18, 21, "BarWeekBrush", "Wrap up, dinner and social time."))
        phases.Add(NewPhase("Wind Down", 21, 23, "BarMotivationBrush", "Dim the lights and ease off screens before bed."))
    End Sub

    Private Function NewPhase(name As String, startHour As Double, endHour As Double, brushKey As String, tip As String) As Phase
        Return New Phase With {.Name = name, .StartHour = startHour, .EndHour = endHour, .BrushKey = brushKey, .Tip = tip}
    End Function

    Private Sub DrawDial()
        For Each ph In phases
            Dim a1 = ph.StartHour / 24 * 360 + SegmentGapDegrees
            Dim a2 = ph.EndHour / 24 * 360 - SegmentGapDegrees

            Dim seg As New Shapes.Path With {
                .Data = CreateRingSegment(a1, a2),
                .StrokeThickness = 0,
                .ToolTip = ph.Name & "  " & FormatRange(ph) & Environment.NewLine & ph.Tip
            }
            seg.SetResourceReference(Shapes.Shape.FillProperty, ph.BrushKey)
            seg.SetResourceReference(Shapes.Shape.StrokeProperty, "TextPrimaryBrush")

            ph.Segment = seg
            ' Insert below the hand and center text declared in XAML.
            canvasDial.Children.Insert(0, seg)
        Next

        ' Hour labels around the dial.
        For h = 0 To 21 Step 3
            Dim pt = PointOnDial(134, h / 24 * 360)
            Dim tb As New TextBlock With {
                .Text = h.ToString(),
                .FontSize = 10,
                .FontWeight = FontWeights.SemiBold,
                .Width = 24,
                .TextAlignment = TextAlignment.Center
            }
            tb.SetResourceReference(TextBlock.ForegroundProperty, "TextSecondaryBrush")
            Canvas.SetLeft(tb, pt.X - 12)
            Canvas.SetTop(tb, pt.Y - 7)
            canvasDial.Children.Add(tb)
        Next
    End Sub

    Private Sub UpdateNow()
        Dim nowTime = Date.Now
        Dim hourVal As Double = nowTime.TimeOfDay.TotalHours

        labelTime.Content = nowTime.ToString("HH:mm:ss")
        textCenterTime.Text = nowTime.ToString("HH:mm")

        ' Marker across the ring at the current time.
        Dim angle = hourVal / 24 * 360
        Dim pInner = PointOnDial(InnerRadius - 6, angle)
        Dim pOuter = PointOnDial(OuterRadius + 6, angle)
        handLine.X1 = pInner.X : handLine.Y1 = pInner.Y
        handLine.X2 = pOuter.X : handLine.Y2 = pOuter.Y

        ' Highlight the current phase.
        Dim current As Phase = Nothing
        For Each ph In phases
            Dim isCurrent = (hourVal >= ph.StartHour AndAlso hourVal < ph.EndHour) OrElse
                            (hourVal + 24 >= ph.StartHour AndAlso hourVal + 24 < ph.EndHour)
            ph.Segment.Opacity = If(isCurrent, 1.0, 0.55)
            ph.Segment.StrokeThickness = If(isCurrent, 1.2, 0)
            If isCurrent Then current = ph
        Next

        If current IsNot Nothing Then
            Dim nextPhase = phases((phases.IndexOf(current) + 1) Mod phases.Count)
            textCenterPhase.Text = current.Name
            labelPhaseName.Content = current.Name & "  ·  " & FormatRange(current)
            textPhaseTip.Text = current.Tip & "  Up next: " & nextPhase.Name & " at " &
                                CInt(current.EndHour Mod 24).ToString("00") & ":00."
        End If
    End Sub

    Private Function FormatRange(ph As Phase) As String
        Return CInt(ph.StartHour Mod 24).ToString("00") & ":00–" & CInt(ph.EndHour Mod 24).ToString("00") & ":00"
    End Function

    ' Angle is measured in degrees clockwise from 00:00 at the top of the dial.
    Private Function PointOnDial(radius As Double, angleDeg As Double) As Point
        Dim rad = angleDeg * Math.PI / 180
        Return New Point(CenterX + radius * Math.Sin(rad), CenterY - radius * Math.Cos(rad))
    End Function

    Private Function CreateRingSegment(startAngle As Double, endAngle As Double) As Geometry
        Dim isLarge = (endAngle - startAngle) > 180

        Dim fig As New PathFigure With {
            .StartPoint = PointOnDial(OuterRadius, startAngle),
            .IsClosed = True
        }
        fig.Segments.Add(New ArcSegment(PointOnDial(OuterRadius, endAngle),
                                        New Size(OuterRadius, OuterRadius), 0, isLarge,
                                        SweepDirection.Clockwise, True))
        fig.Segments.Add(New LineSegment(PointOnDial(InnerRadius, endAngle), True))
        fig.Segments.Add(New ArcSegment(PointOnDial(InnerRadius, startAngle),
                                        New Size(InnerRadius, InnerRadius), 0, isLarge,
                                        SweepDirection.Counterclockwise, True))

        Dim geo As New PathGeometry
        geo.Figures.Add(fig)
        Return geo
    End Function

End Class
