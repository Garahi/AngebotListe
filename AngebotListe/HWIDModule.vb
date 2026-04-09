Imports System.Management

Module HWIDModule

    Public Function GetHWID() As String
        Try
            Dim hwid As String = ""
            Dim mc As New ManagementClass("Win32_Processor")
            Dim moc As ManagementObjectCollection = mc.GetInstances()

            For Each mo As ManagementObject In moc
                hwid = mo.Properties("ProcessorId").Value.ToString()
                Exit For
            Next

            Return hwid
        Catch ex As Exception
            Return "UNKNOWN_HWID"
        End Try
    End Function

End Module

