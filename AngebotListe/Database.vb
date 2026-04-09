Imports System.Data.SqlClient




Public Class Database

    Private Shared connectionString As String = "Data Source=185.187.170.107;Initial Catalog=SalihLicenseAdmin;Persist Security Info=True;User Id=salih;Password=Garahi1943;"

    Public Shared Function GetLicense(key As String) As DataTable
        Dim dt As New DataTable()

        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand("SELECT * FROM Licenses WHERE LicenseKey=@key", con)
                cmd.Parameters.AddWithValue("@key", key)
                con.Open()

                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
        End Using

        Return dt
    End Function
    'Public Shared Function GetLicenseByHWID(hwid As String) As DataTable
    '    Dim dt As New DataTable()

    '    Using con As New SqlConnection(connectionString)
    '        Using cmd As New SqlCommand("SELECT * FROM Licenses WHERE BoundHWID=@hwid AND IsActive=1", con)
    '            cmd.Parameters.AddWithValue("@hwid", hwid)
    '            con.Open()

    '            Dim da As New SqlDataAdapter(cmd)
    '            da.Fill(dt)
    '        End Using
    '    End Using

    '    Return dt
    'End Function
    Public Shared Function GetLicenseByHWID(hwid As String) As DataTable
        Dim dt As New DataTable()

        Using con As New SqlConnection(connectionString)
            Using da As New SqlDataAdapter("
            SELECT * FROM Licenses WHERE BoundHWID = @hwid
        ", con)
                da.SelectCommand.Parameters.AddWithValue("@hwid", hwid)
                da.Fill(dt)
            End Using
        End Using

        Return dt
    End Function


    Public Shared Sub BindHWID(licenseKey As String, hwid As String)
        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand("
            UPDATE Licenses 
            SET BoundHWID = @hwid 
            WHERE LicenseKey = @key
        ", con)

                cmd.Parameters.AddWithValue("@hwid", hwid)
                cmd.Parameters.AddWithValue("@key", licenseKey)

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Public Shared Function GetLicenseByHWID(hwid As String, program As String) As DataTable
        Dim dt As New DataTable()

        Using con As New SqlConnection(connectionString)
            con.Open()

            Dim cmd As New SqlCommand("
SELECT *
FROM Licenses
WHERE BoundHWID = @hwid
  AND ProgramName = @program
  AND IsActive = 1
  AND ExpireDate >= GETDATE()
", con)

            cmd.Parameters.AddWithValue("@hwid", hwid)
            cmd.Parameters.AddWithValue("@program", program)

            dt.Load(cmd.ExecuteReader())
        End Using

        Return dt
    End Function

    'Public Shared Function GetLicenseByHWID(hwid As String, program As String) As DataTable
    '    Dim dt As New DataTable()

    '    Using con As New SqlConnection(connectionString)
    '        Using da As New SqlDataAdapter("
    '        SELECT * FROM Licenses 
    '        WHERE BoundHWID = @hwid 
    '        AND ProgramName = @program
    '    ", con)
    '            da.SelectCommand.Parameters.AddWithValue("@hwid", hwid)
    '            da.SelectCommand.Parameters.AddWithValue("@program", program)
    '            da.Fill(dt)
    '        End Using
    '    End Using

    '    Return dt
    'End Function

End Class

