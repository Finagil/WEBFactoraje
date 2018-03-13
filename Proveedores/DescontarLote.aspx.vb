Partial Public Class DescontarLote
    Inherits System.Web.UI.Page
    Dim total1 As Decimal

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
        Session("Estatus") = "Por Descontar"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = "Facturas para Descontar " & Request("ID")
        total1 = 0
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            total1 += DataBinder.Eval(e.Row.DataItem, "ImporteFactura")
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = "Totales"
            e.Row.Cells(4).Text = total1.ToString("n2")
            e.Row.HorizontalAlign = HorizontalAlign.Center
            e.Row.Font.Bold = True
            Label3.Text = "Total de Facturas: " & total1.ToString("n2")
        End If
        If total1 > 0 Then
            BotonEnviar2.Visible = True
            Label3.Visible = True
        Else
            BotonEnviar2.Visible = False
            Label3.Visible = False
        End If
    End Sub

    Protected Sub BotonEnviar2_Click(sender As Object, e As EventArgs) Handles BotonEnviar2.Click
        If Not IsNothing(Request("ID")) Then
            Dim ta As New Factor100DSTableAdapters.LotesPorDescontarTableAdapter
            ta.UpdateEstatusLote("Pendiente", Request("ID"))
            GeneraCorreoPROV(Request("ID"))
            Response.Redirect("~/Proveedores/ConsultaLote.aspx", True)
        End If
    End Sub

    Sub GeneraCorreoPROV(lote As Decimal)
        Dim ta As New Factor100DSTableAdapters.LotesPorDescontarTableAdapter
        Dim t As New Factor100DS.LotesPorDescontarDataTable
        ta.FillByloteEPO(t, lote, "Pendiente")
        Dim Mensaje As String = ""
        For Each r As Factor100DS.LotesPorDescontarRow In t.Rows
            Mensaje = "<FONT FACE=""arial"">Estimado " & r.Nombre_Persona & "<br><br>Le informamos que la Compañía " & r.Proveedor & " ha descontado " & r.Facturas
            Mensaje += " facturas por un monto total de $" & r.ImporteFactura.ToString("n2")
            Mensaje += " a través de la cesión de derechos de crédito a FINAGIL, S.A. DE C.V. SOFOM E.N.R."
            Mensaje += "En caso de tener cualquier duda o comentario al respecto favor a contactarnos<BR><br>"
            Mensaje += "Leonardo Ayala <layala@finagil.com.mx><BR>"
            Mensaje += "Tel: (01722) 265 3400 / (01722) 214 5533 EXT 1200<br><br>"
            Mensaje += "Leticia Mondragon <lmondragon@finagil.com.mx><BR>"
            Mensaje += "Tel: (01722) 265 3400 / (01722) 214 5533 EXT 1207<br><br></p></FONT>"
            EnviaCorreo("Leonardo Ayala (Finagil) <layala@finagil.com.mx>", r.Correo, Mensaje, "Finagil - Facturas para descuento")
            'EnviaCorreo("Leonardo Ayala (Finagil) <layala@finagil.com.mx>", "ecacerest@finagil.com.mx", Mensaje, "Finagil - Facturas para Descuento")
        Next
    End Sub
End Class