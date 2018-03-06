Partial Public Class LotesFactFinAntiCarga
    Inherits System.Web.UI.Page

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim ID As Integer = GridView1.SelectedDataKey(0)
        Dim UserX As String = GridView1.SelectedDataKey(1)
        Dim Cesion As String = GridView1.SelectedDataKey(2)
        Response.Redirect("~\SubirAnti.aspx?ID=" & ID & "&UserX=" & UserX & "&Cesion=" & Cesion)
    End Sub
End Class