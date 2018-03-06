Public Class FacturasVenc
    Inherits System.Web.UI.Page
    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

End Class