<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreateTimetable.aspx.vb" Inherits="HOD_CreateTimetable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />

<Style>
    
#backbotton
{
     font-size:22px;
    font-weight:600;
    color:#7c858f;
   
    }

    #backbotton:hover
{
     color:#15283c;
    }

.hiddencol
{
    display:none;
    }



    body 
{
    background-color:#f2f3f5;
}

 .heading1
{
    padding-top:2px;
    }
  .heading1 h3
{
    color:#15283c;
    font-size:20px;
    font-weight:600;
   
    }
    
      .line
   {
       content:" ";
       width:100%;
       height:1px;
        background-color:#e1e2e3;
        margin-top:2px;
        margin-bottom:5px;
        
       
   }
   
      
       .Labels
       {
           
            font-size:17px;
    font-weight:500;
    color:#000;
           }
   
   
    .row1
    {
        margin-top:10px;
        }
        
        
        .Submit
 {
     font-size: 18px !important;
      font-weight: 500;
      
      cursor: pointer;
      color:#fff;
      background-color:#1ed085;
      border:none;
      width:20%;
   }
   .Submit:hover
   {
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }
    
    .maincontainer
{
     background-color:#fff;
    border:1px solid #e1e2e3;
    border-radius:8px;
    box-shadow:0 2px 10px 2px #e1e2e3;
    padding-bottom:24px;
}
.addsubject
    {
   color:#15283c;
   font-size: 18px !important;
   cursor: pointer;
   font-weight: 500;
   border:1px solid #15283c; 
   background-color:#fff;
   text-decoration:none;
   text-align :center;
   border-radius:4px;
   padding:3px 10px;
    }
    .addsubject:hover
    {
   color:#fff;
   background-color:#15283c;
   text-decoration:none;
    }

</Style>
</head>
<body>

    <form id="form1" runat="server">
    <div class="container-fluid maincontainer">
          <div class="row">
            <div class="col-md-5 mt-1">
            <table>
    <tr width="100%">
    <td width="30%">
      <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
    </td>
    <td width="70%">
       <div class="heading1">
   <h3>Create TimeTable</h3>
   </div>
    </td>
    </tr>
    </table>
             
              
            </div>
            
            <div class="col-md-4 mt-1">
          
        
     
            </div>


            <div class="col-md-3 mt-1">
            <table>
            <tr width="100%">

          
           

            <td width="50%">
             <asp:Label ID="Label1" runat="server" class="Labels" Text="Academic Year :"></asp:Label>
             </td>
            <td width="50%">
             <asp:Label ID="lblAcademicyear" class="Labels" runat="server" Text=" Academic Year "></asp:Label>
            </td>
            
            </tr>
            </table>
                
            </div>
             <div class="line">
         </div>
           
             
             
            </div>

            <div class="row">
        
           
          
            <div class="col-md-6">
           
               <asp:Label ID="Label2" runat="server" class="Labels" Text="Program :"></asp:Label>
                <asp:Label ID="lblprogram" runat="server" class="Labels" Text=" Program "></asp:Label>
             
            
            
            </div>
             <div class="col-md-6"></div>
            </div>

           
            <div Class="row row1">
            <div class="col-md-2"></div>
            <div class="col-md-3 text-end">
             <asp:Label ID="lblsemyear" runat="server" Text="Semester :"></asp:Label>
            </div>
            <div class="col-md-5">
           
               
            
                <asp:DropDownList ID="ddlsemyear" runat="server" class="form-select">
                </asp:DropDownList>
           
              
          
            </div>
            <div class="col-md-2"></div>
            </div>

           

             <div Class="row row1">
            <div class="col-md-2"></div>
            <div class="col-md-3 text-end">
              <asp:Label ID="Label3" runat="server" Text="Subjects :"></asp:Label>
            </div>
            <div class="col-md-5">
           
               
            
            <asp:DropDownList ID="ddlsubjects" AutoPostBack="true" runat="server" class="form-select">
                </asp:DropDownList>
              
          
            </div>
            <div class="col-md-2"></div>
            </div>
            
             <div Class="row row1">
            <div class="col-md-2"></div>
            <div class="col-md-3 text-end">
              <asp:Label ID="Label4" runat="server" Text="Class :"></asp:Label>
            </div>
            <div class="col-md-5">
           
               
            
            <asp:DropDownList ID="ddlclass" runat="server" class="form-select">
                </asp:DropDownList>
              
          
            </div>
            <div class="col-md-2"></div>
            </div>

               <div Class="row row1">
            <div class="col-md-2"></div>
            <div class="col-md-3 text-end">
              <asp:Label ID="Label5" runat="server" Text="Class Room:"></asp:Label>
            </div>
            <div class="col-md-5">
           
               
            
            <asp:DropDownList ID="ddlclassroom" runat="server" class="form-select">
                </asp:DropDownList>
              
          
            </div>
            <div class="col-md-2"></div>
            </div>
               
                 <div Class="row row1" hidden="true" >
            <div class="col-md-2"></div>
            <div class="col-md-3 text-end">
              <asp:Label ID="Label6" runat="server" Text="Group :"></asp:Label>
            </div>
            <div class="col-md-5">
           
               
            
            <asp:DropDownList ID="ddlgroup" runat="server" class="form-select">
                </asp:DropDownList>
              
          
            </div>
            <div class="col-md-2"></div>
            </div>


             <div Class="row row1">
            <div class="col-md-2"></div>
            <div class="col-md-3 text-end">
              <asp:Label ID="Label10" runat="server" Text="Combine_Class :"></asp:Label>
            </div>
            <div class="col-md-5">
           
               
            
          <asp:DropDownList ID="ddlcombine" class="form-select" runat="server" >
         <asp:ListItem>Select</asp:ListItem>
                </asp:DropDownList>
              
          
            </div>
            <div class="col-md-2"></div>
            </div>

              <div Class="row row1">
            <div class="col-md-2"></div>
            <div class="col-md-3 text-end">
              <asp:Label ID="Label7" runat="server" Text="Type:"></asp:Label>
            </div>
            <div class="col-md-5">
           
           <asp:RadioButtonList ID="rblType" runat="server" Width="250px" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="L">Lecture</asp:ListItem>
                                <asp:ListItem Value="T">Tutorial</asp:ListItem>
                                <asp:ListItem Value="P">Practical</asp:ListItem>
                            </asp:RadioButtonList>
          
            </div>
            <div class="col-md-2"></div>
            </div>


            <div Class="row row1">
            <div class="col-md-2"></div>
            <div class="col-md-3 text-end">
              <asp:Label ID="Label8" runat="server" Text="Week :"></asp:Label>
            </div>
            <div class="col-md-5">
                 <asp:CheckBoxList ID="ChkWeek" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="2">&nbsp;Mon&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">&nbsp;Tue&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="4">&nbsp;Wed&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">&nbsp;Thur&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="6">&nbsp;Fri&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="7">&nbsp;Sat</asp:ListItem>
                            </asp:CheckBoxList>
            
          
            </div>
            <div class="col-md-2"></div>
            </div>


             <div Class="row row1">
            <div class="col-md-2"></div>
            <div class="col-md-3 text-end">
              <asp:Label ID="Label9" runat="server" Text="Period :"></asp:Label>
            </div>
            <div class="col-md-5">
                <asp:TextBox ID="txtPeriod" runat="server" class="form-control" Width="100%"></asp:TextBox>
<%--                            <asp:RequiredFieldValidator ID="valRPeriod" runat="server" ControlToValidate="txtPeriod"
                                Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">{Required}</asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="valPeriod" runat="server" ControlToValidate="txtPeriod" Display="Dynamic"
                                ErrorMessage="RangeValidator" MaximumValue="11" MinimumValue="1" Type="Integer">Invalid Period</asp:RangeValidator>--%>
            
          
            </div>
            <div class="col-md-2"></div>
            </div>

             <div Class="row row1">
            <div class="col-md-3"></div>
             <div class="col-md-6 text-center">
               <asp:Button ID="btnsave" class="btn Submit" runat="server" Text="Save"></asp:Button>
             </div>
              <div class="col-md-3 text-end">
              <%-- <asp:Button ID="btnaddcombine" class="btn Submit" runat="server"></asp:Button>--%>
                  <asp:LinkButton ID="btnaddcombine" runat="server" class="addsubject"><span class="fa fa-plus"></span> Combine_Class</asp:LinkButton>
              </div>
            </div>

            <br />

            <asp:GridView ID="GridView1" Width="100%" AutoGenerateColumns="False" runat="server" CellPadding="0"  CssClass="table table-bordered" 
       Font-Size="10px">
       
      
        <Columns>
            <asp:BoundField ItemStyle-Width="60px" DataField="DayName" HtmlEncode="false" 
                HeaderText="DayName" >
<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="60px" DataField="I" HtmlEncode="false" 
                HeaderText="I" >
<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="60px" DataField="II" HtmlEncode="false" 
                HeaderText="II" >
<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="60px" DataField="III" HtmlEncode="false" 
                HeaderText="III" >
<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="60px" DataField="IV" HtmlEncode="false" 
                HeaderText="IV" >
<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="60px" DataField="V" HtmlEncode="false" 
                HeaderText="V" >
<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="60px" DataField="VI" HtmlEncode="false" 
                HeaderText="VI" >
<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="60px" DataField="VII" HtmlEncode="false" 
                HeaderText="VII" >
<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="60px" DataField="VIII" HtmlEncode="false" 
                HeaderText="VIII" >
<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="60px" DataField="IX" HtmlEncode="false" 
                HeaderText="IX" >
<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="60px" DataField="X" HtmlEncode="false" 
                HeaderText="X" >
<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="60px" DataField="XI" HtmlEncode="false" 
                HeaderText="XI" >
<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
           
        </Columns>
      
    </asp:GridView>


    <asp:GridView ID="GridView3" Width="100%" Visible=false runat="server" AutoGenerateColumns="False"
        DataKeyNames="timetableid" 
        ForeColor="#333333" CellPadding="4" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btndelete" CausesValidation="false" CommandArgument='<%#Eval("timetableid")%>'
                        CommandName="Delete" runat="server" Text="Delete TimeTable" OnClientClick="return conformbox('Are you sure want to delete?');" />
                </ItemTemplate>
            </asp:TemplateField>
           
            <asp:BoundField DataField="Wd" HeaderText="W_Day" />
            <asp:BoundField DataField="Prd" HeaderText="Period" />
            <asp:BoundField DataField="Course" HeaderText="Course" />
            <asp:BoundField DataField="Sem" HeaderText="Sem" />
            <asp:BoundField DataField="Classes" HeaderText="Class" />
            <asp:BoundField DataField="ClassRoom" HeaderText="Class Room" />
            <%-- <asp:BoundField DataField="combinename" HeaderText="Combine Class" />--%>
            <asp:BoundField DataField="Grp" HeaderText="Group" />
            <asp:BoundField DataField="Subject" HeaderText="Subject" />
            <asp:BoundField DataField="Teach_Type" HeaderText="Teach_Type" />
            <asp:BoundField DataField="TimetableCreator" HeaderText="Timetable Creator" />
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Left" 
            Font-Bold="True" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>

    </div>
    </form>
</body>
</html>
