'/*************************************************************************
'
'   Copyright (C) 2015-2016. rollrat. All Rights Reserved.
'
'   Author: HyunJun Jeong
'
'***************************************************************************/

Module Parse

    Private Sub c_put_code_style(ByRef skip As Boolean, _
                ByRef replace_string As String, ByRef skip_string As String, _
                ByRef target As String, ByRef i As Integer)
        If frmMain.is_number(target(i)) Then
            Do
                If Not skip Then replace_string += target(i) Else skip_string += target(i)
                i += 1
                If i >= target.Length Then Exit Do
            Loop While frmMain.is_number(target(i))
            i -= 1
        ElseIf frmMain.is_deli(target(i)) Then
            Do
                If Not skip Then replace_string += target(i) Else skip_string += target(i)
                i += 1
                If i >= target.Length Then Exit Do
            Loop While frmMain.is_deli(target(i))
            i -= 1
        Else
            Do
                If Not skip Then replace_string += target(i) Else skip_string += target(i)
                i += 1
                If i >= target.Length Then Exit Do
            Loop While Not frmMain.is_number(target(i)) AndAlso Not frmMain.is_deli(target(i))
            i -= 1
        End If
    End Sub

    Public Function ReplacePattern(ByVal indexs As List(Of Integer), _
                   ByVal lines As List(Of String), ByVal targets As List(Of String)) As List(Of String)
        Dim return_list As New List(Of String)
        c_already_count = False
        c_count = 0
        force_end = False
        For Each target As String In targets
            Dim pure_count As Integer = 0
            Dim line_count As Integer = 0
            Dim replace_string As String = ""
            Dim skip_string As String = ""
            Dim skip As Boolean = False
            For i As Integer = 0 To target.Length - 1
                If indexs.Contains(pure_count) Then
                    skip = True
                    skip_string = ""
                End If
                c_put_code_style(skip, replace_string, skip_string, target, i)
                If skip Then
                    replace_string += c_parse_function_pattern(lines(line_count), skip_string)
                    If force_end = True Then Return return_list
                    line_count += 1
                    skip = False
                End If
                pure_count += 1
            Next
            return_list.Add(replace_string)
        Next
        Return return_list
    End Function

    Public Function SplitPattern(ByVal target As String) As List(Of String)
        Dim ret As New List(Of String)
        For i As Integer = 0 To target.Length - 1
            Dim replace_string As String = ""
            If frmMain.is_number(target(i)) Then
                Do
                    replace_string += target(i)
                    i += 1
                    If i >= target.Length Then Exit Do
                Loop While frmMain.is_number(target(i))
                i -= 1
            ElseIf frmMain.is_deli(target(i)) Then
                Do
                    replace_string += target(i)
                    i += 1
                    If i >= target.Length Then Exit Do
                Loop While frmMain.is_deli(target(i))
                i -= 1
            Else
                Do
                    replace_string += target(i)
                    i += 1
                    If i >= target.Length Then Exit Do
                Loop While Not frmMain.is_number(target(i)) AndAlso Not frmMain.is_deli(target(i))
                i -= 1
            End If
            ret.Add(replace_string)
        Next
        Return ret
    End Function

    Public Function FindOverlap(ByVal tagt_items As List(Of String), ByRef overlap_int As List(Of Integer), ByVal patterns As CheckedListBox.ObjectCollection)
        Dim first_bo As Boolean = False
        Dim last_ch As List(Of String) = Nothing
        Dim last_int As List(Of Integer) = Nothing

        For Each target As String In tagt_items
            If first_bo = False Then
                first_bo = True
                last_ch = SplitPattern(target)
            Else
                Dim ret_val As List(Of String) = SplitPattern(target)
                Dim overlap_tmp As New List(Of Integer)
                Dim overlap_tmp2 As New List(Of Integer)

                For i As Integer = 0 To last_ch.Count - 1
                    If ret_val(i) = last_ch(i) Then
                        overlap_tmp.Add(i)
                    End If
                Next

                If last_int Is Nothing Then
                    last_int = overlap_tmp
                    last_ch = ret_val
                Else
                    For Each i As Integer In last_int
                        If overlap_tmp.Contains(i) Then
                            overlap_tmp2.Add(i)
                        End If
                    Next
                    last_int.Clear()
                    last_int = overlap_tmp2
                    overlap_tmp.Clear()
                End If
            End If
        Next

        If Not last_ch Is Nothing Then
            overlap_int = last_int
        End If

        Dim lstStructString As String = ""
        Dim lstStruct As List(Of String) = SplitPattern(tagt_items(0))
        For i As Integer = 0 To patterns.Count - 1
            If overlap_int.Contains(i) Then
                lstStructString += lstStruct(i)
            Else
                lstStructString += patterns(i)
            End If
        Next

        Return lstStructString
    End Function





    Public force_end As Boolean = False

    Private c_count As Integer
    Private c_already_count As Boolean = False

    Private c_last_split As String
    Private c_last_loop As Boolean = False

    Private global_line_text As String
    Private global_line_ptr As Integer

    Private Structure c_status
        Dim IsClose As Boolean
        Dim IsInt As Boolean
        Dim IsNone As Boolean
        Dim ivalue As Integer
        Dim cvalue As Char
    End Structure

    Private Structure c_section
        Dim IsCarrot As Boolean
        Dim IsOnly As Boolean
        Dim lvalue As Integer
        Dim rvalue As Integer
    End Structure

    Private Sub c_skip_whitespace()
        For f = global_line_ptr To global_line_text.Length - 1
            If Not global_line_text(f) = " "c Then
                global_line_ptr = f
                Exit Sub
            End If
        Next
    End Sub

    Private Function c_match(ByVal ch As Char) As Boolean
        If global_line_text.Length <= global_line_ptr Then
            Return False
        End If
        If global_line_text(global_line_ptr) = ch Then
            global_line_ptr += 1
            Return True
        Else
            Return False
        End If
    End Function

    Private Function c_process_inside(ByRef status As c_status) As Boolean
        If global_line_text.Length <= global_line_ptr Then
            Return False
        End If
        Dim right As Boolean = False
        If c_match("["c) Then
            status.IsClose = True
        ElseIf c_match("("c) Then
            status.IsClose = False
        Else
            right = True
        End If
        c_skip_whitespace()
        If IsNumeric(global_line_text(global_line_ptr)) Then
            Dim temp As Integer = 0
            Do
                temp *= 10
                temp += Asc(global_line_text(global_line_ptr)) - Asc("0"c)
                global_line_ptr += 1
            Loop While frmMain.is_number(global_line_text(global_line_ptr))
            status.ivalue = temp
            status.IsInt = True
        ElseIf c_match("'"c) Then
            status.IsInt = False
            status.cvalue = global_line_text(global_line_ptr)
            global_line_ptr += 1
            If Not c_match("'"c) Then
                Return False
            End If
        ElseIf c_match("$"c) Or c_match("^"c) Then
            status.IsInt = False
            status.cvalue = global_line_text(global_line_ptr - 1)
        ElseIf c_match(","c) Then
            global_line_ptr -= 1
            status.IsNone = True
        Else
            Return False
        End If
        If c_match("]"c) Then
            status.IsClose = True
        ElseIf c_match(")"c) Then
            status.IsClose = False
        End If
        Return True
    End Function

    Private Function c_inside_overlap_part(ByRef left As c_status, ByRef right As c_status) As Boolean
        c_skip_whitespace()
        If Not c_process_inside(left) Then
            c_show_error("Not found.") : Return False
        End If
        c_skip_whitespace()
        If Not c_match(",") Then
            c_show_error("Not found ',' symbol.") : Return False
        End If
        c_skip_whitespace()
        If Not c_process_inside(right) Then
            c_show_error("Not found.") : Return False
        End If
        c_skip_whitespace()
        Return True
    End Function

    Private Function c_inside_section(ByRef section As c_section) As Boolean
        If global_line_text.Length <= global_line_ptr Then
            Return False
        End If
        c_skip_whitespace()
        If c_match("^"c) Then
            section.IsCarrot = True
        Else
            section.IsCarrot = False
        End If
        c_skip_whitespace()
        If IsNumeric(global_line_text(global_line_ptr)) Then
            Dim temp As Integer = 0
            Do
                temp *= 10
                temp += Asc(global_line_text(global_line_ptr)) - Asc("0"c)
                global_line_ptr += 1
            Loop While frmMain.is_number(global_line_text(global_line_ptr))
            section.IsOnly = True
            section.lvalue = temp
        Else
            c_show_error("It's not numberic.") : Return False
        End If
        c_skip_whitespace()
        If c_match(","c) Then
            c_skip_whitespace()
            If IsNumeric(global_line_text(global_line_ptr)) Then
                Dim temp As Integer = 0
                Do
                    temp *= 10
                    temp += Asc(global_line_text(global_line_ptr)) - Asc("0"c)
                    global_line_ptr += 1
                Loop While frmMain.is_number(global_line_text(global_line_ptr))
                section.IsOnly = False
                section.rvalue = temp
            Else
                c_show_error("It's not numberic.") : Return False
            End If
        End If
        Return True
    End Function

    Private Function c_inside_command(ByVal skip_string As String, ByRef success As Boolean) As String
        Dim closure_pos As Integer
        success = True
        For j As Integer = global_line_text.Length - 1 To 0 Step -1
            If global_line_text(j) = "}"c Then
                closure_pos = j
            End If
        Next
        Dim text As String = Mid(global_line_text, global_line_ptr + 1, closure_pos - 1)
        Dim split_comma As String() = text.Split(","c)
        If split_comma.Length = 1 Then
            If split_comma(0) = "upper" Then
                Return skip_string.ToUpper
            ElseIf split_comma(0) = "lower" Then
                Return skip_string.ToLower
            ElseIf split_comma(0) = "reverse" Then
                Dim t() As Char = skip_string.ToCharArray()
                Array.Reverse(t)
                Return New String(t)
            End If
        ElseIf split_comma.Length <= 3 Then
            Dim left, right As String
            Dim other As String = Nothing
            If split_comma.Length = 3 Then
                other = split_comma(2).Trim
            End If
            left = split_comma(0).Trim
            right = split_comma(1).Trim
            If left <> "replace" AndAlso left <> "hash" AndAlso _
                                                 (Not left.Contains("lock")) Then
                If Not IsNumeric(right) Then
                    success = False
                    c_show_error("Not found in '{' pattern and right side is not numeric.") : Return skip_string
                ElseIf Not other Is Nothing Then
                    If Not IsNumeric(other) Then
                        success = False
                        c_show_error("Not found in '{' pattern and right side is not numeric.") : Return skip_string
                    End If
                End If
            End If
            If left = "left" Then
                Return Strings.Left(skip_string, CInt(right))
            ElseIf left = "mid" Then
                If Not other Is Nothing Then
                    Return Strings.Mid(skip_string, CInt(right))
                Else
                    Return Strings.Mid(skip_string, CInt(right), CInt(other))
                End If
            ElseIf left = "right" Then
                Return Strings.Right(skip_string, CInt(right))
            ElseIf left = "fleft" Then
                If skip_string.Length < CInt(right) Then
                    If Not other Is Nothing Then
                        Dim returnstxt As String = skip_string
                        Dim len As Integer = CInt(right) - skip_string.Length
                        If other.Length >= len Then
                            For ji As Integer = 0 To len - 1
                                returnstxt += other(ji)
                            Next
                        Else
                            For ji As Integer = 1 To len
                                returnstxt += other(0)
                            Next
                        End If
                        Return returnstxt
                    Else
                        success = False
                        c_show_error("Not found fleft others.") : Return skip_string
                    End If
                Else
                    Return Strings.Left(skip_string, CInt(right))
                End If
            ElseIf left = "fright" Then
                If skip_string.Length < CInt(right) Then
                    If Not other Is Nothing Then
                        Dim returnstxt As String = ""
                        Dim len As Integer = CInt(right) - skip_string.Length
                        If other.Length >= len Then
                            For ji As Integer = 0 To len - 1
                                returnstxt += other(ji)
                            Next
                        Else
                            For ji As Integer = 1 To len
                                returnstxt += other(0)
                            Next
                        End If
                        Return returnstxt & skip_string
                    Else
                        success = False
                        c_show_error("Not found fright others.") : Return skip_string
                    End If
                Else
                    Return Strings.Right(skip_string, CInt(right))
                End If
            ElseIf left = "lcount" Then
                If Not c_already_count Then
                    c_count = right
                    c_already_count = True
                Else
                    c_count += 1
                End If
                If Not other Is Nothing Then
                    Dim ctrtxt As String = CStr(c_count)
                    If CInt(other) >= ctrtxt.Length Then
                        Dim len As Integer = CInt(other) - ctrtxt.Length
                        For ji = 1 To len
                            ctrtxt = "0"c & ctrtxt
                        Next
                        Return ctrtxt & skip_string
                    Else
                        success = False
                        c_show_error("Left count max size is too low.") : Return skip_string
                    End If
                Else
                    Return CStr(c_count) & skip_string
                End If
            ElseIf left = "rcount" Then
                If Not c_already_count Then
                    c_count = right
                    c_already_count = True
                Else
                    c_count += 1
                End If
                If Not other Is Nothing Then
                    Dim ctrtxt As String = CStr(c_count)
                    If CInt(other) >= ctrtxt.Length Then
                        Dim len As Integer = CInt(other) - ctrtxt.Length
                        For ji = 1 To len
                            ctrtxt = "0"c & ctrtxt
                        Next
                        Return skip_string & ctrtxt
                    Else
                        success = False
                        c_show_error("Right count max size is too low.") : Return skip_string
                    End If
                Else
                    Return skip_string & CStr(c_count)
                End If
            ElseIf left = "count" Then
                If Not c_already_count Then
                    c_count = right
                    c_already_count = True
                Else
                    c_count += 1
                End If
                If Not other Is Nothing Then
                    Dim ctrtxt As String = CStr(c_count)
                    If CInt(other) >= ctrtxt.Length Then
                        Dim len As Integer = CInt(other) - ctrtxt.Length
                        For ji = 1 To len
                            ctrtxt = "0"c & ctrtxt
                        Next
                        Return ctrtxt
                    Else
                        success = False
                        c_show_error("Count max size is too low.") : Return skip_string
                    End If
                Else
                    Return CStr(c_count)
                End If
            ElseIf left = "replace" Then
                If Not other Is Nothing Then
                    Return skip_string.Replace(right, other)
                Else
                    success = False
                    c_show_error("Too few replace arguments.") : Return skip_string
                End If
            Else
                success = False
                c_show_error("Not founded command.") : Return skip_string
            End If

        Else
            success = False
            If Not c_match(">"c) Then c_show_error("Arguments count are too long.") : Return skip_string
        End If
        Return skip_string
    End Function

    Private Sub c_internal_process_command_overlap(ByVal status As c_status, ByRef position As Integer, ByVal skip_string As String)
        If status.IsInt = False Then
            If status.IsNone = False Then
                If status.cvalue = "^" Then
                    position = 0
                ElseIf status.cvalue = "$" Then
                    position = skip_string.Length - 1
                Else
                    position = 0
                    Do
                        position += 1
                    Loop Until skip_string(position) = status.cvalue
                End If
            Else
                position = 0
            End If
        Else
            position = status.ivalue - 1
        End If
    End Sub

    Private Function c_parse_function_pattern_internal(ByVal line As String, ByVal skip_string As String) As String
        Dim return_string As String = ""

        Dim s_type As Boolean = False

        Dim left_left As c_status
        Dim left_right As c_status

        Dim right_left As c_status
        Dim right_right As c_status

        Dim section As c_section

        Dim s_target As Integer = 0

        If line.Length = 0 Then
            Return skip_string
        End If

        global_line_text = line

        For i As Integer = 0 To line.Length - 1
            global_line_ptr = i
            If c_match("["c) Then
                s_type = False
                If Not c_inside_overlap_part(left_left, left_right) Then Return skip_string
                If c_match("<"c) Then
                    If c_match("-"c) Then
                        If c_match(">"c) Then
                            s_target = 3
                        Else
                            s_target = 2
                        End If
                    End If
                ElseIf c_match("-"c) Then
                    If c_match(">"c) Then
                        s_target = 1
                    Else
                        s_target = 4
                    End If
                End If
                If Not c_inside_overlap_part(right_left, right_right) Then Return skip_string
                If Not c_match("]"c) Then c_show_error("Not found closure.") : Return skip_string
                Exit For
            ElseIf c_match("<"c) Then
                s_type = True
                If Not c_inside_section(section) Then Return skip_string
                If Not c_match(">"c) Then c_show_error("Not found closure.") : Return skip_string
                Exit For
            ElseIf c_match("{"c) Then
                If line.TrimEnd.EndsWith("}"c) Then
                    Dim issuccess As Boolean
                    return_string = c_inside_command(skip_string, issuccess)
                    If issuccess Then
                        Return return_string
                    Else
                        Return skip_string
                    End If
                Else
                    If Not c_match(">"c) Then c_show_error("Not found closure.") : Return skip_string
                End If
                Exit For
            Else
                c_show_error("Not found replace grammar.") : Return skip_string
            End If
        Next

        If s_type Then
            Try
                c_last_loop = True
                If section.IsOnly = True Then
                    If section.lvalue = 0 Then
                        If section.IsCarrot = False Then
                            Return skip_string & c_last_split
                        Else
                            Return c_last_split & skip_string
                        End If
                    Else
                        For i = 0 To section.lvalue - 1
                            return_string += skip_string(i)
                        Next
                    End If
                Else
                    If section.lvalue < section.rvalue Then
                        For i = section.lvalue - 1 To section.rvalue - 1
                            return_string += skip_string(i)
                        Next
                    ElseIf section.lvalue = section.rvalue Then
                        return_string = skip_string(section.lvalue)
                    ElseIf section.lvalue > section.rvalue Then
                        For i = section.rvalue - 1 To section.lvalue - 1
                            return_string += skip_string(section.lvalue - 1 - i)
                        Next
                    End If
                End If
                If section.IsCarrot = False Then
                    Return return_string & c_last_split
                Else
                    Return c_last_split & return_string
                End If
            Catch ex As Exception
                c_show_error("Incorrect index value for all files.")
                WriteLog("       -- lvalue=" & section.lvalue & ";rvalue=" & section.rvalue & ";skip=" & skip_string, True)
                Return skip_string
            End Try
        Else
            Dim lsave As String = ""
            Dim lstart_pos, lend_pos As Integer
            Dim rsave As String = ""
            Dim rstart_pos, rend_pos As Integer

            c_internal_process_command_overlap(left_left, lstart_pos, skip_string)
            c_internal_process_command_overlap(left_right, lend_pos, skip_string)

            If lstart_pos < lend_pos Then
                If lend_pos >= skip_string.Length Then
                    c_show_error("Not found scope number too long.")
                    WriteLog("       -- lend_pos=" & lend_pos & ";skip=" & skip_string.Length, True)
                    Return skip_string
                End If
                For i = lstart_pos To lend_pos
                    lsave += skip_string(i)
                Next
            ElseIf lstart_pos = lend_pos Then
                If lend_pos >= skip_string.Length Then
                    c_show_error("Not found scope number too long.")
                    WriteLog("       -- lend_pos=" & lend_pos & ";skip=" & skip_string.Length, True)
                    Return skip_string
                End If
                lsave += skip_string(lstart_pos)
            ElseIf lend_pos < lstart_pos Then
                If lstart_pos >= skip_string.Length Then
                    c_show_error("Not found scope number too long.")
                    WriteLog("       -- lstart_pos=" & lstart_pos & ";skip=" & skip_string.Length, True)
                    Return skip_string
                End If
                For i = lend_pos To lstart_pos
                    lsave += skip_string(lstart_pos - i)
                Next
            End If

            c_internal_process_command_overlap(right_left, rstart_pos, skip_string)
            c_internal_process_command_overlap(right_right, rend_pos, skip_string)

            If rstart_pos < rend_pos Then
                If rend_pos >= skip_string.Length Then
                    c_show_error("Not found scope number too long.")
                    WriteLog("       -- rend_pos=" & rend_pos & ";skip=" & skip_string.Length, True)
                    Return skip_string
                End If
                For i = rstart_pos To rend_pos
                    rsave += skip_string(i)
                Next
            ElseIf rstart_pos = rend_pos Then
                If rend_pos >= skip_string.Length Then
                    c_show_error("Not found scope number too long.")
                    WriteLog("       -- rend_pos=" & rend_pos & ";skip=" & skip_string.Length, True)
                    Return skip_string
                End If
                rsave += skip_string(rstart_pos)
            ElseIf rend_pos < rstart_pos Then
                If rstart_pos >= skip_string.Length Then
                    c_show_error("Not found scope number too long.")
                    WriteLog("       -- rstart_pos=" & rstart_pos & ";skip=" & skip_string.Length, True)
                    Return skip_string
                End If
                For i = rend_pos To rstart_pos
                    rsave += skip_string(rstart_pos - i)
                Next
            End If

            If s_target = 1 Then
                If rstart_pos < rend_pos Then
                    For i = 0 To rstart_pos - 1
                        return_string += skip_string(i)
                    Next
                    For i = rstart_pos To rend_pos
                        return_string += lsave(i - rstart_pos)
                    Next
                    If rend_pos + 1 <> skip_string.Length - 1 Then
                        For i = rend_pos + 1 To skip_string.Length - 1
                            return_string += skip_string(i)
                        Next
                    End If
                ElseIf rstart_pos = rend_pos Then
                    For i = 0 To rstart_pos - 1
                        return_string += skip_string(i)
                    Next
                    return_string += lsave(0)
                    If rend_pos + 1 <> skip_string.Length - 1 Then
                        For i = rend_pos + 1 To skip_string.Length - 1
                            return_string += skip_string(i)
                        Next
                    End If
                ElseIf rstart_pos > rend_pos Then
                    For i = 0 To rend_pos - 1
                        return_string += skip_string(i)
                    Next
                    For i = 0 To rstart_pos - rend_pos
                        return_string += lsave(rstart_pos - rend_pos - i)
                    Next
                    If rstart_pos + 1 <> skip_string.Length - 1 Then
                        For i = rstart_pos + 1 To skip_string.Length - 1
                            return_string += skip_string(i)
                        Next
                    End If
                End If
            ElseIf s_target = 2 Then
                If lstart_pos < lend_pos Then
                    For i = 0 To lstart_pos - 1
                        return_string += skip_string(i)
                    Next
                    For i = lstart_pos To lend_pos
                        return_string += rsave(i - lstart_pos)
                    Next
                    If lend_pos + 1 <> skip_string.Length - 1 Then
                        For i = lend_pos + 1 To skip_string.Length - 1
                            return_string += skip_string(i)
                        Next
                    End If
                ElseIf lstart_pos = lend_pos Then
                    For i = 0 To lstart_pos - 1
                        return_string += skip_string(i)
                    Next
                    return_string += rsave(0)
                    If lend_pos + 1 <> skip_string.Length - 1 Then
                        For i = lend_pos + 1 To skip_string.Length - 1
                            return_string += skip_string(i)
                        Next
                    End If
                ElseIf lstart_pos > lend_pos Then
                    For i = 0 To lend_pos - 1
                        return_string += skip_string(i)
                    Next
                    For i = lend_pos To lstart_pos
                        return_string += rsave(lstart_pos - lend_pos - i)
                    Next
                    If lstart_pos + 1 <> skip_string.Length - 1 Then
                        For i = lstart_pos + 1 To skip_string.Length - 1
                            return_string += skip_string(i)
                        Next
                    End If
                End If
            ElseIf s_target = 3 Then
                Dim max_s1, min_s2 As Integer
                If lstart_pos <= lend_pos Then max_s1 = lend_pos Else max_s1 = lstart_pos
                If rstart_pos <= rend_pos Then min_s2 = rstart_pos Else min_s2 = rend_pos
                If max_s1 < min_s2 AndAlso Math.Abs(lstart_pos - lend_pos) = Math.Abs(rstart_pos - rend_pos) Then
                    If lstart_pos > lend_pos Then c_swap_int(lstart_pos, lend_pos)
                    For i = 0 To lstart_pos - 1
                        return_string += skip_string(i)
                    Next
                    For i = lstart_pos To lend_pos
                        return_string += rsave(i - lstart_pos)
                    Next
                    If rstart_pos < rend_pos Then
                        For i = lend_pos + 1 To rstart_pos - 1
                            return_string += skip_string(i)
                        Next
                        For i = rstart_pos To rend_pos
                            return_string += lsave(i - rstart_pos)
                        Next
                    ElseIf rstart_pos = rend_pos Then
                        return_string += lsave(0)
                    Else
                        For i = lend_pos + 1 To rend_pos - 1
                            return_string += skip_string(i)
                        Next
                        For i = rend_pos To rstart_pos
                            return_string += lsave(rstart_pos - rend_pos - i)
                        Next
                    End If
                Else
                    c_show_error("Not found scope type. It's incorrect scope syntax.")
                    Return skip_string
                End If
            ElseIf s_target = 4 Then
                If lstart_pos > lend_pos Then c_swap_int(lstart_pos, lend_pos)
                If rstart_pos > rend_pos Then c_swap_int(rstart_pos, rend_pos)
                If lstart_pos <= rstart_pos Then
                    If rend_pos <= lend_pos Then
                        For i = lstart_pos To rstart_pos - 1
                            return_string += skip_string(i)
                        Next
                        For i = rend_pos + 1 To lend_pos
                            return_string += skip_string(i)
                        Next
                    End If
                End If
            End If
        End If

        Return return_string
    End Function

    Private Function c_parse_function_pattern(ByVal line As String, ByVal skip_string As String) As String
        If Not line.Contains("|"c) Then
            Return line
        End If
        Dim return_string As String = skip_string
        Dim after_split As String() = line.Split("|"c)
        c_last_split = after_split(after_split.Length - 1)
        For Each lstr As String In after_split
            c_last_loop = False
            return_string = c_parse_function_pattern_internal(lstr, return_string)
            If force_end Then
                Exit For
            End If
            If c_last_loop Then
                Exit For
            End If
        Next
        Return return_string
    End Function

    Private Sub c_swap_int(ByRef left As Integer, ByRef right As Integer)
        Dim tmp As Integer = left
        left = right
        right = tmp
    End Sub

    Private Sub c_show_error(ByVal error_text As String)
        MsgBox("Error: " & error_text & vbCrLf & _
               "Target : " & global_line_text, MsgBoxStyle.Critical, "RollRat Rename")
        WriteLog("Parse error. global_line=" & global_line_text & ";global_ptr=" & global_line_ptr, True)
        force_end = True
    End Sub

End Module