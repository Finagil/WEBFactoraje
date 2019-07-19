Imports System.Net.Mail
Module Globales

    Public Function CalculaDias() As Integer
        Dim fecha_inicial As Date = Date.Now
        Dim fecha_final As Date = "31/12/" & Date.Now.Year.ToString
        Dim dias As Integer
        While fecha_inicial <= fecha_final
            If fecha_inicial.DayOfWeek <> DayOfWeek.Saturday And fecha_inicial.DayOfWeek <> DayOfWeek.Sunday Then
                dias = dias + 1
            End If
            fecha_inicial = fecha_inicial.AddDays(1)
        End While
        Return (dias)
    End Function

    Public Function DiasHabiles(ByVal F1 As Date, ByVal F2 As Date) As Integer
        Dim dias As Integer
        While F1 < F2
            If F1.DayOfWeek <> DayOfWeek.Saturday And F1.DayOfWeek <> DayOfWeek.Sunday Then
                dias = dias + 1
            End If
            F1 = F1.AddDays(1)
        End While
        Return (dias)
    End Function

    Public Sub EnviaCorreo(ByVal De As String, ByVal Para As String, ByVal Mensaje As String, ByVal Asunto As String, Optional cc As String = "")
        Dim taCorreos As New ProdDSTableAdapters.GEN_Correos_SistemaFinagilTableAdapter
        taCorreos.Insert(De, Para, Asunto, Mensaje, False, Date.Now, "")
        taCorreos.Dispose()
    End Sub

    Sub LlenaDatosFactor100()
        Dim rr As Factor100DS.CO_CLIENRow
        Dim rr2 As Factor100DS.CO_TITCORow
        Dim DSorg As New FactorVWDS1
        Dim DSfin As New Factor100DS
        Dim CIDS As New ComunInfo100DS
        Dim taCliORG As New ComunInfo100DSTableAdapters.CO_CLIENTableAdapter
        Dim taCliFIN As New Factor100DSTableAdapters.CO_CLIENTableAdapter
        Dim taTitORG As New FactorVWDS1TableAdapters.CO_TITCOTableAdapter
        Dim taTitFIN As New Factor100DSTableAdapters.CO_TITCOTableAdapter

        taCliORG.Fill(CIDS.CO_CLIEN)
        taCliFIN.Fill(DSfin.CO_CLIEN)
        If CIDS.CO_CLIEN.Rows.Count <> DSfin.CO_CLIEN.Rows.Count Then
            taCliFIN.Truncate("", "")
            For Each r As DataRow In CIDS.CO_CLIEN.Rows
                rr = DSfin.CO_CLIEN.NewCO_CLIENRow
                For x As Integer = 0 To r.ItemArray.Length - 1
                    rr.Item(x) = r.Item(x)
                Next
                DSfin.CO_CLIEN.AddCO_CLIENRow(rr)
            Next
            DSfin.CO_CLIEN.GetChanges()
            taCliFIN.Update(DSfin.CO_CLIEN)
        End If

        taTitORG.Fill(DSorg.CO_TITCO)
        taTitFIN.Fill(DSfin.CO_TITCO)

        If DSorg.CO_TITCO.Rows.Count <> DSfin.CO_TITCO.Rows.Count Then
            taTitFIN.Truncate("")
            For Each r As DataRow In DSorg.CO_TITCO.Rows
                rr2 = DSfin.CO_TITCO.NewCO_TITCORow
                For x As Integer = 0 To 34
                    rr2.Item(x) = r.Item(x)
                Next
                DSfin.CO_TITCO.AddCO_TITCORow(rr2)
            Next
            DSfin.CO_TITCO.GetChanges()
            taTitFIN.Update(DSfin.CO_TITCO)
        End If
    End Sub

End Module
