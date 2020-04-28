Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Practices.EnterpriseLibrary.Data
Public Class cProvincias
    Dim oDatabase As Database

    Public Sub New()
        oDatabase = DatabaseFactory.CreateDatabase("Conn")
    End Sub

    Public Sub New(ByVal str As String)
    End Sub

    Public Function BuscarTodos() As DataSet
        Try
            Return oDatabase.ExecuteDataSet("ProvinciasBuscarTodos")
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function BuscarPorIdPais(ByVal IdPais As Integer) As DataSet
        Try
            Return oDatabase.ExecuteDataSet("ProvinciasBuscarPorIdPais", IdPais)
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function BuscarPorID(ByVal IdProvincia As Integer) As DataSet
        Try
            Return oDatabase.ExecuteDataSet("ProvinciaBuscarPorId", IdProvincia)
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function Agregar(ByVal IdProvincia As Integer, ByVal Provincia As String, ByVal Activo As Boolean) As Double
        Try
            Return oDatabase.ExecuteScalar("ProvinciaAgregar", IdProvincia, Provincia, Activo)
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function Modificar(ByVal IdProvincia As Integer, ByVal IdPais As Integer, ByVal Provincia As String, ByVal Activo As Boolean) As DataSet
        Try
            Return oDatabase.ExecuteDataSet("ProvinciaModificar", IdProvincia, IdPais, Provincia, Activo)
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

End Class
