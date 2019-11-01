Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Public Class PR_StockActualProductosLotes

#Region "Atributos"

    Private Titulo As String
    Private Tipo As Byte
    Public _tab As SuperTabItem

    Public Property pTitulo() As String
        Get
            Return Titulo
        End Get
        Set(ByVal valor As String)
            Titulo = valor
        End Set
    End Property

    Public Property pTipo() As Byte
        Get
            Return Tipo
        End Get
        Set(ByVal valor As Byte)
            Tipo = valor
        End Set
    End Property

#End Region
#Region "Eventos"
    Private Sub _prCargarReporte()

    End Sub
    Private Sub PR_StockActual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub
    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prBuscar()
    End Sub

    Private Sub checkTodosGrupos_CheckValueChanged(sender As Object, e As EventArgs) Handles checkTodosGrupos.CheckValueChanged
        If (checkTodosGrupos.Checked) Then
            cbGrupos.Enabled = False
        Else
            cbGrupos.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxX1_CheckValueChanged(sender As Object, e As EventArgs) Handles chbTodos.CheckValueChanged
        If (chbTodos.Checked) Then
            txtValor.ReadOnly = True
            txtValor.ResetText()
        Else
            txtValor.ReadOnly = False
            txtValor.Select()
        End If
    End Sub
#End Region

#Region "Metodos"
    Private Sub _prIniciarTodo()
        'L_prAbrirConexion()
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.WindowState = FormWindowState.Maximized
        Me.Text = pTitulo
        _prCargarReporte()
        _prCargarComboGrupos(cbGrupos)
    End Sub
    Private Sub _prBuscar()
        Dim _dt As New DataTable
        _dt = L_fnBuscarStockLote(cbGrupos.Value, checkTodosGrupos.Checked, txtValor.Text, chbTodos.Checked)
        If (_dt.Rows.Count > 0) Then
            Dim objrep As New R_StockActualLote()
            objrep.SetDataSource(_dt)
            'MReportViewer
            MReportViewer.ReportSource = objrep
        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                       My.Resources.INFORMATION, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            MReportViewer.ReportSource = Nothing
        End If

    End Sub
    Private Sub _prCargarComboGrupos(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnObtenerGruposLibreria()
        'a.ylcod2,yldes2
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 60
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("yldes2").Width = 250
            .DropDownList.Columns("yldes2").Caption = "GRUPOS"
            .ValueMember = "yccod3"
            .DisplayMember = "yldes2"
            .DataSource = dt
            .Refresh()
        End With
        If (dt.Rows.Count > 0) Then
            cbGrupos.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _tab.Close()
    End Sub
#End Region
End Class