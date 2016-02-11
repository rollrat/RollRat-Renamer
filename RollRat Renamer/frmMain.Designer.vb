<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.tabControl = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.bPreview = New System.Windows.Forms.Button()
        Me.bRename = New System.Windows.Forms.Button()
        Me.tbOverlap = New System.Windows.Forms.TextBox()
        Me.bFindOverlap = New System.Windows.Forms.Button()
        Me.bFindInvOverlap = New System.Windows.Forms.Button()
        Me.lbReplaceLineCount = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.rtbReplace = New System.Windows.Forms.RichTextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lbChose = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.clbPatternElement = New System.Windows.Forms.CheckedListBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lbTargetList = New System.Windows.Forms.ListBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbSymbol = New System.Windows.Forms.TextBox()
        Me.cbSpaceLetter = New System.Windows.Forms.CheckBox()
        Me.cbNumLetter = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tbPatternMax = New System.Windows.Forms.NumericUpDown()
        Me.cbLetterLen = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbPattern = New System.Windows.Forms.ListBox()
        Me.lbFileCount = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbDragInform = New System.Windows.Forms.Label()
        Me.lvFileList = New System.Windows.Forms.ListView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.cbAnime = New System.Windows.Forms.CheckBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.tbSubtitleAddr = New System.Windows.Forms.TextBox()
        Me.tbMovieAddr = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.bSubtitle = New System.Windows.Forms.Button()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.rtbError = New System.Windows.Forms.RichTextBox()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.bUndo = New System.Windows.Forms.Button()
        Me.lvUndo = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.menuUndo = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.상세한정보확인KToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbName = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbLoadedFolder = New System.Windows.Forms.Label()
        Me.tabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.tbPatternMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.menuUndo.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabControl
        '
        Me.tabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabControl.Controls.Add(Me.TabPage1)
        Me.tabControl.Controls.Add(Me.TabPage2)
        Me.tabControl.Controls.Add(Me.TabPage4)
        Me.tabControl.Controls.Add(Me.TabPage5)
        Me.tabControl.Controls.Add(Me.TabPage3)
        Me.tabControl.Location = New System.Drawing.Point(12, 12)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedIndex = 0
        Me.tabControl.Size = New System.Drawing.Size(1072, 577)
        Me.tabControl.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.bPreview)
        Me.TabPage1.Controls.Add(Me.bRename)
        Me.TabPage1.Controls.Add(Me.tbOverlap)
        Me.TabPage1.Controls.Add(Me.bFindOverlap)
        Me.TabPage1.Controls.Add(Me.bFindInvOverlap)
        Me.TabPage1.Controls.Add(Me.lbReplaceLineCount)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.rtbReplace)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.lbChose)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.clbPatternElement)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.lbTargetList)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.tbSymbol)
        Me.TabPage1.Controls.Add(Me.cbSpaceLetter)
        Me.TabPage1.Controls.Add(Me.cbNumLetter)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.tbPatternMax)
        Me.TabPage1.Controls.Add(Me.cbLetterLen)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.lbPattern)
        Me.TabPage1.Controls.Add(Me.lbFileCount)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.lbDragInform)
        Me.TabPage1.Controls.Add(Me.lvFileList)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1064, 549)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Main"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'bPreview
        '
        Me.bPreview.Location = New System.Drawing.Point(914, 463)
        Me.bPreview.Name = "bPreview"
        Me.bPreview.Size = New System.Drawing.Size(144, 29)
        Me.bPreview.TabIndex = 63
        Me.bPreview.Text = "미리보기"
        Me.bPreview.UseVisualStyleBackColor = True
        '
        'bRename
        '
        Me.bRename.Location = New System.Drawing.Point(914, 498)
        Me.bRename.Name = "bRename"
        Me.bRename.Size = New System.Drawing.Size(144, 41)
        Me.bRename.TabIndex = 62
        Me.bRename.Text = "이름 바꾸기"
        Me.bRename.UseVisualStyleBackColor = True
        '
        'tbOverlap
        '
        Me.tbOverlap.Location = New System.Drawing.Point(359, 355)
        Me.tbOverlap.Name = "tbOverlap"
        Me.tbOverlap.ReadOnly = True
        Me.tbOverlap.Size = New System.Drawing.Size(162, 23)
        Me.tbOverlap.TabIndex = 61
        '
        'bFindOverlap
        '
        Me.bFindOverlap.Location = New System.Drawing.Point(359, 384)
        Me.bFindOverlap.Name = "bFindOverlap"
        Me.bFindOverlap.Size = New System.Drawing.Size(162, 33)
        Me.bFindOverlap.TabIndex = 59
        Me.bFindOverlap.Text = "중복된 부분 찾기"
        Me.bFindOverlap.UseVisualStyleBackColor = True
        '
        'bFindInvOverlap
        '
        Me.bFindInvOverlap.Location = New System.Drawing.Point(359, 423)
        Me.bFindInvOverlap.Name = "bFindInvOverlap"
        Me.bFindInvOverlap.Size = New System.Drawing.Size(162, 33)
        Me.bFindInvOverlap.TabIndex = 60
        Me.bFindInvOverlap.Text = "중복되지 않은 부분 찾기"
        Me.bFindInvOverlap.UseVisualStyleBackColor = True
        '
        'lbReplaceLineCount
        '
        Me.lbReplaceLineCount.AutoSize = True
        Me.lbReplaceLineCount.Location = New System.Drawing.Point(827, 337)
        Me.lbReplaceLineCount.Name = "lbReplaceLineCount"
        Me.lbReplaceLineCount.Size = New System.Drawing.Size(70, 15)
        Me.lbReplaceLineCount.TabIndex = 58
        Me.lbReplaceLineCount.Text = "0 줄 쓰여짐"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(524, 337)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 15)
        Me.Label10.TabIndex = 57
        Me.Label10.Text = "치환 문법 : "
        '
        'rtbReplace
        '
        Me.rtbReplace.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbReplace.Location = New System.Drawing.Point(527, 355)
        Me.rtbReplace.Name = "rtbReplace"
        Me.rtbReplace.Size = New System.Drawing.Size(381, 184)
        Me.rtbReplace.TabIndex = 56
        Me.rtbReplace.Text = ""
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(294, 337)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(59, 15)
        Me.Label11.TabIndex = 55
        Me.Label11.Text = "개 선택됨"
        '
        'lbChose
        '
        Me.lbChose.AutoSize = True
        Me.lbChose.Location = New System.Drawing.Point(276, 337)
        Me.lbChose.Name = "lbChose"
        Me.lbChose.Size = New System.Drawing.Size(14, 15)
        Me.lbChose.TabIndex = 53
        Me.lbChose.Text = "0"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(206, 337)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 15)
        Me.Label9.TabIndex = 52
        Me.Label9.Text = "패턴 요소 : "
        '
        'clbPatternElement
        '
        Me.clbPatternElement.FormattingEnabled = True
        Me.clbPatternElement.HorizontalScrollbar = True
        Me.clbPatternElement.Location = New System.Drawing.Point(209, 355)
        Me.clbPatternElement.Name = "clbPatternElement"
        Me.clbPatternElement.Size = New System.Drawing.Size(144, 184)
        Me.clbPatternElement.TabIndex = 51
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 337)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 15)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "대상 파일 : "
        '
        'lbTargetList
        '
        Me.lbTargetList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTargetList.FormattingEnabled = True
        Me.lbTargetList.ItemHeight = 15
        Me.lbTargetList.Location = New System.Drawing.Point(9, 355)
        Me.lbTargetList.Name = "lbTargetList"
        Me.lbTargetList.Size = New System.Drawing.Size(194, 184)
        Me.lbTargetList.TabIndex = 41
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(712, 270)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(66, 15)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "기호문자 : "
        '
        'tbSymbol
        '
        Me.tbSymbol.Location = New System.Drawing.Point(715, 288)
        Me.tbSymbol.Name = "tbSymbol"
        Me.tbSymbol.Size = New System.Drawing.Size(147, 23)
        Me.tbSymbol.TabIndex = 39
        Me.tbSymbol.Text = "._+-=$[]{}()^%!#&"
        '
        'cbSpaceLetter
        '
        Me.cbSpaceLetter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSpaceLetter.AutoSize = True
        Me.cbSpaceLetter.Checked = True
        Me.cbSpaceLetter.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbSpaceLetter.Location = New System.Drawing.Point(885, 299)
        Me.cbSpaceLetter.Name = "cbSpaceLetter"
        Me.cbSpaceLetter.Size = New System.Drawing.Size(142, 19)
        Me.cbSpaceLetter.TabIndex = 38
        Me.cbSpaceLetter.Text = "공백을 문자처럼 취급"
        Me.cbSpaceLetter.UseVisualStyleBackColor = True
        '
        'cbNumLetter
        '
        Me.cbNumLetter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbNumLetter.AutoSize = True
        Me.cbNumLetter.Location = New System.Drawing.Point(885, 281)
        Me.cbNumLetter.Name = "cbNumLetter"
        Me.cbNumLetter.Size = New System.Drawing.Size(142, 19)
        Me.cbNumLetter.TabIndex = 37
        Me.cbNumLetter.Text = "숫자를 문자처럼 취급"
        Me.cbNumLetter.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(538, 269)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 15)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "패턴 개수 제한"
        '
        'tbPatternMax
        '
        Me.tbPatternMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbPatternMax.Location = New System.Drawing.Point(538, 287)
        Me.tbPatternMax.Name = "tbPatternMax"
        Me.tbPatternMax.Size = New System.Drawing.Size(158, 23)
        Me.tbPatternMax.TabIndex = 25
        Me.tbPatternMax.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'cbLetterLen
        '
        Me.cbLetterLen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbLetterLen.AutoSize = True
        Me.cbLetterLen.Location = New System.Drawing.Point(885, 264)
        Me.cbLetterLen.Name = "cbLetterLen"
        Me.cbLetterLen.Size = New System.Drawing.Size(94, 19)
        Me.cbLetterLen.TabIndex = 24
        Me.cbLetterLen.Text = "글자 수 확인"
        Me.cbLetterLen.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(524, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 15)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Pattern List: "
        '
        'lbPattern
        '
        Me.lbPattern.FormattingEnabled = True
        Me.lbPattern.ItemHeight = 15
        Me.lbPattern.Location = New System.Drawing.Point(527, 27)
        Me.lbPattern.Name = "lbPattern"
        Me.lbPattern.Size = New System.Drawing.Size(531, 214)
        Me.lbPattern.TabIndex = 10
        '
        'lbFileCount
        '
        Me.lbFileCount.AutoSize = True
        Me.lbFileCount.Location = New System.Drawing.Point(442, 9)
        Me.lbFileCount.Name = "lbFileCount"
        Me.lbFileCount.Size = New System.Drawing.Size(14, 15)
        Me.lbFileCount.TabIndex = 9
        Me.lbFileCount.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(382, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 15)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "파일 수: "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 15)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "File List: "
        '
        'lbDragInform
        '
        Me.lbDragInform.AutoSize = True
        Me.lbDragInform.Location = New System.Drawing.Point(172, 165)
        Me.lbDragInform.Name = "lbDragInform"
        Me.lbDragInform.Size = New System.Drawing.Size(166, 15)
        Me.lbDragInform.TabIndex = 5
        Me.lbDragInform.Text = "여기로 폴더를 끌어 오십시오."
        '
        'lvFileList
        '
        Me.lvFileList.AllowDrop = True
        Me.lvFileList.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.lvFileList.FullRowSelect = True
        Me.lvFileList.GridLines = True
        Me.lvFileList.Location = New System.Drawing.Point(6, 27)
        Me.lvFileList.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.lvFileList.Name = "lvFileList"
        Me.lvFileList.Size = New System.Drawing.Size(515, 305)
        Me.lvFileList.TabIndex = 7
        Me.lvFileList.UseCompatibleStateImageBehavior = False
        Me.lvFileList.View = System.Windows.Forms.View.Details
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.cbAnime)
        Me.TabPage2.Controls.Add(Me.Label17)
        Me.TabPage2.Controls.Add(Me.Label16)
        Me.TabPage2.Controls.Add(Me.Label15)
        Me.TabPage2.Controls.Add(Me.tbSubtitleAddr)
        Me.TabPage2.Controls.Add(Me.tbMovieAddr)
        Me.TabPage2.Controls.Add(Me.Label14)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.bSubtitle)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1064, 549)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Tools"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cbAnime
        '
        Me.cbAnime.AutoSize = True
        Me.cbAnime.Location = New System.Drawing.Point(762, 131)
        Me.cbAnime.Name = "cbAnime"
        Me.cbAnime.Size = New System.Drawing.Size(122, 19)
        Me.cbAnime.TabIndex = 71
        Me.cbAnime.Text = "숫자 두 자리 맞춤"
        Me.cbAnime.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(221, 131)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(406, 15)
        Me.Label17.TabIndex = 70
        Me.Label17.Text = "구조가 조금 다르더라도 패턴이 같으면 제일 많이 다른 패턴만 치환합니다."
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(221, 146)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(439, 15)
        Me.Label16.TabIndex = 69
        Me.Label16.Text = "또한, ""숫자 두 자리 맞춤""에 체크가 해제되면 파일 이름 순서에 따라 치환합니다."
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(221, 116)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(481, 15)
        Me.Label15.TabIndex = 68
        Me.Label15.Text = "동영상에선 숫자를 제외한 부분을, 자막에선 숫자 부분만을 가져와 자막에 적용시킵니다."
        '
        'tbSubtitleAddr
        '
        Me.tbSubtitleAddr.AllowDrop = True
        Me.tbSubtitleAddr.Location = New System.Drawing.Point(212, 79)
        Me.tbSubtitleAddr.Name = "tbSubtitleAddr"
        Me.tbSubtitleAddr.Size = New System.Drawing.Size(525, 23)
        Me.tbSubtitleAddr.TabIndex = 67
        '
        'tbMovieAddr
        '
        Me.tbMovieAddr.AllowDrop = True
        Me.tbMovieAddr.Location = New System.Drawing.Point(212, 48)
        Me.tbMovieAddr.Name = "tbMovieAddr"
        Me.tbMovieAddr.Size = New System.Drawing.Size(525, 23)
        Me.tbMovieAddr.TabIndex = 66
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(140, 82)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(66, 15)
        Me.Label14.TabIndex = 65
        Me.Label14.Text = "자막 폴더: "
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(128, 51)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(78, 15)
        Me.Label13.TabIndex = 64
        Me.Label13.Text = "동영상 폴더: "
        '
        'bSubtitle
        '
        Me.bSubtitle.Location = New System.Drawing.Point(762, 54)
        Me.bSubtitle.Name = "bSubtitle"
        Me.bSubtitle.Size = New System.Drawing.Size(176, 41)
        Me.bSubtitle.TabIndex = 63
        Me.bSubtitle.Text = "동영상 && 자막 맞추기"
        Me.bSubtitle.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.rtbError)
        Me.TabPage4.Location = New System.Drawing.Point(4, 24)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(1064, 549)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Log"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'rtbError
        '
        Me.rtbError.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbError.Location = New System.Drawing.Point(3, 3)
        Me.rtbError.Name = "rtbError"
        Me.rtbError.ReadOnly = True
        Me.rtbError.Size = New System.Drawing.Size(1058, 543)
        Me.rtbError.TabIndex = 0
        Me.rtbError.Text = ""
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.bUndo)
        Me.TabPage5.Controls.Add(Me.lvUndo)
        Me.TabPage5.Controls.Add(Me.Label12)
        Me.TabPage5.Location = New System.Drawing.Point(4, 24)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(1064, 549)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Undo"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'bUndo
        '
        Me.bUndo.Location = New System.Drawing.Point(896, 511)
        Me.bUndo.Name = "bUndo"
        Me.bUndo.Size = New System.Drawing.Size(162, 32)
        Me.bUndo.TabIndex = 5
        Me.bUndo.Text = "해당 항목 되돌리기"
        Me.bUndo.UseVisualStyleBackColor = True
        '
        'lvUndo
        '
        Me.lvUndo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lvUndo.ContextMenuStrip = Me.menuUndo
        Me.lvUndo.FullRowSelect = True
        Me.lvUndo.GridLines = True
        Me.lvUndo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvUndo.Location = New System.Drawing.Point(6, 24)
        Me.lvUndo.Name = "lvUndo"
        Me.lvUndo.Size = New System.Drawing.Size(1052, 481)
        Me.lvUndo.TabIndex = 4
        Me.lvUndo.UseCompatibleStateImageBehavior = False
        Me.lvUndo.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Index"
        Me.ColumnHeader1.Width = 70
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "날짜"
        Me.ColumnHeader2.Width = 163
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "경로"
        Me.ColumnHeader3.Width = 678
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "사용가능 여부"
        Me.ColumnHeader4.Width = 89
        '
        'menuUndo
        '
        Me.menuUndo.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.상세한정보확인KToolStripMenuItem})
        Me.menuUndo.Name = "ContextMenuStrip1"
        Me.menuUndo.Size = New System.Drawing.Size(182, 26)
        '
        '상세한정보확인KToolStripMenuItem
        '
        Me.상세한정보확인KToolStripMenuItem.Name = "상세한정보확인KToolStripMenuItem"
        Me.상세한정보확인KToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.상세한정보확인KToolStripMenuItem.Text = "상세한 정보 확인(&K)"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(3, 6)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(98, 15)
        Me.Label12.TabIndex = 3
        Me.Label12.Text = "모든 로그 기록 : "
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label2)
        Me.TabPage3.Controls.Add(Me.lbName)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1064, 549)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Inform"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(262, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Copyright (C) 2015. rollrat. All Rights Reserved."
        '
        'lbName
        '
        Me.lbName.AutoSize = True
        Me.lbName.Location = New System.Drawing.Point(19, 18)
        Me.lbName.Name = "lbName"
        Me.lbName.Size = New System.Drawing.Size(150, 15)
        Me.lbName.TabIndex = 0
        Me.lbName.Text = "RollRat Software Renamer "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("맑은 고딕", 12.0!)
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(298, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 21)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "로드된 폴더: "
        '
        'lbLoadedFolder
        '
        Me.lbLoadedFolder.AutoSize = True
        Me.lbLoadedFolder.Font = New System.Drawing.Font("맑은 고딕", 12.0!)
        Me.lbLoadedFolder.ForeColor = System.Drawing.Color.Blue
        Me.lbLoadedFolder.Location = New System.Drawing.Point(410, 8)
        Me.lbLoadedFolder.Name = "lbLoadedFolder"
        Me.lbLoadedFolder.Size = New System.Drawing.Size(42, 21)
        Me.lbLoadedFolder.TabIndex = 9
        Me.lbLoadedFolder.Text = "없음"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1096, 601)
        Me.Controls.Add(Me.lbLoadedFolder)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tabControl)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.Text = "RollRat Renamer "
        Me.tabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.tbPatternMax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.menuUndo.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents lbName As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbDragInform As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lvFileList As System.Windows.Forms.ListView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbLoadedFolder As System.Windows.Forms.Label
    Friend WithEvents lbFileCount As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lbPattern As System.Windows.Forms.ListBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbPatternMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents cbLetterLen As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbSymbol As System.Windows.Forms.TextBox
    Friend WithEvents cbSpaceLetter As System.Windows.Forms.CheckBox
    Friend WithEvents cbNumLetter As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lbTargetList As System.Windows.Forms.ListBox
    Friend WithEvents lbChose As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents clbPatternElement As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lbReplaceLineCount As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents rtbReplace As System.Windows.Forms.RichTextBox
    Friend WithEvents bFindOverlap As System.Windows.Forms.Button
    Friend WithEvents bFindInvOverlap As System.Windows.Forms.Button
    Friend WithEvents tbOverlap As System.Windows.Forms.TextBox
    Friend WithEvents bPreview As System.Windows.Forms.Button
    Friend WithEvents bRename As System.Windows.Forms.Button
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents rtbError As System.Windows.Forms.RichTextBox
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents bUndo As System.Windows.Forms.Button
    Friend WithEvents lvUndo As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents menuUndo As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 상세한정보확인KToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bSubtitle As System.Windows.Forms.Button
    Friend WithEvents tbSubtitleAddr As System.Windows.Forms.TextBox
    Friend WithEvents tbMovieAddr As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cbAnime As System.Windows.Forms.CheckBox

End Class
