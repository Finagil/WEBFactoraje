Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case UCase(Session.Item("TipoCadena"))
            Case "FACT"
                Session("MasterPage") = "~/PaginaMasterFACT.Master"
                Response.Redirect("~/LotesFactFin.aspx", True)
            Case "USR"
                Session("MasterPage") = "~/PaginaMasterPALM.Master"
                Response.Redirect("~/SubirFacts.aspx", True)
            Case "TES"
                Session("MasterPage") = "~/PaginaMasterPALMT.Master"
                Response.Redirect("~/ConfFondeo.aspx", True)
            Case "FPR"
                Session("MasterPage") = "~/PaginaMasterFACT_PROV.Master"
                Response.Redirect("~/SubirFacts.aspx", True)
            Case "PRV"
                Session("MasterPage") = "~/PaginaMasterPROV.Master"
                Response.Redirect("~/Proveedores/DescontarLote.aspx", True)
            Case Else
                Response.Redirect("~/LoginX.aspx", True)
        End Select
    End Sub

End Class