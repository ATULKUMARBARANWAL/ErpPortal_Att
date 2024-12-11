Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Class Reports_Academicrpt_Raba
    Inherits System.Web.UI.Page

    Dim sql As String = ""
    Private cmd As New dbnew()
    Private saralMastercls As saral.Mastercls = New saral.Mastercls()
    Private saralstudent As saral.student = New saral.student()

    Public tb_rowCols As String
    Public tb_header_Rows As Integer
    Public tableValue As String


    Dim maindt As DataTable
    Public tb_rowCols1 As String
    Public tb_header_Rows1 As Integer
    Public tableValue1 As String
    Dim maindt1 As DataTable
    Dim totalamt As Integer
    Public tabletotal As String


    Private cmdsql As New dbnew()
    Dim dt1 As DataTable

    Dim strHTMLBuilder As StringBuilder = New StringBuilder()
    Dim crystalReport As New ReportDocument()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then



            sql = "select InstitueID,code from Institue   order by Code "
            cmd.FillDropdown(ddlcollege, sql)
            sql = "select examnameid, examname from examname where ayid=" & Request.QueryString("ay") & " "
            cmd.FillDropdown(ddlexamname, sql)
        End If
    End Sub

    Protected Sub ddlcollege_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcollege.SelectedIndexChanged
        'ddlCbranch.Items.Clear()
        'Dim sql As String = "select branchid,code  from branch  where collegeid=" & ddlcollege.SelectedValue.ToString & ""
        'cmd.FillDropdown(ddlCbranch, sql)
        bind_coursetype()
    End Sub
    Sub bind_coursetype()
        ddlcoursetype.Items.Clear()
        ddlcourse.Items.Clear()
        Dim sql As String = "select distinct d.coursetypeid,c.code from Departmentcollege d " & _
              "left join Coursetype c on c.coursetypeid=d.coursetypeid " & _
                  " where d.collegeid=" & ddlcollege.SelectedValue.ToString & ""
        cmd.FillDropdown(ddlcoursetype, sql)



        ' bind_course()
    End Sub
    Protected Sub ddlcoursetype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcoursetype.SelectedIndexChanged
        bind_course()
    End Sub
    Sub bind_course()
        ddlcourse.Items.Clear()
        sql = "select courseid,course from Course where Cid=" & ddlcollege.SelectedValue.ToString & " and Branch=" & ddlcoursetype.SelectedValue.ToString & " and Courseid in (select courseid  from ExamCourse where examid =" & ddlexamname.SelectedValue.ToString & " )"
        cmd.FillDropdown(ddlcourse, sql)

    End Sub
    Protected Sub btnload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnload.Click
        bind_student()
    End Sub
    Sub bind_student()
        Dim w As String = ""
        If Request.QueryString("r") = "27" Or Request.QueryString("r") = "38" Then
            w = w + " where s.UserID=" & Request.QueryString("u") & "  "
        Else
            w = w + " where s.UserID<>1 "
        End If

        If ddlcollege.SelectedIndex > 0 Then
            w = w + " and c.Cid =" & ddlcollege.SelectedValue.ToString & " "
        End If

        If ddlcoursetype.SelectedIndex > 0 Then
            w = w + " and c.Branch ='" & ddlcoursetype.SelectedValue.ToString & "' "
        End If

        If ddlcourse.SelectedIndex > 0 Then
            w = w + " and c.CourseID ='" & ddlcourse.SelectedValue.ToString & "' "
        End If

        If ddlsem.SelectedIndex > 0 Then
            w = w + " and sy.sem ='" & ddlsem.SelectedValue.ToString & "' "
        End If

        'w = w + " and  s.IsStruckOff=0 and  s.RegistrationApproved=1 and sy.Courseid in (select courseid  from ExamCourse where examid =" & ddlexamname.SelectedValue.ToString & " )"

        w = w + " and  s.IsStruckOff=0 and  sy.Courseid in (select courseid  from ExamCourse where examid =" & ddlexamname.SelectedValue.ToString & " )"

        w = w + " and   ISNULL( ddd.status ,0) in (1 ,2)  "


        sql = "select  u.usernamefull as entryby, s.StudentID,  i.Code as Faculty, c.Code as Course ,s.Sem ,b.Batch ,s.AdmissionNo  ,  " & _
                " s.student,s.FatherName ,s.EnrollmentNo , case when s.studenttype='L' then 'Lateral' else 'Regular' end as Studenttype , " & _
                " case when  ISNULL( ddd.status ,0) = 0 then 'Not Apply' when  ISNULL( ddd.status ,1) = 1 then 'Exam From Pending' when  ISNULL( ddd.status ,2) = 2 then 'Exam From Approved' end  as Status  , ee.Rollnoid " & _
                " from student s  left join Course  c on c.CourseID =s.CourseID   left join Batch b on b.BatchID=s.BatchID   left join Institue i on i.InstitueID=c.Cid  " & _
                    " left join Users  u on u.userid=s.UserID   inner join StudentYear sy on sy.StudentID=s.StudentID and sy.ayid=" & Request.QueryString("ay") & "    left join ExamRollno ee on ee.Examid = " & ddlexamname.SelectedValue.ToString & " and ee.Studentid=s.StudentID  " & _
                " left join ( select distinct  Studentid , status from ( select  studentid,1 as  status from studentsubjectregistration where status=0 and Ayid=  " & Request.QueryString("ay") & "  " & _
                    " union all  select studentid , 2 as status from studentsubjectregistration where status=1  and Ayid=" & Request.QueryString("ay") & "   ) as dd ) as ddd on ddd.Studentid=s.StudentID   " & w & " and s.sessionid=" & Request.QueryString("s") & "  and ISNULL( ee.Rollnoid ,0)>0  order by ee.Rollnoid    "


        cmd.FillGrd(grdstudent, sql)
    End Sub
    Protected Sub ddlcourse_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcourse.SelectedIndexChanged
        ddlsem.getsem(ddlcourse.SelectedValue)
    End Sub
    Protected Sub grdstudent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdstudent.RowDataBound
        Dim a As Integer = e.Row.Cells.Count
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells("10").Text = "Not Apply" Then
                e.Row.ForeColor = Drawing.Color.Red
            ElseIf e.Row.Cells("10").Text = "Exam From Pending" Then
                e.Row.ForeColor = Drawing.Color.Blue
            ElseIf e.Row.Cells("10").Text = "Exam From Approved" Then
                e.Row.ForeColor = Drawing.Color.Green
            End If
        End If
    End Sub

    Public Sub Change_student(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chk As CheckBox = CType(grdstudent.HeaderRow.FindControl("chkall"), CheckBox)
        If CType(grdstudent.HeaderRow.FindControl("chkall"), CheckBox).Checked Then
            For Each rw As GridViewRow In grdstudent.Rows
                CType(rw.FindControl("chkselect"), CheckBox).Checked = True
            Next
        Else
            For Each rw As GridViewRow In grdstudent.Rows
                CType(rw.FindControl("chkselect"), CheckBox).Checked = False
            Next
        End If
    End Sub
    Protected Sub btn_cgpa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cgpa.Click
        pnlstudent.Visible = False
        pnlprint.Visible = True


        For Each itm As GridViewRow In grdstudent.Rows
            If CType(itm.FindControl("chkselect"), CheckBox).Checked = True Then
                '  printnewdmc(grdsubject.DataKeys(itm.RowIndex).Values(0))
                printnewdmc(grdstudent.DataKeys(itm.RowIndex).Values(0))
            End If
        Next
    End Sub
    Sub printnewdmc(ByVal sid As String)

        Dim aa As SqlParameter() = {New SqlParameter("@q", "Subject_Registration_regular_new_grd_raba_sheet"), New SqlParameter("@sid", sid), New SqlParameter("@ayid", Request.QueryString("ay")), New SqlParameter("@examname", ddlexamname.SelectedValue.ToString)}
        Dim dt As DataTable = cmd.getDataTable("[dbo].[Fee_Student]", aa, "dt")

        If dt.Rows.Count > 0 Then
        Else
            SaralMsg.Messagebx.Alert(Me, "no records found")
            Exit Sub
        End If

        strHTMLBuilder.Append("<table  align=""center""   width=""100%"" style="" color: #000000; font-family: calibri; font-size: 19px"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th  width=""6%"" rowspan=""6"" align=""center"" >")
        strHTMLBuilder.Append("<img src=""../../Photos/Branchlogo/1.jpg""  style=""  height=""100"" width=""100"" alt=""..."" >")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 23px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("MAHAKAUSHAL UNIVERSITY, JABALPUR (M.P)")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 19px""  width=""94%""  colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("ATTENDANCE SHEET")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        'strHTMLBuilder.Append("<tr>")
        'strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        'strHTMLBuilder.Append("Tentative Examination Time-Table")
        'strHTMLBuilder.Append("</th>")
        'strHTMLBuilder.Append("</tr>")




        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("college").ToString & " ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("course").ToString & "  Examination " & dt.Rows(0)("examname").ToString & " ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Examination " & ddlexamname.SelectedItem.ToString & " ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")




        strHTMLBuilder.Append("</table>")





        strHTMLBuilder.Append("<table  width=""100%""  style="" color: #000000; font-family: Tahoma; font-size: 19px"" cellpadding=""5"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<tD    width=""120px"" align=""left"" >")
        strHTMLBuilder.Append("ROLL NO :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td  width=""200px"" align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("rollnoid").ToString & "")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td  width=""120px""  align=""left"" >")
        strHTMLBuilder.Append("CANDIDATE NAME :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td  width=""200px"" align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("student").ToString & "")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<tD     align=""left"" >")
        strHTMLBuilder.Append("ENROL NO :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td   align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("EnrollmentNo").ToString & "")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td   align=""left"" >")
        strHTMLBuilder.Append("FATHER NAME :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td   align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("FatherName").ToString & "")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")





        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<tD    align=""left""  >")
        strHTMLBuilder.Append("STATUS :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td    align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("Status").ToString & "")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td  align=""left"" >")
        strHTMLBuilder.Append("SEMESTER/YEAR :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td   align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("SEm").ToString & "")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("</tr>")
        strHTMLBuilder.Append("</table>")





        strHTMLBuilder.Append("<table width=""100%""  border=""1""  style="" color: #000000; font-family: Tahoma; font-size: 19px"" cellpadding=""5"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<thead >")
        strHTMLBuilder.Append("<tr>")

        strHTMLBuilder.Append("<th  valign=""top""  nowrap  width=""20px"" align=""center"" >")
        strHTMLBuilder.Append("Sub Code")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th  valign=""top""   align=""left"" >")
        strHTMLBuilder.Append("Subject Name")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th   valign=""top"" align=""left"" >")
        strHTMLBuilder.Append("Exam Date")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th   valign=""top"" nowrap widht=""200px"" align=""center"" >")
        strHTMLBuilder.Append("Answer Book No")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th  valign=""top""   align=""center"" >")
        strHTMLBuilder.Append("Suppl Answer Book No")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th   valign=""top""  align=""center"" >")
        strHTMLBuilder.Append("Candidate Sign ")
        strHTMLBuilder.Append("</th>")



        strHTMLBuilder.Append("<th  valign=""top""   align=""center"" >")
        strHTMLBuilder.Append("Invigilator Sign")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("</thead>")

        Dim xx As Integer = 0
        For Each myRow As DataRow In dt.Rows

            strHTMLBuilder.Append("<tr>")

            strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""20px"" align=""left"">")
            strHTMLBuilder.Append("" + myRow("Code").ToString() + "")
            strHTMLBuilder.Append("</td>")

            strHTMLBuilder.Append("<td  valign=""top""  align=""left"" >")
            strHTMLBuilder.Append("" + myRow("Subject").ToString() + "")
            strHTMLBuilder.Append("</td>")

            strHTMLBuilder.Append("<td   valign=""top""  align=""left"" >")
            strHTMLBuilder.Append("" + myRow("ExamDate").ToString() + "")
            strHTMLBuilder.Append("</td>")


            strHTMLBuilder.Append("<td  valign=""top""  widht=""200px""   align=""center"" >")
            strHTMLBuilder.Append("")
            strHTMLBuilder.Append("</td>")

            strHTMLBuilder.Append("<td  valign=""top""  align=""center"" >")
            strHTMLBuilder.Append("")
            strHTMLBuilder.Append("</td>")

            strHTMLBuilder.Append("<td  valign=""top""  align=""center"" >")
            strHTMLBuilder.Append("")
            strHTMLBuilder.Append("</td>")

            strHTMLBuilder.Append("<td  valign=""top""  align=""center"" >")
            strHTMLBuilder.Append("")
            strHTMLBuilder.Append("</td>")



            strHTMLBuilder.Append("</tr>")

            xx = xx + 1
        Next


        strHTMLBuilder.Append("</table>")
        '  strHTMLBuilder.Append("<p style="" color: #000000; font-family: Tahoma; font-size: 19px"">Note      : S/US GRADE NOT COUNTED FOR CALCULATION OF SGPA. <br><strong> RESULT : " & dt.Rows(0)("result").ToString & "  WITH SGPA " & dt.Rows(0)("sgpa").ToString & " ON 10 POINT SCALE.</strong><p>")



        strHTMLBuilder.Append("<table  width=""100%""  style="" color: #000000; font-family: Tahoma; font-size: 19px"" cellpadding=""5"" cellspacing=""0"" >")
        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td style=""font-size: 19px""  width=""94%""   align=""right"" >")
        strHTMLBuilder.Append("<br><br><br><br><strong>Center Suptd.</strong>")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("</table>")



        strHTMLBuilder.Append("<br />")
        strHTMLBuilder.Append("<br />")
        'strHTMLBuilder.Append("<br />")
        ' strHTMLBuilder.Append(" <pre style="" color: #000000; font-family: Tahoma; font-size: 19px; font-weight: bold"" class=""tab"">   Prepared by                                          Checked by                                          Asstt. Registrar (Academics)</pre>")

        'strHTMLBuilder.Append("<p style="" color: #000000; font-family: Tahoma; font-size: 19px""><strong>Date of issue :" & dt.Rows(0)("ResultDate").ToString & " <br> Place       : Longowal</strong></p>")

        strHTMLBuilder.Append("<p style=""page-break-after:always;""></p>")
        Literal1.Text = strHTMLBuilder.ToString()
    End Sub
    Protected Sub bntdocprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bntdocprint.Click
        Session("ctrl") = pnl1
        ClientScript.RegisterStartupScript(Me.GetType(), "onclick", "<script language=javascript>window.open('print.aspx','PrintMe','height=800px,width=800px,scrollbars=1');</script>")
    End Sub

    Protected Sub btn_back_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back_print.Click
        pnlstudent.Visible = True
        pnlprint.Visible = False

    End Sub

    Protected Sub btnfail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfail.Click

        pnlstudent.Visible = False
        pnlprint.Visible = True



        strHTMLBuilder.Append("<table  align=""center""   width=""100%"" style="" color: #000000; font-family: calibri; font-size: 19px"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th  width=""6%"" rowspan=""6"" align=""center"" >")
        strHTMLBuilder.Append("<img src=""../../Photos/Branchlogo/1.jpg""  style=""  height=""100"" width=""100"" alt=""..."" >")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 23px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("MAHAKAUSHAL UNIVERSITY, JABALPUR (M.P)")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 19px""  width=""94%""  colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("AWARD OF PRACTICAL MARKS")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("" & ddlcollege.SelectedItem.ToString & " ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Examination: " & ddlexamname.SelectedItem.ToString & "  ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")




        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Course : " & ddlcourse.SelectedItem.ToString & "")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("</table>")






        strHTMLBuilder.Append("<table  width=""100%""  style="" color: #000000; font-family: Tahoma; font-size: 19px"" cellpadding=""5"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td  style=""text-align: justify""  >")
        strHTMLBuilder.Append("Practical Exam in Subject: ______________________________________________ Subject Code: ________________Date of Practical Exam: ___________________________ Maximum Marks ____________ Minimum Marks __________No of Students due to Appear ________________ No of Students actually Appeared ________________")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("<tr>")

        strHTMLBuilder.Append("<td   style=""text-align: justify""  >")
        strHTMLBuilder.Append("Certified that the maximum and minimum pass marks as shown in this sheet are as per scheme of examination of the University, I shall be responsible for any error or omission in it.")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("</table>")


        strHTMLBuilder.Append("<table width=""100%""  border=""1""  style="" color: #000000; font-family: Tahoma; font-size: 19px"" cellpadding=""5"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<thead >")
        strHTMLBuilder.Append("<tr>")

        strHTMLBuilder.Append("<th  valign=""center""    rowspan=""2""  nowrap  width=""20px"" align=""center"" >")
        strHTMLBuilder.Append("Sr.No.")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th  valign=""center""  rowspan=""2""  align=""center"" >")
        strHTMLBuilder.Append("Enrollment No")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th   valign=""center""  rowspan=""2""  align=""center"" >")
        strHTMLBuilder.Append("Candidate's Name")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th  colspan=""2"" valign=""top""  align=""center"" >")
        strHTMLBuilder.Append("MARKS ALLOTED")
        strHTMLBuilder.Append("</th>")


        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th   valign=""top""  align=""center"" >")
        strHTMLBuilder.Append("In figures")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th  valign=""top""  width=""200px"" align=""center"" >")
        strHTMLBuilder.Append("In words")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("</thead>")
        Dim x As Integer = 0
        For Each itm As GridViewRow In grdstudent.Rows
            If CType(itm.FindControl("chkselect"), CheckBox).Checked = True Then
                strHTMLBuilder.Append("<tr>")

                strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""20px"" align=""left"">")
                strHTMLBuilder.Append("" & x + 1 & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td  valign=""top"" nowrap align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("8").Text & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td   valign=""top"" nowrap  align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("9").Text & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td  valign=""top"" width=""100px""  align=""center"" >")
                strHTMLBuilder.Append("")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td  valign=""top""  width=""100px""  align=""center"" >")
                strHTMLBuilder.Append("")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("</tr>")
                x = x + 1
            End If
        Next

        strHTMLBuilder.Append("</table>")



        strHTMLBuilder.Append(" <footer style=""position: fixed; bottom: 0mm; width: 100%;"">")

        strHTMLBuilder.Append(" <div class=""page-footer-space""></div>")
        strHTMLBuilder.Append(" <div class=""page-footer"">  ")
        strHTMLBuilder.Append(" <tfoot>")

        strHTMLBuilder.Append("<table  width=""100%""  style="" color: #000000; font-family: Tahoma; font-size: 19px"" cellpadding=""5"" cellspacing=""0"" >")
        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Date")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Date")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Signature of Internal Examiner")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Signature of External  Examiner")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Full Name........................................... ")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Full Name........................................... ")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Designation ........................................")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Designation ........................................")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Address   ............................................. ")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Address   ............................................. ")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("</table>")


        strHTMLBuilder.Append(" </tfoot>")
        strHTMLBuilder.Append("</div>")
        strHTMLBuilder.Append("</div>")
        strHTMLBuilder.Append(" </footer>")




        strHTMLBuilder.Append("<p style=""page-break-after:always;""></p>")
        Literal1.Text = strHTMLBuilder.ToString()




    End Sub

    Protected Sub btnpratcialattendance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpratcialattendance.Click

        pnlstudent.Visible = False
        pnlprint.Visible = True


        strHTMLBuilder.Append("<table  align=""center""   width=""100%"" style="" color: #000000; font-family: calibri; font-size: 19px"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th  width=""6%"" rowspan=""6"" align=""center"" >")
        strHTMLBuilder.Append("<img src=""../../Photos/Branchlogo/1.jpg""  style=""  height=""100"" width=""100"" alt=""..."" >")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 23px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("MAHAKAUSHAL UNIVERSITY, JABALPUR (M.P)")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 19px""  width=""94%""  colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("ATTENDANCE SHEET - PRACTICAL EXAM")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("" & ddlcollege.SelectedItem.ToString & " ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Examination: " & ddlexamname.SelectedItem.ToString & "  ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")




        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Course : " & ddlcourse.SelectedItem.ToString & "")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("</table>")



        strHTMLBuilder.Append("<table  width=""100%""  style="" color: #000000; font-family: Tahoma; font-size: 19px"" cellpadding=""5"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td  style=""text-align: justify""  >")
        strHTMLBuilder.Append("Branch :______________________________ Semester: __________Practical Examination in Subject : ___________________________ Subject Code : ___________ Date of practical examination __________No. due to appear  ________________ No. actually appeared ________________")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("</tr>")

        'strHTMLBuilder.Append("<tr>")

        'strHTMLBuilder.Append("<td   style=""text-align: justify""  >")
        'strHTMLBuilder.Append("Certified that the maximum and minimum pass marks as shown in this sheet are as per scheme of examination of the University, I shall be responsible for any error or omission in it.")
        'strHTMLBuilder.Append("</td>")
        'strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("</table>")

        strHTMLBuilder.Append("<br>")

        strHTMLBuilder.Append("<table width=""100%""  border=""1""  style="" color: #000000; font-family: Tahoma; font-size: 19px"" cellpadding=""5"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<thead >")
        strHTMLBuilder.Append("<tr>")

        strHTMLBuilder.Append("<th  valign=""center"" nowrap  width=""20px"" align=""center"" >")
        strHTMLBuilder.Append("Sr.No.")
        strHTMLBuilder.Append("</th>")


        strHTMLBuilder.Append("<th  valign=""center""  align=""center"" >")
        strHTMLBuilder.Append("Roll No")
        strHTMLBuilder.Append("</th>")


        strHTMLBuilder.Append("<th  valign=""center""  align=""center"" >")
        strHTMLBuilder.Append("Enrollment No")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th   valign=""center"" align=""center"" >")
        strHTMLBuilder.Append("Candidate's Name")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th  valign=""top"" align=""center"" >")
        strHTMLBuilder.Append("Signature")
        strHTMLBuilder.Append("</th>")


        strHTMLBuilder.Append("</tr>")



        'strHTMLBuilder.Append("<tr>")
        'strHTMLBuilder.Append("<th   valign=""top""  align=""center"" >")
        'strHTMLBuilder.Append("In figures")
        'strHTMLBuilder.Append("</th>")

        'strHTMLBuilder.Append("<th  valign=""top""   align=""center"" >")
        'strHTMLBuilder.Append("In words")
        'strHTMLBuilder.Append("</th>")

        'strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("</thead>")
        Dim x As Integer = 0
        For Each itm As GridViewRow In grdstudent.Rows
            If CType(itm.FindControl("chkselect"), CheckBox).Checked = True Then
                strHTMLBuilder.Append("<tr>")

                strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""20px"" align=""left"">")
                strHTMLBuilder.Append("" & x + 1 & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td  valign=""top"" nowrap align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("5").Text & "")
                strHTMLBuilder.Append("</td>")


                strHTMLBuilder.Append("<td  valign=""top"" nowrap align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("8").Text & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td   valign=""top"" nowrap  align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("9").Text & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td  valign=""top"" width=""100px""  align=""center"" >")
                strHTMLBuilder.Append("")
                strHTMLBuilder.Append("</td>")

                'strHTMLBuilder.Append("<td  valign=""top""  width=""100px""  align=""center"" >")
                'strHTMLBuilder.Append("")
                'strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("</tr>")
                x = x + 1
            End If
        Next

        strHTMLBuilder.Append("</table>")
        strHTMLBuilder.Append("<br />")


        strHTMLBuilder.Append(" <footer style=""position: fixed; bottom: 0mm; width: 100%;"">")

        strHTMLBuilder.Append(" <div class=""page-footer-space""></div>")
        strHTMLBuilder.Append(" <div class=""page-footer"">  ")
        strHTMLBuilder.Append(" <tfoot>")



        strHTMLBuilder.Append("<table  width=""100%""  style="" color: #000000; font-family: Tahoma; font-size: 19px"" cellpadding=""5"" cellspacing=""0"" >")
        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Date")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Date")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Signature of Internal Examiner")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Signature of External  Examiner")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Full Name........................................... ")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Full Name........................................... ")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Designation ........................................")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Designation ........................................")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Address   ............................................. ")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Address   ............................................. ")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("</table>")

        strHTMLBuilder.Append(" </tfoot>")
        strHTMLBuilder.Append("</div>")
        strHTMLBuilder.Append("</div>")
        strHTMLBuilder.Append(" </footer>")


        strHTMLBuilder.Append("<p style=""page-break-after:always;""></p>")
        Literal1.Text = strHTMLBuilder.ToString()

    End Sub

    Protected Sub btnTheoryattendance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTheoryattendance.Click

        pnlstudent.Visible = False
        pnlprint.Visible = True


        strHTMLBuilder.Append("<table  align=""center""   width=""100%"" style="" color: #000000; font-family: calibri; font-size: 19px"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th  width=""6%"" rowspan=""6"" align=""center"" >")
        strHTMLBuilder.Append("<img src=""../../Photos/Branchlogo/1.jpg""  style=""  height=""100"" width=""100"" alt=""..."" >")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 23px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("MAHAKAUSHAL UNIVERSITY, JABALPUR (M.P)")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 19px""  width=""94%""  colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("ATTENDANCE SHEET - Theory EXAM")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("" & ddlcollege.SelectedItem.ToString & " ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Examination: " & ddlexamname.SelectedItem.ToString & "  ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")




        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Course : " & ddlcourse.SelectedItem.ToString & "")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("</table>")

        strHTMLBuilder.Append("<br>")

        strHTMLBuilder.Append("<table  width=""100%""  style="" color: #000000; font-family: Tahoma; font-size: 19px"" cellpadding=""5"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td  style=""text-align: justify""  >")
        strHTMLBuilder.Append("Branch :_______________________________ Semester: ________________Practical Examination in Subject : ___________________________ Subject Code : ___________ Date of practical examination __________No. due to appear  ________________ No. actually appeared ________________")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("</tr>")

        'strHTMLBuilder.Append("<tr>")

        'strHTMLBuilder.Append("<td   style=""text-align: justify""  >")
        'strHTMLBuilder.Append("Certified that the maximum and minimum pass marks as shown in this sheet are as per scheme of examination of the University, I shall be responsible for any error or omission in it.")
        'strHTMLBuilder.Append("</td>")
        'strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("</table>")


        strHTMLBuilder.Append("<table width=""100%""  border=""1""  style="" color: #000000; font-family: Tahoma; font-size: 19px"" cellpadding=""5"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<thead >")
        strHTMLBuilder.Append("<tr>")

        strHTMLBuilder.Append("<th  valign=""center"" nowrap  width=""20px"" align=""center"" >")
        strHTMLBuilder.Append("Sr.No.")
        strHTMLBuilder.Append("</th>")



        strHTMLBuilder.Append("<th  valign=""center""  align=""center"" >")
        strHTMLBuilder.Append("Roll No")
        strHTMLBuilder.Append("</th>")


        strHTMLBuilder.Append("<th  valign=""center""  align=""center"" >")
        strHTMLBuilder.Append("Enrollment No")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th   valign=""center"" align=""center"" >")
        strHTMLBuilder.Append("Candidate's Name")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th  valign=""top"" align=""center"" >")
        strHTMLBuilder.Append("Signature")
        strHTMLBuilder.Append("</th>")


        strHTMLBuilder.Append("</tr>")



        'strHTMLBuilder.Append("<tr>")
        'strHTMLBuilder.Append("<th   valign=""top""  align=""center"" >")
        'strHTMLBuilder.Append("In figures")
        'strHTMLBuilder.Append("</th>")

        'strHTMLBuilder.Append("<th  valign=""top""   align=""center"" >")
        'strHTMLBuilder.Append("In words")
        'strHTMLBuilder.Append("</th>")

        'strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("</thead>")
        Dim x As Integer = 0
        For Each itm As GridViewRow In grdstudent.Rows
            If CType(itm.FindControl("chkselect"), CheckBox).Checked = True Then
                strHTMLBuilder.Append("<tr>")

                strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""20px"" align=""left"">")
                strHTMLBuilder.Append("" & x + 1 & "")
                strHTMLBuilder.Append("</td>")


                strHTMLBuilder.Append("<td  valign=""top"" nowrap align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("5").Text & "")
                strHTMLBuilder.Append("</td>")


                strHTMLBuilder.Append("<td  valign=""top"" nowrap align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("8").Text & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td   valign=""top"" nowrap  align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("9").Text & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td  valign=""top"" width=""100px""  align=""center"" >")
                strHTMLBuilder.Append("")
                strHTMLBuilder.Append("</td>")

                'strHTMLBuilder.Append("<td  valign=""top""  width=""100px""  align=""center"" >")
                'strHTMLBuilder.Append("")
                'strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("</tr>")
                x = x + 1
            End If
        Next

        strHTMLBuilder.Append("</table>")




        strHTMLBuilder.Append(" <footer style=""position: fixed; bottom: 0mm; width: 100%;"">")

        strHTMLBuilder.Append(" <div class=""page-footer-space""></div>")
        strHTMLBuilder.Append(" <div class=""page-footer"">  ")
        strHTMLBuilder.Append(" <tfoot>")



        strHTMLBuilder.Append("<table  width=""100%""  style="" color: #000000; font-family: Tahoma; font-size: 19px"" cellpadding=""5"" cellspacing=""0"" >")
        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Date")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Date")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Signature of Internal Examiner")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Signature of External  Examiner")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Full Name........................................... ")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Full Name........................................... ")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Designation ........................................")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Designation ........................................")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Address   ............................................. ")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td width=""50%""  style=""text-align: Left""  >")
        strHTMLBuilder.Append("Address   ............................................. ")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("</table>")


        strHTMLBuilder.Append(" </tfoot>")
        strHTMLBuilder.Append("</div>")
        strHTMLBuilder.Append("</div>")
        strHTMLBuilder.Append(" </footer>")




        strHTMLBuilder.Append("<p style=""page-break-after:always;""></p>")
        Literal1.Text = strHTMLBuilder.ToString()



    End Sub


    Sub printawand(ByVal id As String)
        crystalReport.Close()
        crystalReport.Clone()
        crystalReport.Dispose()
        crystalReport = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()



        Dim w As String = ""
        If Request.QueryString("r") = "27" Or Request.QueryString("r") = "38" Then
            w = w + " where s.UserID=" & Request.QueryString("u") & "  "
        Else
            w = w + " where s.UserID<>1 "
        End If

        If ddlcollege.SelectedIndex > 0 Then
            w = w + " and c.Cid =" & ddlcollege.SelectedValue.ToString & " "
        End If

        If ddlcoursetype.SelectedIndex > 0 Then
            w = w + " and c.Branch ='" & ddlcoursetype.SelectedValue.ToString & "' "
        End If

        If ddlcourse.SelectedIndex > 0 Then
            w = w + " and c.CourseID ='" & ddlcourse.SelectedValue.ToString & "' "
        End If

        If ddlsem.SelectedIndex > 0 Then
            w = w + " and sy.sem ='" & ddlsem.SelectedValue.ToString & "' "
        End If

        'w = w + " and  s.IsStruckOff=0 and  s.RegistrationApproved=1 and sy.Courseid in (select courseid  from ExamCourse where examid =" & ddlexamname.SelectedValue.ToString & " )"

        w = w + " and  s.IsStruckOff=0 and  sy.Courseid in (select courseid  from ExamCourse where examid =" & ddlexamname.SelectedValue.ToString & " )"

        w = w + " and   ISNULL( ddd.status ,0) in (1 ,2)  "


        sql = "select 'Examination: " & ddlexamname.SelectedItem.ToString & "' as ExamName , u.usernamefull as entryby, s.StudentID,  i.Code as Faculty, c.Code as Course ,s.Sem , case when 'pratcialattendancenew'='" & id & "' then 'PRACTICAL EXAM' else 'THEORY EXAM' end as  Batch ,s.AdmissionNo  ,  " & _
                " s.student,s.FatherName ,s.EnrollmentNo , case when s.studenttype='L' then 'Lateral' else 'Regular' end as Studenttype , " & _
                " case when  ISNULL( ddd.status ,0) = 0 then 'Not Apply' when  ISNULL( ddd.status ,1) = 1 then 'Exam From Pending' when  ISNULL( ddd.status ,2) = 2 then 'Exam From Approved' end  as Status  , convert(varchar,ee.Rollnoid) as Rollnoid " & _
                " from student s  left join Course  c on c.CourseID =s.CourseID   left join Batch b on b.BatchID=s.BatchID   left join Institue i on i.InstitueID=c.Cid  " & _
                    " left join Users  u on u.userid=s.UserID   inner join StudentYear sy on sy.StudentID=s.StudentID and sy.ayid=" & Request.QueryString("ay") & "    left join ExamRollno ee on ee.Examid = " & ddlexamname.SelectedValue.ToString & " and ee.Studentid=s.StudentID  " & _
                " left join ( select distinct  Studentid , status from ( select  studentid,1 as  status from studentsubjectregistration where status=0 and Ayid=  " & Request.QueryString("ay") & "  " & _
                    " union all  select studentid , 2 as status from studentsubjectregistration where status=1  and Ayid=" & Request.QueryString("ay") & "   ) as dd ) as ddd on ddd.Studentid=s.StudentID   " & w & " and s.sessionid=" & Request.QueryString("s") & "  and ISNULL( ee.Rollnoid ,0)>0  order by ee.Rollnoid    "



        'Dim CMD As New dbnew()
        Dim A As String = id


        'Dim ss() As SqlParameter = {New SqlParameter("@subid", id)}
        Dim imageDataSet As New DataSet1()
        Dim ds As DataTable = imageDataSet.Tables("raba")
        ds = cmd.getDataTable(sql)

        Dim prm As New CrystalDecisions.Web.Parameter
        prm.Name = "Firm"

        crystalReport = New ReportDocument

        ' crystalReport.SetParameterValue(prm.Name, "Geeta")

        If A = "pratcialattendancenew" Then
            crystalReport.Load(Server.MapPath("../rpt/mkupratcialattendance.rpt"))
        ElseIf A = "mkupratcialattendancefoil" Then
            crystalReport.Load(Server.MapPath("../rpt/mkupratcialattendancefoil.rpt"))
        Else
            crystalReport.Load(Server.MapPath("../rpt/mkupratcialattendance.rpt"))
        End If



        ' Dim id As String = Guid.NewGuid().ToString("N")
        crystalReport.SetDataSource(ds)
        crystalReport.Refresh()

        '  Dim url As String = "../../Reports/reportprint.aspx"
        Session("a") = Nothing
        Session("a") = crystalReport

        crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "AttendanceSheet")
        'Response.Write("<script type=text/javascript> window.top.location.href ='../reportprint.aspx' </script>")
        Response.End()

    End Sub

    Protected Sub btnpratcialattendancenew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpratcialattendancenew.Click

        printawand("pratcialattendancenew")

    End Sub

    Protected Sub btnfailnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfailnew.Click
        printawand("mkupratcialattendancefoil")
    End Sub

    Protected Sub btnTheoryattendancenew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTheoryattendancenew.Click
        printawand("pratcialattendancenewtheory")




    End Sub
End Class
