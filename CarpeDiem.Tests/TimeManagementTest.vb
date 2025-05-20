Imports CarpeDiem.Core

<TestClass()> Public Class TimeManagementTest

    <TestMethod()> Public Sub TimeAfter3Hours()
        Assert.AreEqual(TimeManagement.TimeAfterHours(3).ToShortTimeString(), Now.AddHours(3).ToShortTimeString())
    End Sub

    <TestMethod()> Public Sub TotalMinutesAfter2Hours()
        Dim totalMinutesAfter = TimeManagement.TotalMinutesAfter(2)
        Dim totalMinutesAfterTest = CInt(TimeManagement.TimeAfterHours(2).Subtract(Date.Now).TotalMinutes)
        Debug.Print(totalMinutesAfterTest.ToString())
        Assert.AreEqual(totalMinutesAfter, totalMinutesAfterTest)
    End Sub

    <TestMethod()> Public Sub TimeAfter5Minutes()
        Dim result = TimeManagement.GetTimeAfterMinutes(5)
        Dim timeafter5Min = Now.AddMinutes(5)
        Assert.AreEqual(result, timeafter5Min)
    End Sub

    <TestMethod()> Public Sub TimeFormatTest()
        Dim result = TimeManagement.GetFormattedDateTime("hh:mm:ss tt")
        Dim formatted = Date.Now.ToString("hh:mm:ss tt")
        Assert.AreEqual(result, formatted)
    End Sub

    <TestMethod()> Public Sub CorrectDaySection()
        Assert.AreEqual(TimeManagement.GetDaySection(Date.Parse("13:00")), "2")
    End Sub

    <TestMethod()> Public Sub CorrectSectionEndingTime1()
        Assert.AreEqual(TimeManagement.SectionEndingTime(Date.Parse("1:00")), Date.Parse("8:00"))
    End Sub
    <TestMethod()> Public Sub CorrectSectionEndingTime2()
        Assert.AreEqual(TimeManagement.SectionEndingTime(Date.Parse("10:00")), Date.Parse("16:00"))
    End Sub
    <TestMethod()> Public Sub CorrectSectionEndingTime3()
        Assert.AreEqual(TimeManagement.SectionEndingTime(Date.Parse("17:00")), Date.Parse("23:59"))
    End Sub

    <TestMethod()> Public Sub CorrectSectionStartingTime()
        Assert.AreEqual(TimeManagement.SectionStartingTime(Date.Parse("1:00")), Date.Parse("0:00"))
    End Sub

    <TestMethod()> Public Sub CorrectWeekOfYear()
        Assert.AreEqual(TimeManagement.GetIso8601WeekOfYear(New Date(2016, 10, 25)), 43)
    End Sub

    <TestMethod()> Public Sub CorrectMonthName()
        Assert.AreEqual(TimeManagement.GetMonthName(10), "October")
    End Sub

    <TestMethod()> Public Sub CorrectGetDifferenceTimespan()
        Dim diff = TimeManagement.GetDifferenceTimespan(Date.Parse("0:00"), Date.Parse("1:00"))
        Assert.AreEqual(diff, New TimeSpan(1, 0, 0))
    End Sub

    <TestMethod()> Public Sub DifferencePercentageStartAtZero()
        Dim startTime = Date.Now
        Dim endTime = startTime.AddMinutes(1)
        Dim percentage = TimeManagement.GetDifferencePercentage(startTime, endTime)
        Assert.IsTrue(percentage <= 1 AndAlso percentage >= 0)
    End Sub

    <TestMethod()> Public Sub test()

        Dim msg = "All good"
        Assert.AreEqual(msg, "All good")

    End Sub



End Class