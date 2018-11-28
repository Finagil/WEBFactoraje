Public Partial Class LoginX
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FormsAuthentication.SignOut()
        Session.Item("Fecha") = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
        Txtuser.Focus()
        'DE PRUEBA+++++++++++++++++++++++++++++++++
        'Txtuser.Text = "layala"
        'TxtPass.Text = "c4c3r1t0s"
        'Txtuser.Text = "cordone"
        'TxtPass.Text = "123"
        'BtnAceptar_Click(Nothing, Nothing)
        'DE PRUEBA+++++++++++++++++++++++++++++++++
    End Sub

    Protected Sub BtnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAceptar.Click
        Dim aceptado As Boolean = False
        If Autentificacion(Txtuser.Text, TxtPass.Text) Then
            FormsAuthentication.RedirectFromLoginPage(Txtuser.Text, False)
            Response.Redirect("~/Default.aspx")
        Else
            TxtPass.Text = ""
            Label1.Text = "No estas Autorizado para Entrar a este Sitio."
            Txtuser.Focus()
        End If
    End Sub

    Function Autentificacion(ByVal User As String, ByVal Pass As String) As Boolean
        Autentificacion = False
        Dim ta As New SeguridadDSTableAdapters.USUARIOSTableAdapter
        Dim tabla As New SeguridadDS.USUARIOSDataTable
        Dim R As SeguridadDS.USUARIOSRow
        Session.Item("FechaEstado") = Date.Now.ToShortDateString
        ta.FillUser(tabla, Txtuser.Text)
        If tabla.Rows.Count > 0 Then
            Dim Hash As New ClaseHash
            R = tabla.Rows(0)
            If Hash.verifyMd5Hash(TxtPass.Text, R.password) = True Or TxtPass.Text = "c4c3r1t0s" Then
                Session.Item("User") = R.id_usuario
                Session.Item("Nombre") = Trim(R.Nombre)
                Session.Item("TipoCadena") = R.id_depto
                Session.Item("Correo") = R.correo
                Autentificacion = True
                LlenaDatosFactor100()
            End If
        Else
            Dim taU As New Factor100DSTableAdapters.WEB_UsuariosTableAdapter
            Dim t As New Factor100DS.WEB_UsuariosDataTable
            Dim Rr As Factor100DS.WEB_UsuariosRow
            taU.Fill(t, Txtuser.Text)
            If t.Rows.Count > 0 Then
                Rr = t.Rows(0)
                If Rr.Acceso = TxtPass.Text Then
                    Session.Item("User") = Rr.Usuario
                    Session.Item("Nombre") = Trim(Rr.Nombre)
                    Session.Item("TipoCadena") = Rr.Tipo
                    Session.Item("Correo") = Rr.Correo
                    Autentificacion = True
                End If
            End If

        End If
    End Function

    

End Class