'/*************************************************************************
'
'   Copyright (C) 2015-2016. rollrat. All Rights Reserved.
'
'   Author: HyunJun Jeong
'
'***************************************************************************/

Public Class frmPreview

    Private Sub frmPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim tmp_ret As New List(Of String)
        For i As Integer = 0 To frmMain.previewForward.Count - 1
            Dim err As String = ""

            '
            '   파일이름 중복체크
            '
            If tmp_ret.Contains(frmMain.previewReturn(i)) Then
                err = "Overlapped"
            Else
                tmp_ret.Add(frmMain.previewReturn(i))
            End If

            '
            '   파일이름에서 사용 불가능한 문자 검색
            '
            If frmMain.CheckIrregular(frmMain.previewReturn(i)) Then
                If err.Length <> 0 Then
                    err += "+Irregular"
                Else
                    err = "Irregular"
                End If
            End If

            Dim LI As ListViewItem
            LI = ListView1.Items.Add(New ListViewItem(New String() {frmMain.previewForward(i), frmMain.previewReturn(i), err}))
            LI.StateImageIndex = 0

            If err.Length <> 0 Then
                LI.ForeColor = Color.White
                LI.BackColor = Color.Orange
            End If
        Next
        tmp_ret.Clear()
    End Sub

End Class