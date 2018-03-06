Partial Public Class DetalleFondeoPALM
    Inherits System.Web.UI.Page
    Dim total1 As Decimal
    Dim ID_lot As Integer = 0

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = "Detalle del Lote: " & Request("ID")
        ID_lot = Request("ID")
        total1 = 0

    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            total1 += DataBinder.Eval(e.Row.DataItem, "PrecioOperacion")
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = "Totales"
            e.Row.Cells(4).Text = total1.ToString("n2")
            e.Row.HorizontalAlign = HorizontalAlign.Center
            e.Row.Font.Bold = True
            Label3.Text = "Total del Fondeo: " & total1.ToString("n2")
            Himporte.Value = total1.ToString("n2")
        End If
    End Sub

    
    Protected Sub BotonEnviar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BotonEnviar1.Click
        If Calendar1.SelectedDate = CDate("01/01/2016") Then
            Lberror.Text = "Falrta seleccionar fecha de pago"
            Lberror.Visible = True
        Else
            Lberror.Visible = False
            Dim Lot As New Factor100DSTableAdapters.WEB_LotesTableAdapter
            Dim Fon As New Factor100DSTableAdapters.WEB_FondeoTableAdapter
            Lot.LoteProcesado(ID_lot)
            Fon.ActualizaFechaSol(Calendar1.SelectedDate, ID_lot)

            Dim Msg As String
            Dim Asunto As String
            Msg = "El usuario " & Session("User") & " acaba de confirmar un fondeo:<br>"
            Msg += "Lote: " & ID_lot & "<br>"
            Msg += "Importe: " & Himporte.Value & "<br>"
            Msg += "<A HREF='http://finagil.com.mx/factoraje'>Web de Factoraje</A>"
            Asunto = "Solicitud de fondeo Confirmada Lote: " & ID_lot & " (Factoraje)"

            EnviaCorreo(Session("Correo"), My.Settings.CorreoAdmin, Msg, Asunto)
            EnviaCorreo(Session("Correo"), My.Settings.CorreoAdmin2, Msg, Asunto)
            EnviaCorreo(Session("Correo"), "ecacerest@finagil.com.mx", Msg, Asunto)

            Response.Redirect("~\Default.aspx")
        End If

    End Sub
End Class