<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RollNoGenration.aspx.vb" Inherits="TESTFILES_RollNoGenration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../CSS1/RollnoGeneration.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
    <style>
        body
        {
            background:#f2f3f5;
            }
   .maincontainer {
    border: 2px solid #fff;
    padding: 15px;
    background-color:#fff;
    border-radius: 10px;
    text-align: left;
   }
   .Submit
 {
     font-size: 18px !important;
      font-weight: 500;
      height: 38px;
      cursor: pointer;
      color:White;
      background-color:#1ed085;
      border:none;
     width:14%;
   }
   .Submit:hover
   {
       color:White;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }
.btnRollNoGenerate
{
     font-size: 18px !important;
      font-weight: 500;
      height: 38px;
      cursor: pointer;
      color:White;
      background-color:#1ed085;
      border:none;
     width:18%;
    }
.btnRollNoGenerate:hover
   {
       color:White;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }
.maincontainerm
{
    background-color:#f2f3f5;
    color:#15283c; 
    font-size:20px; 
    width:42px;
    height:42px ;
    border-radius:50%;
    padding-top:9px;
  }
#btnbackstudent
{
    font-size:24px;
    font-weight:700;
    color:#7c858f;
    
}
#btnbackstudent:hover
{
    color:#15283c;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div class="container-fluid maincontainer mt-1">
                    <asp:Panel ID="Panel1" runat="server" Visible ="True">
                    <div class="Main">
                        <div class="row">
                          <div class="col-md-12">
                              <div class="heading1">
                                  <h3>Generate Roll No</h3>
                               </div>
                              <div class="line">
                              </div>
                          </div>
                       </div>
                       
                       <div class="row mt-2">
                            <div class="col-md-1">
                                
                            </div>
                            <div class="col-md-3 pt-2">
                               <asp:Label ID="Label1" runat="server" Text="Label">Academic Year :</asp:Label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="ddlacademicyear" AutoPostBack="true" runat="server" CssClass="form-select">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                               
                            </div>
                            </div>

                       <div class="row mt-2">
                            <div class="col-md-1">
                                
                            </div>
                            <div class="col-md-3 pt-2">
                               <asp:Label ID="Label2" runat="server" Text="Label">Level of Studies :</asp:Label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="ddllevel" AutoPostBack="true" runat="server" CssClass="form-select">
                               <asp:ListItem Value="0">Select Level</asp:ListItem>
                                   <asp:ListItem Value="1">UG</asp:ListItem>
                                     <asp:ListItem Value="2">PG</asp:ListItem>
                                       <asp:ListItem Value="3">Diploma</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                               
                            </div>
                            </div>

                       <div class="row mt-2">
                            <div class="col-md-1">
                                
                            </div>
                            <div class="col-md-3 pt-2">
                               <asp:Label ID="Label3" runat="server" Text="Label">Program/Course :</asp:Label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="ddlcourse" runat="server"  CssClass="form-select">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                               
                            </div>
                            </div>

                       <%--<div class="row mt-2">
                            <div class="col-md-1">
                                
                            </div>
                            <div class="col-md-3 pt-2">
                               <asp:Label ID="Label4" runat="server" Text="Label">Exam Name :</asp:Label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="ddlexamname" runat="server" AutoPostBack=true CssClass="form-select">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                               
                            </div>
                            </div>--%>
           
                       <div class="row mt-2">
                       <div class="col-md-4"></div>
                       <div class="col-md-7 text-left">
                       <asp:Button ID="btnload" class="btn Submit" runat="server" Text="Load"></asp:Button>
                       </div></div>

                       <div class="row mt-3">
                       <div class="col-md-1"></div>
                       <div class="col-md-10">
                 <%--      
               <asp:GridView ID="grdstudent" GridLines="Both"  Width="100%"  DataKeyNames="Courseid"
                 AutoGenerateColumns="False" runat="server" CellPadding="0" ShowHeaderWhenEmpty="true" 
                 CssClass="table table-bordered">
        <Columns>

         <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>

                 <asp:BoundField HeaderText="Courseid" DataField="Courseid"></asp:BoundField>
                 <asp:BoundField HeaderText="Exam Name" DataField="ExamName"></asp:BoundField>
                 <asp:BoundField HeaderText="Course level" DataField="courselevel"></asp:BoundField>
                 
                   <asp:TemplateField HeaderText="View Student">
                       <ItemTemplate>
                          <asp:Button ID="btnview"    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="View" runat="server" CssClass="rounded-2" Text="View Student" />
                       </ItemTemplate>
                   </asp:TemplateField> 
    
                                        
        </Columns>
           
        </asp:GridView>--%>
                        </div>
                       <div class="col-md-1"></div>
                       </div>

                   </div>
                   </asp:Panel>

                    <asp:Panel ID="Panel2" runat="server" Visible ="false">
                      
                        <div class="text-center maincontainerm m-0">
                        <asp:LinkButton ID="btnbackstudent" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
         
                        </div>
                       <div class="row mt-2">
                       <div class="col-md-12 text-start">
                       <asp:Button ID="btngeneraterollno" class="btn btnRollNoGenerate" runat="server" Text="Generate Roll No"></asp:Button>
                       </div></div>

                       <div class="row mt-2">
                       <div class="col-md-12">
                          
                             <asp:GridView ID="GridView1" GridLines="Both"  Width="100%"  DataKeyNames="Registrationid"
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered bg-white">
                   <Columns>

                   <asp:TemplateField HeaderText="S.No.">
                  <HeaderTemplate>
                  <asp:CheckBox ID="CheckBox1" runat="server"  Onclick="javascript:SelectheaderCheckboxes(this)"/>
          
                  </HeaderTemplate>
                  <ItemTemplate>
                  <asp:CheckBox ID="btnselect" runat="server"   CommandName ="select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'/>
                  </ItemTemplate>
                  </asp:TemplateField>

                    <asp:TemplateField HeaderText="S.No.">
                   <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                   </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField HeaderText="RegisId" ItemStyle-Width="0px"  DataField="Registrationid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>           
                   <asp:BoundField HeaderText="Program" ItemStyle-Width="0px"  DataField="Courseid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>           
                   <asp:BoundField HeaderText="Registration No" DataField="RegistrationNo"></asp:BoundField>
                          <asp:TemplateField HeaderText="Student Name">
                          <ItemTemplate >
                          <asp:LinkButton  Text='<%# Eval("FirstName") %>' ID="lnkname" runat="server" CommandName="Studentprile"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="SubjectName"></asp:LinkButton>       
                           </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField HeaderText="Program" DataField="Course" ></asp:BoundField>
                          <asp:BoundField HeaderText="Gender"  DataField="Gender" ></asp:BoundField>
                           <asp:BoundField HeaderText="Father's Name" DataField="FatherName"></asp:BoundField>
                           <asp:BoundField HeaderText="Email" DataField="Email"></asp:BoundField> 
                           <asp:BoundField HeaderText="Mobile" DataField="MobileNo"></asp:BoundField> 
                          <%--
                 <asp:BoundField HeaderText="Admission No" DataField="AdmissionNo"></asp:BoundField>
                   <asp:BoundField HeaderText="Student Name" DataField="StudentName"></asp:BoundField>
                   <asp:BoundField HeaderText="Sem" DataField="Sem"></asp:BoundField>
                 <asp:BoundField HeaderText="FatherName" DataField="FatherName"></asp:BoundField>
                 <asp:BoundField HeaderText="EnrollmentNo" DataField="EnrollmentNo"></asp:BoundField>
                 <asp:BoundField HeaderText="Roll_No" DataField="Roll_No"></asp:BoundField>
                          --%>     
        </Columns>
           
        </asp:GridView>
                       </div>
                       </div>
                    </asp:Panel>   
                   
      
    </div>

    </form>
    <script>
        function SelectheaderCheckboxes(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {



                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {


                        row.style.backgroundColor = "#fff";

                        inputList[i].checked = true;

                    }

                    else {

                        if (row.rowIndex % 2 == 0) {
                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }
 
</script>
</body>
</html>
