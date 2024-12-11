<%@ Page Language="VB" EnableEventValidation="false" ValidateRequest="false" AutoEventWireup="false"
    CodeFile="RegularAttendence.aspx.vb" Inherits="Teacher_RegularAttendence" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/usercontrol/datectr.ascx" TagName="textbox" TagPrefix="datetxt" %>
--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
   <meta name="viewport" content="width=device-width, initial-scale=1">
    
    <script type="text/javascript" src="../bootstrap-3.3.7-dist/js/jquery-3.2.1.min.js">   
    </script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
   
    <script type="text/javascript" src="../bootstrap-3.3.7-dist/js/jquery-3.2.1.min.js">   
    </script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="Stylesheet" />
       <script type="text/javascript" src="../Scripts/manojshort.js"></script>
        <link href="../bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />

       <script type="text/javascript" src="../Scripts/manojshort.js"></script>
    <style type="text/css">
        #btnHidden
        {
            visibility: hidden;
        }
        
         .kh
        {
            visibility: hidden;
        }
        
          b
        {
            color: red;
            background-color :Red;
        }
        d
        {
            color: blue;
        }
        
        tb
        {
             font-size:small;
             vertical-align:top;
             border: 1px solid black;
        }
        .tbl
        {
           width:100%
            }
        
        th {
    white-space: nowrap;
}
td {
    white-space: nowrap;
}
        
        
       div {
    padding: 0px 0px 7px 0px;
  font-size: 12px;
   font-family:Arial;
   }
    .backbotton
{
    font-size:22px;
    font-weight:600;
    color:#7c858f;
    
}

.backbotton:hover
{
    color:#15283c;
}
.heading1
{
    color:#15283c;
    font-size:20px;
    font-weight:500;
}

.mycheckbox
{
    margin-left:5px;
   
    }

.mycheckbox input 
{
   
      margin-right: 3px !important;
      margin-bottom: 12px;
            margin-top: 5px;
    }

     .grid {
        width: 100%;
        border-collapse: collapse;
        font-family: Arial, sans-serif;
        font-size: 14px;
    }

    /* Header row styling */
    .grid th {
      
        color: black;
        padding: 12px;
        text-align: center;
        border: 1px solid #ddd;
    }

    /* Row styling */
    .grid td {
        padding: 12px;
        text-align: center;
        border: 1px solid #ddd;
    }

    /* Alternate row background color */
  
    /* Hover effect on rows */
   

    /* Checkbox styling within GridView */
    .grid input[type="checkbox"] {
        transform: scale(1.2); /* Larger checkboxes */
        cursor: pointer;
    }

    /* Header fixed on scroll */
    .grid-header {
        position: sticky;
        top: 0;
        z-index: 1;
    }

    /* Table responsiveness */
    .table-wrapper {
        overflow-x: auto;
    }

    /* Success message styling */
    .alert-success {
        background-color: #d4edda;
        color: #155724;
        padding: 10px;
        margin-top: 10px;
        border: 1px solid #c3e6cb;
        border-radius: 5px;
    }

    </style>
        
    <script type="text/javascript">
        function boxDisable(t) {
            //   alert(t);
            if ($('#' + t + '').is(':checked')) {
                $(".tbl td").each(function () {
                    $('.' + t + '').prop('checked', true);
                });

            } else {
                $(".tbl td").each(function () {
                    $('.' + t + '').prop('checked', false);
                });
            }
        };
    </script>

    

</head>
<body>
    <form id="form1" runat="server" autocomplete="off">


    <script language="javascript" type="text/javascript">
        function callcollegeid(a) {

            // var t = document.getElementById("txtdt").innerText
            //  alert(a);
            //   ViewState["Name"]= 'inputdt'
            document.getElementById('<%=Retsid.ClientID %>').value = a;
            //  $('#' + a).css("background-color", "#D6D5C3");
            // $('#' + g).css('cursor', 'pointer');
            document.getElementById("<%=btnHidden.ClientID %>").click();
            //document.getElementById("btnSubmitcourse").click();
            //   alert('abc');
        }

        function fun() {

            if (!document.getElementById("<%=Retsid.ClientID%>").value == "") {
                //var g;
                g = parseInt(document.getElementById("<%=Retsid.ClientID%>").value);
                // g = g + 1;
                // $('#datal tr> td').filter(":nth-child(" + g + ")").css("background-color", "#D6D5C3");
                // $('.t').css("background-color", "")
                $('#' + g).css("background-color", "#D6D5C3")
                $('#' + g).css('cursor', 'pointer')
            }
        }



    </script>


    <asp:HiddenField ID="inputcollegeid" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>



   
        <div class="row " style="padding-top:2rem;">
        <div class="col-sm-6"style="margin-left:6rem;"> 
        &nbsp  &nbsp
        <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
        <span class="heading1">  &nbsp &nbsp Attendance </span>
        </div>
            <div class="col-sm-4" style="text-align:right;width:40%; display:flex;">
          <h3 style="font-size:large;margin-top:8px;">  Date : &nbsp;</h3>
                <%-- <datetxt:textbox ID="txtDt"    width="150" class="form-control" AutoPostBack="True" runat="server">
                </datetxt:textbox>--%>
                 <asp:TextBox ID="txtDt" runat="server" class="form-control"  AutoPostBack="true" type="date" style="width:80%;"></asp:TextBox>
            </div>
           
             
        </div>

       
   



    
  
     
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>

        <div style="overflow-x:auto;">
            <div class="col-md-10" style="margin-left:6rem;">
    <small>
        <strong>Instructions <i class="fa-solid fa-circle-info"></i>:</strong>
       
                <i class="fa-solid fa-star" style="color: #1ed085;"></i> here, user are able to mark attendence of "students".
           
                <i class="fa-solid fa-star" style="color: #1ed085;"></i>" when user click on "save" then attendence will marked but when user "save and email" then attendence will marked and email should be transfer to each student.
            
    </small>
</div>
            <asp:GridView ID="StudentGridView" runat="server" AutoGenerateColumns="False" CssClass="grid" 
                           HeaderStyle-ForeColor="White" style="width:86%;margin-left:7%;">
                <Columns>
                    <asp:BoundField DataField="SerialNumber" HeaderText="S.No" />
                    <asp:BoundField DataField="StudentID" HeaderText="Student ID" />
                    <asp:BoundField DataField="Student" HeaderText="Student Name" />
                     <asp:TemplateField HeaderText="Select All">
            <HeaderTemplate>
                <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="AttendanceCheckbox" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <div class="alert-success">
                <strong>!</strong> <label id="msgsave" runat="server"></label>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="txtDt" EventName="TextChanged" />
    </Triggers>
</asp:UpdatePanel>

  

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
    <asp:Panel ID="Panel1" runat="server">
   
 <div class="col-sm-5">
 <div class="row input-group-sm ">
                <%-- <asp:Button ID="btnSave" class="btn btn-primary btn-xs"  runat="server" Text="Save" CommandName="S"
                    />--%>
     <input id="btnSave" type="button"    class="btn btn-success btn-xs" runat="server"  onserverclick="btnSave_Click" value="Save" />

     <asp:Button 
    id="Btnwithsm" 
    runat="server" 
    Text="Save & Email" 
    CssClass="btn btn-success btn-xs" 
    OnClick="btnSaveAndEmail_Click" />


                      &nbsp;<asp:Button ID="btnDelete"  class="btn btn-success btn-xs" runat="server" Text="Delete" />

                           <asp:CheckBoxList ID="chkcopyatt" DataTextField="prd" CssClass="mycheckbox" DataValueField="prd" runat="server"
                        RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
            </div>
            </div>
        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
       
    </asp:Panel>
  </ContentTemplate>
    <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnHidden" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:Button ID="btnHidden" CausesValidation="false" runat="server" Text="Button" />
    <input id="Retsid" runat="server" type="hidden" style="width: 0px; position: static;" />


    
    


    </form>
</body>
</html>