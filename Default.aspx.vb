Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case UCase(Session.Item("TipoCadena"))
            Case "FACT"
                Response.Redirect("~/LotesFactFin.aspx", True)
            Case "USR"
                Response.Redirect("~/SubirFacts.aspx", True)
            Case "TES"
                Response.Redirect("~/ConfFondeo.aspx", True)
            Case Else
                Response.Redirect("~/LoginX.aspx", True)
        End Select
    End Sub

End Class