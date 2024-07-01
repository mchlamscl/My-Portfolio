Module BasicFunction
    Public Function GenerateRandomNumber() As Integer
        Dim random As New Random()
        Return random.Next(100000, 999999)
    End Function
End Module
