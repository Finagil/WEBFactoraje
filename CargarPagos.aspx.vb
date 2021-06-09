Partial Public Class CargarPagos

    Inherits System.Web.UI.Page
    Dim Lote As Integer = 0
    Dim UserX As String = ""
    Dim Importe As Decimal = 0
    Dim Bandera As Boolean

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

    Private Sub SubirAnti_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Bandera = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Lote = Request("ID")
        UserX = Request("User")
        'Cesion = Request("Cesion")
        'Label1.Text = " Lote: " & Lote & " Cesión: " & Cesion
    End Sub

    Protected Sub Submit1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Submit1.ServerClick
        Dim ta As New ProdDSTableAdapters.GEN_ComandosCMDTableAdapter
        Submit1.Disabled = True
        ta.InsertaComando("C:\Jobs\", "CargaMINDS.exe", " FACTORAJE", UserX)
        Lberror.Visible = True
    End Sub


End Class