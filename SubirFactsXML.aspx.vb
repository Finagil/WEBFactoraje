Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections
Imports System.Xml

Partial Public Class WebForm1XML

    Inherits System.Web.UI.Page
    Dim Lote As Integer = 0
    Dim Bandera As Boolean
    Dim dt As New DataTable()
    Private Sub WebForm1XML_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Bandera = False
    End Sub

    Private Sub WebFormXML_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

    Protected Sub Submit1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Submit1.ServerClick
        Dim DVencimientos As New Factor100DSTableAdapters.WEB_DVencimientoTableAdapter
        Dim dtF100 As New FactorVWDS1TableAdapters.Vw_ChequesDetalleTableAdapter
        Dim dtWebXML As New FactorVWDS1TableAdapters.WEB_FacturasXMLTableAdapter
        Dim dsF100 As New FactorVWDS1.Vw_ChequesDetalleDataTable

        'columnas
        dt.Columns.Add(New DataColumn("Factura", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("RFC", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("Procesar", System.Type.GetType("System.Boolean")))
        dt.Columns.Add("UUID")
        dt.Columns.Add("MPago")
        dt.Columns.Add("Moneda")
        dt.Columns.Add(New DataColumn("Importe", System.Type.GetType("System.Double")))
        dt.Columns.Add("FFactura")

        dt.Columns.Add("Anticipo")
        dt.Columns.Add("FVencimiento")
        dt.Columns.Add("PFinagil")
        dt.Columns.Add("Serie")
        dt.Columns.Add("Folio")
        dt.Columns.Add("TCambio")
        dt.Columns.Add("Existe")

        Dim contador As Integer = 0
        Dim total As Double = 0
        Dim row As DataRow
        Try
            If FileUpload1.HasFiles Then
                Dim rootPath As String = Server.MapPath("Temp")
                For Each arch As HttpPostedFile In FileUpload1.PostedFiles
                    Dim GU As Guid = Guid.NewGuid
                    Dim ext() As String = arch.FileName.Split(".")

                    Dim Nom As String = GU.ToString & "." & ext(ext.Length - 1)
                    arch.SaveAs(Path.Combine(rootPath, Nom))

                    'Agregar Datos    
                    row = dt.NewRow()
                    contador += 1
                    row("Factura") = contador.ToString
                    row("RFC") = leeXMLF(Path.Combine(rootPath, Nom), "RFCR")
                    row("Procesar") = True
                    row("UUID") = leeXMLF(Path.Combine(rootPath, Nom), "UUID")
                    row("MPago") = leeXMLF(Path.Combine(rootPath, Nom), "MetodoPago")
                    row("Moneda") = leeXMLF(Path.Combine(rootPath, Nom), "Moneda")
                    row("TCambio") = Val(leeXMLF(Path.Combine(rootPath, Nom), "TipoCambio") + "")
                    row("Importe") = leeXMLF(Path.Combine(rootPath, Nom), "Total")

                    row("FFactura") = CDate(leeXMLF(Path.Combine(rootPath, Nom), "Fecha")).ToShortDateString
                    row("Serie") = leeXMLF(Path.Combine(rootPath, Nom), "Serie")
                    row("Folio") = leeXMLF(Path.Combine(rootPath, Nom), "Folio")

                    row("Anticipo") = ""

                    Dim DiasVencimiento As Integer = DVencimientos.ObtieneDVenc(leeXMLF(Path.Combine(rootPath, Nom), "RFCE"))
                    Dim FechaVencimiento As Date = CDate(row("FFactura")).AddDays(DiasVencimiento)
                    CalculaFecVecnt(FechaVencimiento, leeXMLF(Path.Combine(rootPath, Nom), "RFCE"))
                    row("FVencimiento") = CDate(leeXMLF(Path.Combine(rootPath, Nom), "Fecha")).ToShortDateString
                    row("PFinagil") = ""

                    Dim var As String = dtF100.Existe_ScalarQuery(row("RFC"), row("Serie") & row("Folio"), row("Importe"))
                    Dim var_xml As String = dtWebXML.Existe_ScalarQuery(row("UUID"))

                    'dtF100.Existe_FillBy(row("RFC"), row("Serie") & row("Folio"), row("Importe"))

                    If var = "0" = True Then
                        row("Existe") = "False"
                    Else
                        If var_xml = "NE" Then
                            row("Existe") = "True"
                            total += CDbl(row("Importe"))
                        Else
                            row("Existe") = "False"
                        End If
                    End If

                    'ValidaDatos(Path.Combine(rootPath, Nom).ToString, CDec(row("Importe")), row("RFC"), row("Serie"), row("Folio"), row("Importe"), row("FFactura"))

                    dt.Rows.Add(row)
                Next
            End If
            LberrorXML0.Visible = True
            LberrorXML0.Text = "Importe total de facturas relacionadas: " & Format(total, "##,##0.00")
        Catch ex As Exception
            LberrorXML.Visible = True
            LberrorXML.Text = ex.ToString
        End Try

        GridView1.DataSource = dt
        GridView1.DataBind()

        FileUpload1.Visible = False
        Submit1.Visible = False
        Button1.Visible = True
    End Sub



    Public Function leeXMLF(docXML As String, nodo As String)
        Dim doc As XmlDataDocument
        doc = New XmlDataDocument
        doc.Load(docXML)
        Dim CFDI As XmlNode
        Dim retorno As String = ""

        CFDI = doc.DocumentElement
        If nodo = "RFCR" Or nodo = "RFCE" Then
            For Each comprobante As XmlNode In CFDI.ChildNodes
                If comprobante.Name = "cfdi:Receptor" And nodo = "RFCR" Then
                    For Each receptor As XmlNode In comprobante.Attributes
                        If receptor.Name = "Rfc" Then
                            retorno = receptor.Value.ToString
                            Return retorno
                        End If
                    Next
                End If
                If comprobante.Name = "cfdi:Emisor" And nodo = "RFCE" Then
                    For Each receptor As XmlNode In comprobante.Attributes
                        If receptor.Name = "Rfc" Then
                            retorno = receptor.Value.ToString
                            Return retorno
                        End If
                    Next
                End If
            Next
        End If

        If nodo <> "UUID" Then
            For Each Comprobante As XmlNode In CFDI.Attributes
                If Comprobante.Name = "Moneda" And nodo = "Moneda" Then
                    retorno = Comprobante.Value.ToString
                    Return retorno
                    Exit Function
                ElseIf Comprobante.Name = "TipoCambio" And nodo = "TipoCambio" Then
                    retorno = Comprobante.Value.ToString
                    Return retorno
                    Exit Function
                ElseIf (Comprobante.Name = "Total" Or Comprobante.Name = "total") And nodo = "Total" Then
                    retorno = Comprobante.Value.ToString
                    Return retorno
                    Exit Function
                ElseIf (Comprobante.Name = "MetodoPago" Or Comprobante.Name = "metodoDePago") And nodo = "MetodoPago" Then
                    retorno = Comprobante.Value.ToString
                    Return retorno
                    Exit Function
                ElseIf Comprobante.Name = "FormaPago" And nodo = "FormaPago" Then
                    retorno = Comprobante.Value.ToString
                    Return retorno
                    Exit Function
                ElseIf (Comprobante.Name = "Serie" Or Comprobante.Name = "serie") And nodo = "Serie" Then
                    retorno = Comprobante.Value.ToString
                    Return retorno
                    Exit Function
                ElseIf (Comprobante.Name = "Folio" Or Comprobante.Name = "folio") And nodo = "Folio" Then
                    retorno = Comprobante.Value.ToString
                    Return retorno
                    Exit Function
                ElseIf Comprobante.Name = "Fecha" And nodo = "Fecha" Then
                    retorno = Comprobante.Value.ToString
                    Return retorno
                    Exit Function
                End If
            Next
        Else
            For Each Comprobante As XmlNode In CFDI.ChildNodes
                For Each Complemento As XmlNode In Comprobante
                    If Complemento.Name = "tfd:TimbreFiscalDigital" Then
                        For Each atributos As XmlNode In Complemento.Attributes
                            If atributos.Name = "UUID" Then
                                retorno = atributos.Value.ToString
                                Return retorno
                                Exit Function
                            End If
                        Next
                    End If
                Next
            Next
        End If
    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim taXML As New Factor100DSTableAdapters.WEB_FacturasXMLTableAdapter
        Dim ta As New Factor100DSTableAdapters.WEB_FacturasTableAdapter
        Dim LOT As New Factor100DSTableAdapters.WEB_LotesTableAdapter
        Dim cont As Integer = 0
        Try
            For Each filas As GridViewRow In GridView1.Rows
                If filas.Cells(13).Text = "True" Then
                    taXML.Insert(CInt(filas.Cells(2).Text.ToString), filas.Cells(1).Text.ToString.Replace("&nbsp;", "") & filas.Cells(2).Text.ToString, filas.Cells(3).Text, CDbl(filas.Cells(4).Text), 0, filas.Cells(7).Text, Nothing, False, Nothing, Nothing, filas.Cells(5).Text.ToString, filas.Cells(1).Text.ToString.Replace("&nbsp;", ""), CInt(filas.Cells(2).Text.ToString), filas.Cells(11).Text, filas.Cells(12).Text, filas.Cells(10).Text)
                    cont += 1
                End If
            Next
            If cont > 0 Then
                LberrorXML0.Visible = True
                LberrorXML0.Text = cont.ToString & " comprobantes enviados correctamente..."
            Else
                LberrorXML0.Visible = True
                LberrorXML0.Text = "No hay comprobantes por enviar..."
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        GridView1.DataSource = dt
        GridView1.DataBind()

        FileUpload1.Visible = True
        Submit1.Visible = True
        Button1.Visible = False
        'LberrorXML0.Visible = False
    End Sub



    Sub InsertaFactura(ByRef RFC As String, ByVal Serie As String, ByVal Folio As String, ByVal Importe As String, ByVal FechaF As String)

        Dim SerieFolio As String = Serie & Folio

        Dim ta As New Factor100DSTableAdapters.WEB_FacturasTableAdapter
        Dim tb As New Factor100DSTableAdapters.WEB_ClientesTableAdapter
        Dim DVencimientos As New Factor100DSTableAdapters.WEB_DVencimientoTableAdapter
        Dim total As Double

        If LberrorXML.Visible = False Then
            total = 0
            Dim LOT As New Factor100DSTableAdapters.WEB_LotesTableAdapter
            Dim RFCA As String = ""
            Dim Banco As String = tb.SacaBanco(RFC)
            Dim FecVecn As String

            Lote = LOT.UltimoID()
            Dim NuloFec As Nullable(Of Date)

            If Session.Item("TipoCadena") = "FPR" Then
                If RFCA = "" Then
                    RFCA = RFC
                End If
                total += CDec(Importe)
                FecVecn = DVencimientos.ObtieneDVenc(RFC)
                CalculaFecVecnt(CDate(FechaF).AddDays(CDbl(FecVecn)), RFC)
                If ta.ExisteFactura(SerieFolio, RFC) <= 0 And RFCA = RFC Then
                    ta.Insert(Lote, SerieFolio, RFC, Importe, 0, FechaF, FecVecn, False, "", NuloFec)
                End If
            End If
            GeneraCorreoPROV(Lote)
        End If

    End Sub

    Sub GeneraCorreoPROV(lote As Decimal)
        Dim ta As New Factor100DSTableAdapters.LotesPorDescontarTableAdapter
        Dim t As New Factor100DS.LotesPorDescontarDataTable
        ta.FillByLote(t, lote, "Por Descontar")
        Dim Mensaje As String = ""
        For Each r As Factor100DS.LotesPorDescontarRow In t.Rows
            Mensaje = "<FONT FACE=""arial"">Estimado " & r.Nombre_Persona & "<br><br>Le informamos que la Compañía " & r.NombreCliente & " ha publicado " & r.Facturas
            Mensaje += " facturas a favor de  " & r.Proveedor & " por un monto total de $" & r.ImporteFactura.ToString("n2")
            Mensaje += " mismas que usted podrá solicitar su pago anticipado a través de la cesión de derechos de crédito que realice a FINAGIL, S.A. DE C.V. SOFOM E.N.R. en la siguiente liga:"
            Mensaje += "<br><br><A HREF='https://finagil.com.mx/factoraje'>Web de Factoraje Finagil</A><br><br>"
            Mensaje += "Al aceptar la trasmisión de los derechos de crédito a FINAGIL, S.A. DE C.V. SOFOM E.N.R. usted estará recibiendo el pago el mismo día que realice su autorización, considerando un horario de operación de 9:00 AM a 12:30 PM.<br><br>"
            Mensaje += "En caso de tener cualquier duda o comentario al respecto favor a contactarnos<BR><br>"
            Mensaje += "Leonardo Ayala <layala@finagil.com.mx><BR>"
            Mensaje += "Tel: (01722) 265 3400 / (01722) 214 5533 EXT 1200<br><br>"
            Mensaje += "Leticia Mondragon <lmondragon@finagil.com.mx><BR>"
            Mensaje += "Tel: (01722) 265 3400 / (01722) 214 5533 EXT 1207<br><br></p></FONT>"
            EnviaCorreo("Leonardo Ayala (Finagil) <layala@finagil.com.mx>", r.Correo, Mensaje, "Finagil - Facturas para descuento")
            EnviaCorreo("Leonardo Ayala (Finagil) <layala@finagil.com.mx>", "ecacerest@finagil.com.mx", Mensaje, "Finagil - Facturas para Descuento")
            EnviaCorreo("Leonardo Ayala (Finagil) <layala@finagil.com.mx>", "layala@finagil.com.mx", Mensaje, "Finagil - Facturas para Descuento")
        Next
    End Sub

    Sub ValidaDatos(ByVal Archivo As String, ByRef total As Decimal, ByRef RFC As String, ByVal Serie As String, ByVal Folio As String, ByVal Importe As String, ByVal FechaF As String)

        Dim SerieFolio As String = Serie & Folio

        Dim ta As New Factor100DSTableAdapters.WEB_FacturasTableAdapter
        Dim tb As New Factor100DSTableAdapters.WEB_ClientesTableAdapter
        Dim DVencimientos As New Factor100DSTableAdapters.WEB_DVencimientoTableAdapter

        Dim user As String = RFC
        If tb.ExisteClienteNOM(Session.Item("User"), RFC) <= 0 Then
            If tb.ExisteClienrteRFC(RFC, Session.Item("User")) <= 0 Then
                LberrorXML.Visible = True
                LberrorXML.Text = LberrorXML.Text & "<BR> Cliente no configurado " & RFC
            End If
        End If

        If InStr(SerieFolio.Trim, " ") > 0 Then
            LberrorXML.Visible = True
            LberrorXML.Text = LberrorXML.Text & "<BR> Referencia de Factura no valida " & SerieFolio
        End If
        If ta.ExisteFactura(SerieFolio, RFC) > 0 Then
            LberrorXML.Visible = True
            LberrorXML.Text = LberrorXML.Text & "<BR> La factura ya fue cargada " & SerieFolio
        End If

    End Sub

    Sub CalculaFecVecnt(ByRef FecVecn As Date, ByVal RFC As String)
        If FecVecn.DayOfWeek = DayOfWeek.Saturday Then FecVecn = FecVecn.AddDays(-1)
        If FecVecn.DayOfWeek = DayOfWeek.Sunday Then FecVecn = FecVecn.AddDays(-2)

        Dim Festivos As New Factor100DSTableAdapters.WEB_DiasFestivosTableAdapter

        If Festivos.EsFestivo(FecVecn) > 0 Then
            FecVecn = FecVecn.AddDays(1)
            CalculaFecVecnt(FecVecn, RFC)
        End If
        Festivos.Dispose()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        GridView1.DataSource = dt
        GridView1.DataBind()
        FileUpload1.Visible = True
        Submit1.Visible = True
        Button1.Visible = False
        Button2.Visible = False
        LberrorXML0.Visible = False
    End Sub
End Class