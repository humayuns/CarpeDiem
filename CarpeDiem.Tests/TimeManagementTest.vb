Imports System.Text
Imports CarpeDiem.Core
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class TimeManagementTest

    <TestMethod()> Public Sub TimeAfter3Hours()

        Assert.AreEqual(TimeManagement.TimeAfterHours(3), Now.AddHours(3))
    End Sub

End Class