Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Net.Mail

Partial Class StudentList
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Ac_year") = Request.QueryString("acyr")
            ViewState("courseid") = Request.QueryString("rid")

            fetchddlacademicyear()
            fetchDdlprogram()
            Ddlsemyear.Items.Clear()
            fillddlsemyear()
            Bindgridstudentlist()
        End If
    End Sub

    Private Sub fetchddlacademicyear()
        
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select * from Exam_Session order by Academicyear  desc")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlacademicyear.DataSource = dt
                        ddlacademicyear.DataTextField = "Academicyear"
                        ddlacademicyear.DataValueField = "Academicyear"
                        ddlacademicyear.DataBind()
                        Dim Year As Integer
                        Year = Convert.ToInt32(Now.ToString("yyyy"))

                        ddlacademicyear.Items.FindByValue(Year).Selected = True

                    End Using
                End Using
            End Using
        End Using

        'Catch ex As Exception


        'End Try


    End Sub

    Private Sub fetchDdlprogram()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Cs.Academicyear, Cs.Courseid, C.Course from Exam_CourseSession Cs join Exam_Course C on Cs.Courseid =C.Courseid " & _
" where Cs.Academicyear ='" & ViewState("Ac_year") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlprogram.DataSource = dt
                        Ddlprogram.DataTextField = "Course"
                        Ddlprogram.DataValueField = "Courseid"
                        Ddlprogram.DataBind()
                        'Dim Year As Integer
                        'Year = Convert.ToInt32(Now.ToString("yyyy"))

                        Ddlprogram.Items.FindByValue(ViewState("courseid")).Selected = True

                    End Using
                End Using
            End Using
        End Using

    End Sub

    Private Sub fillddlsemyear()

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Coursesessionid, Academicyear, Courseid, CourseType, case when Coursetype Like '%sem%' then Duration*2 " & _
" when Coursetype like '%year%' then Duration*1 when Coursetype like '%quart%' then Duration*4 end as 'Totalsem', " & _
 " Duration, Coursetype from Exam_CourseSession where  Academicyear ='" & ViewState("Ac_year") & "' and Courseid = '" & Ddlprogram.SelectedValue & "' ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlsemyear.DataSource = dt
                        Dim totalsem As String = dt.Rows(0)("Totalsem").ToString()
                        Lblsemyear.Text = dt.Rows(0)("CourseType").ToString()
                        Dim i As Integer
                        For i = 1 To totalsem
                            Ddlsemyear.Items.Add(New ListItem(i.ToString(), i.ToString()))

                        Next

                        Ddlsemyear.Items.Insert(0, New ListItem("All", ""))
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub Bindgridstudentlist()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = " Select S.* from Student S Where S.Academicyear ='" & ViewState("Ac_year") & "' and S.CourseID ='" & Ddlprogram.SelectedValue & "' "
                'If Not String.IsNullOrEmpty(txtsearch.Text.Trim()) Then
                '    query += " and Sub.Subject LIKE '%' + @Subject + '%' "
                '    cmd.Parameters.AddWithValue("@Subject", txtsearch.Text.Trim())
                'End If
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        gridstudentlist.DataSource = dt
                        gridstudentlist.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Private Sub Bindgridstudentlistsemwise()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = " Select S.* from Student S Where S.Academicyear ='" & ViewState("Ac_year") & "' and S.CourseID ='" & Ddlprogram.SelectedValue & "' and S.Sem ='" & Ddlsemyear.SelectedItem.Text & "'"
                'If Not String.IsNullOrEmpty(txtsearch.Text.Trim()) Then
                '    query += " and Sub.Subject LIKE '%' + @Subject + '%' "
                '    cmd.Parameters.AddWithValue("@Subject", txtsearch.Text.Trim())
                'End If
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        gridstudentlist.DataSource = dt
                        gridstudentlist.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub Ddlprogram_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlprogram.SelectedIndexChanged

        Ddlsemyear.Items.Clear()
        fillddlsemyear()
        Bindgridstudentlist()
    End Sub

    Protected Sub OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        pnlTextBox.Visible = False
        If DdlTemp.SelectedItem.Value = "1" Then
            pnlTextBox.Visible = True
        End If
    End Sub


    Private Sub fetchemail()
        Using con As New SqlConnection(constr)
            Dim condition As String = String.Empty


            For Each row As GridViewRow In gridstudentlist.Rows
                If (TryCast(row.FindControl("btnselect"), CheckBox)).Checked Then
                    condition += If(String.Format("'{0}',", row.Cells(3).Text), String.Empty)

                End If

            Next

            If Not String.IsNullOrEmpty(condition) Then
                condition = String.Format("SELECT Email FROM Student WHERE StudentID in ({0})", condition.Substring(0, condition.Length - 1))
            End If

            Using cmd As New SqlCommand(condition)
                Using sda As New SqlDataAdapter(cmd)
                    cmd.Connection = con

                    Using dt As New DataTable()
                        sda.Fill(dt)
                        DdlSendTo.DataTextField = "Email"

                        DdlSendTo.DataSource = dt
                        DdlSendTo.DataBind()
                        DdlSendTo.Items.Insert(0, New ListItem("See Your Selected Emails", ""))
                        DdlSendTo.Items(0).Selected = True
                        DdlSendTo.Items(0).Attributes.Add("disabled", "true")
                        For i = 1 To DdlSendTo.Items.Count - 1
                            DdlSendTo.Items(i).Attributes.Add("disabled", "true")
                            Dim h As String = String.Empty
                            For Each row As GridViewRow In gridstudentlist.Rows
                                If (TryCast(row.FindControl("btnselect"), CheckBox)).Checked Then
                                    If h = "" Then
                                        h = If(String.Format("{0}", row.Cells(8).Text), String.Empty)
                                    Else
                                        h += If(String.Format(",{0}", row.Cells(8).Text), String.Empty)
                                    End If


                                End If
                                Session("Emails") = h

                            Next

                        Next

                    End Using
                End Using
            End Using



        End Using
    End Sub
    Private Sub sendEmail()
        Try
            Dim smtp As SmtpClient = New SmtpClient()
            smtp.Host = "smtp.gmail.com"
            smtp.Port = 587
            smtp.Credentials = New System.Net.NetworkCredential("mk214642@gmail.com", "wwgcfkigjakmlbze")
            smtp.EnableSsl = True
            Dim msg As MailMessage = New MailMessage()
            msg.Subject = "Regarding Diwali Holiday"

            'msg.Body = "Your code is" + code + ""


            Dim Body As String = "<div class=""container"" style=""margin-left:50px;"">"
            Body += "<div class=""logo"">"
            'Body += "<img class=""logo"" src=""http://localhost:49429/Email Temp/img/logo-ig-png-32464.png"" align=""middle"" style=""Height:50px;width:300px;""/></div>"
            Body += "<br/>"
            Body += "<div class=""card p-6 p-lg-10 space-y-4"">"
            Body += " <h3 class=""h3 fw-700"">"
            Body += "Dear Student "",</h3>"
            'Body += "Dear " + TextBox2.Text + ",</h3>"

            Body += "<p>"
            Body += "" + txtNote.Text + "</b>"
            Body += "" + txteditor2.Text + "</b>"
            'Body += "<b>Note: Your OTP is valid for 10 minute from the time of generation.<br/>"
            'Body += "Kindly do not share your OTP, as it is confidential.</b><br/><br/>"
            Body += "In case of any technical diffculty drop us a mail on <a href=""#"" style=""Text-decoration:underline""> saralerppvtltd123@gmail.com</a><br/><br/>"
            Body += "</p>"
            Body += "<br/>"
            Body += "Regards,<br/>"
            Body += "Team Saral ERP<br/>"
            Body += "<div class=""SocialMedia"">"
            Body += "<div class=""fb""><a href=""#"">"
            Body += "<img src=""http://localhost:49429/Email Temp/img/facebook-logo-480.jpg"" style=""height:20px;width:20px; margin-top:10px; margin-left:10px; margin-bottom:10px; float:left; ""/></a></div>"
            Body += "<div class=""tweeter"">"
            Body += "  <a href=""#""> <img src=""http://localhost:49429/Email Temp/img/logo-twitter-png-5860.png"" style=""height:20px;width:20px; margin-top:10px; margin-left:10px; margin-bottom:10px;float:left""/></a></div>"
            Body += "<div class=""insta"">"
            Body += "<a href=""#""><img src=""http://localhost:49429/Email Temp/img/logo-ig-png-32464.png"" style=""height:20px;width:20px; margin-top:10px;margin-bottom:10px; margin-left:10px;"" /></a></div>"
            Body += "</div>"
            Body += "</div>"
            Body += "<div class=""footer"">"
            Body += "AB-1, Kamal Cinema Complex,<br/>"
            Body += "Safdarjung Enclave,<br/>"
            Body += "New Delhi - 110 029, India<br/>"
            Body += "Mobile : +91 - 9654410748"
            Body += "</div>"
            Body += "</div>"
            msg.Body = Body

            msg.IsBodyHtml = True


            Dim toaddress As String = Session("Emails")
            msg.To.Add(toaddress)
            Dim fromaddress As String = "SaralERP<mk2146422@gmail.com>"
            msg.From = New MailAddress(fromaddress)
            Try
                smtp.Send(msg)

            Catch
                Throw
            End Try
            'a = code.d.SendEmailrj_placement("", txtemail.Text, "OTP by MKU ", "Dear Studnet, OTP to login to Mahakaushal University is " & code & ". Do not share with anyone")
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("MISdashboard.aspx")
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        For Each row As GridViewRow In gridstudentlist.Rows
            If (TryCast(row.FindControl("btnselect"), CheckBox)).Checked Then
                Dim Studentid As String = row.Cells(3).Text
                Dim Email As String = row.Cells(8).Text
                Dim Empid As String = 1197
                Me.InsertEmailmsg(Studentid, Email, Empid)
            End If
        Next

    End Sub

    Private Sub InsertEmailmsg(ByVal Studentid As String, ByVal Email As String, ByVal Empid As String)
        Using con As SqlConnection = New SqlConnection(constr)
            Using cmd As SqlCommand = New SqlCommand("proc_EmailCommunication")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@studentid", Studentid)
                cmd.Parameters.AddWithValue("@studentemail", Email)
                cmd.Parameters.AddWithValue("@empid", Empid)
                cmd.Parameters.AddWithValue("@empmail", ddlEmailSelect.SelectedItem.Text)
                cmd.Parameters.AddWithValue("@EmailType", DdlEmailType.SelectedItem.Text)
                cmd.Parameters.AddWithValue("@TemplateMsg", txteditor2.Text)
                cmd.Parameters.AddWithValue("@Note", txtNote.Text)
                cmd.Parameters.AddWithValue("@Status", "0")
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

            End Using
        End Using

        Me.sendEmail()

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        pnlTextBox.Visible = True
        pnlTextBox.Visible = False
    End Sub


    Private Sub CountEmail()
        Dim Count As Integer = 0
        Dim Count1 As Integer = 0
        For Each row As GridViewRow In gridstudentlist.Rows
            If (TryCast(row.FindControl("btnselect"), CheckBox)).Checked Then
                Count += 1

            End If

        Next
        
        Lblcountstndt.Text = Count

    End Sub
    Protected Sub lnkbtnemail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnemail.Click

        fetchemail()
        CountEmail()

    End Sub



    Protected Sub gridstudentlist_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridstudentlist.RowCommand
        If e.CommandName = "Edit" Then
            gridstudentlist.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gridstudentlist.Rows(rowIndex)
            ViewState("studentid") = row.Cells(3).Text
            ViewState("programname") = Ddlprogram.SelectedItem.Text
            Response.Redirect("Student.aspx?stuid=" & ViewState("studentid") & "&program=" & Ddlprogram.SelectedItem.Text & "&programid=" & Ddlprogram.SelectedValue & "&acyr=" & ViewState("Ac_year"))
        End If

        If e.CommandName = "Studentprofile" Then
            gridstudentlist.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gridstudentlist.Rows(rowIndex)
            ViewState("studentid") = row.Cells(3).Text
            ViewState("programname") = Ddlprogram.SelectedItem.Text
            Response.Redirect("Studentprofile.aspx?stuid=" & ViewState("studentid") & "&programid=" & Ddlprogram.SelectedValue & "&acyr=" & ViewState("Ac_year"))
        End If

    End Sub

    Protected Sub Ddlsemyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlsemyear.SelectedIndexChanged
        If Ddlsemyear.SelectedItem.Text = "All" Then
            Bindgridstudentlist()
        Else
            Bindgridstudentlistsemwise()
        End If
    End Sub
End Class
