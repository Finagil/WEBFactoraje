Public Partial Class WebForm2
    Inherits System.Web.UI.Page

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
        If Session.Item("TipoCadena") = "FPR" Then
            Session("Estatus") = "Por Descontar"
        Else
            Session("Estatus") = "Pendiente"
        End If
    End Sub

End Class