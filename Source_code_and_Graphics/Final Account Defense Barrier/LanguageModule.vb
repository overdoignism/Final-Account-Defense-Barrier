Module LanguageModule

    Public Enum WorkType
        FileW = 0
        FileR = 1
        FileC = 2
        FileD = 3
    End Enum

    Public Enum UsingTxt

        'Title/Head
        Ti_Err = 1
        Ti_Cnfm = 2
        Ti_Info = 3
        Ti_Cri = 4
        Ti_Att = 5

        'Login
        Lo_NoMatch = 8
        Lo_IfNew = 9
        Lo_AllNo = 10
        Lo_DoNew = 11
        Lo_Login_tt1 = 12
        Lo_Login_tt2 = 13

        'Catalog

        Ca_TD = 16
        Ca_DuD = 17
        Ca_AwT = 18
        Ca_NPrq = 19
        Ca_IDel = 20
        Ca_Upd = 21
        Ca_TRd = 22
        Ca_DuUpd = 23
        Ca_CLin = 24
        Ca_Load = 25
        Ca_PwdIn = 26
        Ca_Tro = 27
        Ca_Tabt = 28
        Ca_IwT = 29
        Ca_NoA = 30

        'Regular work
        RG_DuD = 33
        RG_DuUpd = 34
        RG_Add = 35
        RG_Updd = 36
        RG_MUp = 37
        RG_MDn = 38
        RG_Ded = 39
        RG_Ted = 40
        RG_Oed = 41
        RG_DuA = 42

        'File Info
        FI_bt = 45
        FI_Lwt = 46
        FI_Fn = 47
        FI_Cf = 48

        'CSV Work
        CS_AuIC = 51
        CS_CId = 52
        CS_CIf = 53
        CS_Mnc = 54
        CS_Pcp = 55
        CS_Ncc = 56
        CS_Pnm = 57
        CS_NcA = 58
        CS_Svd = 59

        'Univers Error
        UN_Err = 62

        'Item string
        IT_nc = 65
        IT_un = 66
        IT_dk = 67
        IT_md = 68
        IT_Ah = 69
        IT_DI = 70
        IT_co = 71
        IT_cpc = 72
        IT_sk = 73
        IT_Hy = 74
        IT_30s = 75
        IT_min = 76

        'ToolTip
        TT_Pw = 79
        TT_Pv = 80
        TT_Tt = 81
        TT_Ur = 82
        TT_Un = 83
        TT_Rm = 84
        TT_N1 = 85
        TT_N2 = 86

        'Password / Key input and BIP39
        PK_Pi = 89
        PK_Pvf = 90
        PK_Pv1 = 91
        PK_Pv2 = 92
        PK_Pv3 = 93
        PK_Pv4 = 94
        PK_Fad1 = 95
        PK_Fad2 = 96
        PK_C2s = 97

        'other message/text
        OT_Ld = 100
        OT_Cd = 101
        OT_Df = 102
        OT_UL = 103
        OT_Fz = 104
        OT_CPC = 105
        OT_HKC = 106
        OT_SDnw = 107
        OT_OSo = 108
        OT_dbg = 109
        OT_bwf = 110
        OT_SEr = 111
        OT_ROM = 112
        OT_LOK = 113
        OT_FSK = 114
        OT_nAC = 115
        OT_SDce = 116
        OT_Dsf = 117

        'Windows error
        WE_Ad = 120
        WE_Wp = 121
        WE_Wf = 122
        WE_Rf = 123
        WE_DE = 124
        WE_SV = 125
        WE_DF = 126
        WE_oth = 127
        WE_Ec = 128
        Er_Fw = 129
        Er_Fr = 130
        Er_Fc = 131
        Er_Fd = 132
        Er_Ab = 133

        'Other error
        Err_Fde = 137
        Err_SDf = 138
        Err_CPf = 139
        Err_CPfc = 140
        Err_Unk = 141

        'AT Field
        AT_D = 144
        AT_uD = 145
        AT_dn = 146
        AT_Df = 147

        'Other/new coming
        Er2_RCs = 150
        Er2_Fco = 151

        'KDF MAGI-C Error
        KDF_Me1 = 154
        KDF_Me2 = 155
        KDF_Me3 = 156

        'MIT
        MIT_Tol = 159

        'WINE
        WINE_WARN = 160


    End Enum

    Public LangDatas(4, 0) As String
    Public LIdx As Integer = 0

    'LangStrs(LIdx, UsingTxt. )
    'MSGBOXNEW(GetSimpleErrorMessage(hresult, 0), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)

    Public Function LangStrs(LangTypes As Integer, StrIdx As UsingTxt) As String

        Dim Tmpstr As String = LangDatas(LangTypes, StrIdx)

        If Tmpstr IsNot Nothing Then
            Return Tmpstr
        Else
            Return LangDatas(0, StrIdx)
        End If

    End Function

    'Public Sub LoadLang()

    '    Dim FileReader As New IO.StreamReader("languageTxt.csv")
    '    Dim TmpArray(0) As String

    '    Do Until FileReader.EndOfStream

    '        ParseCSV_L1(FileReader.ReadLine(), TmpArray)

    '        ReDim Preserve LangDatas(3, Counter1)
    '        LangDatas(0, Counter1) = TmpArray(0)
    '        LangDatas(1, Counter1) = TmpArray(1)
    '        LangDatas(2, Counter1) = TmpArray(2)

    '        Counter1 += 1

    '    Loop
    '    FileReader.Close()

    'End Sub

    Public Sub Load_Language_From_Resource()

        Dim Counter1 As Integer = 1
        Dim memoryStream As New IO.MemoryStream()

        Using archive As New System.IO.Compression.ZipArchive(New IO.MemoryStream(My.Resources.Resource1.TXTFile))
            For Each entry As System.IO.Compression.ZipArchiveEntry In archive.Entries
                If entry.Name = "LanguageFile.csv" Then
                    Using entryStream As IO.Stream = entry.Open()
                        entryStream.CopyTo(memoryStream)
                        memoryStream.Position = 0
                        Dim sr As New IO.StreamReader(memoryStream, True)

                        Dim TmpArray(0) As String

                        Do Until sr.EndOfStream

                            ParseCSV_L1(sr.ReadLine(), TmpArray)

                            ReDim Preserve LangDatas(4, Counter1)
                            LangDatas(0, Counter1) = TmpArray(0)
                            LangDatas(1, Counter1) = TmpArray(1)
                            LangDatas(2, Counter1) = TmpArray(2)

                            Counter1 += 1

                        Loop

                    End Using
                    Exit For
                End If
            Next
        End Using


    End Sub


    Public Sub Load_Language_File(LoadFile As String)

        Dim IOreader1 As System.IO.StreamReader = Nothing

        Try
            If System.IO.File.Exists(LoadFile) Then
                If New System.IO.FileInfo(LoadFile).Length <= 25600 Then
                    IOreader1 = New System.IO.StreamReader(LoadFile)
                    For IDX01 As Integer = 1 To LangDatas.GetUpperBound(1)
                        If Not IOreader1.EndOfStream Then
                            LangDatas(4, IDX01) = Replace(IOreader1.ReadLine, "\n", vbCrLf)
                            LangDatas(4, IDX01) = Replace(LangDatas(4, IDX01), Chr(0), "")
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

            If IOreader1 IsNot Nothing Then
                IOreader1.Close()
                IOreader1.Dispose()
            End If

        End Try

    End Sub

    Public Sub ParseCSV_L1(ByRef InputStr As String, ByRef ReturnStrArray() As String)


        Dim Quo_On As Boolean = False
        Dim RSA_IDX As Integer = 0
        Dim TmpEachItem() As String
        Dim TmpSTB As New System.Text.StringBuilder

        ReDim ReturnStrArray(RSA_IDX)

        For Each TmpChar As Char In InputStr

            If TmpChar = """" Then
                Quo_On = Not Quo_On
            End If

            If TmpChar = "," Then
                If Not Quo_On Then
                    TmpSTB.Append(TmpChar)
                Else
                    TmpSTB.Append(Chr(13))
                End If
            Else
                TmpSTB.Append(TmpChar)
            End If
        Next

        TmpEachItem = TmpSTB.ToString().Split(",")

        For Each TmpStr As String In TmpEachItem

            ReDim Preserve ReturnStrArray(RSA_IDX)

            TmpStr = Replace(TmpStr, Chr(13), ",")

            If TmpStr IsNot Nothing Then
                If TmpStr.StartsWith("""") Then TmpStr = TmpStr.Substring(1, TmpStr.Length - 1)
                If TmpStr.EndsWith("""") Then TmpStr = TmpStr.Substring(0, TmpStr.Length - 1)
            Else
                TmpStr = ""
            End If

            TmpStr = Replace(TmpStr, """""", """")

            ReturnStrArray(RSA_IDX) = TmpStr
            RSA_IDX += 1

        Next

        For idx01 As Integer = 0 To ReturnStrArray.Length - 1
            ReturnStrArray(idx01) = Replace(ReturnStrArray(idx01), "\n", vbCrLf)
        Next

    End Sub

    Public Function GetSimpleErrorMessage(ByVal hresult As Integer, WorkType As WorkType) As String

        Dim WorkTypeStr As String = ""

        Select Case WorkType
            Case WorkType.FileW 'Write file
                WorkTypeStr = LangStrs(LIdx, UsingTxt.Er_Ab).Replace("$$$", LangStrs(LIdx, UsingTxt.Er_Fw))
            Case WorkType.FileR 'Read file
                WorkTypeStr = LangStrs(LIdx, UsingTxt.Er_Ab).Replace("$$$", LangStrs(LIdx, UsingTxt.Er_Fr))
            Case WorkType.FileC 'File or dir create
                WorkTypeStr = LangStrs(LIdx, UsingTxt.Er_Ab).Replace("$$$", LangStrs(LIdx, UsingTxt.Er_Fc))
            Case WorkType.FileD 'File or dir delete
                WorkTypeStr = LangStrs(LIdx, UsingTxt.Er_Ab).Replace("$$$", LangStrs(LIdx, UsingTxt.Er_Fd))
            Case Else
        End Select

        Dim TmpStr As String = WorkTypeStr + D_vbcrlf + LangStrs(LIdx, UsingTxt.WE_Ec) + " 0x" + hresult.ToString("x") + D_vbcrlf

        Select Case hresult
            Case &H80070005 ' ERROR_ACCESS_DENIED
                Return TmpStr + LangStrs(LIdx, UsingTxt.WE_Ad)
            Case &H80070013 ' ERROR_WRITE_PROTECT
                Return TmpStr + LangStrs(LIdx, UsingTxt.WE_Wp)
            Case &H8007001D ' ERROR_WRITE_FAULT
                Return TmpStr + LangStrs(LIdx, UsingTxt.WE_Wf)
            Case &H8007001E ' ERROR_READ_FAULT
                Return TmpStr + LangStrs(LIdx, UsingTxt.WE_Rf)
            Case &H8007001F ' ERROR_GEN_FAILURE
                Return TmpStr + LangStrs(LIdx, UsingTxt.WE_DE)
            Case &H80070020, &H80070021 ' ERROR_SHARING_VIOLATION
                Return TmpStr + LangStrs(LIdx, UsingTxt.WE_SV)
            Case &H80070027, &H80070070 ' ERROR_DISK_FULL
                Return TmpStr + LangStrs(LIdx, UsingTxt.WE_DF)
            Case Else
                Return TmpStr + LangStrs(LIdx, UsingTxt.WE_oth)
        End Select
    End Function


    Public Function GetSysLangCode() As Integer

        Dim NameStr As String = Threading.Thread.CurrentThread.CurrentCulture.Name

        If NameStr = "ja" Then Return 1
        If NameStr = "zh-TW" Then Return 2
        If NameStr = "zh-CN" Then Return 3
        If NameStr.StartsWith("zh-") Then Return 2

        Return 0

    End Function

    Public Function GetBitmapFromCode(TheCode As Integer) As Bitmap

        Select Case TheCode
            Case 0
                Return My.Resources.Resource1.Lang_en
            Case 1
                Return My.Resources.Resource1.Lang_jp
            Case 2
                Return My.Resources.Resource1.Lang_zhtw
            Case 3
                Return My.Resources.Resource1.Lang_zhcn
        End Select

        Return Nothing

    End Function

    Public Sub MakeZh_cnTxt()

        Dim zhTraditional As New Globalization.CultureInfo("zh-Hant") ' 繁體
        Dim zhSimplified As New Globalization.CultureInfo("zh-Hans")  ' 簡體

        For IDX01 As Integer = 0 To LangDatas.GetUpperBound(1)
            LangDatas(3, IDX01) = Strings.StrConv(LangDatas(2, IDX01), VbStrConv.SimplifiedChinese, zhSimplified.LCID)
        Next

    End Sub

End Module
