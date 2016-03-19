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
        Dim formatted = DateTime.Now.ToString("hh:mm:ss tt")
        Assert.AreEqual(result, formatted)
    End Sub

End Class