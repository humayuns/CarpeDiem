﻿Imports CarpeDiem.Core

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

    <TestMethod()> Public Sub test()

        Dim msg = "All good"
        Assert.AreEqual(msg, "All good")

    End Sub



End Class