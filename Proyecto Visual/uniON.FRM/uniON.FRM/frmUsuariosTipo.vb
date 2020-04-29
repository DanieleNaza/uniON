Imports uniON.AD


Public Class frmUsuariosTipo



#Region "Variables"
    Private eEstado As EstadodelFormulario
#End Region

#Region "Enumeraciones"
    Public Enum EstadodelFormulario
        eConsulta = 1
        eAgregar = 2
        eEditar = 3
    End Enum
#End Region

#Region "Propiedades"
    Public Property Estado() As EstadodelFormulario
        Get
            Return eEstado
        End Get
        Set(ByVal vNewValue As EstadodelFormulario)

            Select Case vNewValue

                Case EstadodelFormulario.eConsulta

                    Limpiar()
                    DesHabilitarEdicion()
                    DesHabilitarComandos()
                    cmdAgregar.Enabled = True
                    cmdAceptar.Enabled = False
                    cmdCancelar.Enabled = False
                    grlGrilla.Enabled = True
                    Panel1.BackColor = Color.White
                    lblAccion.Text = "Consultando"
                    lblAccion.ForeColor = Color.Black

                Case EstadodelFormulario.eAgregar

                    HabilitarEdicion()
                    txtIdUsuario.Enabled = False
                    cmdAceptar.Enabled = True
                    cmdCancelar.Enabled = True
                    DesHabilitarComandos()
                    grlGrilla.Enabled = False
                    Limpiar()
                    txtDescripcion.Focus()
                    Panel1.BackColor = Color.MediumAquamarine
                    lblAccion.Text = "Agregando"
                    lblAccion.ForeColor = Color.Black

                Case EstadodelFormulario.eEditar

                    HabilitarEdicion()
                    txtIdUsuario.Enabled = False
                    cmdAceptar.Enabled = True
                    cmdCancelar.Enabled = True
                    DesHabilitarComandos()
                    grlGrilla.Enabled = False
                    Panel1.BackColor = Color.MediumAquamarine
                    lblAccion.Text = "Modificando"
                    lblAccion.ForeColor = Color.Black

            End Select
            eEstado = vNewValue
        End Set
    End Property
#End Region

#Region "Formulario"
    Private Sub frmUsuariosTipo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Estado = EstadodelFormulario.eConsulta
    End Sub
#End Region

#Region "Procedimientos"
    Private Sub CargarGrilla()
        BuscarTodos()
    End Sub

    Private Sub BuscarTodos()
        Dim oDs As New DataSet
        Dim oUsuarioTipo As New cUsuarioTipo

        oDs = oUsuarioTipo.BuscarTodos

        grlGrilla.DataSource = oDs.Tables(0)

        grlGrilla.Columns(0).HeaderText = "Cod."
        grlGrilla.Columns(0).Width = 50

        oDs = Nothing
        oUsuarioTipo = Nothing
    End Sub

    Private Sub BuscarPorID(ByVal IdTipoUsuario As Integer)
        Dim oDs As New DataSet
        Dim oUsuarioTipo As New cUsuarioTipo

        oDs = oUsuarioTipo.BuscarPorId(IdTipoUsuario)

        txtIdUsuario.Text = oDs.Tables(0).Rows(0).Item("IdTipoUsuario")
        txtDescripcion.Text = oDs.Tables(0).Rows(0).Item("Descripcion")
        chkActivo.Checked = oDs.Tables(0).Rows(0).Item("Activo")

        oDs = Nothing
        oUsuarioTipo = Nothing
    End Sub

    Private Sub Limpiar()
        CargarGrilla()
        txtIdUsuario.Text = ""
        txtDescripcion.Text = ""
        chkActivo.Checked = True
    End Sub

    Private Sub HabilitarEdicion()
        txtIdUsuario.Enabled = True
        txtDescripcion.Enabled = True
        chkActivo.Enabled = True
    End Sub

    Private Sub DesHabilitarEdicion()
        txtIdUsuario.Enabled = False
        txtDescripcion.Enabled = False
        chkActivo.Enabled = False
    End Sub

    Private Sub HabilitarComandos()
        cmdAgregar.Enabled = True
        cmdModificar.Enabled = True
    End Sub

    Private Sub DesHabilitarComandos()
        cmdAgregar.Enabled = False
        cmdModificar.Enabled = False
    End Sub


#End Region

#Region "Botones de comando"
    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs)
        Me.Estado = EstadodelFormulario.eAgregar
    End Sub

    Private Sub cmdModificar_Click(sender As Object, e As EventArgs) Handles cmdModificar.Click
        Me.Estado = EstadodelFormulario.eEditar
    End Sub

    Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        Try
            If Validar() = True Then

                Dim oPais As New cPaises

                If Me.Estado = EstadodelFormulario.eEditar Then
                    oPais.Modificar(txtIdUsuario.Text, txtDescripcion.Text, chkActivo.Checked)
                    MsgBox("Se modificó correctamente el premio con el código nro: " + txtIdUsuario.Text, MsgBoxStyle.Information, "Exitos!")
                End If

                If Me.Estado = EstadodelFormulario.eAgregar Then
                    Dim resultado As Integer
                    resultado = oPais.Agregar(txtDescripcion.Text, chkActivo.Checked)
                    MsgBox("Se agregó correctamente el premio " + txtDescripcion.Text + " con el código nro: " + resultado.ToString, MsgBoxStyle.Information, "Exitos!")
                End If

                Me.Estado = EstadodelFormulario.eConsulta
            End If
        Catch
            MsgBox("Sucedió un error", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub cmdCancelar_Click(sender As Object, e As EventArgs) Handles cmdCancelar.Click
        If MsgBox("¿Está seguro que desea cancelar?" & vbCrLf &
                "Se perderán las últimas modificaciones",
                vbYesNo, "Confirmación de acción") = MsgBoxResult.Yes Then

            Me.Estado = EstadodelFormulario.eConsulta

        End If
        Exit Sub
    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As EventArgs) Handles cmdSalir.Click
        Close()
    End Sub

    Private Sub cmdLimpiar_Click(sender As Object, e As EventArgs) Handles cmdLimpiar.Click

        Me.Estado = EstadodelFormulario.eConsulta

    End Sub


#End Region

#Region "Funciones"
    Private Function Validar() As Boolean
        If txtDescripcion.Text = "" Then

            MsgBox("Complete el nombre del Premio", MsgBoxStyle.Exclamation, "Mensaje")

            Return False
        End If
        Return True
    End Function
#End Region


End Class