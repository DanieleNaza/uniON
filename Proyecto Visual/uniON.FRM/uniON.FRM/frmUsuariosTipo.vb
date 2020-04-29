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
                    cmb_agregar.Enabled = True
                    cmb_aceptar.Enabled = False
                    cmb_cancelar.Enabled = False
                    grl_grilla.Enabled = True
                    Panel.BackColor = Color.White
                    lbl_mensaje.Text = "Consultando"
                    lbl_mensaje.ForeColor = Color.Black



                Case EstadodelFormulario.eAgregar

                    HabilitarEdicion()
                    txtIdTipoUsuario.Enabled = False
                    cmb_aceptar.Enabled = True
                    cmb_cancelar.Enabled = True
                    DesHabilitarComandos()
                    grl_grilla.Enabled = False
                    Limpiar()
                    txtNombre.Focus()
                    Panel.BackColor = Color.Red
                    lbl_mensaje.Text = "Agregando"
                    lbl_mensaje.ForeColor = Color.White


                Case EstadodelFormulario.eEditar

                    HabilitarEdicion()
                    txtIdTipoUsuario.Enabled = False
                    cmb_aceptar.Enabled = True
                    cmb_cancelar.Enabled = True
                    DesHabilitarComandos()
                    grl_grilla.Enabled = False
                    Panel.BackColor = Color.Red
                    lbl_mensaje.Text = "Modificando"
                    lbl_mensaje.ForeColor = Color.White

            End Select
            eEstado = vNewValue

        End Set
    End Property

#End Region

#Region "Formulario"


    Private Sub frmUsuariosTipo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Estado = EstadodelFormulario.eConsulta

    End Sub
    Private Sub frmUsuariosTipos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.F3 And cmb_agregar.Enabled = True Then 'agregar
            Me.Estado = EstadodelFormulario.eAgregar
        End If

        If e.KeyCode = Keys.F4 And cmb_modificar.Enabled = True Then 'Modificar
            Me.Estado = EstadodelFormulario.eEditar
        End If


        If e.KeyCode = Keys.F12 And cmb_limpiar.Enabled = True Then 'Limpiar
            Me.Estado = EstadodelFormulario.eConsulta
        End If
    End Sub

    Private Sub frmUsuariosTipo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

        If Asc(e.KeyChar) = Keys.Escape Then
            Me.Dispose()
        End If

    End Sub
#End Region


#Region "Procedimientos"

    Private Sub CargarGrilla()
        BuscarTodos()
    End Sub

    Private Sub BuscarTodos()

        Dim oDs As New DataSet
        Dim oUsuariosTipo As New cUsuariosTipo


        oDs = oUsuariosTipo.BuscarTodos
        grl_grilla.DataSource = oDs.Tables(0)
        grl_grilla.Columns(0).Width = 50
        oDs = Nothing
        oUsuariosTipo = Nothing

    End Sub

    Private Sub BuscarPorId(ByVal IdTipoUsuario As Integer)

        Dim oDs As New DataSet
        Dim oUsuariosTipo As New cUsuariosTipo

        oDs = oUsuariosTipo.BuscarPorId(IdTipoUsuario)

        txtIdTipoUsuario.Text = oDs.Tables(0).Rows(0).Item(IdTipoUsuario)
        txtNombre.Text = oDs.Tables(0).Rows(0).Item("Descripcion")
        chk_activo.Checked = oDs.Tables(0).Rows(0).Item("Activo")


        oDs = Nothing
        oUsuariosTipo = Nothing

    End Sub

    Private Sub Limpiar()

        CargarGrilla()
        txtIdTipoUsuario.Text = ""
        txtNombre.Text = ""

    End Sub

    Private Sub HabilitarEdicion()
        txtIdTipoUsuario.Enabled = True
        txtNombre.Enabled = True
        chk_activo.Enabled = True

    End Sub

    Private Sub DesHabilitarEdicion()
        txtNombre.Enabled = False
        txtIdTipoUsuario.Enabled = False
        chk_activo.Enabled = False

    End Sub

    Private Sub HabilitarComandos()

        cmb_agregar.Enabled = True
        cmb_modificar.Enabled = True


    End Sub

    Private Sub DesHabilitarComandos()

        cmb_agregar.Enabled = False
        cmb_modificar.Enabled = False

    End Sub

#End Region

#Region "Botones  de comando"

    Private Sub cmb_limpiar_Click(sender As Object, e As EventArgs) Handles cmb_limpiar.Click

        Me.Estado = EstadodelFormulario.eConsulta

    End Sub

    Private Sub cmb_modificar_Click(sender As Object, e As EventArgs) Handles cmb_modificar.Click

        Me.Estado = EstadodelFormulario.eEditar

    End Sub

    Private Sub cmb_agregar_Click(sender As Object, e As EventArgs) Handles cmb_agregar.Click

        Me.Estado = EstadodelFormulario.eAgregar

    End Sub

    Private Sub cmb_aceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_aceptar.Click

        Try
            If Validar() = True Then

                Dim oUsuariosTipo As New cUsuariosTipo

                If Me.Estado = EstadodelFormulario.eEditar Then

                    oUsuariosTipo.Modificar(txtIdTipoUsuario.Text, txtNombre.Text, chk_activo.Checked)
                    MsgBox("Se modifico correctamente el tipo de Usuario Nro: " + txtNombre.Text + "con el código Nro: " + txtIdTipoUsuario.Text, MsgBoxStyle.Information, "Exitos")

                End If
                If Me.Estado = EstadodelFormulario.eAgregar Then
                    Dim Resultado As Integer
                    Resultado = oUsuariosTipo.Agregar(txtNombre.Text, chk_activo.Checked)
                    MsgBox("Se agrego correctamente el Tipo de Usuario" + txtNombre.Text + "Con el Nro: " + Resultado.ToString, MsgBoxStyle.Information, "Exitos")
                End If
                Me.Estado = EstadodelFormulario.eConsulta


            End If

        Catch
            MsgBox("Sucedio un error", MsgBoxStyle.Critical, "ERROR")

        End Try


    End Sub

    Private Sub cmb_cancelar_Click(sender As Object, e As EventArgs) Handles cmb_cancelar.Click

        If MsgBox("Esta seguro de cancelar?" & vbCrLf & "se perderan las modificaciones", vbYesNo, "Confirmacion de accion") = MsgBoxResult.Yes Then

            Me.Estado = EstadodelFormulario.eConsulta
        End If


    End Sub

#End Region

#Region "GRILLA"

    Private Sub grl_grilla_DoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grl_grilla.CellContentClick
        BuscarPorId(grl_grilla.CurrentRow.Cells(0).Value)
        cmb_modificar.Enabled = True

    End Sub


#End Region
#Region "FUNCIONES"

    Private Function Validar() As Boolean

        If txtNombre.Text = "" Then

            MsgBox(" Complete el nombre del Usuario", MsgBoxStyle.Exclamation, "Mensaje")
            Return False

        End If

        Return True
    End Function

#End Region
End Class