﻿Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Practices.EnterpriseLibrary.Data
Public Class cPremiosTipo

    Public Sub New()
        oDatabase = DatabaseFactory.CreateDatabase("Conn")
    End Sub

    Public Sub New(ByVal str As String)

    End Sub

    Public Function BuscarPorId(ByVal IdTipoPremio As Integer) As DataSet
        Try
            Return oDatabase.ExecuteDataSet("PremiosBuscarPorId", IdTipoPremio)
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function BuscarTodos() As DataSet
        Try
            Return oDatabase.ExecuteDataSet("PremiosBuscarTodos")
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

End Class
