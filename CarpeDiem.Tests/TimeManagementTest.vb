﻿Imports System.Text
Imports CarpeDiem.Core
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class TimeManagementTest

    <TestMethod()> Public Sub TimeAfter3Hours()

        Assert.AreEqual(TimeManagement.TimeAfterHours(3), Now.AddHours(3))
    End Sub

    <TestMethod()> Public Sub TotalMinutesAfter2Hours()

        Dim totalMinutesAfter = TimeManagement.TotalMinutesAfter(2)
        Dim totalMinutesAfterTest = CInt(TimeManagement.TimeAfterHours(2).Subtract(Date.Now).TotalMinutes)
        Debug.Print(totalMinutesAfterTest.ToString())
        Assert.AreEqual(totalMinutesAfter, totalMinutesAfterTest)
    End Sub

End Class