Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class StudentAdm_StudentProfile
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Private Sub getstudentinfo()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "  Select S.Session,st.CorrAddress2 ,corstate.Name as corrstat , corrciy.Name as corrcityy  , corr.Name as corrcountry, PrC.Name as PrmCountry , Prcity.Name as Prmcity, Prs.Name as Prmstate, case when studenttype = 'R' then 'Regular' when studenttype ='L' then 'Lateral' when studenttype = 'm' then 'Migrate' else '' end as stutype , Isnull( Strl.RollNo, 'Not Available') RollNo, it.Institue as Department  ," & _
 " Convert(varchar,st.Dob,101) as DateOfBirth, c.course, isnull(st.EnrollmentNo,'Not available') Enrollment_No, CountM.Name as countryname, " & _
  " state.Name as statename, city.Name as cityname, st.* from Student st left join Exam_Studentrollno Strl on st.AdmissionNo=Strl.AdmissionNo " & _
" join Exam_Course C on st.CourseID = C.Courseid left join CountryMaster CountM on st.CorrCountry=CountM.ID left join Exam_Session S on " & _
" S.Academicyear =St.Academicyear left join StateMaster state on st.CorrState = state.Id left join CityMaster city on st.CorrCity =city.ID " & _
" left join Institue it on st.InstitueID =it.InstitueID  left join CountryMaster PrC on st.PrmCountry =Prc.ID left join StateMaster PrS " & _
 "on St.PrmState  =Prs.ID left join CityMaster  Prcity on St.PrmCity = Prcity.ID left join CountryMaster Corr on st.CorrCountry =Corr.ID " & _
" left join StateMaster corstate on st.CorrState =corstate.ID left join CityMaster corrciy on st.CorrCity =corrciy.ID " & _
" where st.StudentID='" & ViewState("uid") & "'"
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                If dt.Rows.Count > 0 Then
                    '  lbl.Text = dt.Rows(0)("AdmissionNo").ToString
                    lblStuName.Text = dt.Rows(0)("Student").ToString
                    lblgender.Text = dt.Rows(0)("Gender").ToString
                    lblDOB.Text = dt.Rows(0)("DateOfBirth").ToString
                    lblProgram.Text = dt.Rows(0)("course").ToString
                    lblDepartment.Text = dt.Rows(0)("Department").ToString
                    Lbladhar.Text = dt.Rows(0)("AdharNo").ToString
                    lblSemester.Text = dt.Rows(0)("sem").ToString
                    lblStudenttype.Text = dt.Rows(0)("stutype").ToString
                    lblFathername.Text = dt.Rows(0)("fatherName").ToString
                    lblMothername.Text = dt.Rows(0)("MotherName").ToString
                    LblAcdmic.Text = dt.Rows(0)("Session").ToString
                    Lblenrolment.Text = dt.Rows(0)("EnrollmentNo").ToString
                    Lblfatherincm.Text = dt.Rows(0)("FamilyIncome").ToString
                    Lblfatherocupation.Text = dt.Rows(0)("FatherOccupation").ToString
                    lblstumobile.Text = dt.Rows(0)("Mobile").ToString
                    lblstuemail.Text = dt.Rows(0)("Email").ToString
                    Lbladmisinno.Text = dt.Rows(0)("AdmissionNo").ToString
                    lblmotherocupation.Text = dt.Rows(0)("MotherOccupation").ToString
                    Lblfatherincm.Text = dt.Rows(0)("Income").ToString
                    lblguardinName.Text = dt.Rows(0)("Guardian").ToString
                    LblgrdnRlsn.Text = dt.Rows(0)("Relation").ToString
                    LblGEmail.Text = dt.Rows(0)("GEmail").ToString
                    LblGmobile.Text = dt.Rows(0)("GuardMobile").ToString
                    LblPAdd1.Text = dt.Rows(0)("PrmAddress1").ToString
                    LblPadd2.Text = dt.Rows(0)("PrmAddress2").ToString
                    LblPcountry.Text = dt.Rows(0)("PrmCountry").ToString
                    LblPstate.Text = dt.Rows(0)("Prmstate").ToString
                    LblPcity.Text = dt.Rows(0)("Prmcity").ToString
                    LblPpincode.Text = dt.Rows(0)("PrmPinCode").ToString
                    LblCAdd1.Text = dt.Rows(0)("CorrAddress1").ToString
                    LblCadd2.Text = dt.Rows(0)("CorrAddress2").ToString
                    LblCcountry.Text = dt.Rows(0)("corrcountry").ToString
                    LblCstate.Text = dt.Rows(0)("corrstat").ToString
                    LblCcity.Text = dt.Rows(0)("corrcityy").ToString
                    LblCpincode.Text = dt.Rows(0)("CorrPinCode").ToString
                    Lblnationlity.Text = dt.Rows(0)("Nationality").ToString
                    Lblreligion.Text = dt.Rows(0)("Religion").ToString
                    Lblcaste.Text = dt.Rows(0)("CasteCategory").ToString
                    Lblheight.Text = dt.Rows(0)("Height").ToString
                    Lblweight.Text = dt.Rows(0)("Weight").ToString
                    Lblhobbies.Text = dt.Rows(0)("Hobbies").ToString

                    Lblphysical.Text = dt.Rows(0)("PassportNo").ToString

                    LblRollno.Text = dt.Rows(0)("RollNo").ToString
                    Lblenrolment.Text = dt.Rows(0)("EnrollmentNo").ToString


                End If
            End Using
        End Using

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("uid") = Request.QueryString("stuid")
            ViewState("Acyr") = Request.QueryString("acyr")
            ViewState("courseid") = Request.QueryString("programid")
            getstudentinfo()
            Bindgridstudentlist()
            BINDPHOTO()
        End If
    End Sub
    Sub BINDPHOTO()
        '   Image1.ImageUrl = "~/Photos/Passbook/" & ViewState("inqid") & ".*"
        Dim files1 As String() = System.IO.Directory.GetFiles(Server.MapPath("~/Photos/users"), "" & ViewState("uid") & ".*")
        If files1.ToArray.Length > 0 Then
            Dim exten As String = files1(0)
            exten = "." & exten.Substring(exten.LastIndexOf(".") + 1)
            Image1.ImageUrl = "~\Photos\users\" & ViewState("uid").ToString & exten & "" + "?" + DateTime.Now.Ticks.ToString()
        Else

        End If

    End Sub

    Private Sub Bindgridstudentlist()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select * from StudentEducation where StudentID ='" & ViewState("uid") & "'", con)

                Dim da As New DataSet
                Dim ds As New SqlDataAdapter(cmd)
                ds.Fill(da)
                Dim i = da.Tables(0).Rows.Count
                If i > 0 Then
                    Pnlstueducation.Visible = True
                    Using cmd1 As New SqlCommand()
                        Dim query As String = "Select * from StudentEducation where StudentID ='" & ViewState("uid") & "'"
                        
                        cmd1.CommandText = query
                        Using sda As New SqlDataAdapter()
                            cmd1.Connection = con
                            sda.SelectCommand = cmd1
                            Using dt As New DataTable()
                                sda.Fill(dt)
                                Grideducationdetail.DataSource = dt
                                Grideducationdetail.DataBind()
                            End Using
                        End Using
                    End Using
                End If
            End Using
        End Using

    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
     
        Response.Redirect("StudentList.aspx?rid=" & ViewState("courseid") & "&acyr=" & ViewState("Acyr"))


    End Sub
End Class
