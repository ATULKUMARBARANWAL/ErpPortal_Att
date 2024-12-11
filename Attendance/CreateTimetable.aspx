<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreateTimetable.aspx.vb" Inherits="HOD_CreateTimetable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />

<Style>
    .custom-alert {
        position: fixed;
        top: 100px;
        right: -350px; /* Initially hidden offscreen */
        background-color: #c00; /* Strong red background similar to the image */
        color: white;
        padding: 14px 20px; /* More padding for a compact look */
        border-radius: 3px; /* Slight rounding of corners */
        z-index: 1000;
        box-shadow: 0px 2px 8px rgba(0, 0, 0, 0.2); /* Soft shadow */
        transition: right 0.5s ease-in-out, opacity 0.5s ease; /* Smooth transition */
        opacity: 0;
        font-family: 'Arial', sans-serif;
        font-size: 20px;
        padding:10px 20px;
        display: flex;
        align-items: center; /* Vertically align content */
        justify-content: space-between; /* Space between icon/text and close button */
        width: 350px; /* Fixed width */
    }

    .custom-alert.show {
        right: 20px; /* Bring into view */
        opacity: 1;
    }

    .custom-alert i {
        margin-right: 10px;
        font-size: 18px;
    }

    .close-btn {
        margin-left: 10px;
        cursor: pointer;
        font-size: 18px;
        color: white;
    }

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
      width:30%;
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
    .tablefield tr td
    {
        padding-top:10px;
    }
    
    .timetable
    {
        color:Black;
        text-decoration:none;
        }
        .custom-alert.warning {
    background-color: yellow;
    color: #333; /* Use dark text on yellow background */
}
    
</Style>
 <script>
     function showCustomAlert(message) {
         var alertBox = document.getElementById('customAlert');
         var alertMessage = document.getElementById('customAlertMessage');
         var alertIcon = alertBox.querySelector('i'); // Assuming there's an <i> tag for the icon

         // Set the custom message
         alertMessage.innerHTML = message;

         // Check if message contains the word "success"
         if (message.toLowerCase().includes('success')) {
             alertBox.style.backgroundColor = '#28a745'; // Green background
             alertIcon.className = 'fa fa-check-circle'; // Update icon to right-tick
         } else {
             alertBox.style.backgroundColor = 'rgb(255, 243, 205)';
             alertBox.style.color = '#856404';
              // Default red background
             alertIcon.className = 'fa-exclamation-circle'; // Default warning icon
         }

         // Make the alert visible with transition
         alertBox.style.display = 'flex'; // Ensure display is flex to align items
         setTimeout(function () {
             alertBox.classList.add('show'); // Add class to slide it in
         }, 100); // Delay for smooth transition

         // Hide the alert after 3 seconds
         setTimeout(function () {
             alertBox.classList.remove('show'); // Slide it out
             setTimeout(function () {
                 alertBox.style.display = 'none'; // Hide the alert completely
             }, 500); // Wait for the slide-out animation to complete
         }, 3000); // Show for 3 seconds
     }

     // Function to manually close the alert
     function closeCustomAlert() {
         var alertBox = document.getElementById('customAlert');
         alertBox.classList.remove('show'); // Slide it out
         setTimeout(function () {
             alertBox.style.display = 'none'; // Hide the alert completely
         }, 500); // Wait for the slide-out animation to complete
     }
</script>
</head>
<body>

    <form id="form1" runat="server">
    <div class="container-fluid maincontainer">
    
       <div id="customAlert" class="custom-alert">
    <i class="fa fa-exclamation-triangle"></i>
    <span id="customAlertMessage">This is a custom alert!</span>
    <span class="close-btn" onclick="closeCustomAlert()">&times;</span>
</div>

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
        
           
          
            <div class="col-md-8">
           
               <asp:Label ID="Label2" runat="server" class="Labels" Text="Program :"></asp:Label>
                <asp:Label ID="lblprogram" runat="server" class="Labels " Text=" Program "></asp:Label> 
                <asp:Label ID="Label11" runat="server" Text=">>"></asp:Label>&nbsp  <asp:Label ID="lblSem" class="Labels hiddencol"
                    runat="server" Text="Label"></asp:Label><asp:Label
                        ID="Lblsection" class="hiddencol" runat="server" Text="Label"></asp:Label>
             <asp:Label ID="Lblsxn"  class="Labels " runat="server" Text="Label"></asp:Label>
            
            
            </div>
             <div class="col-md-4"></div>
            </div>

           <br />

           <div class="row">
           <div class="col-md-1"></div>
           <div class="col-md-12">
    <small>
        <strong>Instructions <i class="fa-solid fa-circle-info"></i>:</strong>
       
                <i class="fa-solid fa-star" style="color: #1ed085;"></i> Here You Are Able to add timetable by filling the form 
            
                <i class="fa-solid fa-star" style="color: #1ed085;"></i> Timetable is also be visible below the form
           
    </small>
</div>

           <div class="col-md-10">

                                        <table width="100%" class="tablefield">
                                        <tr width="100%">
                                        <td width="30%">
                                      <asp:Label ID="Label12" runat="server" Text="Date :"></asp:Label>
           
                                        </td>
                                        <td width="15%">
                                            <asp:TextBox ID="txtfromdate"  class="form-control" type="date" runat="server"></asp:TextBox>
          
                                        </td>
                                        <td width="7%" align="center">To </td>
                                        <td width="15%">
                                                      <asp:TextBox ID="txttodate" class="form-control" type="date" runat="server"></asp:TextBox>
             
                                        </td>
                                         <td width="33%" align="center"> </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                        <asp:Label ID="Label3" runat="server" Text="Subjects :"></asp:Label>
              </td>
                                        <td width="70%" colspan="3">
                                           <asp:DropDownList ID="ddlsubjects" Width="500px"  AutoPostBack="true" runat="server" class="form-select">
                </asp:DropDownList>
                                           </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                         <asp:Label ID="Label4" runat="server" Text="Class :"></asp:Label>
          
                                        </td>
                                        <td width="70%" colspan="3">
                                         <asp:DropDownList Width="500px" ID="ddlclass" Height="38px" AutoPostBack="true" runat="server" class="form-select">
                </asp:DropDownList>
                                           </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                           <asp:Label ID="Label5" runat="server" Text="Class Room:"></asp:Label>
          
                                      </td>
                                        <td width="70%" colspan="3">
                                         
            
            <asp:DropDownList ID="ddlclassroom" Width="500px" runat="server" class="form-select">
                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr width="100%" >
                                        <td width="30%">
                                             <asp:Label ID="Label6" runat="server" Text="Group :"></asp:Label>
                                        </td>
                                        <td width="70%" colspan="3">
                                           <asp:DropDownList ID="ddlgroup" Width="500px" runat="server" class="form-select">
                </asp:DropDownList>
              
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                         <asp:Label ID="Label10" runat="server" Text="Combine_Class :"></asp:Label>
           
                                        </td>
                                        <td width="70%" colspan="3">
                                     <asp:DropDownList ID="ddlcombine" Width="500px" Height="38px" class="form-select" runat="server" >
         <asp:ListItem>Select</asp:ListItem>
                </asp:DropDownList>
                                      </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                    <asp:Label ID="Label7" runat="server" Text="Type:"></asp:Label>
                                     </td>
                                        <td width="70%" colspan="3">
                                         <asp:RadioButtonList ID="rblType" runat="server" Width="250px" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="L">Lecture</asp:ListItem>
                                <asp:ListItem Value="T">Tutorial</asp:ListItem>
                                <asp:ListItem Value="P">Practical</asp:ListItem>
                            </asp:RadioButtonList>
           
                                         </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                       <asp:Label ID="Label8" runat="server" Text="Week :"></asp:Label>
                                        </td>
                                        <td width="70%" colspan="3">
                                       <asp:CheckBoxList ID="ChkWeek" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="2">&nbsp;Mon&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">&nbsp;Tue&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="4">&nbsp;Wed&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">&nbsp;Thur&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="6">&nbsp;Fri&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="7">&nbsp;Sat</asp:ListItem>
                            </asp:CheckBoxList>
            
          
                                         </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label9" runat="server" Text="Period :"></asp:Label>
                                        </td>
                                        <td width="70%" colspan="3">
                                             <asp:DropDownList ID="ddlperiod" Height="38px" Width="500px" class="form-select" runat="server">
                <asp:ListItem value="1">1</asp:ListItem>
                <asp:ListItem value="2">2</asp:ListItem>
                <asp:ListItem value="3">3</asp:ListItem>
                <asp:ListItem value="4">4</asp:ListItem>
                <asp:ListItem value="5">5</asp:ListItem>
                <asp:ListItem value="6">6</asp:ListItem>
                <asp:ListItem value="7">7</asp:ListItem>
                <asp:ListItem value="8">8</asp:ListItem>
                <asp:ListItem value="9">9</asp:ListItem>
                <asp:ListItem value="10">10</asp:ListItem>
                <asp:ListItem value="11">11</asp:ListItem>
                </asp:DropDownList>
                                         </td>
                                        </tr>
                                       
                                        </table>
                                        
                                        </div>
                                         <div class="col-md-1"></div>
          </div>


            <div Class="row row1" hidden="true">
            <div class="col-md-2"></div>
            <div class="col-md-3 text-end">
             <asp:Label ID="lblsemyear" runat="server" Text="Semester :"></asp:Label>
            </div>
            <div class="col-md-5">
           
               
            
                <asp:DropDownList ID="ddlsemyear" Width="500px" runat="server" class="form-select">
                </asp:DropDownList>
           
              
          
            </div>
            <div class="col-md-2"></div>
            </div>

           



             

             <div Class="row row1">
            <div class="col-md-3"></div>
             <div class="col-md-6 text-center">
               <asp:Button ID="btnsave" class="btn Submit" runat="server" Text="Save"></asp:Button>
                <asp:Button ID="Btnupdate" class="btn Submit" Visible="false" runat="server" Text="Update"></asp:Button>
                 <asp:Button ID="btndelete" class="btn Submit" Visible="false" runat="server" Text="Delete"></asp:Button>
             </div>
              <div class="col-md-3 text-end">
            
                  <asp:LinkButton ID="btnaddcombine" hidden="true" runat="server" class="addsubject"><span class="fa fa-plus"></span> Combine_Class</asp:LinkButton>
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
      
            <asp:TemplateField ItemStyle-Width="60px" HeaderText="I">
  <ItemTemplate >
    <asp:LinkButton ID="LinkI" runat="server"  Text='<%# Eval("I") %>'  CommandName="I" CssClass="timetable"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>

  </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width="60px" HeaderText="II">
  <ItemTemplate >
    <asp:LinkButton ID="LinkII" runat="server"  Text='<%# Eval("II") %>'  CommandName="II" CssClass="timetable"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
  </ItemTemplate>
</asp:TemplateField>
         
          <asp:TemplateField ItemStyle-Width="60px" HeaderText="III">
  <ItemTemplate >
    <asp:LinkButton ID="LinkIII" runat="server"  Text='<%# Eval("III") %>'  CommandName="III" CssClass="timetable"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
  </ItemTemplate>
</asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="60px" HeaderText="IV">
  <ItemTemplate >
    <asp:LinkButton ID="LinkIV" runat="server"  Text='<%# Eval("IV") %>'  CommandName="IV" CssClass="timetable"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
  </ItemTemplate>
</asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="60px" HeaderText="V">
  <ItemTemplate >
    <asp:LinkButton ID="LinkV" runat="server"  Text='<%# Eval("V") %>'  CommandName="V" CssClass="timetable"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
  </ItemTemplate>
</asp:TemplateField>
          <asp:TemplateField ItemStyle-Width="60px" HeaderText="VI">
  <ItemTemplate >
    <asp:LinkButton ID="LinkVI" runat="server"  Text='<%# Eval("VI") %>'  CommandName="VI" CssClass="timetable"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
  </ItemTemplate>
</asp:TemplateField>
           <asp:TemplateField ItemStyle-Width="60px" HeaderText="VII">
  <ItemTemplate >
    <asp:LinkButton ID="LinkVII" runat="server"  Text='<%# Eval("VII") %>'  CommandName="VII" CssClass="timetable"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
  </ItemTemplate>
</asp:TemplateField>
          <asp:TemplateField ItemStyle-Width="60px" HeaderText="VIII">
  <ItemTemplate >
    <asp:LinkButton ID="LinkVIII" runat="server"  Text='<%# Eval("VIII") %>'  CommandName="VIII" CssClass="timetable"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
  </ItemTemplate>
</asp:TemplateField>
           <asp:TemplateField ItemStyle-Width="60px" HeaderText="IX">
  <ItemTemplate >
    <asp:LinkButton ID="LinkIX" runat="server"  Text='<%# Eval("IX") %>'  CommandName="IX" CssClass="timetable"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
  </ItemTemplate>
</asp:TemplateField>
           <asp:TemplateField ItemStyle-Width="60px" HeaderText="X">
  <ItemTemplate >
    <asp:LinkButton ID="LinkX" runat="server"  Text='<%# Eval("X") %>'  CommandName="X" CssClass="timetable"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
  </ItemTemplate>
</asp:TemplateField>
           <asp:TemplateField ItemStyle-Width="60px" HeaderText="XI">
  <ItemTemplate >
    <asp:LinkButton ID="LinkXI" runat="server"  Text='<%# Eval("XI") %>'  CommandName="XI" CssClass="timetable"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
  </ItemTemplate>
</asp:TemplateField>
           
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
