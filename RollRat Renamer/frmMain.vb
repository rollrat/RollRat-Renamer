'/*************************************************************************
'
'   Copyright (C) 2015. rollrat. All Rights Reserved.
'
'   Author: HyunJun Jeong
'
'***************************************************************************/

Imports System.Security.Principal
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Text

Public Class frmMain

    Public Shared loadedFolderAddress As String
    Public Shared loadedFiles As New ArrayList

    Public Shared chkLetterCount As Boolean = False
    Public Shared chkNumLetter As Boolean = False
    Public Shared chkSpaceLetter As Boolean = True

    Public Shared listPattern As New ArrayList
    Public Shared listPatternOverlap As New ArrayList

    Public Shared selectedPattern As String

    Public Shared previewForward As List(Of String)
    Public Shared previewReturn As List(Of String)

    Public Sub WriteLog(ByVal text As String, Optional ByVal err As Boolean = False)
        rtbError.AppendText(Log.WriteLog(text, err) & vbCrLf)
    End Sub

    Public Sub WriteLines(ByVal txts As String())
        Log.WriteLines(txts)
        For i As Integer = 0 To txts.Length - 1
            rtbError.AppendText(txts(i) & vbCrLf)
        Next
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 관리자 권한 체크
        If (New WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator) Then
            ShowMsgCritical("이 프로그램은 관리자권한으로 실행할 수 없습니다.") : End
        End If

        Text += Version.VersionText
        lbName.Text += Version.VersionText

        InitListViewColumn()

        WriteLog("----------Program Start----------")
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        WriteLog("----------Program End----------")
    End Sub

#Region "Message Box"

    Private Sub ShowMsgCritical(ByVal body As String, Optional ByVal title As String = Version.ProgramName)
        MsgBox(body, MsgBoxStyle.Critical, title)
    End Sub
    Private Sub ShowMsgWarning(ByVal body As String, Optional ByVal title As String = Version.ProgramName)
        MsgBox(body, MsgBoxStyle.Exclamation, title)
    End Sub
    Private Sub ShowMsgInform(ByVal body As String, Optional ByVal title As String = Version.ProgramName)
        MsgBox(body, MsgBoxStyle.Information, title)
    End Sub

#End Region

#Region "Drag & Drop"

    Private Sub lvFileList_DragDrop(sender As Object, e As DragEventArgs) Handles lvFileList.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim filePaths As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If filePaths.Length <> 1 Then
                ShowMsgCritical("하나의 폴더만 끌어오십시오.")
            Else
                Listing_File(CType(e.Data.GetData(DataFormats.FileDrop), String())(0))
            End If
        End If
    End Sub
    Private Sub lvFileList_DragEnter(sender As Object, e As DragEventArgs) Handles lvFileList.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub tbMovieAddr_DragDrop(sender As Object, e As DragEventArgs) Handles tbMovieAddr.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim filePaths As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If filePaths.Length <> 1 Then
                ShowMsgCritical("하나의 폴더만 끌어오십시오.")
            Else
                tbMovieAddr.Text = CType(e.Data.GetData(DataFormats.FileDrop), String())(0)
            End If
        End If
    End Sub
    Private Sub tbMovieAddr_DragEnter(sender As Object, e As DragEventArgs) Handles tbMovieAddr.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub tbSubtitleAddr_DragDrop(sender As Object, e As DragEventArgs) Handles tbSubtitleAddr.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim filePaths As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If filePaths.Length <> 1 Then
                ShowMsgCritical("하나의 폴더만 끌어오십시오.")
            Else
                tbSubtitleAddr.Text = CType(e.Data.GetData(DataFormats.FileDrop), String())(0)
            End If
        End If
    End Sub
    Private Sub tbSubtitleAddr_DragEnter(sender As Object, e As DragEventArgs) Handles tbSubtitleAddr.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Listing_File(ByVal pathstr As String)
        If My.Computer.FileSystem.DirectoryExists(pathstr) Then
            loadedFolderAddress = pathstr
            lbLoadedFolder.Text = loadedFolderAddress
            lbDragInform.Hide()
            loadedFiles.Clear()
            lvFileList.Items.Clear()
            Dim filesInfo As FileInfo() = (New DirectoryInfo(pathstr)).GetFiles()
            lbFileCount.Text = filesInfo.Count
            For Each file As FileInfo In filesInfo
                Dim lvi As ListViewItem
                Dim sizeKiB As Integer = file.Length / 1024
                If sizeKiB = 0 Then
                    lvi = lvFileList.Items.Add(New ListViewItem(New String() {file.Name, "0 KB", file.Extension, file.LastAccessTime.ToString()}))
                Else
                    lvi = lvFileList.Items.Add(New ListViewItem(New String() {file.Name, sizeKiB.ToString("#,#") & " KB", file.Extension, file.LastAccessTime.ToString()}))
                End If
                lvi.StateImageIndex = 0
                loadedFiles.Add(file.Name)
            Next
            UpdatePattern()
            WriteLog("Load Folder: " & pathstr)
        Else
            ShowMsgCritical("유효한 경로가 아닙니다.")
        End If
    End Sub

#End Region

#Region "ListView Column Sorting"

    Private Sub InitListViewColumn()
        Dim columnsTrans As New List(Of ColHeader)
        For Each column As ColumnHeader In lvFileList.Columns
            columnsTrans.Add(New ColHeader(column.Text, column.Width, column.TextAlign, True))
        Next
        lvFileList.Columns.Clear()
        lvFileList.Columns.AddRange(columnsTrans.ToArray)
    End Sub

    Declare Unicode Function StrCmpLogicalW Lib "shlwapi.dll" (ByVal s1 As String, ByVal s2 As String) As Integer

    Public Shared Function ComparePath(addr1 As String, addr2 As String) As Integer
        Return StrCmpLogicalW(addr1, addr2)
    End Function

    Public Class PathComparer
        Implements IComparer

        Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
            Return StrCmpLogicalW(x, y)
        End Function
    End Class
    Public Shared Function GetPathComparer() As IComparer
        Return CType(New PathComparer(), IComparer)
    End Function

    'https://msdn.microsoft.com/ko-kr/library/ms229643(v=vs.90).aspx
    Public Class SortWrapper
        Friend sortItem As ListViewItem
        Friend sortColumn As Integer

        Public Sub New(ByVal Item As ListViewItem, ByVal iColumn As Integer)
            sortItem = Item
            sortColumn = iColumn
        End Sub

        Public ReadOnly Property [Text]() As String
            Get
                Return sortItem.SubItems(sortColumn).Text
            End Get
        End Property

        Public Class SortComparer
            Implements IComparer
            Private ascending As Boolean

            Public Sub New(ByVal asc As Boolean)
                Me.ascending = asc
            End Sub

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare

                If IsNothing(x) Or IsNothing(y) Then Return 0

                Dim xItem As SortWrapper = CType(x, SortWrapper)
                Dim yItem As SortWrapper = CType(y, SortWrapper)

                Dim xText As String = xItem.sortItem.SubItems(xItem.sortColumn).Text
                Dim yText As String = yItem.sortItem.SubItems(yItem.sortColumn).Text

                If xText.EndsWith(" KB") AndAlso yText.EndsWith(" KB") Then
                    Dim xNum As String = xText.Substring(0, xText.Length - 3).Replace(",", "")
                    Dim yNum As String = yText.Substring(0, yText.Length - 3).Replace(",", "")

                    If IsNumeric(xNum) AndAlso IsNumeric(yNum) Then
                        Return IIf(Convert.ToInt32(xNum) >= Convert.ToInt32(yNum), 1, -1) * IIf(Me.ascending, 1, -1)
                    End If
                End If

                Return ComparePath(xText, yText) * IIf(Me.ascending, 1, -1)
            End Function
        End Class
    End Class

    Public Class ColHeader
        Inherits ColumnHeader
        Public ascending As Boolean

        Public Sub New(ByVal [text] As String, ByVal width As Integer, ByVal align As HorizontalAlignment, ByVal asc As Boolean)
            Me.Text = [text]
            Me.Width = width
            Me.TextAlign = align
            Me.ascending = asc
        End Sub
    End Class

    Private Sub lvFileList_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvFileList.ColumnClick
        On Error Resume Next

        Dim clickedCol As ColHeader = CType(Me.lvFileList.Columns(e.Column), ColHeader)
        clickedCol.ascending = Not clickedCol.ascending
        Dim numItems As Integer = Me.lvFileList.Items.Count
        Me.lvFileList.BeginUpdate()

        Dim SortArray As New ArrayList
        Dim i As Integer
        For i = 0 To numItems - 1
            SortArray.Add(New SortWrapper(Me.lvFileList.Items(i), e.Column))
        Next i

        SortArray.Sort(0, SortArray.Count, New SortWrapper.SortComparer(clickedCol.ascending))

        Me.lvFileList.Items.Clear()
        Dim z As Integer
        For z = 0 To numItems - 1
            Me.lvFileList.Items.Add(CType(SortArray(z), SortWrapper).sortItem)
        Next z

        Me.lvFileList.EndUpdate()

        lvFileList.SetSortIcon(e.Column, clickedCol.ascending)

        UpdatePattern()

    End Sub

#End Region

#Region "Parse Pattern"

    Public Shared Function CheckIrregular(ByVal stritem As String) As Boolean
        Dim irregular As String = "\/:*?""<>|"

        For Each ch As Char In stritem
            For Each ch_t As Char In irregular
                If ch_t = ch Then
                    Return True
                End If
            Next
        Next

        Return False
    End Function

    Public Shared symbolList As String = "._+-=$[]{}()^%!#&"

    Public Shared Function is_number(ByVal ch As Char) As Boolean
        Dim i As Integer = AscW(ch)
        If chkNumLetter Then
            Return False
        End If
        If AscW("0"c) <= i AndAlso i <= AscW("9"c) Then
            Return True
        End If
        Return False
    End Function

    Public Shared Function is_deli(ByVal ch As Char) As Boolean
        Dim deli As String = symbolList
        If Not chkSpaceLetter Then
            If ch = " "c Then
                Return True
            End If
        End If
        For Each ch_t As Char In deli
            If ch_t = ch Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function ParsePattern(ByVal target As String) As String
        Dim ret As New StringBuilder
        Dim j As Integer = 0
        Dim p_count As Integer = 0
        For i As Integer = 0 To target.Length - 1
            If is_number(target(i)) Then
                j = 0
                Do
                    i += 1 : j += 1
                    If i >= target.Length Then Exit Do
                Loop While is_number(target(i))
                i -= 1
                If chkLetterCount = True Then
                    ret.Append("숫자(" & j & ")+")
                Else
                    ret.Append("숫자+")
                End If
            ElseIf is_deli(target(i)) Then
                j = 0
                Do
                    i += 1 : j += 1
                    If i >= target.Length Then Exit Do
                Loop While is_deli(target(i))
                i -= 1
                If chkLetterCount = True Then
                    ret.Append("기호(" & j & ")+")
                Else
                    ret.Append("기호+")
                End If
            Else
                j = 0
                Do
                    i += 1 : j += 1
                    If i >= target.Length Then Exit Do
                Loop While Not is_number(target(i)) AndAlso Not is_deli(target(i))
                i -= 1
                If chkLetterCount = True Then
                    ret.Append("문자(" & j & ")+")
                Else
                    ret.Append("문자+")
                End If
            End If
            p_count += 1
            If p_count = tbPatternMax.Value Then
                Exit For
            End If
        Next
        ret.Remove(ret.Length - 1, 1)
        Return ret.ToString
    End Function

    Private Sub UpdatePattern()
        If Not loadedFiles Is Nothing Then
            lbPattern.Items.Clear()
            listPattern.Clear()
            listPatternOverlap.Clear()
            tbOverlap.Clear()
            For Each addr As String In loadedFiles
                Dim file_name As String = Path.GetFileName(addr)
                Dim extension As String = Path.GetExtension(addr)
                Dim pattern As String = ParsePattern(file_name)
                If listPattern.Contains(pattern) Then
                    If Not listPatternOverlap.Contains(pattern) Then
                        listPatternOverlap.Add(pattern)
                    End If
                Else
                    listPattern.Add(pattern)
                End If
            Next
            For Each pattern As String In listPattern
                If listPatternOverlap.Contains(pattern) Then
                    lbPattern.Items.Add("★" & pattern)
                Else
                    lbPattern.Items.Add(pattern)
                End If
            Next
        End If
    End Sub

#End Region

#Region "Box Event"

    Private Sub cbLetterLen_CheckedChanged(sender As Object, e As EventArgs) Handles cbLetterLen.CheckedChanged
        chkLetterCount = cbLetterLen.Checked
        UpdatePattern()
    End Sub
    Private Sub cbNumLetter_CheckedChanged(sender As Object, e As EventArgs) Handles cbNumLetter.CheckedChanged
        chkNumLetter = cbNumLetter.Checked
        UpdatePattern()
    End Sub
    Private Sub cbSpaceLetter_CheckedChanged(sender As Object, e As EventArgs) Handles cbSpaceLetter.CheckedChanged
        chkSpaceLetter = cbSpaceLetter.Checked
        UpdatePattern()
    End Sub
    Private Sub tbSymbol_TextChanged(sender As Object, e As EventArgs) Handles tbSymbol.TextChanged
        symbolList = tbSymbol.Text
        UpdatePattern()
    End Sub
    Private Sub tbPatternMax_ValueChanged(sender As Object, e As EventArgs) Handles tbPatternMax.ValueChanged
        UpdatePattern()
    End Sub

#End Region

#Region "Preview & Rename Button Event"

    Private Sub bPreview_Click(sender As Object, e As EventArgs) Handles bPreview.Click
        Dim rich_count As Integer = 0
        Dim rich_lines As New List(Of String)

        Dim chkd_whats As New List(Of Integer)
        Dim chkd_lines As New List(Of String)

        symbolList = tbSymbol.Text

        For Each line As String In rtbReplace.Lines
            rich_lines.Add(line)
        Next

        For clbsi As Integer = 0 To clbPatternElement.Items.Count - 1
            If clbPatternElement.GetItemChecked(clbsi) = True Then
                If rich_lines.Count <= rich_count Then
                    ShowMsgCritical("입력값이 부족합니다.")
                    WriteLog("<-- Too low input --> [PreviewButton];rich_lines=" & rich_lines.Count & ";rich_count=" & rich_count, True)
                    Exit Sub
                End If
                chkd_lines.Add(rich_lines(rich_count))
                rich_count += 1
                chkd_whats.Add(clbsi)
            End If
        Next

        If rich_count = 0 Then Exit Sub

        previewForward = New List(Of String)

        For index As Integer = 0 To lbTargetList.Items.Count - 1
            previewForward.Add(CStr(lbTargetList.Items(index)))
        Next

        force_end = False
        previewReturn = ReplacePattern(chkd_whats, chkd_lines, previewForward)
        chkd_whats.Clear()
        chkd_lines.Clear()
        If force_end = True Then
            previewReturn.Clear()
            Exit Sub
        End If

        If previewReturn.Count <> previewForward.Count Then
            ShowMsgCritical("알 수 없는 내부 오류가 발생했습니다.")
            WriteLog("<-- None --> [PreviewButton];preview_return=" & previewReturn.Count & ";preview_forward=" & previewForward.Count, True)
            Exit Sub
        End If

        frmPreview.Show()
    End Sub

    Private Sub bRename_Click(sender As Object, e As EventArgs) Handles bRename.Click

        Dim rich_count As Integer = 0
        Dim rich_lines As New List(Of String)

        Dim chkd_whats As New List(Of Integer)
        Dim chkd_lines As New List(Of String)

        symbolList = tbSymbol.Text

        For Each line As String In rtbReplace.Lines
            rich_lines.Add(line)
        Next

        For clbsi As Integer = 0 To clbPatternElement.Items.Count - 1
            If clbPatternElement.GetItemChecked(clbsi) = True Then
                If rich_lines.Count <= rich_count Then
                    ShowMsgCritical("입력값이 부족합니다.")
                    WriteLog("<-- Too low input --> [PreviewButton];rich_lines=" & rich_lines.Count & ";rich_count=" & rich_count, True)
                    Exit Sub
                End If
                chkd_lines.Add(rich_lines(rich_count))
                rich_count += 1
                chkd_whats.Add(clbsi)
            End If
        Next

        force_end = False

        If rich_count = 0 Then Exit Sub

        Dim tagt_items As New List(Of String)

        For index As Integer = 0 To lbTargetList.Items.Count - 1
            tagt_items.Add(CStr(lbTargetList.Items(index)))
        Next

        Dim ret As List(Of String) = ReplacePattern(chkd_whats, chkd_lines, tagt_items)
        chkd_whats.Clear()
        chkd_lines.Clear()
        If force_end = True Then
            ret.Clear()
            Exit Sub
        End If

        If ret.Count <> tagt_items.Count Then
            ShowMsgCritical("알 수 없는 내부 오류가 발생했습니다.")
            WriteLog("<-- None --> [RenameButton];ret=" & ret.Count & ";tagt_items=" & tagt_items.Count, True)
            Exit Sub
        End If

        Dim tmp_ret As New List(Of String)
        For Each item As String In ret
            If tmp_ret.Contains(item) Then
                ShowMsgCritical("중복되는 파일이름을 발견해 계속 실행할 수 없습니다. 자세한 내용은 미리보기를 참고하십시오." & vbCrLf & vbCrLf & item)
                WriteLog("<-- Overlapped Error --> [RenameButton];" & item, True)
                Exit Sub
            Else
                tmp_ret.Add(item)
            End If
        Next
        tmp_ret.Clear()

        For Each item As String In ret
            If CheckIrregular(item) Then
                ShowMsgCritical("파일이름으로 사용할 수 없는 문자가 발견되었습니다." & vbCrLf & _
                       "대상 : " & item)
                WriteLog("<-- Irregular Error --> [RenameButton];item=" & item, True)
                Exit Sub
            End If
        Next

        Dim maximum_message As Integer = 25
        Dim message_bottom As String = ""
        If tagt_items.Count < maximum_message Then
            For i = 0 To tagt_items.Count - 1
                If maximum_message = 0 Then
                    Exit For
                End If
                message_bottom += tagt_items(i) & " -> " & ret(i) & vbCrLf
                maximum_message -= 1
            Next
        Else
            maximum_message = 12
            For i = 0 To tagt_items.Count - 1
                If maximum_message = 0 Then
                    Exit For
                End If
                message_bottom += tagt_items(i) & " -> " & ret(i) & vbCrLf
                maximum_message -= 1
            Next
            message_bottom += vbTab & "." & vbCrLf & vbTab & "." & vbCrLf & vbTab & "." & vbCrLf
            For i = tagt_items.Count - 12 To tagt_items.Count - 1
                message_bottom += tagt_items(i) & " -> " & ret(i) & vbCrLf
            Next
        End If

        If MsgBox("다음 원래(왼쪽)이름들이 오른쪽과 같이 바뀌는 것에 동의하십니까?" & vbCrLf & vbCrLf & message_bottom, _
                  MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "RollRat Rename") = MsgBoxResult.Yes Then

            Dim str_str As New List(Of String)

            Try
                For i = 0 To tagt_items.Count - 1
                    If tagt_items(i) <> ret(i) Then
                        My.Computer.FileSystem.RenameFile(loadedFolderAddress & "\" & tagt_items(i), ret(i))
                        str_str.Add(vbTab & tagt_items(i) & vbTab & vbTab & "->" & vbTab & vbTab & ret(i))
                    End If
                Next
            Catch ex As Exception
                ShowMsgCritical("작업에 오류가 생겨 작동을 중지하였습니다. 자세한 사항은 로그기록을 참조하십시오.")
                WriteLog("Aborted the requested operation. [RenameButton]", True)
                WriteLog("  ErrMsg:" & ex.Message & vbCrLf & "  Success : " & str_str.Count)
                If str_str.Count <> 0 Then
                    WriteLog(" ------- Successful Rename List Begin -------")
                    WriteLog("   - addr=" & loadedFolderAddress)
                    WriteLines(str_str.ToArray())
                    WriteLog(" ------- Successful Rename List End -------")
                End If
                Exit Sub
            End Try

            ShowMsgInform("요청하신 작업이 완료되었습니다.")
            Listing_File(loadedFolderAddress)
            WriteLog("   - addr=" & loadedFolderAddress)
            WriteLines(str_str.ToArray())
            WriteLog("Complete the requested operation. [RenameButton]")
        End If

    End Sub

#End Region

#Region "Undo View"

    Public Structure _dataset
        Dim BeforeFileName As String
        Dim AfterFileName As String
        Dim BeforeFileExist As Boolean
        Dim AfterFileExist As Boolean
    End Structure

    Private Structure _dataset_event
        Dim datas As _dataset()
        Dim s_date As String
        Dim address As String
    End Structure

    Dim _Log As New List(Of _dataset_event)

    Public Shared ChkedFolderAddr As String
    Public Shared ChkedDataSet As _dataset()

    Private Sub GetAddressAndDate(ByVal Line As String, ByRef _Date As String, ByRef _Addr As String)
        Dim i As Integer = 1
        Dim __date As New StringBuilder
        Dim __addr As New StringBuilder
        Do : If (Line(i) = "]"c) Then Exit Do
            __date.Append(Line(i))
            i += 1 : Loop
        Do : i += 1
            If (Line(i) = "="c) Then i += 1 : Exit Do
        Loop
        For j As Integer = i To Line.Length - 1 : __addr.Append(Line(j)) : Next
        _Date = __date.ToString()
        _Addr = __addr.ToString()
    End Sub

    Private Sub LoadUndo()
        lvUndo.Items.Clear()
        _Log.Clear()
        Dim LogLines As String() = ReadAllLogLines()
        If LogLines Is Nothing Then
            ShowMsgCritical("로그기록파일이 존재하지 않거나 내용이 삭제되어 더 이상 진행할 수 없습니다.")
            Exit Sub
        End If
        For i As Integer = 0 To LogLines.Length - 1
            If LogLines(i)(0) = vbTab Then
                Dim _event As _dataset_event
                Dim _date As String = Nothing
                Dim _addr As String = Nothing
                GetAddressAndDate(LogLines(i - 1), _date, _addr)
                _event.s_date = _date
                _event.address = _addr

                Dim j As Integer = i

                Dim _Data As New List(Of _dataset)

                Do
                    Dim k As Integer = 1
                    Dim _Set As _dataset
                    Dim BeforeFileName As New StringBuilder
                    Dim AfterFileName As New StringBuilder

                    Do
                        BeforeFileName.Append(LogLines(j)(k)) : k += 1
                    Loop Until LogLines(j)(k) = vbTab
                    k += 6
                    Do
                        AfterFileName.Append(LogLines(j)(k)) : k += 1
                    Loop Until k = LogLines(j).Length

                    _Set.BeforeFileName = BeforeFileName.ToString()
                    _Set.AfterFileName = AfterFileName.ToString()

                    _Set.BeforeFileExist = My.Computer.FileSystem.FileExists(_addr & "\" & _Set.BeforeFileName)
                    _Set.AfterFileExist = My.Computer.FileSystem.FileExists(_addr & "\" & _Set.AfterFileName)

                    _Data.Add(_Set)
                    j += 1
                Loop While j < LogLines.Length AndAlso LogLines(j)(0) = vbTab

                i = j

                _event.datas = _Data.ToArray()
                _Log.Add(_event)
            End If
        Next
        For i As Integer = _Log.Count - 1 To 0 Step -1
            Dim LI As ListViewItem
            Dim _Useable As Boolean = True

            For jk As Integer = 0 To _Log(i).datas.Length - 1
                Dim j = _Log(i).datas(jk)
                If Not (Not j.BeforeFileExist AndAlso j.AfterFileExist) Then
                    _Useable = False
                    Exit For
                End If
            Next

            LI = lvUndo.Items.Add(New ListViewItem(New String() {_Log.Count - i, _Log(i).s_date, _Log(i).address, _Useable.ToString}))
            LI.StateImageIndex = 0
            If Not _Useable Then
                LI.ForeColor = Color.White
                LI.BackColor = Color.Orange
            End If
        Next
    End Sub

    Private Sub tabControl_Selected(sender As Object, e As TabControlEventArgs) Handles tabControl.Selected
        If e.TabPageIndex = 3 Then
            LoadUndo()
        End If
    End Sub

    Private Sub lvUndo_DoubleClick(sender As Object, e As EventArgs) Handles lvUndo.DoubleClick
        For i As Integer = 0 To lvUndo.SelectedItems.Count - 1
            If lvUndo.SelectedItems.Item(i).SubItems(3).Text = "True" Then
                Process.Start(lvUndo.SelectedItems.Item(i).SubItems(2).Text)
            End If
            Exit For
        Next
    End Sub

    Private Sub bUndo_Click(sender As Object, e As EventArgs) Handles bUndo.Click
        For i As Integer = 0 To lvUndo.SelectedItems.Count - 1
            If lvUndo.SelectedItems.Item(i).SubItems(3).Text = "True" Then
                Dim BeforeFN As New List(Of String)
                Dim AfterFN As New List(Of String)
                Dim _LogIndex As Integer = lvUndo.Items.Count - CInt(lvUndo.SelectedItems.Item(i).Text)
                For Each _DS As _dataset In _Log(_LogIndex).datas
                    BeforeFN.Add(_DS.AfterFileName)
                    AfterFN.Add(_DS.BeforeFileName)
                Next

                Dim maximum_message As Integer = 25
                Dim message_bottom As String = ""
                If BeforeFN.Count < maximum_message Then
                    For j = 0 To BeforeFN.Count - 1
                        If maximum_message = 0 Then
                            Exit For
                        End If
                        message_bottom += BeforeFN(j) & " -> " & AfterFN(j) & vbCrLf
                        maximum_message -= 1
                    Next
                Else
                    maximum_message = 12
                    For j = 0 To BeforeFN.Count - 1
                        If maximum_message = 0 Then
                            Exit For
                        End If
                        message_bottom += BeforeFN(j) & " -> " & AfterFN(j) & vbCrLf
                        maximum_message -= 1
                    Next
                    message_bottom += vbTab & "." & vbCrLf & vbTab & "." & vbCrLf & vbTab & "." & vbCrLf
                    For j = BeforeFN.Count - 12 To BeforeFN.Count - 1
                        message_bottom += BeforeFN(j) & " -> " & AfterFN(j) & vbCrLf
                    Next
                End If

                If MsgBox("다음 원래(왼쪽)이름들이 오른쪽과 같이 바뀌는 것에 동의하십니까?" & vbCrLf & vbCrLf & message_bottom, _
                          MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "RollRat Rename") = MsgBoxResult.Yes Then

                    Dim str_str As New List(Of String)

                    '
                    '   이름 바꾸기 실행 & 로그 작성
                    '
                    Try
                        For j = 0 To BeforeFN.Count - 1
                            If BeforeFN(j) <> AfterFN(j) Then
                                My.Computer.FileSystem.RenameFile(lvUndo.SelectedItems.Item(i).SubItems(2).Text & "\" & BeforeFN(j), AfterFN(j))
                                str_str.Add(vbTab & BeforeFN(j) & vbTab & vbTab & "->" & vbTab & vbTab & AfterFN(j))
                            End If
                        Next
                    Catch ex As Exception
                        ShowMsgCritical("작업에 오류가 생겨 작동을 중지하였습니다. 자세한 사항은 로그기록을 참조하십시오.")
                        WriteLog("Aborted the requested operation. [RenameButton]", True)
                        WriteLog("  ErrMsg:" & ex.Message & vbCrLf & "  Success : " & str_str.Count)
                        If str_str.Count <> 0 Then
                            WriteLog(" ------- Successful Rename List Begin -------")
                            WriteLog("   - addr=" & lvUndo.SelectedItems.Item(i).SubItems(2).Text)
                            WriteLines(str_str.ToArray())
                            WriteLog(" ------- Successful Rename List End -------")
                        End If
                        Exit Sub
                    End Try

                    ShowMsgInform("요청하신 작업이 완료되었습니다.")
                    WriteLog("   - addr=" & lvUndo.SelectedItems.Item(i).SubItems(2).Text)
                    WriteLines(str_str.ToArray())
                    WriteLog("Complete the requested operation. [RenameButton]")
                End If
            Else
                ShowMsgCritical("해당 항목으로 되돌리기를 실행할 수 없습니다. 자세한 정보는 메뉴얼을 참고하십시오.")
            End If
            Exit For
        Next

        LoadUndo()
    End Sub

    Private Sub 상세한정보확인KToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 상세한정보확인KToolStripMenuItem.Click
        For i As Integer = 0 To lvUndo.SelectedItems.Count - 1
            Dim _LogIndex As Integer = lvUndo.Items.Count - CInt(lvUndo.SelectedItems.Item(i).Text)
            ChkedDataSet = _Log(_LogIndex).datas
            ChkedFolderAddr = _Log(_LogIndex).address
            frmRecoveryChk.Show()
            Exit For
        Next
    End Sub

#End Region

    Private Sub lvFileList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvFileList.SelectedIndexChanged
        Dim sii As String
        For i As Integer = 0 To lvFileList.SelectedItems.Count - 1
            sii = ParsePattern(lvFileList.SelectedItems.Item(i).Text)
            If listPatternOverlap.Contains(sii) Then
                sii = "★" & sii
            End If
            For j As Integer = 0 To lvFileList.Items.Count - 1
                If lbPattern.Items.Item(j) = sii Then
                    lbPattern.SelectedIndex = j
                    Exit Sub
                End If
            Next
        Next
    End Sub

    Private Sub lvFileList_KeyUp(sender As Object, e As KeyEventArgs) Handles lvFileList.KeyUp
        If e.KeyCode = Keys.Delete Then
            For Each i As ListViewItem In lvFileList.SelectedItems
                lvFileList.Items.Remove(i)
            Next
            UpdatePattern()
        End If
    End Sub

    Private Sub lbPattern_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbPattern.SelectedIndexChanged
        clbPatternElement.Items.Clear()
        lbTargetList.Items.Clear()

        If Not lbPattern.SelectedItem Is Nothing Then
            Dim addr As String = lbPattern.SelectedItem
            selectedPattern = addr
            For Each a As String In addr.Replace("★"c, "").Split("+"c)
                clbPatternElement.Items.Add(a)
            Next
            For Each lvi As ListViewItem In lvFileList.Items
                If ParsePattern(lvi.Text) = addr.Replace("★"c, "") Then
                    lbTargetList.Items.Add(lvi.Text)
                End If
            Next
        End If
    End Sub

    Private Sub clbPatternElement_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbPatternElement.ItemCheck
        Dim val As Integer
        If e.NewValue = CheckState.Checked Then
            val = clbPatternElement.CheckedItems.Count + 1
        Else
            val = clbPatternElement.CheckedItems.Count - 1
        End If
        lbChose.Text = val
    End Sub

    Private Sub bFindOverlap_Click(sender As Object, e As EventArgs) Handles bFindOverlap.Click
        Dim tagt_items As New List(Of String)
        Dim overlap_int As New List(Of Integer)

        For index As Integer = 0 To lbTargetList.Items.Count - 1
            tagt_items.Add(CStr(lbTargetList.Items(index)))
        Next

        tbOverlap.Text = FindOverlap(tagt_items, overlap_int, clbPatternElement.Items)

        For i As Integer = 0 To clbPatternElement.Items.Count - 1
            clbPatternElement.SetItemChecked(i, False)
        Next
        For Each i As Integer In overlap_int
            clbPatternElement.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub bFindInvOverlap_Click(sender As Object, e As EventArgs) Handles bFindInvOverlap.Click
        Dim tagt_items As New List(Of String)
        Dim overlap_int As New List(Of Integer)
        Dim disoverlap_int As New List(Of Integer)

        For index As Integer = 0 To lbTargetList.Items.Count - 1
            tagt_items.Add(CStr(lbTargetList.Items(index)))
        Next

        tbOverlap.Text = FindOverlap(tagt_items, overlap_int, clbPatternElement.Items)

        For i As Integer = 0 To clbPatternElement.Items.Count - 1
            If Not overlap_int.Contains(i) Then
                disoverlap_int.Add(i)
            End If
        Next

        For i As Integer = 0 To clbPatternElement.Items.Count - 1
            clbPatternElement.SetItemChecked(i, False)
        Next
        For Each i As Integer In disoverlap_int
            clbPatternElement.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub rtbReplace_TextChanged(sender As Object, e As EventArgs) Handles rtbReplace.TextChanged
        lbReplaceLineCount.Text = rtbReplace.Lines.Length & " 줄 쓰여짐"
    End Sub

#Region "Movie Subtitle"

    Private Function CheckPatternOverlap(files As String(), type As String) As Boolean
        Dim first_pattern As String = Nothing
        Dim first_pattern_already As Boolean = False
        For Each x In files
            If first_pattern_already Then
                If Not first_pattern = ParsePattern(Path.GetFileNameWithoutExtension(x)) Then
                    ShowMsgCritical(type & " 동일하지 않은 패턴을 발견하여 계속 실행할 수 없습니다." & vbCrLf & _
                                    "대상:" & x & vbCrLf & _
                                    "패턴:" & first_pattern)
                    Return False
                End If
            Else
                first_pattern_already = True
                first_pattern = ParsePattern(Path.GetFileNameWithoutExtension(x))
            End If
        Next
        Return True
    End Function

    Private Sub bSubtitle_Click(sender As Object, e As EventArgs) Handles bSubtitle.Click
        Dim filesMovie = My.Computer.FileSystem.GetFiles(tbMovieAddr.Text, FileIO.SearchOption.SearchTopLevelOnly, "*.*").ToArray
        Dim filesSubtitle = My.Computer.FileSystem.GetFiles(tbSubtitleAddr.Text, FileIO.SearchOption.SearchTopLevelOnly, "*.*").ToArray

        Array.Sort(filesMovie, GetPathComparer())
        Array.Sort(filesSubtitle, GetPathComparer())

        If filesMovie.Count = filesSubtitle.Count Then

            If Not CheckPatternOverlap(filesMovie, "[동영상]") Or Not CheckPatternOverlap(filesSubtitle, "[자막]") Then Exit Sub

            Dim filenameMovie As New List(Of String)
            Dim filenameSubtitle As New List(Of String)
            Dim extensionSubtitle As String = Path.GetExtension(filesSubtitle(0))
            For Each fullname In filesMovie
                filenameMovie.Add(Path.GetFileNameWithoutExtension(fullname))
            Next
            For Each fullname In filesSubtitle
                If extensionSubtitle = Path.GetExtension(fullname) Then
                    filenameSubtitle.Add(Path.GetFileName(fullname))
                Else
                    ShowMsgCritical("확장자가 다른 파일이 존재합니다." & vbCrLf &
                                     "대상1: " & extensionSubtitle & vbCrLf &
                                     "대상2: " & Path.GetExtension(fullname))
                    Exit Sub
                End If
            Next

            Dim splitsMovie As List(Of String) = SplitPattern(filenameMovie(0))
            Dim splitsSubtitle As List(Of String) = SplitPattern(filenameSubtitle(0))
            Dim movieDiff As New List(Of Integer)
            Dim subtitleDiff As New List(Of Integer)

            For i As Integer = 0 To splitsMovie.Count - 1
                movieDiff.Add(0)
                subtitleDiff.Add(0)
            Next

            For i As Integer = 1 To filesMovie.Count - 1
                Dim splitsMovieA As List(Of String) = SplitPattern(filenameMovie(i))
                For j As Integer = 0 To splitsMovie.Count - 1
                    If splitsMovieA(j) <> splitsMovie(j) Then
                            movieDiff(j) += 1
                    End If
                Next
            Next
            For i As Integer = 1 To filesMovie.Count - 1
                Dim splitsSubtitleA As List(Of String) = SplitPattern(filenameSubtitle(i))
                For j As Integer = 0 To splitsSubtitle.Count - 1
                    If splitsSubtitleA(j) <> splitsSubtitle(j) Then
                        If Not cbAnime.Checked Then
                            subtitleDiff(j) += 1
                        Else
                            If IsNumeric(splitsSubtitleA(j).Trim()) AndAlso IsNumeric(splitsSubtitle(j).Trim()) Then
                                subtitleDiff(j) += 1
                            End If
                        End If
                    End If
                Next
            Next

            Dim maxMovie As Integer = movieDiff(0)
            Dim maxSubtitle As Integer = subtitleDiff(0)
            Dim maxMoviePos As Integer = 0
            Dim maxSubtitlePos As Integer = 0
            For i As Integer = 0 To movieDiff.Count - 1
                If maxMovie < movieDiff(i) Then
                    maxMovie = movieDiff(i)
                    maxMoviePos = i
                End If
            Next
            For i As Integer = 0 To subtitleDiff.Count - 1
                If maxSubtitle < subtitleDiff(i) Then
                    maxSubtitle = subtitleDiff(i)
                    maxSubtitlePos = i
                End If
            Next

            Dim resultSubtitle As New List(Of String)
            For i As Integer = 0 To filenameSubtitle.Count - 1
                If Not cbAnime.Checked Then
                    Dim movieFront As String = ""
                    Dim movieBack As String = ""
                    Dim splitsMovieNow = SplitPattern(filenameMovie(i))
                    For j As Integer = 0 To maxMoviePos - 1
                        movieFront += splitsMovieNow(j)
                    Next
                    For j As Integer = maxMoviePos + 1 To movieDiff.Count - 1
                        movieBack += splitsMovieNow(j)
                    Next
                    movieBack &= extensionSubtitle
                    resultSubtitle.Add(movieFront & SplitPattern(filenameSubtitle(i))(maxSubtitlePos) & movieBack)
                Else
                    Dim j As Integer
                    Dim replacePartSubtitle As String = SplitPattern(filenameSubtitle(i))(maxSubtitlePos)
                    For j = 0 To filenameSubtitle.Count
                        If j = filenameSubtitle.Count Then
                            ShowMsgCritical("이 모드로 실행할 수 없습니다.")
                            Exit Sub
                        End If
                        If replacePartSubtitle.TrimStart("0"c) = SplitPattern(filenameMovie(j))(maxMoviePos).TrimStart("0"c) Then Exit For
                    Next
                    Dim movieFront As String = ""
                    Dim movieBack As String = ""
                    Dim splitsMovieNow = SplitPattern(filenameMovie(j))
                    For k As Integer = 0 To maxMoviePos - 1
                        movieFront += splitsMovieNow(k)
                    Next
                    For k As Integer = maxMoviePos + 1 To movieDiff.Count - 1
                        movieBack += splitsMovieNow(k)
                    Next
                    movieBack &= extensionSubtitle
                    resultSubtitle.Add(movieFront & SplitPattern(filenameSubtitle(i))(maxSubtitlePos).PadLeft(2, "0") & movieBack)
                End If
            Next

            '//////////////////////////////////////////////////////////////////

            Dim maximum_message As Integer = 25
            Dim message_bottom As String = ""
            If filenameSubtitle.Count < maximum_message Then
                For i = 0 To filenameSubtitle.Count - 1
                    If maximum_message = 0 Then
                        Exit For
                    End If
                    message_bottom += filenameSubtitle(i) & " -> " & resultSubtitle(i) & vbCrLf
                    maximum_message -= 1
                Next
            Else
                maximum_message = 12
                For i = 0 To filenameSubtitle.Count - 1
                    If maximum_message = 0 Then
                        Exit For
                    End If
                    message_bottom += filenameSubtitle(i) & " -> " & resultSubtitle(i) & vbCrLf
                    maximum_message -= 1
                Next
                message_bottom += vbTab & "." & vbCrLf & vbTab & "." & vbCrLf & vbTab & "." & vbCrLf
                For i = filenameSubtitle.Count - 12 To filenameSubtitle.Count - 1
                    message_bottom += filenameSubtitle(i) & " -> " & resultSubtitle(i) & vbCrLf
                Next
            End If

            If MsgBox("다음 원래(왼쪽)이름들이 오른쪽과 같이 바뀌는 것에 동의하십니까?" & vbCrLf & vbCrLf & message_bottom, _
                      MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "RollRat Rename") = MsgBoxResult.Yes Then

                Dim str_str As New List(Of String)

                Try
                    For i = 0 To filenameSubtitle.Count - 1
                        If filenameSubtitle(i) <> resultSubtitle(i) Then
                            My.Computer.FileSystem.RenameFile(tbSubtitleAddr.Text & "\" & filenameSubtitle(i), resultSubtitle(i))
                            str_str.Add(vbTab & filenameSubtitle(i) & vbTab & vbTab & "->" & vbTab & vbTab & resultSubtitle(i))
                        End If
                    Next
                Catch ex As Exception
                    ShowMsgCritical("작업에 오류가 생겨 작동을 중지하였습니다. 자세한 사항은 로그기록을 참조하십시오.")
                    WriteLog("Aborted the requested operation. [Subtitle]", True)
                    WriteLog("  ErrMsg:" & ex.Message & vbCrLf & "  Success : " & str_str.Count)
                    If str_str.Count <> 0 Then
                        WriteLog(" ------- Successful Rename List Begin -------")
                        WriteLog("   - addr=" & tbSubtitleAddr.Text)
                        WriteLines(str_str.ToArray())
                        WriteLog(" ------- Successful Rename List End -------")
                    End If
                    Exit Sub
                End Try
                
                ShowMsgInform("요청하신 작업이 완료되었습니다.")
                Listing_File(tbSubtitleAddr.Text)
                WriteLog("   - addr=" & tbSubtitleAddr.Text)
                WriteLines(str_str.ToArray())
                WriteLog("Complete the requested operation. [Subtitle]")
            End If

        Else
            ShowMsgCritical("파일 개수가 달라 진행할 수 없습니다.")
        End If

    End Sub

#End Region

End Class