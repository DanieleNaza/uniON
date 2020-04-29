Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Practices.EnterpriseLibrary.Data
Public Class cUsuarioTipo
    Dim oDatabase As Database


    Public Sub New()
        oDatabase = DatabaseFactory.CreateDatabase("Conn")
    End Sub

    Public Sub New(ByVal str As String)
    End Sub

    Public Function BuscarPorId(ByVal IdTipoUsuario As Integer) As DataSet
        Try
            Return oDatabase.ExecuteDataSet("UsuarioTiposBuscarPorId", IdTipoUsuario)
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function BuscarTodos() As DataSet
        Try
            Return oDatabase.ExecuteDataSet("UsuariosTiposBuscarTodos")
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function Agregar(ByVal Descripcion As String, ByVal Activo As Boolean) As Double
        Try
            Return oDatabase.ExecuteScalar("UsuariosTiposAgregar", Descripcion, Activo)
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function Modificar(ByVal IdTipoUsuario As Integer, ByVal Descripcion As String, ByVal Activo As Boolean) As DataSet
        Try
            Return oDatabase.ExecuteDataSet("UsuariosTiposModificar", IdTipoUsuario, Descripcion, Activo)
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function
    Public Function Eliminar(ByVal IdTipoUsuario As Integer) As Double
        Return oDatabase.ExecuteScalar("UsuariosTiposEliminar", IdTipoUsuario)
    End Function

End Class
