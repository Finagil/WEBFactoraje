Partial Public Class PaginaMasterDefault
    Inherits System.Web.UI.MasterPage

    Private m_Titulo As String
    Public Property Titulo() As String
        Get
            Return m_Titulo
        End Get
        Set(ByVal value As String)
            m_Titulo = value
            Me.LbDias.Text = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LbDias.Text = "Factoraje FINAGIL"
        Context.Request.Browser.Adapters.Clear()
    End Sub

End Class