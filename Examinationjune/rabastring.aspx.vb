'Design And Developed By Avaneesh Yadav

Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Partial Class Examinationjune_rabastring
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
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            ViewState("courseexamid") = Request.QueryString("courseexamid")
            ViewState("courseid") = Request.QueryString("rid")
            ViewState("Academicyear") = Request.QueryString("acyr")
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("ayid") = Request.QueryString("ay")
            labeldata()

            BindCourseExamdropdown()
            ddlExamN.Items.FindByValue(ViewState("courseexamid")).Selected = True


            fetchddlProgram()
            ddlAcademicyear.Items.FindByValue(ViewState("courseid")).Selected = True

            Me.BindGrid()
            ViewState("StudentID") = Request.QueryString("StudentID")
        End If
    End Sub

    
    Private Sub BindCourseExamdropdown()
        Dim query As String = ("Select * from Exam_CourseExam where Sessionid='" & ViewState("Sessionid") & "' and Ayid='" & ViewState("ayid") & "'")
        BindDropDownList1(ddlExamN, query, "ExamName", "CourseExamid", "Select Exam Name")

    End Sub
    Private Sub BindDropDownList1(ByVal ddl1 As DropDownList, ByVal query As String, ByVal text As String, ByVal value As String, ByVal defaultText As String)

        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(constr)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                con.Open()
                ddl1.DataSource = cmd.ExecuteReader()
                ddl1.DataTextField = text
                ddl1.DataValueField = value
                ddl1.DataBind()
                con.Close()
            End Using
        End Using
        ddl1.Items.Insert(0, New ListItem(defaultText, "0"))
    End Sub

    Private Sub fetchddlProgram()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select C.Course, Ce.* from Exam_CourseExam Ce join Exam_Course C on Ce.Courseid=C.Courseid where Ce.CourseExamid='" & ddlExamN.SelectedValue & "' and Ce.Ayid='" & ViewState("ayid") & "' and Ce.SessionId='" & ViewState("Sessionid") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlAcademicyear.DataSource = dt
                        ddlAcademicyear.DataTextField = "Course"
                        ddlAcademicyear.DataValueField = "Courseid"
                        ddlAcademicyear.DataBind()


                    End Using
                End Using
            End Using
        End Using
        ddlAcademicyear.Items.Insert(0, New ListItem("Select Program", "0"))
    End Sub


    Protected Sub ddlExamN_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExamN.SelectedIndexChanged
        fetchddlProgram()
    End Sub

    Private Sub labeldata()
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand("select courseid,Coursecode from Exam_Course where course='" & Request.QueryString("rid") & "'", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                ViewState("Courseid") = Session("Courseid")
                ViewState("Coursecode") = ds.Tables(0).Rows(0)("Coursecode").ToString()
            End If
            con.Close()

        End Using

    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        ViewState("Courseid") = Session("Courseid")
        'Session("Otherid") = row.Cells(2).Text
        Session("ExamName") = Session("Examname")
        Session("Course") = Session("Course")
        Session("Semyear") = Session("Semyear")
        Response.Redirect("CreatedExams.aspx?rid=" & Session("Courseid") & "&acyr=" & ViewState("Academicyear") & "&u=" & Request.QueryString("u") & "&s=" & Request.QueryString("s") & "&ay=" & ViewState("ayid"))
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

    Private Sub BindGrid()

        If ddlAcademicyear.SelectedValue = "0" Then
        Else
            Using con As New SqlConnection(constr)
                'Using cmd As New SqlCommand("select s.StudentId,s.AdmissionNo, s.Student,Convert(nvarchar, s.DOB, 103) as DOB ,ce.Course, s.Roll_no,s.EnrollmentNo,s.FatherName,s.Mobile from Student s left join Exam_Course ce on ce.CourseID=s.CourseID Left Join Exam_CourseExam ece on ce.Courseid = ece.Courseid where ece.ExamName = '" & Session("ExamName") & "' and ece.Courseid='" & Session("Courseid") & "'and ece.Semyear='" & Session("Semyear") & "'", con)
                Using cmd As New SqlCommand("Select C.Course, Sy.StudentID, St.AdmissionNo, St.Student, St.EnrollmentNo, St.Roll_No,St.FatherName, St.Gender , " & _
        " St.Email,St.Mobile ,CONVERT(varchar,St.DOB,107) as Dateofbirth,ce.* from Exam_CourseExam ce join StudentYear sy on ce.Courseid=sy.Courseid " & _
        " and ce.Sessionid=Sy.SessionID and ce.Ayid=Sy.ayid and ce.Semyear=Sy.SEm " & _
        " join Student st on sy.StudentID=st.StudentID join Exam_Course C on Ce.Courseid=C.Courseid " & _
        " where Ce.CourseExamid='" & ddlExamN.SelectedValue & "' and Ce.Sessionid='" & ViewState("Sessionid") & "' and Ce.ayid='" & ViewState("ayid") & "' and St.IsStruckOff='0' and  " & _
   " Sy.StudentID in (Select Studentid from Exam_Examformstudent where Courseexamid='" & ddlExamN.SelectedValue & "' and Isexamformverified='1') ", con)
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            grdstudent.DataSource = dt
                            grdstudent.DataBind()
                        End Using
                    End Using
                End Using
            End Using
        End If
        
    End Sub

    Protected Sub btnraba_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnraba.Click
        pnlmain.Visible = False

        pnlprint.Visible = True

        Dim sem As String = ""
        Dim studentid As String = ""

        For Each itm As GridViewRow In grdstudent.Rows
            If CType(itm.FindControl("chkselect"), CheckBox).Checked = True Then
                studentid = itm.Cells(5).Text

                sem = itm.Cells(10).Text
                '  printnewdmc(grdsubject.DataKeys(itm.RowIndex).Values(0))
                printnewdmc(studentid, sem)
            End If
        Next
    End Sub

    Sub printnewdmc(ByVal sid As String, ByVal sem As String)

        Dim aa As SqlParameter() = {New SqlParameter("@q", "Rawa"), New SqlParameter("@Courseexamid", ddlExamN.SelectedValue), New SqlParameter("@courseid", ddlAcademicyear.SelectedValue.ToString), New SqlParameter("@sem", sem), New SqlParameter("@sid", sid)}
        Dim dt As DataTable = cmd.getDataTable("[dbo].[ReportSheet]", aa, "dt")

        If dt.Rows.Count > 0 Then
        Else
            SaralMsg.Messagebx.Alert(Me, "no records found")
            Exit Sub
        End If

        strHTMLBuilder.Append("<table  align=""center""   width=""100%"" style="" color: #000000; font-family: calibri; font-size: 18px"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th  width=""6%"" rowspan=""6"" align=""center"" >")
        strHTMLBuilder.Append("<img src=""../../Photos/Branchlogo/1.jpg""  style=""  height=""100"" width=""100"" alt=""..."" >")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 22px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("Institue").ToString & " ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 16px""  width=""94%""  colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("RABA SHEET")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        'strHTMLBuilder.Append("<tr>")
        'strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        'strHTMLBuilder.Append("Tentative Examination Time-Table")
        'strHTMLBuilder.Append("</th>")
        'strHTMLBuilder.Append("</tr>")




        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 14px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("Course").ToString & " ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 14px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("Course").ToString & "  Examination " & dt.Rows(0)("ExamName").ToString & " ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 14px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Examination " & dt.Rows(0)("ExamName").ToString & "  ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")




        strHTMLBuilder.Append("</table>")





        strHTMLBuilder.Append("<table  width=""100%""  style="" color: #000000; font-family: Tahoma; font-size: 16px"" cellpadding=""5"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<tD    width=""120px"" align=""left"" >")
        strHTMLBuilder.Append("ROLL NO :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td  width=""200px"" align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("Roll_no").ToString & "")
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
        strHTMLBuilder.Append("" & dt.Rows(0)("sem").ToString & "")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("</tr>")
        strHTMLBuilder.Append("</table>")





        strHTMLBuilder.Append("<table width=""100%"" class=""table table-bordered mt-3"" style="" color: #000000; font-family: Tahoma; font-size: 16px"" >")

        strHTMLBuilder.Append("<thead >")
        strHTMLBuilder.Append("<tr>")

        strHTMLBuilder.Append("<th  valign=""top"" class=""hiddencol""  nowrap  align=""center"" >")
        strHTMLBuilder.Append("Sub id")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th   valign=""top"" align=""left"" >")
        strHTMLBuilder.Append("Subject code ")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th  valign=""top""   align=""left"" >")
        strHTMLBuilder.Append("Subject Name")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th   valign=""top"" align=""left"" >")
        strHTMLBuilder.Append("Exam Date")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th   valign=""top"" nowrap align=""center"" >")
        strHTMLBuilder.Append("Answer Book No")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th  valign=""top""   align=""center"" >")
        strHTMLBuilder.Append("Supply Book No")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th   valign=""top""  align=""center"" >")
        strHTMLBuilder.Append("Student Sign")
        strHTMLBuilder.Append("</th>")



        strHTMLBuilder.Append("<th  valign=""top""   align=""center"" >")
        strHTMLBuilder.Append("Invigilator Sign")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("</thead>")

        Dim xx As Integer = 0
        For Each myRow As DataRow In dt.Rows

            strHTMLBuilder.Append("<tr>")

            strHTMLBuilder.Append("<td  valign=""top"" class=""hiddencol"" nowrap align=""left"">")
            strHTMLBuilder.Append("" + myRow("Subjectid").ToString() + "")
            strHTMLBuilder.Append("</td>")

            strHTMLBuilder.Append("<td  valign=""top""  align=""left"" >")
            strHTMLBuilder.Append("" + myRow("Subjectcode").ToString() + "")
            strHTMLBuilder.Append("</td>")

            strHTMLBuilder.Append("<td   valign=""top""  align=""left"" >")
            strHTMLBuilder.Append("" + myRow("Subject").ToString() + "")
            strHTMLBuilder.Append("</td>")

            strHTMLBuilder.Append("<td   valign=""top""  align=""left"" >")
            strHTMLBuilder.Append("" + myRow("ExamDate").ToString() + "")
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

    Protected Sub btnattendence_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnattendence.Click
        fetchprogramdetail()
        pnlmain.Visible = False
        pnlprint.Visible = True


        strHTMLBuilder.Append("<table  align=""center""  style="" color: #000000; font-family: calibri; font-size: 18px"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th  width=""6%"" rowspan=""6"" align=""center"" >")
        strHTMLBuilder.Append("<img src=""../../Photos/Branchlogo/1.jpg""  style=""  height=""100px"" width=""100px"" alt=""..."" >")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 23px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append(Session("CollegeName"))
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 19px""  width=""94%""  colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("ATTENDANCE SHEET - Theory EXAM")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("" & Session("Examname") & " ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Examination: " & Session("Examname") & "")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")




        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Program : " & Session("programname") & "")
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


        strHTMLBuilder.Append("<table class=""table table-bordered"" style="" color: #000000; font-family: Tahoma; font-size: 18px"" cellpadding=""5"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<thead >")
        strHTMLBuilder.Append("<tr>")

        strHTMLBuilder.Append("<th  valign=""center"" nowrap align=""center"" >")
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
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("16").Text & "")
                strHTMLBuilder.Append("</td>")


                strHTMLBuilder.Append("<td  valign=""top"" nowrap align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("7").Text & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td   valign=""top"" nowrap  align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("6").Text & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td  valign=""top""  align=""center"" >")
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

    Private Sub fetchprogramdetail()
        Try

            Dim ds As New DataSet
            Using con As New SqlConnection(constr)
                con.Open()
                Dim cmd1 As New SqlCommand(" Select I.Institue,C.Cid, C.Course, Ce.* from Exam_CourseExam Ce join Exam_Course C on Ce.Courseid=C.Courseid " & _
" join Institue I on C.Cid=I.InstitueID " & _
" where Ce.Sessionid='" & ViewState("Sessionid") & "' and Ce.Ayid='" & ViewState("ayid") & "' and Ce.CourseExamid='" & ddlExamN.SelectedValue & "' and Ce.Courseid='" & ddlAcademicyear.SelectedValue & "' ", con)
                Dim da As New SqlDataAdapter(cmd1)
                cmd1.Connection = con
                da.Fill(ds)
                Dim i = ds.Tables(0).Rows.Count()
                If (i > 0) Then
                    Session("CollegeName") = ds.Tables(0).Rows(0)("Institue").ToString()
                    Session("programname") = ds.Tables(0).Rows(0)("Course").ToString()
                    Session("Examname") = ds.Tables(0).Rows(0)("ExamName").ToString()
                    Session("Sem1") = ds.Tables(0).Rows(0)("Semyear").ToString()

                End If
                con.Close()

            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnfoil_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfoil.Click
        fetchprogramdetail()
        pnlmain.Visible = False
        pnlprint.Visible = True



        strHTMLBuilder.Append("<table  align=""center""   width=""100%"" style="" color: #000000; font-family: calibri; font-size: 18px"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th  width=""6%"" rowspan=""6"" align=""center"" >")
        strHTMLBuilder.Append("<img src=""../../Photos/Branchlogo/1.jpg""  style=""  height=""100"" width=""100"" alt=""..."" >")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 23px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append(Session("CollegeName"))
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 19px""  width=""94%""  colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("AWARD OF PRACTICAL MARKS")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")



        'strHTMLBuilder.Append("<tr>")
        'strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        'strHTMLBuilder.Append("" & Session("") & " ")
        'strHTMLBuilder.Append("</th>")
        'strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Examination: " & Session("ExamName") & "  ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")




        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Program  : " & Session("programname") & "")
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


        strHTMLBuilder.Append("<table width=""100%"" class=""table table-bordered""  style="" color: #000000; font-family: Tahoma; font-size: 18px"" >")

        strHTMLBuilder.Append("<thead >")
        strHTMLBuilder.Append("<tr>")

        strHTMLBuilder.Append("<th  valign=""center""    rowspan=""2""  nowrap  align=""center"" >")
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

        strHTMLBuilder.Append("<th  valign=""top"" align=""center"" >")
        strHTMLBuilder.Append("In words")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("</thead>")
        Dim x As Integer = 0
        For Each itm As GridViewRow In grdstudent.Rows
            If CType(itm.FindControl("chkselect"), CheckBox).Checked = True Then
                strHTMLBuilder.Append("<tr>")

                strHTMLBuilder.Append("<td  valign=""top""  nowrap align=""left"">")
                strHTMLBuilder.Append("" & x + 1 & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td  valign=""top"" nowrap align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("7").Text & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td   valign=""top"" nowrap  align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("6").Text & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td  valign=""top"" align=""center"" >")
                strHTMLBuilder.Append("")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td  valign=""top"" align=""center"" >")
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

    Protected Sub btn_back_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back_print.Click
        pnlmain.Visible = True
        pnlprint.Visible = False
    End Sub

    Protected Sub bntdocprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bntdocprint.Click
        Session("ctrl") = pnl1
        ClientScript.RegisterStartupScript(Me.GetType(), "onclick", "<script language=javascript>window.open('print.aspx','PrintMe','height=800px,width=800px,scrollbars=1');</script>")
    End Sub

    Protected Sub btntheory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btntheory.Click
        fetchprogramdetail()
        pnlmain.Visible = False
        pnlprint.Visible = True



        strHTMLBuilder.Append("<table  align=""center""   width=""100%"" style="" color: #000000; font-family: calibri; font-size: 19px"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th  width=""6%"" rowspan=""6"" align=""center"" >")
        strHTMLBuilder.Append("<img src=""../../Photos/Branchlogo/1.jpg""  style=""  height=""100"" width=""100"" alt=""..."" >")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 23px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append(Session("CollegeName"))
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 19px""  width=""94%""  colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("ATTENDANCE SHEET - PRACTICAL EXAM")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")



        'strHTMLBuilder.Append("<tr>")
        'strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        'strHTMLBuilder.Append("" & Session("") & " ")
        'strHTMLBuilder.Append("</th>")
        'strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Examination: " & Session("ExamName") & "  ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")




        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""94%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("Program : " & Session("programname") & "")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("</table>")



        strHTMLBuilder.Append("<table  width=""100%""  style="" color: #000000; font-family: Tahoma; font-size: 18px"" cellpadding=""5"" cellspacing=""0"" >")

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

        strHTMLBuilder.Append("<table width=""100%""  border=""1"" class=""table table-bordered ""  style="" color: #000000; font-family: Tahoma; font-size: 18px"" cellpadding=""5"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<thead >")
        strHTMLBuilder.Append("<tr>")

        strHTMLBuilder.Append("<th  valign=""center"" nowrap  width=""18px"" align=""center"" >")
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
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("16").Text & "")
                strHTMLBuilder.Append("</td>")


                strHTMLBuilder.Append("<td  valign=""top"" nowrap align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("7").Text & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td   valign=""top"" nowrap  align=""left"" >")
                strHTMLBuilder.Append("" & grdstudent.Rows(itm.RowIndex).Cells("6").Text & "")
                strHTMLBuilder.Append("</td>")

                strHTMLBuilder.Append("<td  valign=""top""   align=""center"" >")
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

   
End Class
