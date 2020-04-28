Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Practices.EnterpriseLibrary.Data
Public Class cPaises
    Dim oDatabase As Database

    Public Sub New()
        oDatabase = DatabaseFactory.CreateDatabase("Conn")
    End Sub

    Public Sub New(ByVal str As String)
    End Sub

    Public Function BuscarPorId(ByVal IdRubro As Integer) As DataSet
        Try
            Return oDatabase.ExecuteDataSet("PaisBuscarPorId", IdRubro)
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function BuscarTodos() As DataSet
        Try
            Return oDatabase.ExecuteDataSet("PaisBuscarTodos")
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function Agregar(ByVal Pais As String, ByVal Activo As Boolean) As Double
        Try
            Return oDatabase.ExecuteScalar("PaisAgregar", Pais, Activo)
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function Modificar(ByVal IdPais As Integer, ByVal Pais As String, ByVal Activo As Boolean) As DataSet
        Try
            Return oDatabase.ExecuteDataSet("PaisModificar", IdPais, Pais, Activo)
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

End Class
