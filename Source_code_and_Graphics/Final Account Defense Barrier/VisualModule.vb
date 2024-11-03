Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Module VisualModule

    '================ For window grayed out visual ==============

    <DllImport("Shcore.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Function GetScaleFactorForMonitor(hMon As IntPtr, ByRef pScale As DEVICE_SCALE_FACTOR) As Integer 'HRESULT
    End Function

    <DllImport("User32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Function MonitorFromWindow(ByVal hwnd As IntPtr, ByVal dwFlags As Integer) As IntPtr
    End Function

    Public Enum DEVICE_SCALE_FACTOR
        SCALE_100_PERCENT = 100
        SCALE_120_PERCENT = 120
        SCALE_125_PERCENT = 125
        SCALE_140_PERCENT = 140
        SCALE_150_PERCENT = 150
        SCALE_160_PERCENT = 160
        SCALE_175_PERCENT = 175
        SCALE_180_PERCENT = 180
        SCALE_200_PERCENT = 200
        SCALE_225_PERCENT = 225
        SCALE_250_PERCENT = 250
        SCALE_300_PERCENT = 300
        SCALE_350_PERCENT = 350
        SCALE_400_PERCENT = 400
        SCALE_450_PERCENT = 450
        SCALE_500_PERCENT = 500
    End Enum

    Public Const MONITOR_DEFAULTTONULL As Integer = &H0
    Public Const MONITOR_DEFAULTTOPRIMARY As Integer = &H1
    Public Const MONITOR_DEFAULTTONEAREST As Integer = &H2

    Public ScaleFolat As Single = 1.0F

    Public Sub GetMonScale(ByRef WhatForm As Form)

        Dim hMon As IntPtr = MonitorFromWindow(WhatForm.Handle, MONITOR_DEFAULTTOPRIMARY)
        Dim scaleFactor As DEVICE_SCALE_FACTOR
        GetScaleFactorForMonitor(hMon, scaleFactor)
        ScaleFolat = scaleFactor / 100

    End Sub

    Public Sub MakeWindowsMono(ByRef WhatForm As Form, ByRef WhatImgToPut As PictureBox)

        SetWindowDisplayAffinity(WhatForm.Handle, 0)

        Dim NewPos As Point
        NewPos.X = WhatForm.Location.X * ScaleFolat
        NewPos.Y = WhatForm.Location.Y * ScaleFolat
        Dim NewSize As Size
        NewSize.Width = WhatForm.Size.Width * ScaleFolat
        NewSize.Height = WhatForm.Size.Height * ScaleFolat

        Dim bmpOrg As New Bitmap(NewSize.Width, NewSize.Height)

        Using g1 As Graphics = Graphics.FromImage(bmpOrg)
            g1.CopyFromScreen(NewPos, Point.Empty, NewSize)
        End Using

        WhatImgToPut.Image = MonoBitmap(bmpOrg) 'newBitmap
        WhatImgToPut.BringToFront()
        WhatImgToPut.Visible = True

        If Not Sys_Chk.Screen_Capture_Allowed Then 'Disable Screen Capture
            SetWindowDisplayAffinity(WhatForm.Handle, WDA_EXCLUDEFROMCAPTURE)
        End If

    End Sub

    Public Sub UnMakeWindowsMono(ByRef WhatImgToPut As PictureBox)
        WhatImgToPut.Visible = False
        WhatImgToPut.SendToBack()
        System.Windows.Forms.Application.DoEvents()
    End Sub


    Public Function Make_Button_brighter(ByRef Orig_Bitmap As Bitmap, Optional BrightPN As Single = 1.3) As Bitmap

        ' 建立目標圖像的副本
        Dim targetBitmap As New Bitmap(Orig_Bitmap.Width, Orig_Bitmap.Height)

        ' 創建ColorMatrix，用於調整圖像亮度
        Dim brightenMatrix As New Imaging.ColorMatrix(New Single()() _
            {New Single() {BrightPN, 0, 0, 0, 0},
             New Single() {0, BrightPN, 0, 0, 0},
             New Single() {0, 0, BrightPN, 0, 0},
             New Single() {0, 0, 0, 1, 0},
             New Single() {0.12, 0.12, 0.12, 0, 1}})

        ' 創建ImageAttributes對象，並設定ColorMatrix
        Dim imageAttributes As New Imaging.ImageAttributes()
        imageAttributes.SetColorMatrix(brightenMatrix)

        ' 創建Graphics對象，並使用ImageAttributes繪製目標圖像
        Dim graphics As Graphics = Graphics.FromImage(targetBitmap)
        graphics.DrawImage(Orig_Bitmap, New Rectangle(0, 0, Orig_Bitmap.Width, Orig_Bitmap.Height), 0, 0, Orig_Bitmap.Width, Orig_Bitmap.Height, GraphicsUnit.Pixel, imageAttributes)

        ' 釋放資源
        graphics.Dispose()

        ' 顯示調整後的圖像
        Return targetBitmap

    End Function

    Public Function Make_Button_Gray(ByRef Orig_Bitmap As Bitmap, Optional LowDimm As Single = 0) As Bitmap

        ' 建立目標圖像的副本
        Dim targetBitmap As New Bitmap(Orig_Bitmap.Width, Orig_Bitmap.Height)

        ' 創建ColorMatrix，用於調整圖像亮度
        Dim brightenMatrix As New Imaging.ColorMatrix(New Single()() _
            {New Single() {0.3F, 0.3F, 0.3F, 0, 0},
             New Single() {0.59F, 0.59F, 0.59F, 0, 0},
             New Single() {0.11F, 0.11F, 0.11F, 0, 0},
             New Single() {0, 0, 0, 1, 0},
             New Single() {LowDimm, LowDimm, LowDimm, 0, 1}})

        ' 創建ImageAttributes對象，並設定ColorMatrix
        Dim imageAttributes As New Imaging.ImageAttributes()
        imageAttributes.SetColorMatrix(brightenMatrix)

        ' 創建Graphics對象，並使用ImageAttributes繪製目標圖像
        Dim graphics As Graphics = Graphics.FromImage(targetBitmap)
        graphics.DrawImage(Orig_Bitmap, New Rectangle(0, 0, Orig_Bitmap.Width, Orig_Bitmap.Height), 0, 0, Orig_Bitmap.Width, Orig_Bitmap.Height, GraphicsUnit.Pixel, imageAttributes)

        ' 釋放資源
        graphics.Dispose()

        ' 顯示調整後的圖像
        Return targetBitmap

    End Function

    Public Function Make_Button_HueChange(ByVal Orig_Bitmap As Bitmap, HueAngle As Single) As Bitmap

        ' 建立目標圖像的副本
        Dim r As Double = HueAngle * Math.PI / 180 ' degrees to radians

        ' 創建ColorMatrix，用於調整圖像亮度
        Dim theta As Single = HueAngle / 360 * 2 * Math.PI ' Degrees --> Radians
        Dim c As Single = Math.Cos(theta)
        Dim s As Single = Math.Sin(theta)

        Dim A00 As Single = 0.213 + 0.787 * c - 0.213 * s
        Dim A01 As Single = 0.213 - 0.213 * c + 0.413 * s
        Dim A02 As Single = 0.213 - 0.213 * c - 0.787 * s

        Dim A10 As Single = 0.715 - 0.715 * c - 0.715 * s
        Dim A11 As Single = 0.715 + 0.285 * c + 0.14 * s
        Dim A12 As Single = 0.715 - 0.715 * c + 0.715 * s

        Dim A20 As Single = 0.072 - 0.072 * c + 0.928 * s
        Dim A21 As Single = 0.072 - 0.072 * c - 0.283 * s
        Dim A22 As Single = 0.072 + 0.928 * c + 0.072 * s

        ' 建立目標圖像的副本
        Dim targetBitmap As New Bitmap(Orig_Bitmap.Width, Orig_Bitmap.Height)

        ' 創建ColorMatrix，用於調整圖像亮度
        Dim hueMatrix As New Imaging.ColorMatrix(New Single()() _
            {New Single() {A00, A01, A02, 0, 0},
             New Single() {A10, A11, A12, 0, 0},
             New Single() {A20, A21, A22, 0, 0},
             New Single() {0, 0, 0, 1, 0},
             New Single() {0, 0, 0, 0, 1}})

        ' 創建ImageAttributes對象，並設定ColorMatrix
        Dim imageAttributes As New Imaging.ImageAttributes()
        imageAttributes.SetColorMatrix(hueMatrix)

        ' 創建Graphics對象，並使用ImageAttributes繪製目標圖像
        Dim graphics As Graphics = Graphics.FromImage(targetBitmap)
        graphics.DrawImage(Orig_Bitmap, New Rectangle(0, 0, Orig_Bitmap.Width, Orig_Bitmap.Height), 0, 0, Orig_Bitmap.Width, Orig_Bitmap.Height, GraphicsUnit.Pixel, imageAttributes)

        ' 釋放資源
        graphics.Dispose()

        ' 顯示調整後的圖像
        Return targetBitmap

    End Function

    Public Function ResizeBitmap(originalBitmap As Bitmap, ScaleX As Single, ScaleY As Single) As Bitmap
        Dim newWidth As Integer = originalBitmap.Width * ScaleX
        Dim newHeight As Integer = originalBitmap.Height * ScaleY

        Dim resizedBitmap As New Bitmap(newWidth, newHeight)

        Using g As Graphics = Graphics.FromImage(resizedBitmap)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(originalBitmap, New Rectangle(0, 0, newWidth, newHeight))
        End Using

        Return resizedBitmap
    End Function

    Public Function MonoBitmap(ByRef bmpOrg As Bitmap) As Bitmap

        Dim newBitmap As New Bitmap(bmpOrg.Width, bmpOrg.Height)
        Dim g As Graphics = Graphics.FromImage(newBitmap)

        '創建ColorMatrix
        Dim colorMatrix As New Imaging.ColorMatrix(New Single()() _
        {
            New Single() {0.3F, 0.3F, 0.3F, 0, 0},
            New Single() {0.59F, 0.59F, 0.59F, 0, 0},
            New Single() {0.11F, 0.11F, 0.11F, 0, 0},
            New Single() {0, 0, 0, 1, 0},
            New Single() {-0.4F, -0.4F, -0.4F, 0, 1}
        })
        '創建ImageAttributes
        Dim imageAttributes As New Imaging.ImageAttributes()
        imageAttributes.SetColorMatrix(colorMatrix)

        bmpOrg = ResizeBitmap(bmpOrg, 1 / ScaleFolat, 1 / ScaleFolat)
        '使用Graphics將原始圖片繪製到Bitmap中，同時應用ImageAttributes
        g.DrawImage(bmpOrg, New Rectangle(0, 0, bmpOrg.Width, bmpOrg.Height), 0, 0, bmpOrg.Width, bmpOrg.Height, GraphicsUnit.Pixel, imageAttributes)

        Return newBitmap

    End Function

    Private Function CenterBitmapInNewImage(ByVal originalBmp As Bitmap) As Bitmap

        ' 設定新圖片的大小，寬高各加 6
        Dim newWidth As Integer = originalBmp.Width + 6
        Dim newHeight As Integer = originalBmp.Height + 6
        Dim newBmp As New Bitmap(newWidth, newHeight)

        ' 使用 Graphics 將原始圖片繪製到新圖片的中間位置
        Using g As Graphics = Graphics.FromImage(newBmp)

            g.Clear(Color.FromArgb(0, 0, 0, 0))

            ' 計算置中的位置
            Dim x As Integer = (newWidth - originalBmp.Width) \ 2
            Dim y As Integer = (newHeight - originalBmp.Height) \ 2

            ' 繪製原始圖片到新圖片中
            g.DrawImage(originalBmp, x, y)
        End Using

        ' 返回新圖片
        Return newBmp
    End Function

    Public Function CropBitmap(ByVal original As Bitmap) As Bitmap
        ' 定義要裁剪的區域 (原圖尺寸減去上下左右各 2 像素)
        Dim cropArea As New Rectangle(3, 3, original.Width - 4, original.Height - 4)

        ' 創建一個新 Bitmap，尺寸為裁剪區域的尺寸
        Dim croppedBitmap As New Bitmap(cropArea.Width, cropArea.Height)

        ' 使用 Graphics 將原始圖片的裁剪區域繪製到新 Bitmap 上
        Using g As Graphics = Graphics.FromImage(croppedBitmap)
            g.DrawImage(original, New Rectangle(0, 0, croppedBitmap.Width, croppedBitmap.Height), cropArea, GraphicsUnit.Pixel)
        End Using

        ' 返回裁剪後的 Bitmap
        Return croppedBitmap
    End Function


    Public Function The_Shine_Visual_Filter2(orgbitmap As Bitmap) As Bitmap

        Dim bmp1 As Bitmap = CenterBitmapInNewImage(orgbitmap)

        ' 鎖定 Bitmap 的位元
        Dim rect As New Rectangle(0, 0, bmp1.Width, bmp1.Height)
        Dim bmpData1 As System.Drawing.Imaging.BitmapData = bmp1.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp1.PixelFormat)
        Dim St1 As Integer = bmpData1.Stride

        ' 取得第一行的位址
        Dim ptr1 As IntPtr = bmpData1.Scan0

        ' 宣告一個陣列來儲存 Bitmap 的字節
        Dim bytes1 As Integer = Math.Abs(St1) * bmp1.Height
        Dim rgbValues1(bytes1 - 1) As Byte

        ' 將 RGB 值複製到陣列中
        System.Runtime.InteropServices.Marshal.Copy(ptr1, rgbValues1, 0, bytes1)

        Dim rgbValues2 As Single()
        rgbValues2 = Byte2Single(rgbValues1)


        Dim TheMartix() As Single = {0.003F, 0.006F, 0.012F, 0.018F, 0.012F, 0.006F, 0.003F,
                                     0.006F, 0.012F, 0.018F, 0.038F, 0.018F, 0.012F, 0.006F,
                                     0.012F, 0.018F, 0.038F, 0.12F, 0.038F, 0.018F, 0.012F,
                                     0.018F, 0.038F, 0.12F, 0.0F, 0.12F, 0.038F, 0.018F,
                                     0.012F, 0.018F, 0.038F, 0.12F, 0.038F, 0.018F, 0.012F,
                                     0.006F, 0.012F, 0.018F, 0.038F, 0.018F, 0.012F, 0.006F,
                                     0.003F, 0.006F, 0.012F, 0.018F, 0.012F, 0.006F, 0.003F}


        Dim xyPos, xyPosShift As Integer
        Dim xyPosRGB(2) As Single
        Dim Stride7Pos() As Integer = {St1 * -3, St1 * -2, St1 * -1, 0, St1 * 1, St1 * 2, St1 * 3}
        Dim Y_Scan_Pos As Integer
        Dim TheMartixCount As Integer

        'Dim AreaBrightest() As Single
        Dim ByteWidth As Integer = 4
        Dim Factory1 As Single = 0.9787
        Dim passMode As Integer

        'ReDim AreaBrightest(2)

        'Search Area Brightest
        Dim BrightestMap(rgbValues1.GetUpperBound(0)) As Byte
        Dim BrightestMapSng() As Single
        For IDX_Y As Integer = 3 To bmp1.Height - 4
            For IDX_X As Integer = 3 To bmp1.Width - 4
                xyPos = (IDX_Y * St1) + (IDX_X * ByteWidth)
                For Y_Scan As Integer = St1 * -3 To St1 * 3 Step St1
                    Y_Scan_Pos = xyPos + Y_Scan
                    For X_Scan_Pos As Integer = -12 To 12 Step ByteWidth
                        xyPosShift = Y_Scan_Pos + X_Scan_Pos
                        BrightestMap(xyPos) = Math.Max(rgbValues1(xyPosShift), BrightestMap(xyPos))
                        BrightestMap(xyPos + 1) = Math.Max(rgbValues1(xyPosShift + 1), BrightestMap(xyPos + 1))
                        BrightestMap(xyPos + 2) = Math.Max(rgbValues1(xyPosShift + 2), BrightestMap(xyPos + 2))
                    Next
                Next

            Next
        Next
        BrightestMapSng = Byte2Single(BrightestMap)

        'Pass1
        For IDX_Y As Integer = 3 To bmp1.Height - 4
            For IDX_X As Integer = 3 To bmp1.Width - 4

                xyPos = (IDX_Y * St1) + (IDX_X * ByteWidth)

                passMode = 0
                If rgbValues1(xyPos) = 0 AndAlso rgbValues1(xyPos + 1) = 0 AndAlso rgbValues1(xyPos + 2) = 0 Then
                    passMode = 1
                ElseIf rgbValues1(xyPos + 3) = 0 Then
                    passMode = 1
                End If

                If passMode = 1 Then

                    rgbValues2(0) = CSng(rgbValues1(xyPos))
                    rgbValues2(1) = CSng(rgbValues1(xyPos + 1))
                    rgbValues2(2) = CSng(rgbValues1(xyPos + 2))

                Else

                    xyPosRGB(0) = CSng(rgbValues1(xyPos))
                    xyPosRGB(1) = CSng(rgbValues1(xyPos + 1))
                    xyPosRGB(2) = CSng(rgbValues1(xyPos + 2))

                    TheMartixCount = 0
                    For Y_Scan As Integer = 0 To 6

                        Y_Scan_Pos = xyPos + Stride7Pos(Y_Scan)

                        For X_Scan_Pos As Integer = -12 To 12 Step ByteWidth

                            xyPosShift = Y_Scan_Pos + X_Scan_Pos

                            rgbValues2(xyPosShift) = Math.Min((rgbValues2(xyPosShift) + (xyPosRGB(0) * TheMartix(TheMartixCount))) * Factory1, BrightestMapSng(xyPosShift))
                            rgbValues2(xyPosShift + 1) = Math.Min((rgbValues2(xyPosShift + 1) + (xyPosRGB(1) * TheMartix(TheMartixCount))) * Factory1, BrightestMapSng(xyPosShift + 1))
                            rgbValues2(xyPosShift + 2) = Math.Min((rgbValues2(xyPosShift + 2) + (xyPosRGB(2) * TheMartix(TheMartixCount))) * Factory1, BrightestMapSng(xyPosShift + 2))

                            TheMartixCount += 1
                        Next
                    Next
                End If

            Next
        Next

        'Pass2 
        For IDX_Y As Integer = 0 To bmp1.Height - 1
            For IDX_X As Integer = 0 To bmp1.Width - 1

                xyPos = (IDX_Y * St1) + (IDX_X * ByteWidth)
                passMode = 0

                If CInt(rgbValues1(xyPos)) + CInt(rgbValues1(xyPos + 2)) = 0 Then
                    If rgbValues1(xyPos + 1) = 255 Then
                        rgbValues2(xyPos) = 0
                        rgbValues2(xyPos + 1) = 255
                        rgbValues2(xyPos + 2) = 0
                        passMode = 1
                    End If
                End If


                If passMode = 0 Then
                    rgbValues2(xyPos) = Math.Max(rgbValues1(xyPos), rgbValues2(xyPos))
                    rgbValues2(xyPos + 1) = Math.Max(rgbValues1(xyPos + 1), rgbValues2(xyPos + 1))
                    rgbValues2(xyPos + 2) = Math.Max(rgbValues1(xyPos + 2), rgbValues2(xyPos + 2))
                End If

            Next
        Next

        rgbValues1 = Single2Byte(rgbValues2)

        ' 將修改過的 RGB 值複製回 Bitmap
        System.Runtime.InteropServices.Marshal.Copy(rgbValues1, 0, ptr1, bytes1)

        ' 解鎖位元
        bmp1.UnlockBits(bmpData1)

        bmp1 = CropBitmap(bmp1)
        '-----------
        'bmp1.Save("Test_image.png", Imaging.ImageFormat.Png)

        Return bmp1

    End Function

    Public Function Byte2Single(InputByte() As Byte) As Single()

        Dim UpCount As Integer = InputByte.Length - 1
        Dim TmpSingle(UpCount) As Single

        For IDX As Integer = 0 To UpCount
            TmpSingle(IDX) = CSng(InputByte(IDX))
        Next

        Return TmpSingle

    End Function

    Public Function Single2Byte(InputSng() As Single) As Byte()

        Dim UpCount As Integer = InputSng.Length - 1
        Dim TmpByte(UpCount) As Byte

        For IDX As Integer = 0 To UpCount
            TmpByte(IDX) = CByte(InputSng(IDX))
        Next

        Return TmpByte

    End Function

    Public FormLoginBitmap0 As Bitmap
    Public FormLoginBitmap13 As Bitmap
    Public FormLoginBitmap2 As Bitmap
    Public FormConfigBitmap As Bitmap
    Public FormKDFBitmap As Bitmap
    Public FormMainBitmap As Bitmap
    Public FormFileExpBitmap As Bitmap
    Public FormMITBitmap As Bitmap

    Public Sub Load_Bitmap_For_Threads(WhatToLoad As Integer)

        Select Case WhatToLoad

            Case 0
                FormLoginBitmap0 = The_Shine_Visual_Filter2(My.Resources.Resource1.Title_LOGIN)
            Case 1
                FormLoginBitmap13 = The_Shine_Visual_Filter2(My.Resources.Resource1.Title_NORMAL)
            Case 2
                FormLoginBitmap2 = The_Shine_Visual_Filter2(My.Resources.Resource1.Title_Password)
            Case 4
                FormConfigBitmap = The_Shine_Visual_Filter2(My.Resources.Resource1.Form_Config)
            Case 5
                FormKDFBitmap = The_Shine_Visual_Filter2(My.Resources.Resource1.PWD_KDF)
            Case 6
                FormMainBitmap = The_Shine_Visual_Filter2(My.Resources.Resource1.Form_Main)
            Case 7
                FormFileExpBitmap = The_Shine_Visual_Filter2(My.Resources.Resource1.File_expo_New)
            Case 8
                FormMITBitmap = The_Shine_Visual_Filter2(My.Resources.Resource1.mit_lic)
        End Select

    End Sub

    Public Function CountDownOnImg(OrigBmp As Bitmap, WhatStrOn As String, FontSize As Integer) As Bitmap

        Dim RndFnt() As Font = {New System.Drawing.Font("Arial", FontSize, FontStyle.Bold)}

        Dim BitA As New Bitmap(OrigBmp)
        Dim GrA As Graphics = Graphics.FromImage(BitA)
        Dim Brs1 As Brush = New System.Drawing.SolidBrush(Color.FromArgb(168, 0, 118, 232))
        Dim Brs2 As New SolidBrush(Color.FromArgb(128, 255, 255, 255))

        'GrA.Clear(Color.White)

        GrA.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias

        Dim textSize As SizeF = GrA.MeasureString(WhatStrOn, RndFnt(0), Nothing, Nothing, 1, 1)
        'GrA.DrawString(WhatStrOn, RndFnt(0), Brs1, (BitA.Width / 2) - (textSize.Width / 2), (BitA.Height / 2) - (textSize.Height / 2) + 5)

        Dim borderSize As Single = 2.0F

        For x As Integer = -1 To 1
            For y As Integer = -1 To 1
                GrA.DrawString(WhatStrOn, RndFnt(0), Brs2, x + borderSize, y + borderSize)
            Next
        Next
        GrA.DrawString(WhatStrOn, RndFnt(0), Brs1, borderSize, borderSize)

        Return BitA 'DirectCast(BitA.Clone(), Image)

    End Function


    Public Function CreateGradientBitmap(ByVal width As Integer, ByVal height As Integer) As Bitmap

        Dim bmp As New Bitmap(width, height)

        ' 使用 Graphics 對象來繪製
        Using g As Graphics = Graphics.FromImage(bmp)
            ' 定義漸層範圍
            Dim rect As New Rectangle(0, 0, width, height)

            ' 創建 LinearGradientBrush，從上方（藍色）到下方（白色）漸變
            Using lgb As New Drawing2D.LinearGradientBrush(rect, Color.FromArgb(239, 232, 200), Color.White, Drawing2D.LinearGradientMode.Vertical)
                ' 使用 Graphics 將漸層填充到矩形區域
                g.FillRectangle(lgb, rect)
            End Using

        End Using

        Return bmp

    End Function

    Public Function TwoBmpStack(Bmp1 As Bitmap, Bmp2 As Bitmap) As Bitmap

        Dim NewBmp As New Bitmap(Bmp1.Width, Bmp1.Height)

        Using g As Graphics = Graphics.FromImage(NewBmp)
            g.DrawImage(Bmp1, 0, 0)
            g.DrawImage(Bmp2, 0, 0)
        End Using

        Return NewBmp

    End Function

    Public Function MirrorBitmap(originalBitmap As Bitmap, HorV As Integer) As Bitmap

        Dim mirroredBitmap As New Bitmap(originalBitmap.Width, originalBitmap.Height)

        ' 使用 Graphics 繪製到新 Bitmap 上
        Using g As Graphics = Graphics.FromImage(mirroredBitmap)
            ' 設置翻轉變換
            g.TranslateTransform(originalBitmap.Width, 0) ' 移動到右側

            If HorV = 0 Then '=0 水平 =1 垂直
                g.ScaleTransform(-1, 1) ' 水平翻轉
            Else
                g.ScaleTransform(1, -1) ' 水平翻轉
            End If

            ' 繪製原始 Bitmap
            g.DrawImage(originalBitmap, 0, 0)
        End Using

        Return mirroredBitmap

    End Function

    Public Function MakeRedArrow(bmp0 As Bitmap) As Bitmap

        Dim bmp1 As Bitmap = bmp0.Clone(New Rectangle(0, 0, bmp0.Width, bmp0.Height), PixelFormat.Format32bppArgb)

        Dim rect As New Rectangle(0, 0, bmp1.Width, bmp1.Height)
        Dim bmpData1 As System.Drawing.Imaging.BitmapData = bmp1.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp1.PixelFormat)
        Dim St1 As Integer = bmpData1.Stride

        ' 取得第一行的位址
        Dim ptr1 As IntPtr = bmpData1.Scan0

        ' 宣告一個陣列來儲存 Bitmap 的字節
        Dim bytes1 As Integer = Math.Abs(St1) * bmp1.Height
        Dim rgbValues1(bytes1 - 1) As Byte

        ' 將 RGB 值複製到陣列中
        System.Runtime.InteropServices.Marshal.Copy(ptr1, rgbValues1, 0, bytes1)

        Dim xyPos As Integer
        Dim ByteWidth As Integer = 4

        For IDX_Y As Integer = 0 To bmp1.Height - 1
            For IDX_X As Integer = 2 To (St1 - 1 - ByteWidth) Step ByteWidth

                xyPos = (IDX_Y * St1) + IDX_X
                If rgbValues1(xyPos) <= 168 Then rgbValues1(xyPos) = 168
                'rgbValues1(xyPos + 1) = 255
            Next
        Next

        ' 將修改過的 RGB 值複製回 Bitmap
        System.Runtime.InteropServices.Marshal.Copy(rgbValues1, 0, ptr1, bytes1)

        ' 解鎖位元
        bmp1.UnlockBits(bmpData1)

        Dim graphics As Graphics = graphics.FromImage(bmp1)

        graphics.Dispose()

        Return bmp1

    End Function


End Module



