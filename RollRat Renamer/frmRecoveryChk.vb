'/*************************************************************************
'
'   Copyright (C) 2015. rollrat. All Rights Reserved.
'
'   Author: HyunJun Jeong
'
'***************************************************************************/

Public Class frmRecoveryChk

    Private Sub frmRecoveryChk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each _dataset As frmMain._dataset In frmMain.ChkedDataSet
            Dim SoTrue As Boolean = True
            If _dataset.BeforeFileExist = True Or _dataset.AfterFileExist = False Then
                SoTrue = False
            End If

            Dim i1, i2, i5, i6, i7 As String
            i1 = _dataset.BeforeFileName
            i2 = _dataset.AfterFileName
            If _dataset.BeforeFileExist Then i5 = "○" Else i5 = "X"
            If _dataset.AfterFileExist Then i6 = "○" Else i6 = "X"
            If SoTrue = False Then i7 = "Impossible" Else i7 = ""

            Dim LI As ListViewItem
            LI = ListView1.Items.Add(New ListViewItem(New String() {i1, i2, i5, i6, i7}))
            LI.StateImageIndex = 0
            If Not SoTrue Then
                LI.ForeColor = Color.White
                LI.BackColor = Color.Orange
            End If
        Next
    End Sub

End Class