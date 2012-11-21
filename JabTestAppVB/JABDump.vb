Imports JabApiLib.JavaAccessBridge
Imports System.Globalization
Imports System.Threading

Public Class JABDump
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim windowHandle As Integer

        If Integer.TryParse(txtWindowHandle.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, windowHandle) Then
            txtTreeOutput.Text = "Loading...."
            Application.DoEvents()

            Thread.Sleep(100)

            Dim vmID As Int32
            Dim javaTree As JabHelpers.AccessibleTreeItem = JabHelpers.GetComponentTree(windowHandle, vmID)
            txtTreeOutput.Text = vmID.ToString()
            txtTreeOutput.Text = txtTreeOutput.Text & JabHelpers.screenContentsString

        End If

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Init the java access bridge.  This needs to be in the form load.
        JabApi.Windows_run()
    End Sub

End Class
